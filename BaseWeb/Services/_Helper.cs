using Base.Models;
using Base.Services;
using System.Collections.Generic;

namespace BaseWeb.Services
{

    public static class _Helper
    {
        //public const string XgRequired = "xg-required";     //for label
        public const string XdRequired = "required";     //for input ??

        /*
        //for helper binding
        public static void GetMetaValue<TParameter, TValue>(out string fid, out string value, Expression<Func<TParameter, TValue>> expression, ViewDataDictionary<TParameter> viewData)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, viewData);
            fid = metadata.PropertyName;
            value = metadata.Model == null ? "" : metadata.Model.ToString();
            //return metadata.Model != null ? metadata.Model.ToString() : "";
        }
        */

        /// <summary>
        /// get label html string with required sign.
        /// </summary>
        /// <param name="required"></param>
        /// <returns></returns>
        public static string GetRequiredSpan(bool required)
        {
            return required ? "<span class='xg-required'>*</span>" : "";
        }

        /// <summary>
        /// get label tip with icon
        /// </summary>        
        public static string GetIconTip()
        {
            return "<i class='ico-info'></i>";
        }

        #region get attribute
        /// <summary>
        /// get attr of: data-fid,name,readonly, ext attributes
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="editable"></param>
        /// <param name="extAttr"></param>
        /// <param name="setNameAttr">set name attribute or not</param>
        /// <returns></returns>
        public static string GetBaseAttr(string fid, bool editable = true, string extAttr = "", bool setNameAttr = true)
        {
            //set data-fid, name
            var attr = $" data-fid='{fid}'";
            if (setNameAttr)
                attr += $" name='{fid}'";
            if (!editable)
                attr += " readonly";
            if (extAttr != "")
                attr += " " + extAttr;
            return attr;
        }

        //add placeholder attribute
        //placeholder could have quota, use escape
        public static string GetPlaceHolder(string inputTip)
        {
            return (inputTip == "")
                ? ""
                : " placeholder='" + inputTip + "'";
        }

        //get required attribute
        public static string GetRequired(bool required)
        {
            return required ? " required" : "";
        }

        //get maxlength attribute
        public static string GetMaxLength(int maxLen)
        {
            return (maxLen > 0) 
                ? " maxlength='" + maxLen + "'" 
                : "";
        }

        /*
        //return ext class
        public static string GetClass(string extClass)
        {
            return " class='form-control" +
                (extClass == "" ? "'" : " " + extClass + "'");
        }

        //set prop.FnOnChange
        public static string GetFnOnChange(string fnName, PropBaseDto prop, string arg)
        {
            return (prop.FnOnChange == "")
                ? ""
                : " " + fnName + "='" + (prop.FnOnChange.IndexOf("(") > 0 ? prop.FnOnChange : prop.FnOnChange + "(" + arg + ")") + "'";
        }
        */
        #endregion

        /// <summary>
        /// get date view component html string
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="required"></param>
        /// <param name="prop"></param>
        /// <param name="rb"></param>
        /// <returns></returns>
        public static string GetDateStr(string fid, string value, bool required, PropDateDto prop, BaseResDto rb)
        {
            //TODO: base attribute
            var attr = GetBaseAttr(fid) +
                GetPlaceHolder(prop.PlaceHolder) +
                GetRequired(required);

            //ext class
            //var extClass = GetExtClass(required, prop.ExtClass);

            //value -> date format
            value = _Date.StrToDateStr(value, rb.FrontDateFormat);

            //placeholder may has quota, use escape
            //input add xd-date class for check date field !!
            //input-group & input-group-addon are need for datepicker.
            return string.Format(@"
<div class='input-group date xg-date' data-provide='datepicker'>
    <input{0} value='{1}' type='text' class='form-control xd-date {2}'>
    <div class='input-group-addon'></div>
    <span>
        <i class='ico-delete' onclick='_idate.clean(this)'></i>
        <i class='ico-date' onclick='_idate.toggle(this)'></i>
    </span>
</div>
", attr, value, prop.ExtClass);
//< span data-id2='{3}' class='{4}'></span>
//", attr, value, extClass, fid + _WebFun.ErrTail, _WebFun.ErrLabCls);

        }

        /// <summary>
        /// get select view component html string
        /// </summary>
        /// <param name="br"></param>
        /// <param name="fid"></param>
        /// <param name="value"></param>
        /// <param name="required"></param>
        /// <param name="rows"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        /*
        public static string GetSelectHtml(BaseResDto br, string fid, string value, 
            bool required, List<IdStrDto> rows,
            string inputTip,
            bool editable, 
            string extAttr, string extClass,
            string fnOnChange, bool addEmptyRow,
            PropSelectDto prop = null)
        {
            //prop ??= new PropSelectDto();

            //attr
            var attr = GetBaseAttr(fid, editable, extAttr) +
                GetPlaceHolder(inputTip);
            if (fnOnChange != "")
                attr += $" onchange='{fnOnChange}'";
            //ext class
            //var extClass = required ? XdRequired : "";
            //if (prop.ExtClass != "")
            //    extClass = " " + prop.ExtClass;

            //option item
            var items = "";
            const string htmlItem = "<option value='{0}'{2}>{1}</option>";

            //add first empty row & set its title='' to show placeHolder !!
            //if (prop != null && prop.Columns <= 1)
            if (addEmptyRow)
            {
                //var rb = _Locale.RB;
                items += string.Format(htmlItem, "", br.PlsSelect, "title=''");
            }
            //{
            //    var item1 = (prop.PlaceHolder != "") ? prop.PlaceHolder : _Fun.Select;
            //    list += String.Format(htmlRow, "", item1, "");
            //}

            var len = (rows == null) ? 0 : rows.Count;
            for (var i = 0; i < len; i++)
            {
                var selected = (value == rows[i].Id) ? " selected" : "";
                items += string.Format(htmlItem, rows[i].Id, rows[i].Str, selected);
            }

            //set data-width='100%' for RWD !!
            //use class for multi columns !!
            //xg-select-col for dropdown inner width=100%, xg-select-colX for RWD width
            return $@"
<select{attr} data-type='select' class='form-control xi-border {extClass}'>
    {items}
</select>";
        }
        */

        /// <summary>
        /// get input field html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="title"></param>
        /// <param name="required"></param>
        /// <param name="labelTip"></param>
        /// <param name="cols">ary0(是否含 row div), ary1,2(for 水平), ary1(for 垂直)</param>
        /// <returns></returns>
        public static string InputAddLayout(string html, string title, bool required, 
            string labelTip, bool inRow, string cols)
        {
            //cols = cols ?? _Fun.DefHCols;
            var colList = GetCols(cols);
            var labelTip2 = "";
            var iconTip = "";
            if (!string.IsNullOrEmpty(labelTip))
            {
                labelTip2 = " title='" + labelTip + "'";
                iconTip = GetIconTip();
            }
            var reqSpan = GetRequiredSpan(required);
            string result;
            if (colList.Count > 1)
            {
                //horizontal
                result = string.Format(@"
<div class='col-md-{0} xg-label'{2}>{3}</div>
<div class='col-md-{1} xg-input'>
    {4}
</div>
", colList[0], colList[1], labelTip2, (reqSpan + title + iconTip), html);
            }
            else
            {
                //vertical
                result = string.Format(@"
<div class='col-md-{0} xg-row'>
    <div class='xg-label'{1}>{2}</div>
    <div class='xg-input'>
        {3}
    </div>
</div>
", colList[0], labelTip2, (reqSpan + title + iconTip), html);
            }

            //if not in row, add row container
            if (!inRow)
                result = "<div class='row'>" + result + "</div>";
            return result;
        }

        private static List<int> GetCols(string cols)
        {
            var values = _Str.ToIntList(cols);
            return (values == null) ? _Fun.DefHCols : values;
            /*
            var len = values.Count;
            if (len == 1)
                values.Add(values[0]);
            return values;
            */
        }

        /*
        /// <summary>
        /// ??
        /// 傳回下拉式欄位屬性字串 (bootstrap-select)
        /// </summary>
        /// <param name="prop">select 屬性 model</param>
        /// <param name="title">select title, 如果空白, 則會使用 prop.PlaceHolder</param>
        /// <returns>屬性字串 for select tag</returns>
        //public static string GetSelectProp(SelectPropModel prop, string title = "")
        public static string GetSelectProp(Select2PropModel prop)
        {
            //initial result
            //寬度必須為 data-width='100%' 才會有 RWD效果 !!
            //20161201 增加 disableslimscroll class, 手機 scrolling 才會正常 !!
            //20161215 Bruce : disableslimscroll 移到 _fun.js 處理
            var result = "data-style='" + (prop!=null?prop.ButtonClass:"") + " xg-select-btn' data-width='100%'";
            var className = "selectpicker";     //bootstrap-select default class

            //change title if need, 必須使用空白字元字碼, 直接使用空白字元無作用 !!
            //var title = (prop == null || prop.PlaceHolder == "") ? "&nbsp;" : prop.PlaceHolder;
            //20161205 Bruce : 必須將單引號轉換成為 &#39; 才能在前端正確顯示 !!
            var title = (prop == null || String.IsNullOrEmpty(prop.PlaceHolder)) ? "&nbsp;" : prop.PlaceHolder.Replace("'", "&#39;");

            result += " title='" + title + "'";

            if (prop != null)
            {

                //顯示筆數, for 考慮下拉式欄位空間
                if (prop.Size > 0)
                    result += " data-size='" + prop.Size + "'";

                //change className
                //xg-select-col 用來設定dropdown內框width=100%, xg-select-colX 用來設定RWD寬度
                if (prop.Columns > 1)
                    className += " xg-select-col xg-select-col" + prop.Columns;
                if (prop.ClassName != "")
                    className += " " + prop.ClassName;

                //add onchange
                if (!string.IsNullOrEmpty(prop.OnChange))
                    result += " onchange='" + prop.OnChange + "(this)'";

                //data-separator 用來傳遞自訂參數
                result += " data-separator='" + prop.Separator + "'";

                //dragupauto
                if (!prop.DropUpAuto)
                    result += " data-dropup-auto='false'";
            }
            result += " class='" + className + "'";
            return result;
        }
        */

    }//class
}
