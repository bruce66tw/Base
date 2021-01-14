using Base.Models;
using Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace BaseWeb.Attributes
{
    public class XgProgAuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// function type, see CrudEst
        /// </summary>
        //public string FunType = "";
        private string _funType;

        public XgProgAuthAttribute(string funType = "")
        {
            _funType = funType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //get controller name
            var ctrl = (string)context.RouteData.Values["Controller"];
            //var ctrl = context.Controller...ActionDescriptor.ControllerDescriptor.ControllerName;
            //if (Prog == "")
            //    Prog = ctrl;

            //讀取 session
            //var sess = _Xp.GetSession();

            //=== check program right ===
            var userInfo = _Fun.GetBaseU();
            var isLogin = userInfo.IsLogin;
            if (isLogin && _Fun.CheckProgAuth(userInfo.ProgList, ctrl, _funType))
            {
                //case of ok
                base.OnActionExecuting(context);
                return;
            }

            /*
            if (!userInfo.IsLogin)
            {
                //檢查子功能權限是否只能admin才能執行
                if (Prog == "Home" || Prog == ctrl || _Session.IsAdmin || !_Link.IsSubProgOnlyAdmin(Prog, ctrl))
                {
                    //case of ok
                    base.OnActionExecuting(context);
                    return;
                }
            }
            */

            //=== not login or no access right below ===
            //log
            //_Log.Error("No Permission: " + Prog + "->" + filterContext.ActionDescriptor.ActionName);

            //判斷 action 種類來決定回傳結果
            //var msg = "您尚未有後台相關權限，請洽人事處進行權限申請。";
            var msg = !isLogin
                ? _Fun.GetBaseR().NotLogin
                : _Fun.GetBaseR().NoProgAuth;
            //var type = ((ReflectedActionDescriptor)context.ActionDescriptor).MethodInfo.ReturnType;
            //var returnType = context.ActionDescriptor....a.MethodInfo.ReturnType;
            var returnType = "ActionResult";    //TODO: get from routeData ??
            //if (returnType == typeof(ActionResult))
            //1.return view
            if (returnType == "ActionResult")
            {
                if (!isLogin)
                {
                    //redirect to login action
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Home" },
                            { "action", "Login" }
                        });
                }
                else
                {
                    //return view of no access right.
                    context.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/NoProgAuth.cshtml",
                    };
                }
            }
            //2.return model
            else if (returnType == "JsonResult")
            {
                context.Result = new JsonResult(new
                {
                    Value = new ResultDto() { ErrorMsg = msg }
                });
            }
            //3.return others
            //else if (type == typeof(ContentResult))
            else
            {
                //return error msg for client side
                var json = _Json.GetError(msg);
                context.Result = new ContentResult()
                {
                    Content = _Json.ToStr(json),
                };
            }
        }

    } //class
}
