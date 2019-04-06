using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CA0101View : BasePage
    {
        //实例化逻辑层
        CustomerBLL _CustomerBLL = new CustomerBLL();


        /// <summary>
        /// 数据初始化绑定
        /// </summary>
        private void DataBind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["CA01001"]))
            {
                Response.Redirect("CA0101List.aspx");
            }
            DataTable dt = _CustomerBLL.SelectCustomerBaseForList(string.Format(" AND CA01001 = '{0}'", Request.QueryString["CA01001"]));

            this.labCA01002.Text = dt.Rows[0]["CA01002"].ToString();
            this.labCA01003.Text = dt.Rows[0]["CA01003"].ToString();
            this.labCA01004.Text = dt.Rows[0]["CA01004"].ToString().Replace("’", "'"); ;
            this.labCA01005.Text = dt.Rows[0]["CA01005"].ToString();
            this.labCA01006.Text = dt.Rows[0]["CA01006"].ToString();
            this.labCA01007.Text = dt.Rows[0]["CA01007"].ToString();
            this.labCA01008.Text = dt.Rows[0]["CA01008"].ToString();
            this.labCA01009.Text = dt.Rows[0]["CA01009"].ToString();
            this.labCA01010.Text = dt.Rows[0]["CA01010"].ToString();
            this.labCA01011.Text = dt.Rows[0]["CA01011"].ToString();
            this.labCA01012.Text = dt.Rows[0]["CA01012"].ToString();
            this.labCA01014.Text = dt.Rows[0]["CA01014"].ToString();
            this.labCA01015.Text = dt.Rows[0]["CA01015"].ToString();
            this.labCA01017.Text = dt.Rows[0]["CA01017"].ToString();
            this.labCA01021.Text = dt.Rows[0]["CA01021"].ToString();
            this.labCA01026.Text = dt.Rows[0]["CA01026"].ToString();
            this.labCA01027.Text = dt.Rows[0]["CA01027"].ToString();
            this.labCA01028.Text = dt.Rows[0]["CA01028"].ToString();
            this.labCA01029.Text = dt.Rows[0]["CA01029"].ToString();
            this.labCA01030.Text = dt.Rows[0]["CA01030"].ToString();
            this.labCA01031.Text = dt.Rows[0]["CA01031"].ToString();
            this.labCA01032.Text = dt.Rows[0]["CA01032"].ToString();
            this.labCA01033.Text = dt.Rows[0]["CA01033"].ToString();
            this.labCA01034.Text = dt.Rows[0]["CA01034"].ToString();
            this.labCA01035.Text = dt.Rows[0]["CA01035"].ToString();
            this.labCA01036.Text = dt.Rows[0]["CA01036"].ToString();
            this.labCA01037.Text = dt.Rows[0]["CA01037"].ToString();
            this.labCA01038.Text = dt.Rows[0]["CA01038"].ToString();
            this.labCA01039.Text = dt.Rows[0]["CA01039"].ToString();
            this.labCA01040.Text = dt.Rows[0]["CA01040"].ToString();
            this.labCA01041.Text = dt.Rows[0]["CA01041"].ToString();
            this.labCA01042.Text = dt.Rows[0]["CA01042"].ToString();
            this.labCA01043.Text = dt.Rows[0]["CA01043"].ToString();
            this.labCA01044.Text = dt.Rows[0]["CA01044"].ToString();
            this.labCA01045.Text = dt.Rows[0]["CA01045"].ToString();
            this.labCA01046.Text = dt.Rows[0]["CA01046"].ToString();
            this.labCA01048.Text = dt.Rows[0]["CA01048"].ToString();
            this.labCA01049.Text = dt.Rows[0]["CA01049"].ToString();
            this.labCA01050.Text = dt.Rows[0]["CA01050"].ToString();
            this.labCA01051.Text = dt.Rows[0]["CA01051"].ToString();
            this.labGA03Province.Text = dt.Rows[0]["ProvinceName"].ToString();
            this.labGA03City.Text = dt.Rows[0]["CityName"].ToString();
            this.labCA01020.Text = dt.Rows[0]["CA01020"].ToString();
            this.labCA01020.Text = dt.Rows[0]["CB04002"].ToString();
            this.labCA01016.Text = dt.Rows[0]["CB01002"].ToString();
            this.labCA01018.Text = dt.Rows[0]["CB02002"].ToString();
            this.labCA01019.Text = dt.Rows[0]["CB03002"].ToString();
            this.labCA01022.Text = dt.Rows[0]["GA05003"].ToString();
            this.labCA01023.Text = dt.Rows[0]["GA06003"].ToString();
            this.labCA01024.Text = dt.Rows[0]["CA01024"].ToString() == "1" ? "是" : dt.Rows[0]["CA01024"].ToString() == "0" ? "否" : "";
            this.labCA01025.Text = dt.Rows[0]["CA01025"].ToString() == "1" ? "是" : dt.Rows[0]["CA01025"].ToString() == "0" ? "否" : "";
            this.labCA01052.Text = dt.Rows[0]["CA01052"].ToString() == "1" ? "是" : dt.Rows[0]["CA01052"].ToString() == "0" ? "否" : "";
            this.labCA01047.Text = dt.Rows[0]["CA01047"].ToString() == "1" ? "是" : dt.Rows[0]["CA01047"].ToString() == "0" ? "否" : "";
        }

        /// <summary>
        /// 编辑页面跳转
        /// </summary>
        private void LinkEdit()
        {
            Response.Redirect(string.Format("CA0101Edit.aspx?CA01001={0}&source=View&PageIndex={1}", Request.QueryString["CA01001"], Request.QueryString["PageIndex"]));
        }

        /// <summary>
        /// 列表页面跳转
        /// </summary>
        private void LinkReturn()
        {
            Response.Redirect(string.Format("CA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]));
        }

        /// <summary>
        /// 页面加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        /// <summary>
        /// 返回跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            LinkReturn();
        }

        /// <summary>
        /// 进入编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkEdit();
        }
    }
}