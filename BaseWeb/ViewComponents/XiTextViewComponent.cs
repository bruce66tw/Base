using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    public class XiTextViewComponent : ViewComponent
    {
        //async
        public HtmlString Invoke(string title, string fid, string value,
            int maxLen = 0, bool required = false, bool inRow = false, 
            string type = "text", bool editable = true, string width = "100%",
            string labelTip = "", string inputTip = "",
            string extAttr = "", string extClass = "", 
            string cols = "")
        {
            //prop ??= new PropTextDto();

            //base attr: fid,name,readonly,ext attr
            var attr = _Helper.GetBaseAttr(fid, editable, extAttr) +
                $" type='{type}' value='{value}' style='width:{width}'" +     //default 100%
                _Helper.GetPlaceHolder(inputTip) +
                _Helper.GetRequired(required) +
                _Helper.GetMaxLength(maxLen);

            //get input html
            var html = $"<input{attr} data-type='text' class='form-control xi-border {extClass}'>";

            //add title,required,tip,cols for single form
            //consider this field could in datatable(no title) !!
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, labelTip, inRow, cols);

            return new HtmlString(html);
        } 

    }//class
}
