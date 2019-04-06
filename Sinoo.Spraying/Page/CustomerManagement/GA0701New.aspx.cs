using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;
namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class GA0701New : BasePage
    {
        CustomerBLL _CustomerBLL = new CustomerBLL();
        /// <summary>
        /// 列表页面跳转
        /// </summary>
        private void LinkReturn()
        {
            Response.Redirect(string.Format("GA0701List.aspx?PageIndex={0}",Request.QueryString["PageIndex"]));
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        private void DataSave()
        {
            int result = 0;
            List<Appendix> list = new List<Appendix>();
            #region 保存客户文件
            HttpFileCollection files = HttpContext.Current.Request.Files;
            try
            {
                for (int iFile = 0; iFile < files.Count; iFile++)
                {
                    HttpPostedFile postedFile = files[iFile];
                    string fileName, fileExtension, newFileName;
                    fileName = System.IO.Path.GetFileName(postedFile.FileName);//上传文件名
                    string fileNameGuid = Guid.NewGuid().ToString("N");
                    fileExtension = System.IO.Path.GetExtension(fileName);//后缀名
                    newFileName = fileNameGuid + fileExtension;//重命名
                    string fileSrc = string.Format("/File/{0}", fileName);
                    if (fileName != "")
                    {
                        Appendix _Appendix = new Appendix();
                        _Appendix.GA07002 =Request.QueryString["CA01001"].ToString();
                        _Appendix.GA07003 = fileExtension;//文件后缀
                        _Appendix.GA07004 = postedFile.ContentLength;//文件大小
                        _Appendix.GA07005 = fileSrc;
                        _Appendix.GA07006 = fileName;//旧文件名字
                        _Appendix.GA07007 = newFileName;//新文件名字
                        _Appendix.GA07008 = 1;//1 代表客户对应的文件
                        _Appendix.GA07997 = 0;
                        _Appendix.GA07998 = DateTime.Now;
                        postedFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath("/File/") + newFileName);
                        list.Add(_Appendix);
                    }
                }
                result = _CustomerBLL.AddCustomerFileBase(list);//执行保存文件
            }
            catch (System.Exception Ex)
            {
                throw Ex;
            }


            #endregion


            //成功失败提示
            new Sinoo.Common.MessageShow().InsertMessage(this, result, "DataClear();");
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CA01Data()
        {
            int CA01001 =Convert.ToInt32(Request.QueryString["CA01001"]);
            DataTable dtCA01Data = _CustomerBLL.SelectCustomerBaseByID(CA01001);
            this.txtCA01002.Text = dtCA01Data.Rows[0]["CA01002"].ToString();
            this.txtCA01003.Text = dtCA01Data.Rows[0]["CA01003"].ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                CA01Data();
            }
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSave();
        }

        /// <summary>
        /// 返回操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            LinkReturn();
        }
    }
}