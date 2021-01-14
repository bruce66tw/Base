using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    /// <summary>
    /// checkbox, 用在單筆/多筆維護UI
    /// 驗証錯誤時, 將文字變成紅色, 然後顯示 msgbox
    /// </summary>
    public class XiCheckViewComponent : ViewComponent
    {
        /// <summary>
        /// checkbox, 文字欄位含 error msg
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="isCheck"></param>
        /// <param name="value"></param>
        /// <param name="title">0表示在dt內</param>
        /// <param name="label"></param>
        /// <param name="cols"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, string fid = "", 
            bool isCheck = false, string value = "1", string label = "", 
            bool inRow = false, string cols = null, string tip = "",
            PropCheckDto prop = null)
        {
            //var isInDt = _Helper.IsInDt(title);
            prop = prop ?? new PropCheckDto();
            //if (!isInDt && label == "")
            //if (label == "")
            //    label = "&nbsp;";   //add space, or position will wrong

            //attr
            var attr = _Helper.GetBaseAttr(fid, prop) +
                _Helper.GetFnOnChange("onclick", prop, "this.checked");
            if (isCheck)
                attr += " checked";

            //box class
            //var boxClass = prop.Inline ? "xg-inline" : "";
            //var boxClass = "";
            //if (prop.BoxClass != "")
            //    boxClass += " " + prop.BoxClass;

            //ext class
            var extClass = (prop.ExtClass == "") ? "" : " " + prop.ExtClass;
            if (label == "")
                extClass += " xg-no-label";

            //if (prop.ReadOnly)
            //    attr += " readonly";
            //if (prop.IsCenter)
            //    extClass += " xd-center";

            //add error span
            var html = $@"
<label class='xg-check{extClass}'>
    <input{attr} data-type='check' type='checkbox' value='{value}'>{label}
    <span></span>
</label>";

            //add title
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, false, tip, inRow, cols);

            return new HtmlString(html);
        }

    }//class
}