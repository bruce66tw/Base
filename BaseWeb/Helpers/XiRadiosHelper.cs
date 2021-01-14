using Base.Models;
using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

//todo
namespace BaseWeb.Helpers
{
    public static class XiRadiosHelper
    {
        /// <summary>
        /// Radio button group, consider horizontal or vertical
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="rows"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static IHtmlContent XiRadios(this IHtmlHelper helper, string title, 
            string fid, string value, List<IdStrDto> rows, 
            int[] cols = null, string tip = "", PropRadioDto prop = null)
        {
            return GetStr(title, fid, value, rows, cols, tip, prop);
        }

        /*
        /// <summary>
        /// todo
        /// Radio button group with Binding Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IHtmlContent XiRadiosFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, string title, Expression<Func<TModel, TProperty>> expression, 
            List<IdStrDto> rows, int[] cols = null, string tip = "",
            PropRadioDto prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expression, htmlHelper.ViewData);
            return GetStr(title, fid, value, rows, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(string title, 
            string fid, string value, List<IdStrDto> rows, 
            int[] cols, string tip, PropRadioDto prop)
        {
            prop = prop ?? new PropRadioDto();

            //box class
            var boxClass = prop.IsVertical ? "" : "xg-inline";
            if (prop.BoxClass != "")
                boxClass += " " + prop.BoxClass;

            //ext class
            var extClass = "";
            if (prop.ExtClass != "")
                extClass = " " + prop.ExtClass;

            //default input this
            prop.FnOnChange = _Helper.GetFnOnChange("onclick", prop, "this");

            //one radio
            var htmlRow = @"
<label class='xg-radio{0}'>
	<input type='radio'{1}>{2}
	<span></span>
</label>
";
            // name='PublishPlace' id='PublishPlace' value='1' onclick='_me.onChangePlace(1)'>@(_isQ ? '外網、內網' : '內網')
            //"<label{4}><input type='radio' name='{0}' value='{1}'{2}>{3}</label>";
            var list = "";
            for (var i=0; i<rows.Count; i++)
            {
                //change empty to nbsp, or radio will be wrong !!
                var row = rows[i];
                if (row.Str == "")
                    row.Str = "&nbsp;";

                //attr, no consider readonly
                var attr = (i == 0 ? " data-id='" + fid + "'" : "") +
                    " name='" + fid + "'" +
                    " value='" + row.Id + "'" +
                    prop.FnOnChange +
                    (row.Id == value ? " checked" : "");

                list += String.Format(htmlRow, extClass, attr, row.Str);
            }

            //add error span 
            var html = @"
<div class='{0}'>
    {1}
</div>";
//    < span data - id2 = '{2}' class='{3}'></span>
            html = String.Format(html, boxClass, list);

            //add title outside
            //consider this field could in datatable(no title) !!
            if (title != "")
                html = _Helper.InputAddLayout(html, title, false, tip, cols);
            /*
            {
                //var reqSpan = _Helper.GetLabelRequired(required);
                html = string.Format(@"
<div class='row xg-row'>
    <div class='col-md-{0} xg-label'>{2}</div>
    <div class='col-md-{1} xg-input'>
        {3}
    </div>
</div>
", 12 - cols, cols, title, html);
            }
            */

            return new HtmlString(html);
        }

    }//class
}
