using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    public static class XiTextViewComponent
    {
        /// <summary>
        /// text input
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="useFid">1:欄位使用id, 其他:使用 data-id</param>
        /// <param name="title"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="maxLen"></param>
        /// <param name="required"></param>
        /// <param name="tip">label tooltip</param>
        /// <param name="inputCols"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static IHtmlContent XiText(this IHtmlHelper helper, string title, string fid, string value = "", 
            int maxLen = 0, bool required = false, 
            int[] cols = null, string tip = "", PropTextDto prop = null)
        {
            return GetStr(title, fid, value, maxLen, required, cols, tip, prop);
        }

        /*
        public static IHtmlContent XiTextFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expression, 
            int maxLen = 0, bool required = false,
            int[] cols = null, string tip = "", PropTextDto prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expression, helper.ViewData);
            return GetStr(title, fid, value, maxLen, required, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(string title, string fid, string value, 
            int maxLen, bool required, 
            int[] cols, string tip, PropTextDto prop)
        {
            prop = prop ?? new PropTextDto();

            //base attr: id,name,readonly,ext attr
            var attr = _Helper.GetBaseAttr(fid, prop) +
                " type='" + prop.Type + "'" +
                " value='" + value + "'" +
                " style='width:" + prop.Width + "'" +    //default 100%
                _Helper.GetPlaceHolder(prop.PlaceHolder) +
                _Helper.GetRequired(required) +
                _Helper.GetMaxLength(maxLen);

            //html
            //error時, 找 InputBoxCls(call closest) -> ErrLabCls
            var html = string.Format(@"
<input{0} class='form-control {1} {2}'>
", attr, prop.ExtClass, _WebFun.InputBoxCls);

            //add title,required,tip,cols
            //consider this field could in datatable(no title) !!
            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, tip, cols);

            return new HtmlString(html);
        } 

    }//class
}
