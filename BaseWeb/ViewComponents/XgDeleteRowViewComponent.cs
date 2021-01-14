using Base.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    //add row button
    public class XgDeleteRowViewComponent : ViewComponent
    {
        public HtmlString Invoke(string onClickFn)
        {
            //var rb = _Locale.RB;
            var html = string.Format(@"
<button type='button' onclick='{0}' class='btn btn-link'>
    <i class='icon-times' title='{1}'></i>
</button>", onClickFn, _Fun.GetBaseR().TipDeleteRow);

            return new HtmlString(html);
        }        

    }//class
}
