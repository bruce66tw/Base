using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    //配合 _xp.js
    public static class XgThViewComponent
    {
        /// <summary>
        /// table th
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="title">prog path list</param>
        /// <param name="tip"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        public static IHtmlContent XgTh(this IHtmlHelper helper, string title, string tip = "", bool required = false)
        {
            title = _Helper.GetRequiredSpan(required) + title;
            var html = (tip == "")
                ? "<th>" + title + "</th>"
                : "<th title='" + tip + "'>" + title + "<i class='icon-info'></i></th>";
            return new HtmlString(html);
        }

    } //class
}