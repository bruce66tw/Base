using Base.Enums;
using Base.Models;
using System;
using System.Collections.Generic;

namespace Base.Services
{
    //global function
    public static class _Fun
    {
        #region constant
        //hidden input for CRSF
        public const string HideKey = "_hideKey";

        //date format for save into db
        public const string BackDateFormat = "yyyy/M/d";

        //carrier
        public const string TextCarrier = "\r\n";     //for string
        public const string HtmlCarrier = "<br>";     //for html

        //null string for checking ??
        public const string NullString = "_null";

        //default view cols for layout(row div, label=2, input=3)(水平) 
        public static List<int> DefHCols = new List<int>() { 2, 3 };
        #endregion

        #region base varibles
        //ap physical path, has right slash
        public static string DirRoot = _Str.GetLeft(AppDomain.CurrentDomain.BaseDirectory, "bin\\");

        //temp folder
        public static string DirTemp = DirRoot + "_temp\\";
        #endregion

        #region Db variables
        //database type
        private static DbTypeEnum _dbType;

        //lightInject DI
        //private static ServiceContainer _DI;
        private static IServiceProvider _DI;
        
        //for read page rows
        public static string ReadPageSql;

        //for delete rows
        public static string DeleteRowsSql;

        //db status, when db get error, it will change to false
        //public static bool DbStatus = true;
        #endregion

        #region others variables
        //from config file
        public static ConfigDto Config;

        public static SmtpDto Smtp = default(SmtpDto);

        //session type(web only), 1:session, 2:redis, 3:custom(config must set SessionService field, use Activator)
        //public static int SessionServiceType = 1;
        #endregion

        /*
        //constructor
        static _Fun()
        {
        }
        */

        /// <summary>
        /// initial db environment for Ap with db function !!
        /// </summary>
        public static void Init(IServiceProvider di, DbTypeEnum dbType)
        {
            //set instance variables
            _dbType = dbType;
            _DI = di;

            #region set smtp
            var smtp = Config.Smtp;
            if (smtp != "")
            {
                try
                {
                    var cols = smtp.Split(',');
                    Smtp = new SmtpDto()
                    {
                        Host = cols[0],
                        Port = int.Parse(cols[1]),
                        Ssl = bool.Parse(cols[2]),
                        Id = cols[3],
                        Pwd = cols[4],
                        FromEmail = cols[5],
                        FromName = cols[6]
                    };
                }
                catch
                {
                    _Log.Error("config Smtp is wrong(7 cols): Host,Port,Ssl,Id,Pwd,FromEmail,FromName");
                }
            }
            #endregion

            #region set DB variables
            //0:select, 1:order by, 2:start row(base 0), 3:rows count
            switch (_dbType)
            {
                case DbTypeEnum.MSSql:
                    //for sql 2012, more easy
                    //note: offset/fetch not sql argument
                    ReadPageSql = @"
select {0} {1}
offset {2} rows fetch next {3} rows only
";
                    DeleteRowsSql = "delete {0} where {1} in ({2})";    
                    break;

                case DbTypeEnum.MySql:
                    ReadPageSql = @"
select {0} {1}
limit {2},{3}
";
                    //TODO: set delete sql for MySql
                    //DeleteRowsSql = 
                    break;

                case DbTypeEnum.Oracle:
                    //for Oracle 12c after(same as mssql)
                    ReadPageSql = @"
Select {0} {1}
Offset {2} Rows Fetch Next {3} Rows Only
";
                    //TODO: set delete sql for Oracle
                    //DeleteRowsSql = 
                    break;
            }
            #endregion
        }

        //get DI
        public static IServiceProvider GetDI()
        {
            return _DI;
        }

        //get db type
        public static DbTypeEnum GetDbType()
        {
            return _dbType;
        }

        //get system error string
        public static ResultDto GetSystemError()
        {
            return new ResultDto() { ErrorMsg = "System Error, Please check admin !" };
        }

        //get base resource for base component
        public static BaseResourceDto GetBaseR()
        {
            var service = (IBaseResourceService)_DI.GetService(typeof(IBaseResourceService));
            return service.GetData();
        }

        //get base user info for base component
        public static BaseUserInfoDto GetBaseU()
        {
            var service = (IBaseUserInfoService)_DI.GetService(typeof(IBaseUserInfoService));
            return service.GetData();
        }

        public static string GetLocale()
        {
            return GetBaseU().Locale;
        }

        /// <summary>
        /// check program access right
        /// </summary>
        /// <param name="progList">program list</param>
        /// <param name="prog">program id</param>
        /// <param name="funType">function type, see CrudEstr, empty for controller, value for action</param>
        /// <returns>bool</returns>
        public static bool CheckProgAuth(string progList, string prog, string funType)
        {
            progList = "," + progList + ",";
            if (string.IsNullOrEmpty(funType))
            {
                //prog add tail of ','
                return progList.IndexOf("," + prog + ",") >= 0;
            }
            else
            {
                //prog add tail of ':'
                var funList = _Str.GetMid(progList, "," + prog + ":", ",");
                return string.IsNullOrEmpty(funList)
                    ? false
                    : funList.IndexOf(funType) >= 0;

            }
        }
    } //class
}
