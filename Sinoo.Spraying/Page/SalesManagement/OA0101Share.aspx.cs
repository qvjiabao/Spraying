using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0101Share : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取团队
                this.ddlUA01013.DataSource = this.GetTeamData();
                this.ddlUA01013.DataTextField = "Value";
                this.ddlUA01013.DataValueField = "Key";
                this.ddlUA01013.DataBind();
            }
        }
    }
}