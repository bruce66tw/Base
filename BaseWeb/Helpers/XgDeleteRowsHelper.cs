using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWeb.Helpers
{
    public static class XgDeleteRowsHelper
    {
        /// <summary>
        /// datatables header checkbox for delete all rows
        /// checkbox onclick fixed call _crud.onCheckAll()
        /// called:
        ///   1.list from (datatable) delete rows
        ///   2.edit form delete rows
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="isReadForm">true(for read form), false(for edit form)</param>
        /// <param name="boxStr">box container, is _multi.src for isReadForm=false</param>
        /// <param name="dataFid">check fid (data-id value)</param>
        /// <param name="fnAfterDelete">(ignore now)for edit form only</param>
        /// <returns></returns>
        //public static IHtmlContent XgDeleteAll(this IHtmlHelper htmlHelper, string box = "_me.divRead", string dataFid = "_check1", string fnOnClickDeleteAll = "_crud.onClickDeleteAll(_me.divRead)")
        public static IHtmlContent XgDeleteRows(this IHtmlHelper htmlHelper, bool isReadForm = true, string boxStr = "_me.divRead", string dataFid = "_check1", string fnAfterDelete = "")
        {
            var fnOnClickDeleteRows = isReadForm
                ? "_crud.onDeleteRows(" + boxStr + ", '" + dataFid + "')"
                : "_multi.onClickDeleteRows(" + boxStr + ")";

            //tail span for checkbox
            var html = string.Format(@"
<th width='70' class='text-center'>
    <label class='xg-check xg-delete-all'>&nbsp
        <input type='checkbox' class='checkboxes' onclick='_crud.onCheckAll(this, {0}, &quot;{1}&quot;)'>
        <span></span>
    </label>
    <button type='button' class='btn xg-delete-all-btn' onclick='{2}'>
        <i class='icon-times' style='font-size:larger; color:white;'></i>
    </button>
</th>
", boxStr, dataFid, fnOnClickDeleteRows);

            return new HtmlString(html);
        }

    } //class
}
