using Base.Models;
using Base.Services;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BaseWeb.ViewComponents
{
    public class XiSelectViewComponent : ViewComponent
    {
        /// <summary>
        /// dropdown
        /// </summary>
        /// <param name="title">0表示在Datatable內</param>
        /// <param name="fid">field id</param>
        /// <param name="value">value</param>
        /// <param name="rows">data source, 擴充方法不可使用dynamic</param>
        /// <param name="required"></param>
        /// <param name="cols">欄位layout</param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public HtmlString Invoke(string title, string fid, string value, 
            List<IdStrDto> rows, bool required = false, 
            bool inRow = false, string cols = "",
            string labelTip = "", string inputTip = "",
            bool editable = true,
            string extAttr = "", string extClass = "",
            string fnOnChange = "", bool addEmptyRow = true)
        {
            //set default value
            //prop = prop ?? new PropSelectDto();
            //cols = cols ?? new int[] { 12, 2, 10 };

            var br = _Fun.GetBaseRes();
            //var html = _Helper.GetSelectHtml(br, fid, value, required, rows, prop);

            //=== start
            var attr = _Helper.GetBaseAttr(fid, editable, extAttr) +
                _Helper.GetPlaceHolder(inputTip);
            if (fnOnChange != "")
                attr += $" onchange='{fnOnChange}'";
            //ext class
            //var extClass = required ? XdRequired : "";
            //if (prop.ExtClass != "")
            //    extClass = " " + prop.ExtClass;

            //option item
            var optItems = "";
            var tplOpt = "<option value='{0}'{2}>{1}</option>";

            //add first empty row & set its title='' to show placeHolder !!
            //if (prop != null && prop.Columns <= 1)
            if (addEmptyRow)
                optItems += string.Format(tplOpt, "", br.PlsSelect, "title=''");
            //{
            //    var item1 = (prop.PlaceHolder != "") ? prop.PlaceHolder : _Fun.Select;
            //    list += String.Format(htmlRow, "", item1, "");
            //}

            var len = (rows == null) ? 0 : rows.Count;
            for (var i = 0; i < len; i++)
            {
                var selected = (value == rows[i].Id) ? " selected" : "";
                optItems += string.Format(tplOpt, rows[i].Id, rows[i].Str, selected);
            }

            //set data-width='100%' for RWD !!
            //use class for multi columns !!
            //xg-select-col for dropdown inner width=100%, xg-select-colX for RWD width
            var html = $@"
<select{attr} data-type='select' class='form-control xi-border {extClass}'>
    {optItems}
</select>";
            //=== end

            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, labelTip, inRow, cols);

            return new HtmlString(html);
        } 

    }//class
}
