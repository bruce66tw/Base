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
            //placeholder可能會包含單引號, 所以escape處理(")
            //會顯示紅色錯誤框的element 必須在 error label 上面且相鄰 !!
            //var attr = "";
            //var extClass = "";
            //var titleCols = 12 - inputCols;
            //if (fid != "")
            //    attr += " id='" + fid + "' name='" + fid + "'";
            var attr = _Helper.GetBaseAttr(fid);
            var html = $"<label{attr} data-type='read' class='form-control xi-read'>{label}</label>";

            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, false, "", inRow, cols);

            /*
            if (title != "")
            {
                //var required2 = required ? "<span class='xg-required'>*</span>" : "";
                html = string.Format(@"
<div class='row col-md-{0} xg-row'>
    <div class='col-md-{1} col-sm-12 xg-label'>{3}</div>
    <div class='col-md-{2} col-sm-12 xg-input'>
        {4}
    </div>
</div>
", cols[0], cols[1], cols[2], title, html);
            }

            html = string.Format(html, attr, label);
            */
            return new HtmlString(html);
        }

    }//class
}
