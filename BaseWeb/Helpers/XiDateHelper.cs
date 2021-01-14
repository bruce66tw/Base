using Base.Models;
using Base.Services;
using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    //https://bootstrap-datepicker.readthedocs.io/en/latest/index.html
    public static class XiDateHelper
    {
        /// <summary>
        /// date field
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="hint"></param>
        /// <returns></returns>
        /// string value = "", string title = "", int maxLen = 0, bool required = false, int inputCols = 10, PropTextDto prop = null
        public static IHtmlContent XiDate(this IHtmlHelper helper, string title, 
            string fid, string value = "", 
            bool required = false, int[] cols = null, string tip = "",
            PropDateDto prop = null)
        {
            var rb = _Locale.RB;
            return GetStr(rb, title, fid, value, required, cols, tip, prop);
        }

        /*
        /// <summary>
        /// binding model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static IHtmlContent XiDateFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expr, 
            bool required = false, int[] cols = null, string tip = "",
            PropDateDto prop = null)
        {
            var rb = _Locale.RB;
            _Helper.GetMetaValue(out string fid, out string value, expr, helper.ViewData);
            return GetStr(rb, title, fid, value, required, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(RBDto rb, 
            string title, string fid, string value,
            bool required, int[] cols, string tip,
            PropDateDto prop)
        {
            //set default value
            prop = prop ?? new PropDateDto();
            //cols = cols ?? new int[] { 12, 2, 10 };

            var html = _Helper.GetDateStr(fid, value, required, prop, rb);
            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, tip, cols);

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
