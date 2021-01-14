using Base.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XgCreateViewComponent : ViewComponent
    {
        public HtmlString Invoke(string onClickFn = "_crud.onCreate()")
        {
            //var rb = _Locale.RB;
            var html = "<button type='button' class='btn btn-success xg-btn-size' onclick='{0}'>{1}<i class='icon-plus'></i></button>";
            return new HtmlString(string.Format(html, onClickFn, _Fun.GetBaseR().BtnCreate));
        }

    } //class
}