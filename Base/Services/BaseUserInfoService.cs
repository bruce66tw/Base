using Base.Models;

namespace Base.Services
{
    public class BaseUserInfoService : IBaseUserInfoService
    {
        private BaseUserInfoDto _baseU;

        //constructor
        public BaseUserInfoService()
        {
            _baseU = new BaseUserInfoDto()
            {
                UserId = "U01",
                UserName = "U01 name",
                DeptId = "D01",
                DeptName = "D01 name",
                Locale = _Fun.Config.DefaultLocale,
                FrontDtFormat = _Fun.Config.DefaultFrontDtFormat,
                HourDiff = 0,
            };
        }

        //get base info
        public BaseUserInfoDto GetData()
        {
            return _baseU;
        }
    }
}
