using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.Model;
using Sinoo.BLL;
using System.Data;
namespace Sinoo.Spraying.UserControl
{
    public partial class UserControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            OB02001 = this.txtOB02001.Value;
            CB03001 = this.txtFMenu.Value;

        }
        /// <summary>
        /// 应用代码
        /// </summary>
        public string OB02001 { get; set; }
        /// <summary>
        /// 行业分类
        /// </summary>
        public string CB03001 { get; set; }

    }
}