using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Sinoo.Common
{
    public class MessageShow : System.Web.UI.Page
    {

        /// <summary>
        /// 弹出新增提示
        /// </summary>
        /// <param name="num">影响行数</param>
        /// <param name="defined">自定义</param>
        public void InsertMessage(Page page, int num, string defined)
        {
            if (num > 0)
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script type='text/javascript'>alert('数据保存成功');{0}</script>", defined));
            }
            else
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script type='text/javascript'>alert('数据保存失败');</script>"));
            }
        }

        /// <summary>
        /// 弹出编辑提示
        /// </summary>
        /// <param name="num">影响行数</param>
        /// <param name="defined">自定义</param>
        public void UpdateMessage(Page page, int num, string defined)
        {
            if (num > 0)
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script type='text/javascript'>alert('数据保存成功');{0}</script>", defined));
            }
            else
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script language='javascript'>alert('数据保存失败');</script>"));
            }
        }


        /// <summary>
        /// 弹出删除提示
        /// </summary>
        /// <param name="num">影响行数</param>
        /// <param name="defined">自定义</param>
        public void RemoveMessage(Page page, int num, string defined)
        {
            if (num > 0)
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script language='javascript'>alert('数据删除成功');{0}</script>", defined));
            }
            else
            {
                page.ClientScript.RegisterStartupScript(Page.GetType()
                      , "message"
                      , string.Format("<script language='javascript'>alert('数据删除失败');</script>"));
            }
        }

        /// <summary>
        /// 导出失败信息
        /// </summary>
        public void ExportErrorMessage(Page page)
        {

            page.ClientScript.RegisterStartupScript(Page.GetType()
                  , "message"
                  , string.Format("<script language='javascript'>alert('暂无数据导出');</script>"));

        }

        /// <summary>
        /// 自定义
        /// </summary>
        public void CommonMessage(Page page,string message)
        {

            page.ClientScript.RegisterStartupScript(Page.GetType()
                  , "message"
                  , string.Format("<script language='javascript'>alert('"+message+"');</script>"));

        }
    }
}
