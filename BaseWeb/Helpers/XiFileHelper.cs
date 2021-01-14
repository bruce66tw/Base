using BaseWeb.Models;
using BaseWeb.Services;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

//todo
namespace BaseWeb.Helpers
{
    /// <summary>
    /// for single row only
    /// </summary>
    public static class XiFileHelper
    {
        /// <summary>
        /// file upload
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="linkUrl"></param>
        /// <param name="title"></param>
        /// <param name="fileType"></param>
        /// <param name="cols"></param>
        /// <param name="labelCols"></param>
        /// <returns></returns>
        /// string value = "", string title = "", int maxLen = 0, bool required = false, int inputCols = 10, PropTextDto prop = null
        public static IHtmlContent XiFile(this IHtmlHelper helper, string title, string fid, 
            string linkUrl = "", string linkText = "", bool required = false,
            int[] cols = null, PropFileDto prop = null)
        {
            return GetStr(title, fid, linkUrl, linkText, required, cols, prop);
        }

        /*
        public static IHtmlContent XiFileFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string title, Expression<Func<TModel, TProperty>> expression, 
            string linkText = "", bool required = false,
            int[] cols = null, PropFileDto prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expression, helper.ViewData);
            return GetStr(title, fid, value, linkText, required, cols, prop);
        }
        */

        public static IHtmlContent GetStr(string title, string fid, string value, 
            string editName, bool required,
            int[] cols, PropFileDto prop)
        {
            /*
            //label onlly show file name
            var fileName = "";
            var pos = linkUrl.LastIndexOf('/');
            if (pos > 0)
                fileName = linkUrl.Substring(pos + 1);
            */

            if (prop == null)
                prop = new PropFileDto();

            //var isInDt = _Helper.IsInDt(title);
            var attr = _Helper.GetBaseAttr(fid, prop);
            //var extClass = "";
            //var titleCols = 2;
            //if (prop != null)
            //{
                //titleCols = prop.TitleCols;
                if (prop.ExtClass != "")
                    attr += " class='" + prop.ExtClass + "'";
            //}

            //when error, red border include up/down component
            //class is xd-xxxx(xxxx is id)
            //also set label class, for easy to access
            //xd-xxx-box: show red border when error
            //var showLink = (linkUrl == "") ? " style='display:none'" : "";
            attr = string.Format("data-fun='' data-id='{0}' data-max='{1}' data-exts='{2}' {3}", fid, prop.MaxSize, prop.FileExts, attr);
            //todo: 未完成
            var html = string.Format(@"
<div class='xg-file-box'>
    <input type='file' onchange='{2}.onFile(this)' style='display:none;' {4}>
    <label{0} class='form-control xg-inputlabel'>{1}</label>
    <a onclick='javascript:{2}._me.onOpenFile(this);'><i class='icon-open'></i></a>
    <a onclick='javascript:{2}._me.onViewFile(this);'><i class='icon-eye'></i></a>
    <a onclick='javascript:{2}._me.onDeleteFile(this);'><i class='icon-times'></i></a>
</div>
", fid, attr, prop.Note, fid + _WebFun.ErrTail, _WebFun.ErrLabCls);

            /*
            var html = string.Format(@"
<span class='xd-{0}-label'{3}>
    <a href='{2}' target='_blank'>{1}</a>
    <input type='button' onclick='_xp.onClickDeleteFile(&quot;{0}&quot;)' style='padding:0;font-size:small;line-height:normal' value='刪除'>
</span>
<input type='file' class='xd-{0}' data-fun='' onchange='_xp.onChangeFile(&quot;{0}&quot; , this)'>
<span class='required'>{4}</span>
", id, fileName, value, show, hint2);
            */

            //add label if need
            if (title != "")
            {
                //if (inputCols <= 0)
                //    inputCols = 10;
                var reqSpan = _Helper.GetRequiredSpan(required);
                html = string.Format(@"
<div class='row col-md-{0} xg-row'>
    <div class='col-md-{1} col-sm-12 xg-label'>{3}{4}</div>
    <div class='col-md-{2} col-sm-12 xg-input'>
        {5}
    </div>
</div>
", cols[0], cols[1], cols[2], reqSpan, title, html);
            }

            return new HtmlString(html);
        }

    } //class
}
