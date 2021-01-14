using Base.Services;
using BaseWeb.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    /// <summary>
    /// for single row only
    /// </summary>
    public class XiFileViewComponent : ViewComponent
    {
        /// <summary>
        /// file upload
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fid"></param>
        /// <param name="cols"></param>
        /// <param name="fileType">I(image),E(excel),W(word)</param>
        /// <returns></returns>
        public HtmlString Invoke(string fid, string title = "", string value = "",
            bool required = false, bool inRow = false, string cols = "",
            string tip = "", int maxSize = 0, string fileType = "I",
            string extAttr = "", string extClass = "",
            string fnOnViewFile = "_me.onViewFile(this)",
            string fnOnOpenFile = "_ifile.onOpenFile(this)",
            string fnOnDeleteFile = "_ifile.onDeleteFile(this)"
            )
        {

            var attr = _Helper.GetBaseAttr(fid);
            if (!string.IsNullOrEmpty(extClass))
                extClass = " " + extClass;
            if (!string.IsNullOrEmpty(extAttr))
                extAttr += " ";
            if (maxSize <= 0)
                maxSize = _Fun.Config.UploadFileMax;
            //fileType to file Ext list
            var exts = _File.TypeToExts(fileType);
            //var attr = $" data-type='file' data-max='{maxSize}' data-exts='{exts}'";

            //button open/delete will be handled by status, but link(view) is not.
            var html = $@"
<label{extAttr} class='form-control xi-border{extClass}' style='margin-bottom:0'>
    <input type='file' onchange='_ifile.onChangeFile(this)' data-max='{maxSize}' data-exts='{exts}' style='display:none'>
    <button type='button' class='btn btn-link' onclick='{fnOnOpenFile}'>
        <i class='icon-open' title=''></i>
    </button>
    <button type='button' class='btn btn-link' onclick='{fnOnDeleteFile}'>
        <i class='icon-times'></i>
    </button>
    <a href='#' onclick='event.preventDefault(); {fnOnViewFile}'>{value}</a>
</label>
<input{attr} data-type='file' type='hidden'>";

            //add label if need
            if (!string.IsNullOrEmpty(title))
                html = _Helper.InputAddLayout(html, title, required, tip, inRow, cols);

            return new HtmlString(html);
        }
        
    } //class
}
