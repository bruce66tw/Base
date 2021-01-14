using Base.Models;
using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    public static class XiSelectViewComponent
    {
        /// <summary>
        /// dropdown
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="title">0表示在Datatable內</param>
        /// <param name="fid">field id</param>
        /// <param name="value">value</param>
        /// <param name="rows">data source, 擴充方法不可使用dynamic</param>
        /// <param name="required"></param>
        /// <param name="cols">欄位layout</param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static IHtmlContent XiSelect(this IHtmlHelper helper, string title, string fid, string value, 
            List<IdStrDto> rows, bool required = false, 
            int[] cols = null, string tip = "", PropSelectDto prop = null)
        {
            return GetStr(helper, title, fid, value, rows, required, cols, tip, prop);
        }

        /*
        public static IHtmlContent XiSelectFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expr, 
            List<IdStrDto> rows, bool required = false, 
            int[] cols = null, string tip = "", PropSelectDto prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expr, helper.ViewData);
            return GetStr(helper, title, fid, value, rows, required, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(IHtmlHelper helper, string title, string fid, string value, 
            List<IdStrDto> rows, bool required, 
            int[] cols, string tip, PropSelectDto prop)
        {
            //set default value
            prop = prop ?? new PropSelectDto();
            //cols = cols ?? new int[] { 12, 2, 10 };

            var html = _Helper.GetSelectStr(helper, fid, value, required, rows, prop);
            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, tip, cols);

            return new HtmlString(html);
        } 

    }//class
}
