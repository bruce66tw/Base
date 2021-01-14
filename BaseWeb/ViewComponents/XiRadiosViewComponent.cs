using Base.Models;
using Base.Models;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

//TODO: pending
namespace BaseWeb.ViewComponents
{
    public class XiRadiosViewComponent : ViewComponent
    {
        /// <summary>
        /// Radio button group, consider horizontal or vertical
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="rows"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, 
            string fid, string value, List<IdStrDto> rows, 
            bool inRow = false, string cols = "", string tip = "", PropRadioDto prop = null)
        {
            prop = prop ?? new PropRadioDto();

            //box class
            var boxClass = prop.IsVertical ? "" : "xg-inline";
            //if (prop.BoxClass != "")
            //    boxClass += " " + prop.BoxClass;

            //ext class
            var extClass = "";
            if (prop.ExtClass != "")
                extClass = " " + prop.ExtClass;

            //default input this
            prop.FnOnChange = _Helper.GetFnOnChange("onclick", prop, "this");

            //one radio
            var htmlRow = @"
<label class='xg-radio{0}'>
	<input data-type='radio' type='radio'{1}>{2}
	<span></span>
</label>
";
            // name='PublishPlace' id='PublishPlace' value='1' onclick='_me.onChangePlace(1)'>@(_isQ ? '外網、內網' : '內網')
            //"<label{4}><input type='radio' name='{0}' value='{1}'{2}>{3}</label>";
            var list = "";
            for (var i=0; i<rows.Count; i++)
            {
                //change empty to nbsp, or radio will be wrong !!
                var row = rows[i];
                if (row.Str == "")
                    row.Str = "&nbsp;";

                //attr, no consider readonly
                var attr = (i == 0 ? " data-id='" + fid + "'" : "") +
                    " name='" + fid + "'" +
                    " value='" + row.Id + "'" +
                    prop.FnOnChange +
                    (row.Id == value ? " checked" : "");

                list += string.Format(htmlRow, extClass, attr, row.Str);
            }

            //add error span 
            var html = @"
<div class='{0}'>
    {1}
</div>";
//    < span data - id2 = '{2}' class='{3}'></span>
            html = string.Format(html, boxClass, list);

            //add title outside
            //consider this field could in datatable(no title) !!
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, false, tip, inRow, cols);
            /*
            {
                //var reqSpan = _Helper.GetLabelRequired(required);
                html = string.Format(@"
<div class='row xg-row'>
    <div class='col-md-{0} xg-label'>{2}</div>
    <div class='col-md-{1} xg-input'>
        {3}
    </div>
</div>
", 12 - cols, cols, title, html);
            }
            */

            return new HtmlString(html);
        }

    }//class
}
