using Base.Models;
using Base.Services;
using Base.Models;
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
            bool inRow = false, string cols = "", string tip = "", PropSelectDto prop = null)
        {
            //set default value
            prop = prop ?? new PropSelectDto();
            //cols = cols ?? new int[] { 12, 2, 10 };

            var baseR = _Fun.GetBaseR();
            var html = _Helper.GetSelectHtml(baseR, fid, value, required, rows, prop);
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, tip, inRow, cols);

            return new HtmlString(html);
        } 

    }//class
}
