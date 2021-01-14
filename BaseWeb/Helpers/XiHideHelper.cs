using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using BaseWeb.Services;

namespace BaseWeb.Helpers
{
    public static class XiHideViewComponent
    {
        /// <summary>
        /// note: Html.Hidden only has name property, no id !!
        /// //for CSRF, field id fixed to _Fun.HideKey
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlContent XiHide(this IHtmlHelper htmlHelper, string fid, string value = "")
        {
            return GetStr(fid, value);
        }

        /*
        public static IHtmlContent XiHideFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            _Helper.GetMetaValue(out string fid, out string value, expression, helper.ViewData);
            return GetStr(fid, value);
        }
        */

        private static IHtmlContent GetStr(string fid, string value)
        {
            //var isInDt = _Helper.IsInDt(title);
            var attr = _Helper.GetBaseAttr(fid);
            var html = "<input{0} type='hidden' value='{1}'>";
            return new HtmlString(String.Format(html, attr, value));
        }

    }//class
}
