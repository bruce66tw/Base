using Base.Models;
using Base.Services;
using Newtonsoft.Json.Linq;

namespace BaseFlow.Services
{
    public class FlowRead
    {
        private ReadModel _crud = new ReadModel()
        {
            Sql = @"
Select *
From dbo.Db
Order by Id
",
        };

        public JObject GetPage(DtModel dtIn)
        {
            return new CrudRead().GetPage(_crud, dtIn);
        }

    } //class
}
