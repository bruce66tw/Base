using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Services;

namespace BaseWeb.Helpers
{
    public static class XgToReadHelper
    {
        public static IHtmlContent XgToRead(this IHtmlHelper helper, string btnToRead, string onClickFn = "_crud.onToRead()")
        {
            //var rb = _Locale.RB;
            var html = string.Format(@"
<button type='button' class='btn xg-btn-size btn-primary' onclick='{0}'>{1}<i class='icon-to-list'></i></button>
", onClickFn, btnToRead);
            return new HtmlString(html);
        }

    } //class
}