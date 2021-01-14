using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
//using LightInject;
using Newtonsoft.Json.Linq;

namespace Base.Services
{
    /// <summary>
    /// package Db class to static 
    /// </summary>
    //public static class _Db<T> where T : Db, new()
    //public static class _Db<U> where U : DateFormatModel, new()
    public class _Db
    {
        /*
        //get DI by MS-SQL
        public static ServiceContainer GetMsSqlDI()
        {
            var di = new ServiceContainer();
            di.Register<string, DbConnection>((factory, arg) => new SqlConnection(arg));   //input args
            di.Register<DbCommand>((factory) => new SqlCommand());
            di.Register<DbTransaction, SqlTransaction>();  //no need new instance
            return di;
        }
        */

        #region GetJson(s)
        public static JObject GetJson(string sql, List<object> args = null, string dbStr = "")
        {
            var rows = GetJsons(sql, args, dbStr);
            return (rows == null || rows.Count == 0) ? null : (JObject)rows[0];
        }

        public static JArray GetJsons(string sql, List<object> args = null, string dbStr = "")
        {
            using (var db = new Db(dbStr))
            {
                return db.GetJsons(sql, args);
            }
        }
        #endregion        

        #region GetModel(s)
        public static T GetModel<T>(string sql, List<object> args = null, string dbStr = "")
        {
            var rows = GetModels<T>(sql, args, dbStr);
            return (rows == null || rows.Count == 0) ? default(T) : rows[0];
        }
        public static List<T> GetModels<T>(string sql, List<object> args = null, string dbStr = "")
        {
            using (var db = new Db(dbStr))
            {
                return db.GetModels<T>(sql, args);
            }
        }
        #endregion
        
        //update
        public static int Update(string sql, List<object> args = null, string dbStr = "")
        {
            using (var db = new Db(dbStr))
            {
                return db.Update(sql, args);
            }
        }

        //set row Status column to true/false
        public static bool SetRowStatus(string table, string kid, object kvalue, bool status, string statusId = "Status", string where = "", string dbStr = "")
        {
            using (var db = new Db(dbStr))
            {
                return db.SetRowStatus(table, kid, kvalue, status, statusId, where);
            }
        }

        #region remark code
        /*
        public static IdStrDto GetIdStr(string sql, Db db = null)
        {
            var rows = GetIdStrs(sql, db);
            return (rows == null) ? null : rows[0];
        }

        public static List<IdStrDto> GetIdStrs(string sql, Db db = null)
        {
            var emptyDb = (db == null);
            if (emptyDb)
                db = new Db();
            var rows = db.GetModels<IdStrDto>(sql);
            if (emptyDb)
                db.Dispose();
            return rows;
        }

        public static IdStrExtModel GetIdStrExt(string sql, Db db = null)
        {
            var rows = GetIdStrExts(sql, db);
            return (rows == null) ? null : rows[0];
        }

        public static List<IdStrExtModel> GetIdStrExts(string sql, Db db = null)
        {
            var emptyDb = (db == null);
            if (emptyDb)
                db = new Db();
            var rows = db.GetModels<IdStrExtModel>(sql);
            if (emptyDb)
                db.Dispose();
            return rows;
        }

        public static bool Update(string sql, string dbStr = "")
        {
            return Update(new SqlArgModel() { Sql = sql }, dbStr);
        }

        //case 1: 直接傳入 sql, 不建立 connection
        public static int InsertIdent(string sql, List<string> argFids = null, List<object> argValues = null, string db = "")
        {
            if (!GetDb(db))
                return 0;

            int ident = _db.InsertIdent(sql, argFids, argValues);
            _db.Close();

            return ident;
        }

        /// <summary>
        /// update db
        /// </summary>
        /// <returns>異動後的 identity JsonArray 字串或 JsonError 字串</returns>
        public static int UpdateDb(string sql, List<string> argFids = null, List<object> argValues = null, string db = "")
        {
            if (!GetDb(db))
                return 0;

            int rows = _db.UpdateDb(sql, argFids, argValues);
            _db.Dispose();

            return rows;
        }
        */
        #endregion

    }//class
}