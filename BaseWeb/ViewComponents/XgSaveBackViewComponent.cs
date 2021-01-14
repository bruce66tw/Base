using Base.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XgSaveBackViewComponent : ViewComponent
    {
        public HtmlString Invoke(string fnSave = "_crud.onSave()", string fnBack = "_crud.onToRead()" )
        {
            var baseR = _Fun.GetBaseR();
            var html = string.Format(@"
<div class='xg-center' style='margin-top:25px;'>
    <button id='btnSave' type='button' class='btn xg-btn-size btn-success' onclick='{0}'>{1}<i class='icon-save'></i></button>
    <button id='btnToRead' type='button' class='btn xg-btn-size btn-primary' onclick='{2}'>{3}<i class='icon-to-list'></i></button>
</div>
", fnSave, baseR.BtnSave, fnBack, baseR.BtnToRead);

            return new HtmlString(html);
        }

    } //class
}