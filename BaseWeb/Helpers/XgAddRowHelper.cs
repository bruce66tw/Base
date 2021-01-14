using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    //add row button
    public static class XgAddRowViewComponent
    {
        public static IHtmlContent XgAddRow(this IHtmlHelper helper, string btnAddRow, string onClickFn)
        {
            //var rb = _Locale.RB;
            var html = string.Format(@"
<button type='button' onclick='{0}' class='btn btn-success xg-btn-size'>{1}
    <i class='icon-plus'></i>
</button>
", onClickFn, btnAddRow);

            return new HtmlString(html);
        }        

    }//class
}
