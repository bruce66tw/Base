
using System.Collections.Generic;

namespace Base.Services
{
    /// <summary>
    /// crud edit afterSave
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public delegate string AfterSave(Db db);
    
    /// <summary>
    /// check row, return error msg if any
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="row"></param>
    /// <returns></returns>
    public delegate string CheckImportRow<T>(T row) where T : class, new();

    /// <summary>
    /// save import rows
    /// </summary>
    /// <param name="rowNos"></param>
    /// <param name="okRows"></param>
    /// <returns></returns>
    public delegate List<string> SaveImportRows<T>(List<T> okRows) where T : class, new();

}
