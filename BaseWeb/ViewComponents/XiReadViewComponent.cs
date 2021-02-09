using BaseWeb.Services;
using Microsoft.AspNetCore.Html;

namespace BaseWeb.ViewComponents
{
    public class XiReadViewComponent
    {
        //顯示文字
        public HtmlString Invoke(string title, string fid, 
            string label = "", bool inRow = false, string cols = "")
        {
            var attr = _Helper.GetBaseAttr(fid, true, "", false);
            var html = $"<label{attr} data-type='read' class='form-control xi-read'>{label}</label>";

            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, false, "", inRow, cols);

            return new HtmlString(html);
        }

    }//class
}
