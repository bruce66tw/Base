using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XiHideViewComponent : ViewComponent
    {
        /// <summary>
        /// hidden field
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlString Invoke(string fid, string value = "")
        {
            //var isInDt = _Helper.IsInDt(title);
            var attr = _Helper.GetBaseAttr(fid);
            var html = $"<input{attr} data-type='text' type='hidden' value='{value}'>";
            return new HtmlString(html);
        }

    }//class
}
