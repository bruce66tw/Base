using Base.Models;
using System.IO;

namespace Base.Services
{
    //singleton called type
    public class BaseResourceService : IBaseResourceService
    {
        private BaseResourceDto _baseR;

        //constructor, single locale only
        public BaseResourceService()
        {
            _baseR = new BaseResourceDto(); //initial value
            var file = _Fun.DirRoot + "wwwroot/locale/" + _Fun.Config.DefaultLocale + "/BR.json";
            if (!File.Exists(file))
            {
                _Log.Error("no file: " + file);
                return;
            }

            //set _baseR
            var json = _Json.StrToJson(_File.ToStr(file));
            _Json.CopyToModel(json, _baseR);
        }

        //get base info
        public BaseResourceDto GetData()
        {
            return _baseR;
        }
    }
}
