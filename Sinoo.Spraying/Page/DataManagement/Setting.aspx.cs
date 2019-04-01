using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;

namespace Sinoo.Spraying.Page.DataManagement
{
    public partial class Setting : System.Web.UI.Page
    {

        AreaBLL _AreaBLL = new AreaBLL();  //实例化

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = _AreaBLL.GetSettings();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtValue.Text = dt.Rows[0]["Value"].ToString();
                    this.SettingId.Value = dt.Rows[0]["SettingId"].ToString();
                }
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtValue.Text.Trim())&& !string.IsNullOrEmpty(this.SettingId.Value))
            {

                int num = _AreaBLL.UpdateSettings(this.SettingId.Value, this.txtValue.Text.Trim());  //执行新增方法
                new Sinoo.Common.MessageShow().InsertMessage(this, num, string.Empty);
            }
        }
    }
}