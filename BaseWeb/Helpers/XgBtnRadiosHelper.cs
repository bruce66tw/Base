using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Models;
using Base.Services;
using BaseWeb.Services;

namespace BaseWeb.Helpers
{
    public static class XgBtnRadiosHelper
    {
        /// <summary>
        /// radio buttons group
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="rows"></param>
        /// <param name="className">class name for style</param>
        /// <param name="btnsPerRow">每一列顯的 button 數目</param>
        /// <param name="onClickFn"></param>
        /// <returns></returns>
        public static IHtmlContent XgBtnRadios(this IHtmlHelper htmlHelper, string fid, string value, List<IdStrDto> rows, string className = "", int btnsPerRow = 0, string onClickFn = "")
        {
            return GetStr(fid, value, rows, className, btnsPerRow, onClickFn);
        }

        /*
        public static IHtmlContent XgBtnRadiosFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, List<IdStrDto> rows, string className = "", int btnsPerRow = 0, string onClickFn = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var fid = metadata.PropertyName;
            var value = metadata.Model != null ? metadata.Model.ToString() : "";
            return GetStr(fid, value, rows, className, btnsPerRow, onClickFn);
        }
        */

        private static IHtmlContent GetStr(string fid, string value, List<IdStrDto> rows, string className = "", int btnsPerRow = 0, string onClickFn = "")
        {
            //上一層div使用固定名稱 xxx_box, button onclick時, 必須在js改變其他按鈕為無選取 !!
            //按鈕清單放在最上面(0-n), xxx_now value 記錄目前選取的按鈕
            //error label 必須放在 _box下面且相鄰, 欄位有誤時, 會設定 _box xg-errorbox !!
            //使用hidden欄位的 data-index/data-old來記錄目前選取的button index/value
            var html = @"
<div class='btn-group' role='group' id='{0}_box'>
    {2}
    <input type='hidden' id='{0}_now' value='{3}'>
    <input type='hidden' id='{0}' name='{0}' value='{1}'>
</div>	
<div id='{5}' class='{4}'></div>
";

            //onclick時傳入參數: this, id, idx
            string htmlRow;
            int i;
            var list = "";
            var nowIdx = -1;
            var len = rows.Count;
            if (btnsPerRow > 0)
            {
                //case of 限制每一列的 button 數目
                var col = _Num.NumToRwdCol(btnsPerRow);
                //無法使用@來串字串(因為裡面有")
                htmlRow = @"
<div class='col-md-{5}'>
    <button type='button' class='btn btn-default xg-btnradio {0} {6}' data-old={4} onclick='_iRadio._onClickBtnRadio(this, ""{1}"", ""{2}""{7})'>{3}
    </button>
</div>
";
                for (i = 0; i < len; i = i + btnsPerRow)
                {
                    var row = "";
                    for (var j=0; j<btnsPerRow; j++ )
                    {
                        var k = i + j;
                        if (k >= len)
                            break;

                        var isActive = (rows[k].Id == value);
                        var active = isActive ? "active" : "";
                        if (isActive)
                            nowIdx = i;
                        row += String.Format(htmlRow, active, fid, rows[k].Id, rows[k].Str, k, col, className, (onClickFn=="") ? "" : (","+onClickFn));
                        //row += String.Format(htmlRow, active, fid, k, rows[k].Value, k, col, className);
                    }
                    list += "<div class='row'>" + row + "</div>";
                }
            }
            else
            {
                //case of 不限制每一列的 button 數目
                htmlRow = @"
<button type='button' class='btn btn-default xg-btnradio {0} {5}' data-old={4} onclick='_iRadio._onClickBtnRadio(this, ""{1}"", ""{2}""{6})'>{3}
</button>
";
                for (i = 0; i < len; i++)
                {
                    var isActive = (rows[i].Id == value);
                    var active = isActive ? "active" : "";
                    if (isActive)
                        nowIdx = i;
                    list += String.Format(htmlRow, active, fid, rows[i].Id, rows[i].Str, i, className, (onClickFn == "") ? "" : ("," + onClickFn));
                    //list += String.Format(htmlRow, active, fid, i, rows[i].Value, i, className, onClickFn == "" ? "\"\"" : onClickFn);
                }
            }

            html = String.Format(html, fid, value, list, nowIdx, _WebFun.ErrLabCls, fid + _WebFun.ErrTail);
            return new HtmlString(html);
        }
    }//class
}
