using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XiTextViewComponent : ViewComponent
    {
        //async
        public HtmlString Invoke(
            string title, string fid, string value,
            int maxLen, bool required = false, bool inRow = false, 
            string cols = "", string tip = "", PropTextDto prop = null)
        {
            prop ??= new PropTextDto();

            //base attr: id,name,readonly,ext attr
            var attr = _Helper.GetBaseAttr(fid, prop) +
                $" type='{prop.Type}' value='{value}' style='width:{prop.Width}'" +     //default 100%
                _Helper.GetPlaceHolder(prop.PlaceHolder) +
                _Helper.GetRequired(required) +
                _Helper.GetMaxLength(maxLen);

            //html
            var html = $"<input{attr} data-type='text' class='form-control xi-border {prop.ExtClass}'>";

            //add title,required,tip,cols
            //consider this field could in datatable(no title) !!
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, tip, inRow, cols);

            return new HtmlString(html);
        } 

    }//class
}
