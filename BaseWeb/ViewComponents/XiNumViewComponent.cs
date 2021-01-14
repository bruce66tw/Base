using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XiNumViewComponent : ViewComponent
    {
        /// <summary>
        /// numeric input
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="title">0表示在Datatable內</param>
        /// <param name="required"></param>
        /// <param name="inputCols"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, 
            string fid, string value = "", bool required = false, 
            bool inRow = false, string cols = "", string tip = "", PropNumDto prop = null)
        {
            prop = prop ?? new PropNumDto();

            //attr
            var type = prop.IsDigit ? "digits" : "number";
            var attr = _Helper.GetBaseAttr(fid, prop) +
                $" type='{type}' value='{value}' style='text-align:right; width:{prop.Width}'" +
                _Helper.GetRequired(required) +
                _Helper.GetPlaceHolder(prop.PlaceHolder);

            if (prop.MinValue != null)
                attr += " min='" + prop.MinValue + "'";
            if (prop.MaxValue != null)
                attr += " max='" + prop.MaxValue + "'";

            //html
            var html = $"<input{attr} data-type='num' class='form-control xi-border {prop.ExtClass}'>";

            //add title
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, tip, inRow, cols);

            return new HtmlString(html);
        } 

    }//class
}
