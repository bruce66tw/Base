using Base.Enums;
using Base.Models;
using Base.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseFlow.Services
{
    public class _Flow
    {
        //match to Flow.js
        private const char OrSep = ':';
        private const char AndSep = ';';
        private const char ColSep = ',';

        public static string SqlUserMgr = @"
select d.MgrId 
from dbo.Dept d
inner join dbo.[User] u on d.Id=u.DeptId
where u.Id='{0}'
";
        public static string SqlUserName = "select Name from dbo.[User] where Id='{0}'";
        public static string SqlDeptMgr = "select MgrId from dbo.Dept where Id='{0}'";

        /// <summary>
        /// create workflow signing rows
        /// </summary>
        /// <param name="row"></param>
        /// <param name="userFid">field of user id that own row</param>
        /// <param name="flowName"></param>
        /// <param name="sourceType"></param>
        /// <param name="sourceId"></param>
        /// <param name="db"></param>
        /// <returns>error msg if any</returns>
        public static string CreateSignRows(JObject row, string userFid, string flowName, 
            string sourceType, string sourceId, Db db)
        {
            //var
            //var profile = db.GetUserProfile();
            var error = string.Empty;
            //var db = new Db();

            //return error if sign rows existed

            //get flow lines
            var sql = string.Format(@"
select 
    FromNodeId=l.FromNode,
	FromNodeName=nf.Name,
	FromNodeType=nf.NodeType,
    ToNodeId=l.ToNode,
    ToNodeName=nt.Name,
	ToNodeType=nt.NodeType,
	nf.SignerType, nf.SignerValue,
    l.Sort, l.CondStr
from dbo._FlowLine l
join dbo._Flow f on l.FlowId=f.Id
join dbo._FlowNode nf on l.FromNode=nf.Id
join dbo._FlowNode nt on l.ToNode=nt.Id
where f.Name='{0}'
order by l.FromNode, l.Sort
", flowName);
            var lines = db.GetModels<SignLineDto>(sql);

            /*
            //get start node
            var checkLines = lines
                .Where(a => a.FromNodeType == EnumNodeType.Start)
                .OrderBy(a => a.Sort)
                .ToList();
            var firstLine = checkLines.FirstOrDefault();

            //now start node
            var nowNodeId = firstLine.FromNodeId;
            var nowNodeName = firstLine.FromNodeName;
            */

            //get now line
            var firstLine = lines
                .Where(a => a.FromNodeType == NodeTypeEstr.Start)
                .OrderBy(a => a.Sort)
                .FirstOrDefault();
            if (firstLine == null)
            {
                error = "No Start Node for Flow " + flowName;
                goto lab_exit;
            }

            var nowNodeId = firstLine.FromNodeId;
            var nowNodeName = firstLine.FromNodeName;

            //
            //string nowNodeId = "", nowNodeName = "";
            //var first = true;
            var findIdxs = new List<int>(); //find index list
            //List<SignLineModel> checkLines = null;
            while(true)
            {
                /*
                if (first)
                {
                    checkLines = lines
                        .Where(a => a.FromNodeType == EnumNodeType.Start)
                        .OrderBy(a => a.Sort)
                        .ToList();

                    first = false;

                    //now start node
                    var firstLine = checkLines.FirstOrDefault();
                    nowNodeId = firstLine.FromNodeId;
                    nowNodeName = firstLine.FromNodeName;
                }
                else
                {
                */
                    var checkLines = lines
                        .Where(a => a.FromNodeId == nowNodeId)
                        .OrderBy(a => a.Sort)
                        .ToList();
                //}
                

                //get matched line
                SignLineDto findLine = null;
                foreach(var line in checkLines)
                {
                    var status = IsLineMatch(row, line.CondStr);
                    if (status == "1")  //case of match
                    {
                        findLine = line;
                        break;
                    }
                    if (status != "0") //case of error
                    {
                        return status;
                    }
                }

                //return error if no matched line
                if (findLine == null)
                {
                    error = "No Match Line for FromNode=" + nowNodeName;
                    goto lab_exit;
                }

                //check endless loop
                var idx = lines.IndexOf(findLine);
                if (findIdxs.IndexOf(idx) >= 0)
                {
                    error = "Find Node Twice(" + checkLines[idx].FromNodeName + ")";
                    goto lab_exit;
                }

                //add find index
                findIdxs.Add(idx);

                //case of end node
                if (findLine.ToNodeType == NodeTypeEstr.End)
                    break;

                //set variables
                nowNodeId = findLine.ToNodeId;
                nowNodeName = findLine.ToNodeName;
            }

            //case of ok, write db
            sql = @"
insert into dbo._FlowSign(
    Id, SourceType, SourceId,
    NodeName, LevelNo, TotalLevel,
    SignerId, SignerName, SignTime) values(
    @Id, @SourceType, @SourceId,
    @NodeName, @LevelNo, @TotalLevel,
    @SignerId, @SignerName, @SignTime
)
";

            //write first node
            var totalLevel = findIdxs.Count - 1;
            /*
            string signerId = "", signerName = "";
            if (row[userFid] == null)
            {
                error = "row[" + userFid + "] is empty.";
                goto lab_exit;
            }

            signerId = row[userFid].ToString();
            signerName = db.GetStr(string.Format(SqlUserName, signerId));

            db.Update(sql, new List<object>() {
                "Id", _Str.NewId(),
                "SourceType", sourceType,
                "SourceId", sourceId,
                "NodeName", firstLine.FromNodeName,
                "LevelNo", 0,
                "TotalLevel", totalLevel,
                "SignerId", signerId,
                "SignerName", signerName,
            });
            */

            //string signerId = "", signerName = "";
            var level = 0;
            foreach (var idx in findIdxs)
            {
                #region get signerId by rules
                var line = lines[idx];
                var signerId = "";
                DateTime? signTime = null;
                if (level == 0)
                {
                    signTime = DateTime.Now;
                    if (row[userFid] != null)
                        signerId = row[userFid].ToString();
                } 
                else
                {
                    switch (line.SignerType)
                    {
                        /*
                        case EnumSignerType.User:
                            signerId = line.SignerValue;
                            break;
                            */
                        case SignerTypeEstr.Field:
                            if (row[line.SignerValue] != null)
                                signerId = row[line.SignerValue].ToString();
                            break;
                        case SignerTypeEstr.UserMgr:
                            if (row[userFid] != null)
                                signerId = db.GetStr(string.Format(SqlUserMgr, row[userFid].ToString()));
                            break;
                        case SignerTypeEstr.DeptMgr:
                            if (line.SignerValue != null)
                                signerId = db.GetStr(string.Format(SqlDeptMgr, line.SignerValue));
                            break;
                    }
                }
                #endregion

                if (string.IsNullOrEmpty(signerId))
                {
                    error = "cannot get signerId";
                    goto lab_exit;
                }

                //get signer Name
                var signerName = db.GetStr(string.Format(SqlUserName, signerId));

                //update db: add row
                db.Update(sql, new List<object>() {
                    "Id", _Str.NewId(),
                    "SourceType", sourceType,
                    "SourceId", sourceId,
                    "NodeName", line.FromNodeName,
                    "LevelNo", level,
                    "TotalLevel", totalLevel,
                    "SignerId", signerId,
                    "SignerName", signerName,
                    "SignTime", signTime,
                });
                level++;
            }

            lab_exit:
            //db.Dispose();
            if (error != string.Empty)
                error = "_Flow.cs CreateSignRows() wrong: " + error;
            return error;
        }

        /// <summary>
        /// check is line match condition string or not
        /// refer Flow.js condStrToList()
        /// </summary>
        /// <param name="row"></param>
        /// <param name="condStr"></param>
        /// <returns>1:match, 0:not match, other:error msg</returns>
        private static string IsLineMatch(JObject row, string condStr)
        {
            if (string.IsNullOrEmpty(condStr))
                return "1";

            //var list = [];
            //var k = 0;
            var orList = condStr.Split(OrSep);
            var orLen = orList.Length;
            var hasOr = (orLen > 1);
            for (var i = 0; i < orLen; i++)
            {
                var match = true;  //line match or not
                var andList = orList[i].Split(AndSep);
                //decimal rowValue2, condValue2;
                foreach (var andItem in andList)
                {
                    #region check column 
                    //check column length
                    var cols = andItem.Split(ColSep);
                    if (cols.Length != 3)
                        return "cols.Length should be 3(" + andItem + ")";

                    //check column existed
                    if (row[cols[0]] == null)
                        return "no column (" + cols[0] + ")";

                    #region check condition
                    var rowValue = row[cols[0]].ToString();
                    var condValue = cols[2];
                    switch (cols[1])
                    {
                        case LineOpEstr.Eq:
                            if (decimal.TryParse(rowValue, out var rowValue2) && decimal.TryParse(condValue, out var condValue2))
                            {
                                if (rowValue2 != condValue2)
                                    match = false;
                            }
                            else
                            {
                                if (rowValue != condValue)
                                    match = false;
                            }
                            break;
                        case LineOpEstr.NotEq:
                            if (decimal.TryParse(rowValue, out rowValue2) && decimal.TryParse(condValue, out condValue2))
                            {
                                if (rowValue2 == condValue2)
                                    match = false;
                            }
                            else
                            {
                                if (rowValue == condValue)
                                    match = false;
                            }
                            break;
                        case LineOpEstr.Gt:
                            if (!decimal.TryParse(rowValue, out rowValue2) || !decimal.TryParse(condValue, out condValue2) || !(rowValue2 > condValue2))
                                match = false;
                            break;
                        case LineOpEstr.Ge:
                            if (!decimal.TryParse(rowValue, out rowValue2) || !decimal.TryParse(condValue, out condValue2) || !(rowValue2 >= condValue2))
                                match = false;
                            break;
                        case LineOpEstr.St:
                            if (!decimal.TryParse(rowValue, out rowValue2) || !decimal.TryParse(condValue, out condValue2) || !(rowValue2 < condValue2))
                                match = false;
                            break;
                        case LineOpEstr.Se:
                            if (!decimal.TryParse(rowValue, out rowValue2) || !decimal.TryParse(condValue, out condValue2) || !(rowValue2 <= condValue2))
                                match = false;
                            break;
                        default:
                            match = false;
                            break;
                    }
                    #endregion (check condition)
                    #endregion (check column)

                    //break loop if not match
                    if (!match)
                        break;  //break for loop(and list)
                }//for and

                if (match)
                    return "1";
            }//for or

            //case of not match
            return "0";
        }

    }//class
}
