using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using System.IO;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0103List : System.Web.UI.Page
    {
        OrderBLL _OrderBLL = new OrderBLL();
        AreaBLL _AreaBLL = new AreaBLL();
        UserBLL _UserBLL = new UserBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 获取页面条件Where语句
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            string strWhere = string.Empty;

            //客户名称
            strWhere += (this.txtCA01003.Text.Trim() == "" ? "" : string.Format(" AND CA01003 LIKE '%{0}%'", this.txtCA01003.Text.Trim()));
            //订单号
            strWhere += (this.txtOA01002.Text.Trim() == "" ? "" : string.Format(" AND OA01002 LIKE '%{0}%'", this.txtOA01002.Text.Trim()));
            //上传时间
            if (!string.IsNullOrEmpty(this.txtOA01009Start.Text.Trim()) && !string.IsNullOrEmpty(this.txtOA01009End.Text.Trim()))
            {
                strWhere += string.Format(" AND OA01009 BETWEEN '{0}' AND '{1}'", this.txtOA01009Start.Text.Trim(), this.txtOA01009End.Text.Trim());
            }
            else if (!string.IsNullOrEmpty(this.txtOA01009Start.Text.Trim()) && string.IsNullOrEmpty(this.txtOA01009End.Text.Trim()))
            {
                strWhere += string.Format(" AND OA01009>= '{0}'", this.txtOA01009Start.Text.Trim());
            }
            else if (string.IsNullOrEmpty(this.txtOA01009Start.Text.Trim()) && !string.IsNullOrEmpty(this.txtOA01009End.Text.Trim()))
            {
                strWhere += string.Format(" AND OA01009<= '{0}'", this.txtOA01009End.Text.Trim());
            }

            Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;
            if (_UserBase.UA01009 == 1)  //如果是1 则为销售员
            {
                DataTable dt = _OrderBLL.SelectCustomerBaseByoOrderID(_UserBase.UA01001.ToString());
                if (dt.Rows.Count > 0)
                {
                    string OA01038 = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        OA01038 += "'" + item["OA01001"].ToString() + "',";
                    }
                    strWhere += string.Format(" AND GA07002 in ({0})", OA01038.Substring(0, OA01038.Length - 1));
                }
                else
                {
                    strWhere += string.Format(" AND 1=2 ");
                }
            }

            if (_UserBase.UA01024 == 44) //Leader权限：customer file、order file、arrival notice、order list、pending alert、weekly pending只能看到自己区域内的
            {
                strWhere += string.Format(" AND UA01013 = '{0}' ", _UserBase.UA01013);
            }
            return strWhere;
        }

        /// <summary>
        /// 获取当前页
        /// </summary>
        /// <returns></returns>
        public int GetPageIndex()
        {
            return Convert.ToInt32(ViewState["PageIndex"]);
        }

        /// <summary>
        /// 根据查询条件绑定数据
        /// </summary>
        private void InitData(int Index)
        {
            string strWhere = GetSqlWhere();

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;


            ViewState["PageIndex"] = Index;

            object RowsCount = null;
            DataTable dt = _OrderBLL.SelectOrderFileBase(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (string.IsNullOrEmpty(strWhere))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">没有符合查询条件的数据</td></tr>" : DataCount;

            this.repGA07.DataSource = dt;

            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);

            AspNetPager1.CurrentPageIndex = Index;

            this.repGA07.DataBind();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveData(object sender, CommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                DataTable dt = _OrderBLL.SelectOrderFileByID(Convert.ToInt32(e.CommandArgument));
                if (dt.Rows.Count > 0)
                {
                    string strFileName = dt.Rows[0]["GA07007"].ToString();
                    string strPath = HttpRuntime.AppDomainAppPath.ToString() + "File\\" + strFileName;
                    File.Delete(strPath);
                }
                int result = _OrderBLL.RemoveOrderFileBaseById(e.CommandArgument.ToString());

                //结果提示
                new Sinoo.Common.MessageShow().RemoveMessage(this.Page, result, "");

                if (result < 1)
                {
                    return;
                }
                string ss = e.CommandArgument.ToString();


                int CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;//当前页
                int PageCount = this.AspNetPager1.PageCount;//页总数
                int RecordCount = this.AspNetPager1.RecordCount;//记录数
                int PageSize = this.AspNetPager1.PageSize;//第页条数

                int PageIndex = CurrentPageIndex == PageCount ? (PageCount * PageSize - RecordCount == (PageSize - 1) ? CurrentPageIndex - 1 : CurrentPageIndex) : CurrentPageIndex;

                InitData(PageIndex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownLoad(object sender, CommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                DataTable GA07Data = _OrderBLL.SelectOrderFileByID(Convert.ToInt32(e.CommandArgument.ToString()));
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Request.MapPath("/File/") + GA07Data.Rows[0]["GA07007"], FileMode.Open);  // 设置文件流,filePath为文件路径
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);  // 读取
                fs.Close();
                Response.ClearContent();  // 清楚缓冲区所有内容
                Response.ClearHeaders();  // 清楚缓冲区所有头
                Response.ContentType = "application/octet-stream";  // 设置输出流的Http MIME类型
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(GA07Data.Rows[0]["GA07006"].ToString(), System.Text.Encoding.UTF8)); //fileName为需要下载的文件名
                Response.BinaryWrite(bytes);  // 写入输入流
                Response.Flush();  // 向客户端发送数据流
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_SESSION"] != null)
            {
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;

                DataTable dt = _UserBLL.SelectSystemRole(Convert.ToInt32(_UserBase.UA01024));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("GA01003 = 'OA0103btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0103btnDel'");  //删除
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript6", "<script>RemoveDelCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0103btnDownLoad'");  //下载
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript2", "<script>RemoveDownLoadCss();</script>");
                    }
                }
            }
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["PageIndex"]))  //判断当前页
                {
                    this.InitData(1);
                }
                else
                {
                    this.InitData(Convert.ToInt32(Request["PageIndex"]));
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            InitData(1);
        }

        /// <summary>
        /// 分布事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex);
        }
    }
}