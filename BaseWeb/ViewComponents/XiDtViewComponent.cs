using Base.Services;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

//todo
namespace BaseWeb.Helpers
{
    //https://bootstrap-datepicker.readthedocs.io/en/latest/index.html
    public class XiDtViewComponent : ViewComponent
    {
        //fids:0(date),1(hour),2(minute)
        public HtmlString Invoke(string title, string fid, string value = "",
            bool editable = true, bool inRow = false, bool required = false,
            string labelTip = "", string inputTip = "", string extAttr = "", string extClass = "",
            int minuteStep = 10,
            string cols = "")
        {
            //set default value
            //var rb = _Locale.RB;
            //prop = prop ?? new PropDateDto();
            //cols = cols ?? new int[] { 12, 2, 10 };

            string date = "", hour = "", min = "";
            if (value != "")
            {
                var dt = _Date.CsToDt(value).Value;
                date = dt.Date.ToString();
                hour = dt.Hour.ToString();
                min = dt.Minute.ToString();
            }
            //var hourProp = new PropSelectDto() { AddEmptyRow = false };
            //var isInDt = _Helper.IsInDt(title);
            var html = string.Format(@"
<div class='xg-inline'>
    {0}
</div>
<div class='xg-inline' style='width:70px;'>
    {1}
</div>
<span>:</span>
<div class='xg-inline' style='width:70px;'>
    {2}
</div>",
_Helper.GetDateHtml($"_{fid}D", date, editable, inputTip, extAttr, extClass),
_Helper.GetSelectHtml($"_{fid}H", hour, _Date.GetHourList(), editable, false),
_Helper.GetSelectHtml($"_{fid}M", min, _Date.GetMinuteList(minuteStep), editable, false)
);

            if (title != "")
                html = _Helper.InputAddLayout(html, title, required, labelTip, inRow, cols);

            return new HtmlString(html);

            /*
            if (title != "")
            {
                var required2 = _Helper.GetRequiredSpan(required);
                html = string.Format(@"
<div class='row col-md-{0} xg-row'>
    <div class='col-md-{1} xg-label'>{3}{4}</div>
    <div class='col-md-{2} xg-input'>
        {5}
    </div>
</div>
", cols[0], cols[1], cols[2], required2, title, html);
            }

            return new HtmlString(html);
            */
        } 

    }//calss
}
