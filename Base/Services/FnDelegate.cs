using Base.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Base.Services
{
    /// <summary>
    /// crud edit afterSave
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public delegate bool FnSetNewKey(CrudEdit editService, JObject inputJson, EditDto edit);

    /// <summary>
    /// crud edit afterSave
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public delegate string FnAfterSave(Db db);
    
    /// <summary>
    /// check row, return error msg if any
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="row"></param>
    /// <returns></returns>
    public delegate string FnCheckImportRow<T>(T row) where T : class, new();

    /// <summary>
    /// save import rows
    /// </summary>
    /// <param name="rowNos"></param>
    /// <param name="okRows"></param>
    /// <returns></returns>
    public delegate List<string> FnSaveImportRows<T>(List<T> okRows) where T : class, new();

}
