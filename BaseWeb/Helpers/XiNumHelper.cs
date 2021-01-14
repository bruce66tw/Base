using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    public static class XiNumHelper
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
        public static IHtmlContent XiNum(this IHtmlHelper helper, string title, 
            string fid, string value = "", bool required = false, 
            int[] cols = null, string tip = "", PropNumDto prop = null)
        {
            return GetStr(title, fid, value, required, cols, tip, prop);
        }

        /*
        public static IHtmlContent XiNumFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expr, 
            bool required = false, int[] cols = null, string tip = "",
            PropNumDto prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expr, helper.ViewData);
            return GetStr(title, fid, value, required, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(string title, string fid, string value, 
            bool required, int[] cols, string tip,
            PropNumDto prop)
        {
            prop = prop ?? new PropNumDto();

            //attr
            var attr = _Helper.GetBaseAttr(fid, prop) +
                " value='" + value + "'" +
                " style='text-align:right; width:" + prop.Width + "'" +
                " type='" + (prop.IsDigit ? "digits" : "number") + "'" +
                _Helper.GetRequired(required) +
                _Helper.GetPlaceHolder(prop.PlaceHolder);

            if (prop.MinValue != null)
                attr += " min='" + prop.MinValue + "'";
            if (prop.MaxValue != null)
                attr += " max='" + prop.MaxValue + "'";

            //html
            var html = string.Format(@"
<input{0} class='form-control {1}'>
", attr, prop.ExtClass);

            //add title
            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, tip, cols);

            return new HtmlString(html);
        } 

    }//class
}
