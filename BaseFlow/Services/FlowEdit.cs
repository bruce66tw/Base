using Base.Models;
using Base.Services;
using Newtonsoft.Json.Linq;

namespace BaseFlow.Services
{
    public class FlowEdit
    {
        private EditModel GetCrud()
        {
            return new EditModel()
            {
                Table = "dbo.Db",
                Kid = "Id",
                Col4 = null,
                Items = new [] {
                    new EitemModel { Fid = "Id" },
                    new EitemModel { Fid = "Name", Required = true },
                    new EitemModel { Fid = "ConnectStr", Required = true },
                    new EitemModel { Fid = "Note" },
                },
            };
        }

        public JObject GetRow(string key)
        {
            return new CrudEdit().GetRow(GetCrud(), key);
        }

        //key為空白表示新增資料
        public ErrorModel Save(bool isNew, JObject row)
        {
            /*
            //set default value for new
            if (isNew)
            {
                row["Id"] = _Str.NewId();
                row["Status"] = 1;
            }
            */

            return new CrudEdit().Save(GetCrud(), isNew, row, new WhenSave(WhenSave));
        }

        //return error msg if any
        private string WhenSave(bool isNew, JObject row)
        {
            /*
            //check input
            if (_Str.IsEmpty(row["Code"]))
                return "代碼不可空白。";
            else if (_Str.IsEmpty(row["Name"]))
                return "縣市名稱不可空白。";

            //「代碼」不可重複。
            var code = row["Code"].ToString();
            var sql = isNew
                ? string.Format("select Code from dbo.Project where Code='{0}'", code)
                : string.Format("select Code from dbo.Project where Code='{0}' and Id != '{1}'", code, row["Id"].ToString());
            if (_Db.GetJson(sql) != null)
                return "代碼已經存在。";
            */

            //case of ok
            return "";
        }

        public ErrorModel DeleteRows(string[] keys)
        {
            //check before delete
            return new CrudEdit().DeleteRow(GetCrud(), keys);
        }

    } //class
}
