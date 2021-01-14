using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Services;

namespace BaseWeb.Helpers
{
    //open modal for table
    //link with button style(always enabled)
    public static class XgOpenModalHelper
    {
        public static IHtmlContent XgOpenModal(this IHtmlHelper helper, string btnOpen, string title, string col, bool required, int maxLen)
        {
            //var rb = _Locale.RB;
            var html = string.Format(@"
<a onclick='javascript:_crud.onOpenModal(this, ""{0}"", ""{1}"", {2}, {3})' class='btn btn-outline-secondary btn-sm'>{4}
</a>", title, col, (required ? "true" : "false"), maxLen, btnOpen);

            return new HtmlString(html);
        }        

    }//class
}
