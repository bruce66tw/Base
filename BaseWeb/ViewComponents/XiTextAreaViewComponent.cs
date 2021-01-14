﻿using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

//TODO: pending
namespace BaseWeb.ViewComponents
{
    /// <summary>
    /// text area
    /// </summary>
    public class XiTextAreaViewComponent : ViewComponent
    {
        /// <summary>
        /// text area with error msg
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, string fid, string value = "", 
            int maxLen = 0, bool required = false,
            bool inRow = false, string cols = "", PropTextAreaDto prop = null)
        {
            if (prop == null)
                prop = new PropTextAreaDto();

            //attr
            //var isInDt = _Helper.IsInDt(title);
            var attr = _Helper.GetBaseAttr(fid, prop) +
                " value='" + value + "'" +
                " style='width:" + prop.Width + "'" + 
                " rows='" + prop.RowsNum + "'";
            if (required)
                attr += " required";
            if (maxLen > 0)
                attr += " maxlength='" + maxLen + "'";
            //placeholder could have quota, use escape
            if (prop.PlaceHolder != "")
                attr += " placeholder='" + prop.PlaceHolder + "'";

            //html
            var html = $"<textarea{attr} data-type='textarea' class='form-control xi-border {prop.ExtClass}'></textarea>";
            /*
            var html = string.Format(@"
<div style='width:{5}; text-align:left; margin-bottom:6px;' class='{6}'>
    <textarea{0} value='{1}' class='form-control {2}'></textarea>
    <span data-id2='{3}' class='{4}'></span>
</div>", attr, value, extClass, fid + _WebFun.ErrTail, _WebFun.ErrLabCls, prop.Width, prop.BoxClass);
            */

            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, "", inRow, cols);

            //html = String.Format(html, attr, _Html.Decode(value), extClass, fid + _WebFun.Error, _WebFun.ErrorLabelClass);
            return new HtmlString(html);            
        }

    } //class
}