using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Services;

namespace BaseWeb.Helpers
{
    public static class XgFindHelper
    {

        public static IHtmlContent XgFind(this IHtmlHelper helper, string btnFind, string onClickFn = "_crud.onFind()")
        {
            //var rb = _Locale.RB;
            var html = "<button type='button' class='btn xg-btn-size btn-primary' onclick='{0}'>{1}<i class='icon-search'></i></button>";
            return new HtmlString(String.Format(html, onClickFn, btnFind));
        }

    } //class
}