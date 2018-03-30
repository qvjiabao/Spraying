using System;
using System.Web;
using System.Text;

namespace Saas.Common
{
    public class JsHandler
    {

//        /// <summary>
//        /// 弹出指定页
//        /// </summary>
//        /// <param name="url"></param>
//        public static void OpenWindow(Page page, string url)
//        {
//            page.ClientScript.RegisterStartupScript(page.GetType(), "openWindow", "window.open(\"" +GetRandUrl( url) + "\");", true);
//        }

//        /// <summary>
//        /// 弹出指定页,是否加验证。isCheck=true为加验证。
//        /// </summary>
//        /// <param name="page">页面对象</param>
//        /// <param name="url">转向的页面路径</param>
//        /// <param name="isCheck">是否做权限控制</param>
//        /// <param name="connect">连接符，？或&</param>
//        public static void OpenWindow(Page page, string url, bool isCheck, string connect)
//        {
//            StringBuilder sbWindowPath = new StringBuilder();
//            if (isCheck)
//            {
//                sbWindowPath.Append("var iden = addCookie();window.open(\"").Append(url).Append(connect).Append("iden=\"+iden);");
//            }
//            else
//            {
//                sbWindowPath.Append("window.open(\"").Append(url).Append("\");");
//            }
//            page.ClientScript.RegisterStartupScript(page.GetType(), "openWindow", sbWindowPath.ToString(), true);
//        }


//        /// <summary>
//        /// 弹出JavaScript小窗口
//        /// </summary>
//        /// <param name="message"></param>
//        public static void ShowMsg(string message, System.Web.UI.Control pPointer)
//        {
//            ScriptManager.RegisterStartupScript(pPointer.Page, pPointer.GetType(), "onekey", "alert('" + message + "');", true);
//        }

//        /// <summary>
//        /// 弹出消息框并且转向到新的URL
//        /// </summary>
//        /// <param name="message">消息内容</param>
//        /// <param name="toURL">连接地址</param>
//        public static void AlertAndRedirect(string message, string toURL)
//        {
//            #region
//            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
//            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
//            #endregion
//        }
//        /// <summary>
//        /// 弹出消息框并且转向到新的URL
//        /// </summary>
//        /// <param name="control">注册控件</param>
//        /// <param name="message">消息内容</param>
//        /// <param name="toURL">连接地址</param>
//        public static void AlertAndRedirect(System.Web.UI.Control source, string message, string toURL)
//        {
//            #region
//            string js = "alert('{0}');window.location.replace('{1}')";
//            ScriptManager.RegisterClientScriptBlock(source, source.GetType(), "msg", string.Format(js, message, toURL), true);
//            #endregion
//        }
//        public static void TopRedirect(string toURL)
//        {
//            #region
//            string js = "<script language=javascript>top.window.location.replace('{0}')</script>";
//            HttpContext.Current.Response.Write(string.Format(js, toURL));
//            #endregion
//        }
//        public static void TopRedirect(string message, string toURL)
//        {
//            #region
//            string js = "<script language=javascript>alert('{0}');top.window.location.replace('{1}')</script>";
//            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
//            #endregion
//        }
//        public static void Redirect(Control source, string frame, string toURL)
//        {
//            ScriptManager.RegisterClientScriptBlock(source, source.GetType(), "top", string.Format("{0}.window.location.replace('{1}');", frame, toURL), true);
//        }

//        /// <summary>
//        /// 弹出消息框并且转向到新的URL
//        /// </summary>
//        /// <param name="page">当前页面this</param>
//        /// <param name="message">消息文本</param>
//        /// <param name="url">转向地址</param>
//        public static void AlertAndRedirect(Page page, string message, string url)
//        {
//            StringBuilder sbScript = new StringBuilder();
//            sbScript.Append("alert('").Append(message).Append("');location.href='").Append(url).Append("';");
//            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", sbScript.ToString(), true);
//        }

//        /// <summary>
//        /// 回到历史页面
//        /// </summary>
//        /// <param name="value">-1/1</param>
//        public static void GoHistory(int value)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    history.go({0});  
//                  </Script>";
//            HttpContext.Current.Response.Write(string.Format(js, value));
//            #endregion
//        }

//        /// <summary>
//        /// 关闭当前窗口
//        /// </summary>
//        public static void CloseWindow(string msg)
//        {
//            #region
//            string js = @"<script language='JavaScript'>
//                    alert('{0}');window.close();
//                  </script>";
//            HttpContext.Current.Response.Write(string.Format(js,msg));
//            HttpContext.Current.Response.End();
//            #endregion
//        }
//        /// <summary>
//        /// 关闭当前窗口并刷新父窗口
//        /// </summary>
//        public static void CloseWindowRefreshParent(string msg)
//        {
//            #region
//            string js = @"<script language='JavaScript'>
//                    alert('" + msg + @"');
//                    if(window.opener && !window.opener.closed){window.opener.location.href=window.opener.location.href;if (window.opener.progressWindow){window.opener.progressWindow.close();}}window.close();
//                  </script>";
//            HttpContext.Current.Response.Write(js);
//            HttpContext.Current.Response.End();
//            #endregion
//        }
//        /// <summary>
//        /// 关闭当前窗口
//        /// </summary>
//        public static void CloseWindow()
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    parent.opener=null;window.close();  
//                  </Script>";
//            HttpContext.Current.Response.Write(js);
//            HttpContext.Current.Response.End();
//            #endregion
//        }

//        /// <summary>
//        /// 刷新父窗口
//        /// </summary>
//        public static void RefreshParent(string url)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    window.opener.location.href='" + url + "';window.close();</Script>";
//            HttpContext.Current.Response.Write(js);
//            #endregion
//        }
//        /// <summary>
//        /// 刷新父窗口
//        /// </summary>
//        public static void RefreshParent(string url, string msg, System.Web.UI.Page pPointer)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    alert('"+msg+@"');
//                    window.opener.location.href='" + url + "';window.close();</Script>";
//            ScriptManager.RegisterStartupScript(pPointer.Page, pPointer.GetType(), "closeRefresh", js, false);
//            #endregion
//        }

//        /// <summary>
//        /// 关闭当前，并刷新父窗口
//        /// </summary>
//        public static void RefreshParent()
//        {
//            string js = @"<script type='text/javascript'>if(window.opener && !window.opener.closed){window.opener.location.href=window.opener.location.href;if (window.opener.progressWindow){window.opener.progressWindow.close();}}window.close();</script>";
//            HttpContext.Current.Response.Write(js);
//        }

        

//        /// <summary>
//        /// 只刷新父窗口t
//        /// </summary>
//        public static void RefreshParentOnly()
//        {
//            string js = @"<script type='text/javascript'>if(window.opener && !window.opener.closed){window.opener.location.href=window.opener.location.href;if (window.opener.progressWindow){window.opener.progressWindow.close();}}</script>";
//            HttpContext.Current.Response.Write(js);
//        }

//        public static void RefreshOpener(System.Web.UI.Page pPointer)
//        {
//            string js = @"<script type='text/javascript'>
///// <summary>
///// 处理随机url
///// 王洪岐
///// </summary>
//function getRandUrl(url) {
//    rand = Math.random();
//    if (url.length == 0) return url;
//    else if (url.indexOf('?') > -1) {
//        return url + '&' + rand;
//    }
//    else {
//        return url + '?' + rand;
//    }
//}
//if(window.opener && !window.opener.closed){window.opener.location.href=getRandUrl(window.opener.location.href);if (window.opener.progressWindow){window.opener.progressWindow.close();}}window.close();</script>";
//            ScriptManager.RegisterStartupScript(pPointer.Page, pPointer.GetType(), "closeRefresh", js, false);
//        }

//        /// <summary>
//        /// 弹出信息并新父窗口
//        /// </summary>
//        public static void RefreshOpener(System.Web.UI.Page pPointer, string msg)
//        {
//            string js = @"<script type='text/javascript'>
///// <summary>
///// 处理随机url
///// 王洪岐
///// </summary>
//function getRandUrl(url) {
//    rand = Math.random();
//    if (url.length == 0) return url;
//    else if (url.indexOf('?') > -1) {
//        return url + '&' + rand;
//    }
//    else {
//        return url + '?' + rand;
//    }
//}
//alert('" + msg + "');if(window.opener && !window.opener.closed){window.opener.location.href=getRandUrl(window.opener.location.href);if (window.opener.progressWindow){window.opener.progressWindow.close();}}window.close();</script>";
//            ScriptManager.RegisterStartupScript(pPointer.Page, pPointer.GetType(), "closeRefresh", js, false);
//        }

//        /// <summary>
//        /// 刷新打开窗口
//        /// </summary>
//        public static void RefreshOpener()
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    opener.location.reload();
//                  </Script>";
//            HttpContext.Current.Response.Write(js);
//            #endregion
//        }


//        /// <summary>
//        /// 打开指定大小的新窗体
//        /// </summary>
//        /// <param name="url">地址</param>
//        /// <param name="width">宽</param>
//        /// <param name="heigth">高</param>
//        /// <param name="top">头位置</param>
//        /// <param name="left">左位置</param>
//        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

//            HttpContext.Current.Response.Write(js);
//            #endregion
//        }

//        /// <summary>
//        /// 打开指定大小的新窗体
//        /// </summary>
//        /// <param name="url">地址</param>
//        /// <param name="width">宽</param>
//        /// <param name="heigth">高</param>
//        /// <param name="top">头位置</param>
//        /// <param name="left">左位置</param>
//        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left, System.Web.UI.Page pPointer)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

//            //HttpContext.Current.Response.Write(js);
//            ScriptManager.RegisterStartupScript(pPointer, pPointer.GetType(), "openWindow", js, false);
//            #endregion
//        }

//        /// <summary>
//        /// 转向Url制定的页面
//        /// </summary>
//        /// <param name="url">连接地址</param>
//        public static void JavaScriptLocationHref(string url)
//        {
//            #region
//            string js = @"<Script language='JavaScript'>
//                    window.location.replace('{0}');
//                  </Script>";
//            js = string.Format(js, url);
//            HttpContext.Current.Response.Write(js);
//            #endregion
//        }

//        /// <summary>
//        /// 打开指定大小位置的模式对话框
//        /// </summary>
//        /// <param name="webFormUrl">连接地址</param>
//        /// <param name="width">宽</param>
//        /// <param name="height">高</param>
//        /// <param name="top">距离上位置</param>
//        /// <param name="left">距离左位置</param>
//        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
//        {
//            #region
//            string features = "dialogWidth:" + width.ToString() + "px"
//                + ";dialogHeight:" + height.ToString() + "px"
//                + ";dialogLeft:" + left.ToString() + "px"
//                + ";dialogTop:" + top.ToString() + "px"
//                + ";center:yes;help=no;resizable:yes;status:no;scroll=yes";
//            ShowModalDialogWindow(webFormUrl, features);
//            #endregion
//        }

//        public static void ShowModalDialogWindow(string webFormUrl, string features)
//        {
//            string js = ShowModalDialogJavascript(webFormUrl, features);
//            HttpContext.Current.Response.Write(js);
//        }

//        public static string ShowModalDialogJavascript(string webFormUrl, string features)
//        {
//            #region
//            string js = @"<script language=javascript>							
//							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
//            return js;
//            #endregion
//        }
//        /// <summary>
//        /// 处理随机url
//        /// </summary>
//        /// <param name="url"></param>
//        /// <returns></returns>
//        public static string GetRandUrl(string url)
//        {
//            Random rd = new Random();
//            int rand = rd.Next(10000);
//            if (url.Length == 0) return url;
//            else if (url.IndexOf("?") > -1)
//            {
//                return url + "&" + rand;
//            }
//            else
//            {
//                return url + "?" + rand;
//            }
//        }
    }
}
