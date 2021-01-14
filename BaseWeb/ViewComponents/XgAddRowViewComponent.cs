using Base.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    //add row button
    public class XgAddRowViewComponent : ViewComponent
    {
        public HtmlString Invoke(string onClickFn)
        {
            var html = string.Format(@"
<button type='button' onclick='{0}' class='btn btn-success xg-btn-size'>{1}
    <i class='icon-plus'></i>
</button>
", onClickFn, _Fun.GetBaseR().BtnAddRow);

            return new HtmlString(html);
        }        

    }//class
}
