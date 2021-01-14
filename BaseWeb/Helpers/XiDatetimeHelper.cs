using Base.Services;
using BaseWeb.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

//todo
namespace BaseWeb.Helpers
{
    //https://bootstrap-datepicker.readthedocs.io/en/latest/index.html
    public static class XiDatetimeHelper
    {
        /*
        /// <summary>
        /// date field
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="fid"></param>
        /// <param name="hint"></param>
        /// <returns></returns>
        /// string value = "", string title = "", int maxLen = 0, bool required = false, int inputCols = 10, PropTextDto prop = null
        public static IHtmlContent XgDt(this IHtmlHelper htmlHelper, string[] fids, string value = "", 
            string title = "", bool required = false, 
            int[] cols = null, PropDateModel prop = null)
        {
            return GetStr(fids, value, title, required, cols, prop);
        }
       
        /// <summary>
        /// binding model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static IHtmlContent XgDtFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expr, 
            string title = "", bool required = false, 
            int[] cols = null, PropDateModel prop = null)
        {
            _Helper.GetMetaValue(out string fid, out string value, expr, helper.ViewData);
            return GetStr(fid, value, title, required, cols, prop);
        }
        */

        //fids:0(date),1(hour),2(minute)
        public static IHtmlContent XiDatetime(this IHtmlHelper helper, string title, string[] fids, 
            string value = "", bool required = false, int minuteStep = 10, 
            int[] cols = null, PropDateDto prop = null)
        {
            //set default value
            var rb = _Locale.RB;
            prop = prop ?? new PropDateDto();
            cols = cols ?? new int[] { 12, 2, 10 };

            string date = "", hour = "", min = "";
            if (value != "")
            {
                var dt = _Date.StrToDt(value).Value;
                date = dt.Date.ToString();
                hour = dt.Hour.ToString();
                min = dt.Minute.ToString();
            }
            var hourProp = new PropSelectDto() { AddEmptyRow = false };
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
_Helper.GetDateStr(fids[0], date, required, prop, rb),
_Helper.GetSelectStr(helper, fids[1], hour, false, _Date.GetHourList(), hourProp),
_Helper.GetSelectStr(helper, fids[2], min, false, _Date.GetMinuteList(minuteStep), hourProp)
);

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
        } 

    }//calss
}
