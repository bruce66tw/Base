﻿using BaseWeb.Services;
using BaseWeb.Models;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    /// <summary>
    /// checkbox, 用在單筆/多筆維護UI
    /// 驗証錯誤時, 將文字變成紅色, 然後顯示 msgbox
    /// </summary>
    public static class XiCheckViewComponent
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
        public static IHtmlContent XiCheck(this IHtmlHelper helper, string title, string fid = "", 
            bool isCheck = false, string value = "1", 
            string label = "", int[] cols = null, string tip = "",
            PropCheckDto prop = null)
        {
            return GetStr(title, fid, isCheck, value, label, cols, tip, prop);
        }

        /*
        public static IHtmlContent XiCheckFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expression, string value = "1", 
            string label = "", int[] cols = null, string tip = "",
            PropCheckDto prop = null)
        {
            var metadata = helper.ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var fid = metadata.PropertyName;
            var isCheck = metadata.Model == null ? false : (bool)metadata.Model;
            return GetStr(title, fid, isCheck, value, label, cols, tip, prop);
        }
        */

        private static IHtmlContent GetStr(string title, string fid, bool isCheck, 
            string value, string label, 
            int[] cols, string tip,
            PropCheckDto prop)
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
            var html = string.Format(@"
<label class='xg-check{0}'>
    <input{1} type='checkbox' value='{2}'>{3}
    <span></span>
</label>
", extClass, attr, value, label);

            //add title
            if (title != "")
                html = _Helper.InputAddLayout(html, title, false, tip, cols);

            return new HtmlString(html);
        }

    }//class
}