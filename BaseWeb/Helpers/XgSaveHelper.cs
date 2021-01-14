using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Services;

namespace BaseWeb.Helpers
{
    public static class XgSaveHelper
    {

        public static IHtmlContent XgSave(this IHtmlHelper helper, string btnSave, string onClickFn = "_crud.onSave()")
        {
            //var rb = _Locale.RB;
            var html = "<button id='_btnSave' type='button' class='btn xg-btn-size btn-success' onclick='{0}'>{1}<i class='icon-save'></i></button>";
            return new HtmlString(String.Format(html, onClickFn, btnSave));
        }

    } //class
}