using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.IO;
using System.Data;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class GA0701List : System.Web.UI.Page
    {
        CustomerBLL _CustomerBLL = new CustomerBLL();
        AreaBLL _AreaBLL = new AreaBLL();
        UserBLL _UserBLL = new UserBLL();
        OrderBLL _OrderBLL = new OrderBLL();

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
            //文件名称
            strWhere += (this.txtGA07006.Text.Trim() == "" ? "" : string.Format(" AND GA07006 LIKE '%{0}%'", this.txtGA07006.Text.Trim()));
            //上传时间
            if (!string.IsNullOrEmpty(this.txtGA07998Start.Text.Trim()) && !string.IsNullOrEmpty(this.txtGA07998End.Text.Trim()))
            {
                strWhere += string.Format(" AND GA07998 BETWEEN '{0}' AND '{1}'", this.txtGA07998Start.Text.Trim(), this.txtGA07998End.Text.Trim());
            }
            else if (!string.IsNullOrEmpty(this.txtGA07998Start.Text.Trim()) && string.IsNullOrEmpty(this.txtGA07998End.Text.Trim()))
            {
                strWhere += string.Format(" AND GA07998>= '{0}'", this.txtGA07998Start.Text.Trim());
            }
            else if (string.IsNullOrEmpty(this.txtGA07998Start.Text.Trim()) && !string.IsNullOrEmpty(this.txtGA07998End.Text.Trim()))
            {
                strWhere += string.Format(" AND GA07998<= '{0}'", this.txtGA07998End.Text.Trim());
            }

            Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;
            if (_UserBase.UA01009 == 1)  //如果是1 则为销售员
            {
                DataTable dt = _OrderBLL.SelectCustomerBaseByUserID(_UserBase.UA01001.ToString());
                if (dt.Rows.Count > 0)
                {
                    string OA01038 = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        OA01038 += "'" + item["OA01038"].ToString() + "',";
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
        /// 绑定页面查询条件Dll
        /// </summary>
        private void BindDll()
        {
            #region 省市
            //获取省
            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();

            this.ddlGA03ProvinceDialog.DataSource = dtGA03;
            this.ddlGA03ProvinceDialog.DataTextField = "GA03002";
            this.ddlGA03ProvinceDialog.DataValueField = "GA03001";
            this.ddlGA03ProvinceDialog.DataBind();
            this.ddlGA03ProvinceDialog.Items.Insert(0, new ListItem("请选择", ""));

            //为市添加请选择选项
            this.ddlGA03CityDialog.Items.Insert(0, new ListItem("请选择", ""));

            #endregion


        }

        /// <summary>
        /// 获取当前页
        /// </summary>
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
            DataTable dt = _CustomerBLL.SelectCustomerFileBase(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (string.IsNullOrEmpty(strWhere))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"7\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"7\">没有符合查询条件的数据</td></tr>" : DataCount;

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

                DataTable dt = _CustomerBLL.SelectCustomerFileByID(Convert.ToInt32(e.CommandArgument));
                if (dt.Rows.Count > 0)
                {
                    string strFileName = dt.Rows[0]["GA07007"].ToString();
                    string strPath = HttpRuntime.AppDomainAppPath.ToString() + "File\\" + strFileName;
                    File.Delete(strPath);
                }

                int result = _CustomerBLL.RemoveCustomerFileBaseById(e.CommandArgument.ToString());

                //结果提示
                new Sinoo.Common.MessageShow().RemoveMessage(this.Page, result, "");

                if (result < 1)
                {
                    return;
                }

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
            try
            {
                if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    DataTable GA07Data = _CustomerBLL.SelectCustomerFileByID(Convert.ToInt32(e.CommandArgument.ToString()));
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
            catch (Exception ex)
            {
                throw ex;
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
                    DataRow[] dr = dt.Select("GA01003 = 'GA07btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'GA07btnDownLoad'");  //下载
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveDownLoadCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'GA07btnDel'");  //删除
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveDelCss();</script>");
                    }

                }
            }

            if (!IsPostBack)
            {
                BindDll();
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