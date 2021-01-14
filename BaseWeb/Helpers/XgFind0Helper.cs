using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Services;

namespace BaseWeb.Helpers
{
    public static class XgFind2Helper
    {

        public static IHtmlContent XgFind2(this IHtmlHelper helper, string btnFind2, string onClickFn = "_crud.onFind2()")
        {
            //var rb = _Locale.RB;
            var html = "<button type='button' class='btn xg-btn-size btn-primary' onclick='{0}'>{1}<i class='icon-find2'></i></button>";
            return new HtmlString(String.Format(html, onClickFn, btnFind2));
        }

    } //class
}