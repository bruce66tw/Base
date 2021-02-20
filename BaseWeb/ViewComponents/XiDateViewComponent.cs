﻿using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.Helpers
{
    //https://bootstrap-datepicker.readthedocs.io/en/latest/index.html
    public class XiDateViewComponent : ViewComponent
    {
        /// <summary>
        /// date field
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, string fid, string value = "",
            bool editable = true, bool inRow = false, bool required = false,
            string labelTip = "", string inputTip = "", string extAttr = "", string extClass = "", 
            string cols = "")
        {

            var html = _Helper.GetDateHtml(fid, value, editable, 
                inputTip, extAttr, extClass);
            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, labelTip, inRow, cols);

            return new HtmlString(html);

            /* 
            //使用 bootstrap-datepicker-mobile 無法寫入初始值, 先不開啟此功能 !!
            if (_Device.IsMobile())
            {
                html = @"
<input type='text' class='date-picker form-control' id='{0}' name='{0}' value='{1}' placeholder=""{2}""
    data-date-format='{5}' data-date='{1}' />
<span id='{3}' class='{4}'></span>
";

            }
            else
            {
            */

            /*
            html = @"
<div class='input-group date datepicker' data-date-format='{5}' style='padding:0px; border-radius:3px;'>
    <input type='text' id='{0}' name='{0}' value='{1}' class='form-control' placeholder=""{2}"">
    <div class='input-group-addon'>
        <i class='fa fa-calendar' aria-hidden='true'></i>
    </div>
</div>
<span id='{3}' class='{4}'></span>
";
            //}
            //暫解
            if (value.Length > 10)
                value = value.Substring(0, 10).TrimEnd();

            //把日期轉換成為所需要的格式
            html = String.Format(html, fid, value, placeHolder, fid + _WebFun.Error, _WebFun.ErrorLabelClass, _Locale.DateFormatFront);
            return new HtmlString(html);
            */
        } 

    }//calss
}
