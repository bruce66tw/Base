using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace BaseWeb.ViewComponents
{
    //多筆資料的新增,刪除按鈕
    public class XgDeleteUpDownViewComponent : ViewComponent
    {
        //use button for control status
        //mName: multiple table name
        public HtmlString Invoke(string mName)
        {
            var html = $@"
<button type='button' class='btn btn-link' onclick='{mName}.onDeleteRow(this)'>
    <i class='icon-times'></i>
</button>
<button type='button' class='btn btn-link' onclick='_table.rowMoveUp(this)'>
    <i class='icon-up'></i>
</button>
<button type='button' class='btn btn-link' onclick='_table.rowMoveDown(this)'>
    <i class='icon-down'></i>
</button>";
            return new HtmlString(html);
        }        

    }//class
}
