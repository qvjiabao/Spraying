using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Sinoo.BLL;
using System.Web.Script.Serialization;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// 唯一检查Handler
    /// </summary>
    public class UniquenessHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string value = string.IsNullOrEmpty(context.Request.Form[0]) ? "" : context.Request.Form[0].Trim();

            if (context.Request.Form.Count == 1) return;

            Dictionary<string, string> d = new Dictionary<string, string>();

            if (context.Request.Form[1] == "CA01New")
            {
                d = CheckCA01New(value);
            }
            else if (context.Request.Form[1] == "GA03NewCity")
            {
                d = CheckGA03City(value);
            }
            else if (context.Request.Form[1] == "GA03NewProvince")
            {
                d = CheckGA03Province(value);
            }
            else if (context.Request.Form[1] == "GA03EditCity")
            {
                d = d = CheckGA03EditCity(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "GA03EditProvince")
            {
                d = d = CheckGA03EditProvince(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "CA01Edit")
            {
                d = CheckCA01Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "GA02New")
            {
                d = CheckGA02New(value);
            }
            else if (context.Request.Form[1] == "GA02Edit")
            {
                d = CheckGA02Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "UA01007New")
            {
                d = CheckUA01007New(value);
            }
            else if (context.Request.Form[1] == "UA01007Edit")
            {
                d = CheckUA01007Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "UA01002New")
            {
                d = CheckUA01002New(value);
            }
            else if (context.Request.Form[1] == "UA01002Edit")
            {
                d = CheckUA01002Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "PA01New")
            {
                d = CheckPA01New(value);
            }
            else if (context.Request.Form[1] == "PA01Edit")
            {
                d = CheckPA01Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "CB04New")
            {
                d = CheckCB04New(value);
            }
            else if (context.Request.Form[1] == "CB04Edit")
            {
                d = CheckCB04Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "OA0102New01")
            {
                d = CheckOA0102New01(value);
            }
            else if (context.Request.Form[1] == "OA0102New02")
            {
                d = CheckOA0102New02(value);
            }
            else if (context.Request.Form[1] == "OA0102Edit")
            {
                d = CheckOA0102Edit(value, context.Request.Form[2]);
            }
            else if (context.Request.Form[1] == "OA0101New")
            {
                d = CheckOA0101New(value);
            }
            else if (context.Request.Form[1] == "OA0101Edit")
            {
                d = CheckOA0101Edit(value, context.Request.Form[2]);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(d));
        }

        /// <summary>
        /// 修改时检查GA03城市
        /// </summary>
        private Dictionary<string, string> CheckGA03EditProvince(string value, string ID)
        {
            DataTable dt = new AreaBLL().SelectSystemAreaByProvinceName(value, ID);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "省份名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 新增时检查GA03省份名称
        /// </summary>
        private Dictionary<string, string> CheckGA03Province(string value)
        {
            DataTable dt = new AreaBLL().SelectSystemAreaByProvinceName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "省份名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 新增时检查GA03城市名称
        /// </summary>
        private Dictionary<string, string> CheckGA03City(string value)
        {
            DataTable dt = new AreaBLL().SelectSystemAreaByCityName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "城市名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 修改时检查GA03省份名称
        /// </summary>
        private Dictionary<string, string> CheckGA03EditCity(string value, string ID)
        {
            DataTable dt = new AreaBLL().SelectSystemAreaByCityName(value, ID);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "城市名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 新增时检查OA0101订单编码
        /// </summary>
        private Dictionary<string, string> CheckOA0101New(string value)
        {
            DataTable dt = new OrderBLL().SelectOrderBaseByName2(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Order No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 修改时检查OA0101订单编码
        /// </summary>
        private Dictionary<string, string> CheckOA0101Edit(string value, string ID)
        {
            DataTable dt = new OrderBLL().SelectOrderBaseByNameForEdit(value, ID);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Order No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 修改时检查OA0102订单编码
        /// </summary>
        private Dictionary<string, string> CheckOA0102Edit(string value, string ID)
        {
            DataTable dt = new OrderBLL().SelectOrderBaseByName2(value, ID);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Order No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 新增时检查OA0102订单编码
        /// </summary>
        private Dictionary<string, string> CheckOA0102New01(string value)
        {
            DataTable dt = new OrderBLL().SelectOrderBaseByName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Order No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }



        /// <summary>
        /// 新增时检查OA0102订单编码（退货前订单号）
        /// </summary>
        private Dictionary<string, string> CheckOA0102New02(string value)
        {
            DataTable dt = new OrderBLL().SelectOrderBaseByName2(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count <= 0)
            {
                d.Add("error", "不存在当前订单号");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 修改时检查CB04行业代码
        /// </summary>
        private Dictionary<string, string> CheckCB04Edit(string value, string ID)
        {
            DataTable dt = new CustomerBLL().SelectCustomerTradeCodeByIDName(ID, value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "SIC已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }



        /// <summary>
        /// 新增时检查CB04行业代码
        /// </summary>
        private Dictionary<string, string> CheckCB04New(string value)
        {
            DataTable dt = new CustomerBLL().SelectCustomerTradeCodeByName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "SIC已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 修改时检查PA01产品型号
        /// </summary>
        private Dictionary<string, string> CheckPA01Edit(string value, string ID)
        {
            DataTable dt = new ProductBLL().SelectProductBaseByIDName(ID, value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Part No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }



        /// <summary>
        /// 新增时检查PA01产品型号
        /// </summary>
        private Dictionary<string, string> CheckPA01New(string value)
        {
            DataTable dt = new ProductBLL().SelectProductBaseByName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "Part No已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 修改时检查GA02角色名称
        /// </summary>
        private Dictionary<string, string> CheckGA02Edit(string value, string ID)
        {
            DataTable dt = new UserBLL().SelectSystemRoleByIdName(ID, value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "角色名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 新增时检查GA02角色名称
        /// </summary>
        private Dictionary<string, string> CheckGA02New(string value)
        {
            DataTable dt = new UserBLL().SelectSystemRoleByName(value);

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "角色名称已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }


        /// <summary>
        /// 新增时检查CA01客户编号
        /// </summary>
        private Dictionary<string, string> CheckCA01New(string value)
        {
            DataTable dt = new CustomerBLL().SelectCustomerBase(string.Format(" AND CA01002 = '{0}' ", value));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "客户编码已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 修改时检查CA01客户编号
        /// </summary>
        private Dictionary<string, string> CheckCA01Edit(string value, string ID)
        {
            DataTable dt = new CustomerBLL().SelectCustomerBase(string.Format(" AND CA01002 = '{0}' AND CA01001 <> '{1}'", value, ID));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "客户编码已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 检查新增用户的身份证号
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CheckUA01007New(string value)
        {
            DataTable dt = new UserBLL().SelectUserBase(string.Format(" AND UA01007 = '{0}'", value));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "身份证号已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 检查修改用户的身份证号
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CheckUA01007Edit(string value, string ID)
        {
            DataTable dt = new UserBLL().SelectUserBase(string.Format(" AND UA01007 = '{0}' AND UA01001 <> '{1}'", value, ID));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "身份证号已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 检查新增用户的系统帐户号
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CheckUA01002New(string value)
        {
            DataTable dt = new UserBLL().SelectUserBase(string.Format(" AND UA01002 = '{0}'", value));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "系统帐户已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        /// <summary>
        /// 检查修改用户的系统帐户号
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CheckUA01002Edit(string value, string ID)
        {
            DataTable dt = new UserBLL().SelectUserBase(string.Format(" AND UA01002 = '{0}' AND UA01001 <> '{1}'", value, ID));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (dt.Rows.Count > 0)
            {
                d.Add("error", "系统帐户已被占用");
            }
            else
            {
                d.Add("ok", "");
            }
            return d;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}