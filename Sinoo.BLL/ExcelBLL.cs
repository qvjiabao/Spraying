using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using System.Data;
using Sinoo.Model;
using Sinoo.Sql;
using System.Reflection;
using System.Data.SqlClient;

namespace Sinoo.BLL
{
    public class ExcelBLL
    {
        //实例化数据访问类
        SqlServerProvider Provider = new SqlServerProvider();

        /// <summary>
        /// 导入EXCEL（产品资料）
        /// </summary>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        public Dictionary<string, string> ImportCustomerBase(DataTable dt)
        {
            CustomerBLL _CustomerBLL = new CustomerBLL();
            AreaBLL _AreaBLL = new AreaBLL();

            List<CustomerBase> listCustomer;
            int result = 0;  //记录错误行号
            CustomerBase _CustomerBase = null;
            DataTable dt1 = _CustomerBLL.SelectCustomerBase(); //所有客户名称
            DataTable dt2 = _AreaBLL.SelectSystemAreaAll();  //查询所有省市
            DataTable dt3 = _CustomerBLL.SelectCustomerType();  //查询所有客户类型 (CB01)
            DataTable dt4 = _CustomerBLL.SelectCustomerCode();  //查询所有CTC (CB02)
            DataTable dt5 = _CustomerBLL.SelectCustomerTradeCode();  //查询所有SIC (CB04)
            DataTable dt6 = _CustomerBLL.SelectElectronType();  //查询所有电子分类 240 (GA05)
            DataTable dt7 = _CustomerBLL.SelectCustomerTradeTypeByCustomerTradeCode();  //查询所有MDT (CB03)
            DataTable dt8 = _CustomerBLL.SelectAuteType();  //查询所有220 (GA06)

            string _str = string.Empty;  //记录所有导入错误的编号
            int i = 0; //fou循环
            int count = 0;  //记录成功条数
            int existsNum = 0;  //记录重复数据
            int errorNum = 0; //异常信息
            foreach (DataRow item in dt.Rows)
            {
                listCustomer = new List<CustomerBase>();

                DataRow[] drArr = dt1.Select(string.Format(" CA01003 = '{0}'", item["Customer Name"]));//查询
                if (drArr.Length > 0)
                {
                    existsNum++;
                    _str += "," + i.ToString();
                    i++;
                    continue;
                }
                try
                {
                    _CustomerBase = new CustomerBase();
                    _CustomerBase.CA01002 = item["Customer Code"].ToString();
                    _CustomerBase.CA01003 = item["Customer Name"].ToString();
                    _CustomerBase.CA01004 = item["Customer Ename"].ToString();
                    _CustomerBase.CA01005 = item["Billing Add/Tel"].ToString();
                    _CustomerBase.CA01006 = item["Bank Name"].ToString();
                    _CustomerBase.CA01007 = item["Bank Account"].ToString();
                    _CustomerBase.CA01008 = item["Tax Registration No"].ToString();
                    _CustomerBase.CA01009 = item["Contact person"].ToString();
                    _CustomerBase.CA01010 = item["Tel"].ToString();
                    _CustomerBase.CA01011 = item["Fax"].ToString();
                    _CustomerBase.CA01012 = item["Post Code"].ToString();
                    DataRow[] dr2 = dt2.Select(string.Format(" GA03002 = '{0}' AND PNAME = '{1}' ", item["City"], item["Province"]));
                    if (dr2.Length > 0)
                    {
                        _CustomerBase.CA01013 = dr2[0]["GA03001"].ToString();
                    }

                    _CustomerBase.CA01014 = item["Credit Days"].ToString();
                    _CustomerBase.CA01015 = item["Credit Amount"].ToString();
                    DataRow[] dr3 = dt3.Select(string.Format(" CB01002 = '{0}'", item["Customer Type"]));
                    if (dr3.Length > 0)
                    {
                        _CustomerBase.CA01016 = Convert.ToInt32(dr3[0]["CB01001"]);
                    }

                    _CustomerBase.CA01017 = item["Grade"].ToString();


                    DataRow[] dr4 = dt4.Select(string.Format(" CB02002 = '{0}'", item["CTC"]));
                    if (dr4.Length > 0)
                    {
                        _CustomerBase.CA01018 = dr4[0]["CB02001"].ToString();
                    }


                    DataRow[] dr7 = dt7.Select(string.Format(" CB03002 = '{0}'", item["MDT"]));
                    if (dr7.Length > 0)
                    {
                        _CustomerBase.CA01019 = Convert.ToInt32(dr7[0]["CB03001"]);
                    }

                    DataRow[] dr5 = dt5.Select(string.Format(" CB04002 = '{0}'", item["SIC"]));
                    if (dr5.Length > 0)
                    {
                        _CustomerBase.CA01020 = Convert.ToInt32(dr5[0]["CB04001"]);
                    }
                    _CustomerBase.CA01021 = item["Devision Code"].ToString();


                    DataRow[] dr6 = dt6.Select(string.Format(" GA05002 = '{0}'", item["240 Category"]));
                    if (dr6.Length > 0)
                    {
                        _CustomerBase.CA01022 = dr6[0]["GA05001"].ToString();
                    }

                    DataRow[] dr8 = dt8.Select(string.Format(" GA06002 = '{0}'", item["220 Category"]));
                    if (dr8.Length > 0)
                    {
                        _CustomerBase.CA01023 = dr8[0]["GA06001"].ToString();
                    }

                    if (item["FGD"] is DBNull
                        || item["FGD"].ToString() == "无"
                        || string.IsNullOrEmpty(item["FGD"].ToString()))
                    {
                        _CustomerBase.CA01024 = null;
                    }
                    else
                    {
                        if (item["FGD"].ToString() == "否")
                        {
                            _CustomerBase.CA01024 = 0;
                        }
                        else
                        {
                            _CustomerBase.CA01024 = 1;
                        }
                    }

                    if (item["Credit Customer"] is DBNull
                        || item["Credit Customer"].ToString() == "无"
                        || string.IsNullOrEmpty(item["Credit Customer"].ToString()))
                    {
                        _CustomerBase.CA01025 = null;
                    }
                    else
                    {
                        if (item["Credit Customer"].ToString() == "否")
                        {
                            _CustomerBase.CA01025 = 0;
                        }
                        else
                        {
                            _CustomerBase.CA01025 = 1;
                        }
                    }


                    //if (item["Customer TypeID"] != null) { _CustomerBase.CA01024 = Convert.ToInt32(item["FGD"].ToString()); }
                    //if (item["Customer TypeID"] != null) { _CustomerBase.CA01025 = Convert.ToInt32(item["Credit Customer"].ToString()); }
                    _CustomerBase.CA01026 = item["Comment"].ToString();
                    _CustomerBase.CA01027 = item["注册地址"].ToString();
                    _CustomerBase.CA01028 = item["实际办公地址"].ToString();
                    _CustomerBase.CA01029 = item["网址"].ToString();
                    _CustomerBase.CA01030 = item["电子信箱"].ToString();
                    _CustomerBase.CA01031 = item["成立日期"].ToString();
                    _CustomerBase.CA01032 = item["法定代表人"].ToString();
                    _CustomerBase.CA01033 = item["注册资本"].ToString();
                    _CustomerBase.CA01034 = item["登记机关"].ToString();
                    _CustomerBase.CA01035 = item["注册号"].ToString();
                    _CustomerBase.CA01036 = item["经营范围"].ToString();
                    _CustomerBase.CA01037 = item["经营期限"].ToString();
                    _CustomerBase.CA01038 = item["企业类型"].ToString();
                    _CustomerBase.CA01039 = item["所属行业"].ToString();
                    _CustomerBase.CA01040 = item["上级主管单位或母公司情况"].ToString();
                    _CustomerBase.CA01041 = item["股东背景"].ToString();
                    _CustomerBase.CA01042 = item["厂区情况"].ToString();
                    _CustomerBase.CA01043 = item["仓库情况"].ToString();
                    _CustomerBase.CA01044 = item["办公环境"].ToString();
                    _CustomerBase.CA01045 = item["员工素质"].ToString();
                    _CustomerBase.CA01046 = item["财务状况"].ToString();

                    if (item["key customer"] is DBNull
                        || item["key customer"].ToString() == "无"
                        || string.IsNullOrEmpty(item["key customer"].ToString()))
                    {
                        _CustomerBase.CA01047 = null;
                    }
                    else
                    {
                        if (item["key customer"].ToString() == "否")
                        {
                            _CustomerBase.CA01047 = 0;
                        }
                        else
                        {
                            _CustomerBase.CA01047 = 1;
                        }
                    }

                    listCustomer.Add(_CustomerBase);
                    result += Math.Abs(_CustomerBLL.AddCustomerBase(listCustomer));
                    if (result == 0)
                    {
                        _str += "," + i.ToString();
                    }
                    else
                    {
                        count++;
                    }
                    i++;
                }
                catch (Exception)
                {
                    errorNum++;
                }

            }

            //插入结果行数

            // errorNum += (listCustomer.Count - result);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("existsNum", existsNum.ToString());
            dictionary.Add("errorNum", errorNum.ToString());
            dictionary.Add("_str", _str);
            dictionary.Add("count", count.ToString());

            return dictionary;
        }

        /// <summary>
        /// 导入EXCEL（产品资料）
        /// </summary>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        public Dictionary<string, int> ImportProductBase(DataTable dt)
        {
            ProductBLL _ProductBLL = new ProductBLL();

            DataTable dt1 = _ProductBLL.SelectProductBase();

            int existsNum = 0;
            int errorNum = 0;
            int InsertCount = 0;
            foreach (DataRow item in dt.Rows)
            {
                DataRow[] drArr = dt1.Select(string.Format(" PA01003 = '{0}'", item["Part No"]));//查询
                if (drArr.Length > 0)
                {
                    existsNum++;
                    continue;
                }
                try
                {
                    ProductBase _ProductBase = new ProductBase();
                    _ProductBase.PA01003 = item["Part No"].ToString();
                    _ProductBase.PA01005 = item["Description"].ToString();
                    _ProductBase.Priceone = item["Guide Price One"].ToString();
                    _ProductBase.Pricetwo = item["Guide Price Two"].ToString();
                    _ProductBase.Pricethree = item["Guide Price Three"].ToString();
                    _ProductBase.Pricefour = item["Guide Price Four"].ToString();
                    _ProductBase.Netprice = item["Net Price"].ToString();
                    InsertCount += Math.Abs(_ProductBLL.InsertProductBase(_ProductBase));
                }
                catch (Exception)
                {
                    errorNum++;
                }
            }
            errorNum += (dt.Rows.Count - existsNum - InsertCount);

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("existsNum", existsNum);
            dictionary.Add("errorNum", errorNum);

            return dictionary;
        }

        /// <summary>
        /// 导入EXCEL（订单资料）
        /// </summary>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        public Dictionary<string, string> ImportOrderBase(DataSet ds, UserBase us)
        {
            UserBLL _UserBll = new UserBLL();
            CustomerBLL _CustomerBLL = new CustomerBLL();
            List<string> _list = new List<string>(); //存数Sql语句
            CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
            CreateSqlHandler<OrderProduct> _CreateSqlHandler1 = new CreateSqlHandler<OrderProduct>();
            CreateSqlHandler<OrderInvoice> _CreateSqlHandler2 = new CreateSqlHandler<OrderInvoice>();
            CreateSqlHandler<OrderPayment> _CreateSqlHandler3 = new CreateSqlHandler<OrderPayment>();
            ProductBLL _ProductBLL = new ProductBLL();
            UserBase _UserBase = new UserBase();
            UserBase _UserBaseSession = us;   //用户登录信息Session
            string error = string.Empty;  //异常信息
            bool bl = true;

            DataTable dt1 = new DataTable();  //订单表
            DataTable dt2 = new DataTable();  //订单明细
            DataTable dt3 = new DataTable();  //发票信息
            DataTable dt4 = new DataTable();  //付款信息
            foreach (DataTable item in ds.Tables)
            {
                switch (item.TableName)
                {
                    case "订单信息":
                        dt1 = item.Copy();
                        break;
                    case "订单明细":
                        dt2 = item.Copy();
                        break;
                    case "发票发货信息":
                        dt3 = item.Copy();
                        break;
                    case "付款信息":
                        dt4 = item.Copy();
                        break;
                }
            }
            DataTable dt5 = _UserBll.SelectUserBasePosition(us.UA01013);  //所有的销售人. （根据用户登录的所属区域）
            DataTable dt6 = _CustomerBLL.SelectAC();  //所有应用代码 
            DataTable dt7 = _CustomerBLL.SelectCustomerBase();  //客户资料
            DataTable dt8 = _ProductBLL.SelectProductBase();  //所有商品
            DataTable dt9 = _UserBll.SelectUserBasePosition(); //查询所有用户

            if (dt1.Rows.Count > 0)
            {
                int a = 2;
                foreach (DataRow item in dt1.Rows)
                {
                    DataRow[] dr = dt1.Select(string.Format(" 订单号 = '{0}' ", item["订单号"].ToString()));
                    if (dr.Length > 1)
                    {
                        error = string.Format("</br>订单信息存在重复订单号 请修改后重新导入！错误行号为：{0}", a.ToString());
                        bl = false;
                    }
                    a++;
                }

                int b = 2;
                foreach (DataRow item in dt2.Rows)  //检索订单明细中的的错误订单号
                {
                    DataRow[] dr = dt1.Select(string.Format(" 订单号 = '{0}' ", item["订单号"].ToString()));
                    if (dr.Length == 0)
                    {
                        error = string.Format("</br>订单详细中存在错误订单号 请修改后重新导入！错误行号为：{0}", b.ToString());
                        bl = false;

                    }
                    b++;
                }

                int c = 2;
                foreach (DataRow item in dt3.Rows)  //检索订单明细中的的错误订单号
                {
                    DataRow[] dr = dt1.Select(string.Format(" 订单号 = '{0}' ", item["订单号"].ToString()));
                    if (dr.Length == 0)
                    {
                        error += string.Format("</br>发票发货信息中存在错误订单号 请修改后重新导入！错误行号为：{0}", c.ToString());
                        bl = false;

                    }
                    c++;
                }

                int d = 2;
                foreach (DataRow item in dt4.Rows)  //检索订单明细中的的错误订单号
                {
                    DataRow[] dr = dt1.Select(string.Format(" 订单号 = '{0}' ", item["订单号"].ToString()));
                    if (dr.Length == 0)
                    {
                        error += string.Format("</br>付款信息中存在错误订单号 请修改后重新导入！错误行号为：{0}", d.ToString());
                        bl = false;

                    }
                    d++;
                }

                #region 转换列名订单
                dt1.Columns[0].ColumnName = "OA01002";
                dt1.Columns[1].ColumnName = "OA01003";
                dt1.Columns[2].ColumnName = "OA01005";
                dt1.Columns[3].ColumnName = "OA01008";
                dt1.Columns[4].ColumnName = "OA01009";
                dt1.Columns[5].ColumnName = "OA01010";
                dt1.Columns[6].ColumnName = "OA01011";
                dt1.Columns[7].ColumnName = "OA01012";
                dt1.Columns[8].ColumnName = "OA01013";
                dt1.Columns[9].ColumnName = "OA01014";
                dt1.Columns[10].ColumnName = "OA01015";
                dt1.Columns[11].ColumnName = "OA01016";
                dt1.Columns[12].ColumnName = "OA01017";
                dt1.Columns[13].ColumnName = "OA01018";
                dt1.Columns[14].ColumnName = "OA01019";
                dt1.Columns[15].ColumnName = "OA01020";
                dt1.Columns[16].ColumnName = "OA01021";
                dt1.Columns[17].ColumnName = "OA01022";
                dt1.Columns[18].ColumnName = "OA01023";
                dt1.Columns[19].ColumnName = "OA01024";
                dt1.Columns[20].ColumnName = "OA01025";
                dt1.Columns[21].ColumnName = "OA01026";
                dt1.Columns[22].ColumnName = "OA01027";
                dt1.Columns[23].ColumnName = "OA01028";
                dt1.Columns[24].ColumnName = "OA01029";
                dt1.Columns[25].ColumnName = "OA01030";
                dt1.Columns[26].ColumnName = "OA01031";
                dt1.Columns[27].ColumnName = "OA01032";
                dt1.Columns[28].ColumnName = "OA01033";
                dt1.Columns[29].ColumnName = "OA01034";
                dt1.Columns[30].ColumnName = "OA01035";
                dt1.Columns[31].ColumnName = "OA01036";
                dt1.Columns[32].ColumnName = "OA01044";
                dt1.Columns[33].ColumnName = "OA01045";
                dt1.Columns[34].ColumnName = "OA01046";
                dt1.Columns[35].ColumnName = "OA01047";
                dt1.Columns[36].ColumnName = "OA01048";
                dt1.Columns[37].ColumnName = "OA01049";
                dt1.Columns[38].ColumnName = "OA01050";
                dt1.Columns[39].ColumnName = "OA01051";
                dt1.Columns[40].ColumnName = "OA01998";
                #endregion

                #region 转换列名商品详细
                dt2.Columns[0].ColumnName = "OA01002";
                dt2.Columns[1].ColumnName = "OB01005";
                dt2.Columns[2].ColumnName = "OB01006";
                dt2.Columns[3].ColumnName = "OB01007";
                dt2.Columns[4].ColumnName = "OB01008";
                dt2.Columns[5].ColumnName = "OB01009";
                dt2.Columns[6].ColumnName = "OB01010";
                dt2.Columns[7].ColumnName = "OB01011";
                dt2.Columns[8].ColumnName = "OB01012";
                dt2.Columns[9].ColumnName = "OB01013";
                dt2.Columns[10].ColumnName = "OB01014";
                dt2.Columns[11].ColumnName = "OB01015";
                dt2.Columns[12].ColumnName = "OB01016";
                dt2.Columns[13].ColumnName = "OB01017";
                dt2.Columns[14].ColumnName = "OB01018";
                dt2.Columns[15].ColumnName = "OB01998";
                #endregion

                #region 转换列名发票
                dt3.Columns[0].ColumnName = "OA01002";
                dt3.Columns[1].ColumnName = "OC01002";
                dt3.Columns[2].ColumnName = "OC01003";
                dt3.Columns[3].ColumnName = "OC01005";
                dt3.Columns[4].ColumnName = "OC01006";
                dt3.Columns[5].ColumnName = "OC01007";
                dt3.Columns[6].ColumnName = "OC01008";
                dt3.Columns[7].ColumnName = "OC01009";
                dt3.Columns[8].ColumnName = "OC01010";
                dt3.Columns[9].ColumnName = "OC01011";
                dt3.Columns[10].ColumnName = "OC01012";
                dt3.Columns[11].ColumnName = "OC01013";
                dt3.Columns[12].ColumnName = "OC01014";
                dt3.Columns[13].ColumnName = "OC01015";
                dt3.Columns[14].ColumnName = "OC01016";
                dt3.Columns[15].ColumnName = "OC01017";
                dt3.Columns[16].ColumnName = "OC01998";
                #endregion

                #region 转换列名付款

                dt4.Columns[0].ColumnName = "OP01002";
                dt4.Columns[1].ColumnName = "OP01003";
                dt4.Columns[2].ColumnName = "OP01004";
                dt4.Columns[3].ColumnName = "OP01005";
                dt4.Columns[4].ColumnName = "OP01006";
                dt4.Columns[5].ColumnName = "OP01007";
                dt4.Columns[6].ColumnName = "OP01008";
                dt4.Columns[7].ColumnName = "OP01009";
                dt4.Columns[8].ColumnName = "OP01010";
                dt4.Columns[9].ColumnName = "OP01011";
                dt4.Columns[10].ColumnName = "OP01012";
                dt4.Columns[11].ColumnName = "OP01013";
                dt4.Columns[12].ColumnName = "OP01014";
                dt4.Columns[13].ColumnName = "OP01015";
                dt4.Columns[14].ColumnName = "OP01016";
                dt4.Columns[15].ColumnName = "OP01998";
                #endregion

                int i = 2;
                foreach (DataRow item in dt1.Rows)
                {
                    #region 验证订单类型
                    if (!(item["OA01003"] is DBNull)
                        && item["OA01003"].ToString().Trim() != ""
                        && (item["OA01003"].ToString().Trim() == "系统订单"
                        || item["OA01003"].ToString().Trim() == "换货订单"))
                    {
                        if (item["OA01003"].ToString().Trim() == "换货订单")
                            item["OA01003"] = 3;
                        else
                            item["OA01003"] = 1;
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中订单类型不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                    }
                    #endregion

                    #region 验证是否为新客户
                    if (!(item["OA01044"] is DBNull)
                        && item["OA01044"].ToString().Trim() != ""
                        && (item["OA01044"].ToString().Trim() == "Y"
                        || item["OA01044"].ToString().Trim() == "N"))
                    {
                        if (item["OA01044"].ToString().Trim() == "Y")
                            item["OA01044"] = 1;
                        else
                            item["OA01044"] = 0;
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中是否为新客户不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion

                    #region 验证Minifogger/是否
                    if (!(item["OA01045"] is DBNull)
                        && item["OA01045"].ToString().Trim() != ""
                        && (item["OA01045"].ToString().Trim() == "Y"
                        || item["OA01045"].ToString().Trim() == "N"))
                    {
                        if (item["OA01045"].ToString().Trim() == "Y")
                            item["OA01045"] = 1;
                        else
                            item["OA01045"] = 0;
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中Minifogger不正确 请修改后重新导入错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion

                    #region 验证订单模式(系统订单)
                    if (!(item["OA01005"] is DBNull)
                        && item["OA01005"].ToString().Trim() != ""
                        && (item["OA01005"].ToString().Trim() == "Y"
                        || item["OA01005"].ToString().Trim() == "N"))
                    {
                        if (item["OA01005"].ToString().Trim() == "Y")
                            item["OA01005"] = 1;
                        else
                            item["OA01005"] = 0;
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中订单模式不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion

                    #region 验证销售员
                    if (item["OA01013"].ToString().Trim() != "")
                    {
                        DataRow[] dr = dt5.Select(string.Format(" UA01005 = '{0}' ", item["OA01013"].ToString()));
                        if (dr.Length <= 0)
                        {
                            error += string.Format("</br>订单信息中销售员不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                            bl = false;
                        }
                        else
                        {
                            item["OA01013"] = Convert.ToInt32(dr[0]["UA01001"]);
                        }
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中销售员不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion

                    #region 验证分享人1
                    if (item["OA01015"].ToString().Trim() != "")
                    {
                        DataRow[] dr = dt9.Select(string.Format(" UA01004 = '{0}' ", item["OA01015"].ToString()));
                        if (dr.Length <= 0)
                        {
                            error += string.Format("</br>订单信息中分享人1不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                            bl = false;
                        }
                        else
                        {
                            item["OA01015"] = dr[0]["UA01004"].ToString();
                        }

                    }
                    #endregion

                    #region 验证分享人2
                    if (item["OA01017"].ToString().Trim() != "")
                    {
                        DataRow[] dr = dt9.Select(string.Format(" UA01004 = '{0}' ", item["OA01017"].ToString()));
                        if (dr.Length <= 0)
                        {
                            error += string.Format("</br>订单信息中分享人2不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                            bl = false;
                        }
                        else
                        {
                            item["OA01017"] = dr[0]["UA01004"].ToString();
                        }
                    }

                    #endregion


                    #region 验证应用代码
                    if (item["OA01025"].ToString().Trim() != "")
                    {
                        DataRow[] dr = dt6.Select(string.Format(" OB02002 = '{0}' ", item["OA01025"].ToString()));
                        if (dr.Length <= 0)
                        {
                            error += string.Format("</br>订单信息中应用代码不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                            bl = false;
                        }
                        else
                        {
                            item["OA01025"] = Convert.ToInt32(dr[0]["OB02001"]);
                        }
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中应用代码不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion


                    #region 验证客户名称
                    if (item["OA01036"].ToString().Trim() != "")
                    {
                        DataRow[] dr = dt7.Select(string.Format(" CA01003 = '{0}' ", item["OA01036"].ToString()));
                        if (dr.Length <= 0)
                        {
                            error += string.Format("</br>订单信息中验证客户名称不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                            bl = false;
                        }
                        else
                        {
                            item["OA01036"] = Convert.ToInt32(dr[0]["CA01001"]);

                            ////验证应用描述

                        }                            //DataTable dt = _CustomerBLL.SelectAD(dr[0]["CA01020"].ToString());  //获得客户应用描述
                        //DataRow[] dr1 = dt.Select(string.Format(" OD01003 = '{0}' ", item["OA01047"].ToString().Trim()));
                        //if (dr1.Length > 0)
                        //{
                        //    item["OA01047"] = Convert.ToInt32(dr1[0]["OD01001"]); //文字转成ID
                        //}
                        //else
                        //{
                        //    error += "</br>订单信息中应用描述不正确 请修改后重新导入！";
                        //    bl = false;
                        //    break;
                        //}
                    }
                    else
                    {
                        error += string.Format("</br>订单信息中验证客户名称不正确 请修改后重新导入！错误行号为：{0}", i.ToString());
                        bl = false;
                    }
                    #endregion
                    i++;
                }
            }

            try
            {
                List<OrderBase> _listOrderBase = this.ConvertOrderBaseToList(dt1);
                List<OrderProduct> _listOrderProduct = new List<OrderProduct>();
                DataTable dtOrderBase = new OrderBLL().SelectOrderBaseForList();  //数据库所有订单

                dt2.Columns.Add(new DataColumn("OrderNo")); //存放订单编码
                dt2.Columns.Add(new DataColumn("OrderGUID"));  //商品GUID
                dt2.Columns.Add(new DataColumn("ProductId"));  //存储商品ID
                int q = 2;
                foreach (OrderBase item in _listOrderBase)
                {
                    DataRow[] dr = dtOrderBase.Select(string.Format("OA01002 = '{0}'", item.OA01002));
                    if (dr.Length == 0)
                    {
                        string strSql = _CreateSqlHandler.Insert(item, "OA01");  //新增订单SQL
                        _list.Add(strSql);
                        if (dt2.Rows.Count > 0)  //订单商品
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                if (dt2.Rows[i]["OA01002"].ToString() == item.OA01002)
                                {
                                    dt2.Rows[i]["OrderNo"] = dt2.Rows[i]["OA01002"];
                                    dt2.Rows[i]["OA01002"] = item.OA01999;
                                }
                                DataRow[] datarow = dt8.Select(string.Format(" PA01003 = '{0}' "
                                    , dt2.Rows[i]["OB01005"].ToString()));

                                if (datarow.Length > 0)
                                {
                                    dt2.Rows[i]["ProductId"] = datarow[0]["PA01001"];
                                }
                                else
                                {
                                    error += string.Format("</br>订单商品名称 请修改后重新导入！错误行号为：{0}", q.ToString());
                                    bl = false;
                                }
                            }
                        }
                    }
                    q++;
                }


                _listOrderProduct = this.ConvertOrderProdustToList(ref dt2);

                if (_listOrderProduct.Count > 0)
                {
                    foreach (OrderProduct item in _listOrderProduct)
                    {
                        string strSql = _CreateSqlHandler1.Insert(item, "OB01");  //新增订单商品SQL
                        _list.Add(strSql);
                    }
                }

                if (dt3.Rows.Count == dt2.Rows.Count)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)    //保证一条商品对应一条发票
                        {
                            dt3.Rows[i]["OC01003"] = dt2.Rows[i]["OrderGUID"];
                        }

                        List<OrderInvoice> _listInvoice = this.ConvertOrderInvoiceToList(dt3);
                        foreach (OrderInvoice item in _listInvoice)
                        {
                            string strSql = _CreateSqlHandler2.Insert(item, "OC01");  //新增订单商品SQL
                            _list.Add(strSql);
                        }
                    }
                }
                else
                {
                    error += "</br>订单商品和发票数量不一致 请修改后重新导入！";
                    bl = false;
                }

                if (dt4.Rows.Count > 0)
                {

                    foreach (OrderBase item in _listOrderBase)
                    {
                        foreach (DataRow item1 in dt4.Rows)
                        {
                            if (item.OA01002 == item1["OP01003"].ToString())
                            {
                                item1["OP01003"] = item.OA01999;
                                break;
                            }
                        }
                    }

                    List<OrderPayment> _listOrderPayment = ConvertOrderPaymentToList(dt4);
                    foreach (OrderPayment item in _listOrderPayment)
                    {
                        string strSql = _CreateSqlHandler3.Insert(item, "OP01");  //新增订单商品SQL
                        _list.Add(strSql);
                    }
                }
            }
            catch (Exception)
            {
                error += "</br>Excel数据有误 请修改后重新导入！";
                bl = false;
            }

            if (bl)  //没有错误数据执行新增
            {
                int num = Math.Abs(Provider.TranExecuteNonQuerys(_list, null));
                if (num <= 0)
                {
                    error += "</br>导入失败！";
                }
                else
                {
                    error += "</br>导入成功！";
                }
            }



            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("error", error);


            return dictionary;
        }

        #region 月报

        /// <summary>
        /// 根据Sql语句获取导出DataSet
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Birthday"></param>
        /// <returns></returns>
        public DataSet SelectDataBySqlToDataSet(string strSql)
        {
            object o = new object();
            try
            {
                DataSet ds = Provider.ReturnDataSetByDataReader(strSql, 0, ref o, null);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件导出客户资料表及链表数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportCustomerBase(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                                       ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                                       ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                                       ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                                       ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                                       ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029]
                                                       ,[CA01030] ,[CA01031] ,[CA01032] ,[CA01033]
                                                       ,[CA01034] ,[CA01035] ,[CA01036] ,[CA01037]
                                                       ,[CA01038] ,[CA01039] ,[CA01040] ,[CA01041]
                                                       ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045],[CA01046]
                                                       ,[CA01047],[CA01997] ,[CA01998] ,[CA01999] ,[CD02002],
		                                                [CB01002] ,[CB02002] ,[CD01003] ,[CB04002] ,[GA05002],
		                                                [CB03002] ,[GA06002], [ProvinceID] ,[ProvinceName],
		                                                [CityID] ,[CityName]
                                                FROM    dbo.CA01
                                                        LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                WHERE CA01997 = 0 {0}
                                                ORDER BY CA01998 DESC
                                                    ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出行业代码统计/Sales by SIC
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSalesbySIC(string strWhere, bool blInvoice)
        {

            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" SELECT CB04002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                        FROM (
		                        SELECT CB04002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022, OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01002 ,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
	                        
                        ) ABCD 
                        GROUP BY CB04002 ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT CB04002, CAST(SUM(OA01020) AS DECIMAL(18,2)) OA01020,
                                                    CAST (SUM(OA01022) AS DECIMAL(18,2)) OA01022,
	                                                COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                                                SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                                                FROM (
	                                                SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                                                FROM (
		                                                SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
			                                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                                                FROM OA01 
		                                                INNER JOIN OB01 ON OA01999 = OB01002
		                                                INNER JOIN OC01 ON OB01999 = OC01003
		                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
		                                                INNER JOIN UA01 ON UA01001 = OA01013
		                                                LEFT JOIN CB04 ON CB04001 = OA01041
		                                                WHERE OA01997 = 0 AND OA01003 <> 3     
			                                                AND  (OA01015 = '' or OA01015 is null) {0}
			                                                AND  (OA01017 = '' or OA01017 is null)
			                                                AND  OA01016 = 0 AND OA01018 = 0
	                                                ) A 
	                                                WHERE A.NUM = 1 
	                                                UNION ALL
	                                                SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                                                FROM (
		                                                SELECT CB04002,OA01020*OA01016 AS OA01020
			                                                ,OA01022*OA01016 AS OA01022
			                                                , OA01002,CA01001,OA01044
			                                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                                                FROM OA01 
		                                                INNER JOIN OB01 ON OA01999 = OB01002
		                                                INNER JOIN OC01 ON OB01999 = OC01003
		                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
		                                                INNER JOIN UA01 ON UA01004 = OA01015
		                                                LEFT JOIN CB04 ON CB04001 = OA01041
		                                                WHERE OA01997 = 0 AND OA01003 <> 3     
			                                                AND (OA01015 <> '' and OA01015 is not null) {0}
	                                                ) A 
	                                                WHERE A.NUM = 1 
	                                                UNION ALL
	                                                SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                                                FROM (
		                                                SELECT CB04002,OA01020*OA01018 AS OA01020
			                                                ,OA01022*OA01018 AS OA01022
			                                                , OA01002,CA01001,OA01044
			                                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                                                FROM OA01 
		                                                INNER JOIN OB01 ON OA01999 = OB01002
		                                                INNER JOIN OC01 ON OB01999 = OC01003
		                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
		                                                INNER JOIN UA01 ON UA01004 = OA01017
		                                                LEFT JOIN CB04 ON CB04001 = OA01041
		                                                WHERE OA01997 = 0 AND OA01003 <> 3   
			                                                AND (OA01017 <> '' and OA01017 is not null)  {0}
	                                                ) A 
	                                                WHERE A.NUM = 1 
	                                                UNION ALL
	                                                SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                                                FROM (
		                                                SELECT CB04002,OA01020*(1-OA01016-OA01018) AS OA01020
			                                                ,OA01022*(1-OA01016-OA01018) AS OA01022
			                                                ,OA01002
			                                                ,CA01001,OA01044
			                                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                                                FROM OA01 
		                                                INNER JOIN OB01 ON OA01999 = OB01002
		                                                INNER JOIN OC01 ON OB01999 = OC01003
		                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
		                                                INNER JOIN UA01 ON UA01001 = OA01013
		                                                LEFT JOIN CB04 ON CB04001 = OA01041
		                                                WHERE OA01997 = 0 AND OA01003 <> 3    
			                                                AND (OA01016 <> 0 OR OA01018 <> 0)  {0}
	                                                ) A 
	                                                WHERE A.NUM = 1 
                                                ) ABCD 
                                                GROUP BY CB04002 ", strWhere);
                }

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出利润率统计/Sakes By GP
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSakesByGP(string strWhere,bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" SELECT * ,Oa01022-OA01019*1.15 as Profit FROM
                        (
							SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009,SUM(OA01022) OA01022
							FROM (
		                        SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2) OA01022
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                            UNION ALL
		                        SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*OA01016 OA01022
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038  
							    INNER JOIN UA01 ON UA01004 = OA01015
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL
								SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*OA01018 OA01022		                        
                                FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL
							    SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) OA01022		                        
                                FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
							) ABCD 
							GROUP BY OA01002,CA01003,UA01004,UA01005,OA01009,OA01019
                        )tab ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT * , CAST(OA01022-OA01019*1.15 AS DECIMAL(18,2)) Profit 
                                    FROM
                                        (
                                            SELECT OA01002 ,CA01003 ,UA01004 ,UA01005
                                                ,dbo.FX_GetIndustryReportPrice(OA01002,OA01022) OA01022
                                                ,OA01019,OA01009
                                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC ) AS NUM
                                            FROM CA01 
                                            JOIN OA01 on OA01038=CA01001
                                            JOIN UA01 on UA01001=OA01013 
                                            JOIN OB01 on OB01002=OA01999 
                                            JOIN OC01 on OC01003=OB01999
                                            WHERE OA01997 = 0 AND OA01003 = 1 {0}
                                         )  GP  WHERE NUM=1", strWhere);
                }

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出重点客户统计列表/Top Customer
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportTopCustomer(string strWhere, bool blInvoice)
        {
            DataSet ds;
            object obj = null;
            try
            {
                string strsql = string.Empty;
                if (blInvoice)
                {
                    strsql = string.Format(@" 
                    SELECT CA01003,CA01004,CB04002,SUM(OA01020) OA01020,
		                        SUM(OA01022) OA01022,SUM(OA01002) OA01002,UA01005
                        FROM (
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0 
				                        AND  (OA01015 = '' or OA01015 is null)   {0}
				                        AND  (OA01017 = '' or OA01017 is null)
                                        AND  OA01016 = 0 AND OA01018 = 0
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01015
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0  {0}
				                         AND (OA01015 <> '' and OA01015 is not null) 
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022 ,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01017
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                         AND (OA01017 <> '' and OA01017 is not null)
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                        AND  (OA01016 <> 0 OR OA01018<>0)
		                         ) A 
		                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
                        ) A  GROUP BY CA01003,CA01004,CB04002,UA01005", strWhere);
                }
                else
                {
                    strsql = string.Format(@" 
                    SELECT CA01003,CA01004,CB04002,SUM(OA01020) OA01020,
		                        SUM(OA01022) OA01022,SUM(OA01002) OA01002,UA01005
                        FROM (
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002, CAST(OA01020 AS DECIMAL(18,2)) OA01020 
                                    ,CAST(OA01022 AS DECIMAL(18,2)) OA01022,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0 
				                        AND  (OA01015 = '' or OA01015 is null)   {0}
				                        AND  (OA01017 = '' or OA01017 is null)
                                        AND  OA01016 = 0 AND OA01018 = 0
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,CAST((OA01020*OA01016)  AS DECIMAL(18,2)) OA01020,
			                        CAST((OA01022*OA01016)  AS DECIMAL(18,2)) OA01022 ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01015
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0    {0}
				                         AND (OA01015 <> '' and OA01015 is not null) 
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,CAST((OA01020*OA01018)  AS DECIMAL(18,2)) OA01020,
			                        CAST((OA01022*OA01018)  AS DECIMAL(18,2)) OA01022  ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01017
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                         AND (OA01017 <> '' and OA01017 is not null)
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,CAST(OA01020*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01020,
			                        CAST(OA01022*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01022 ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0  {0}
				                        AND  (OA01016 <> 0 OR OA01018<>0)
		                         ) A where NUM =1
		                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
                        ) A  GROUP BY CA01003,CA01004,CB04002,UA01005", strWhere);
                }

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出重点产品统计/Hot Part List
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportHotPartList(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT SUM(OB01007) as OA01002,OB01005
                                                 FROM CA01
                                                 JOIN CB04 on CB04001=CA01020
                                                 JOIN OA01 on OA01038=CA01001
                                                 JOIN UA01 on UA01001=OA01013 
                                                 JOIN OB01 on OB01002=OA01999 
                                                 JOIN OC01 on OC01003=OB01999 
                                                 WHERE OA01997 = 0 AND  OA01003 <> 3  {0}
                                                 GROUP BY OB01005  
                                                 ORDER BY SUM(OB01007) DESC 
                                               ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出重点产品统计2/Hot Part List2
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportHotPartListTwo(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" select count(OA01002)as OA01002,OB01005,SUM(OA01022) as oa01022,
		                                         CA01004 ,CB04002 ,
		                                         ROW_NUMBER() OVER(ORDER BY OB01005 ASC ) AS RowNumber
                                                 from CA01
                                                 LEFT join CB04 on CB04001=CA01020
                                                 join OA01 on OA01038=CA01001
                                                 join UA01 on UA01001=OA01013 
                                                 join OB01 on OB01002=OA01999 
                                                 join OC01 on OC01003=OB01999 
                                                 where OA01997 = 0 AND OA01003 = 1 {0}
                                                 group by OB01005,CA01004,CB04002 
                                               ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出销售员统计/Sales b
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSalesB(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@"SELECT UA01005,SUM(OA01022)OA01022,count(OA01002) OA01001
								,SUM(DISTINCT OA01003) OA01003,SUM(DISTINCT OA010031) OA010031
								, count(DISTINCT OA01038) OA01038,SUM(OA01044) OA01044,SUM(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											, OA01038,OA01002
											,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2) OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A 
										GROUP BY UA01005,OA01038,OA01002	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002,OA01016
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null)  {0}
										) B 
										GROUP BY UA01005,OA01002	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002,OA01018
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) {0}
										) C 
										GROUP BY UA01005,OA01002
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
												,SUM(OA01022) OA01022
												,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
												,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
												,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
												, OA01038,OA01002
												,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044
											    ,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
											FROM (
												SELECT  UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01018,OA01016,OA01002
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) {0}
											) D
										GROUP BY UA01005,OA01038,OA01002	
									) D
								)TOTAL
							GROUP BY UA01005
                     ", strWhere);
                }
                else
                {
                    strSql = string.Format(@"
                                              SELECT UA01005,SUM(OA01022)OA01022,count(OA01002) OA01001
								,SUM(DISTINCT OA01003) OA01003,SUM(DISTINCT OA010031) OA010031
								, count(DISTINCT OA01038) OA01038,SUM(OA01044) OA01044,sum(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											, OA01038,OA01002
											,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A WHERE NUM = 1 
										GROUP BY UA01005,OA01038,OA01002,OA01054	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022*OA01016) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002,OA01016
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null)  {0}
										) B WHERE NUM = 1 
										GROUP BY UA01005,OA01002,OA01054	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022*OA01018) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002,OA01018
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) {0}
										) C WHERE NUM = 1 
										GROUP BY UA01005,OA01002,OA01054
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
												,SUM(OA01022*(1-OA01016-OA01018)) OA01022
												,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
												,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
												,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
												, OA01038,OA01002
												,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044,OA01054
											FROM (
												SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01018,OA01016,OA01002
													,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) {0}
											) D WHERE NUM = 1 
										GROUP BY UA01005,OA01038,OA01002,OA01054	
									) D
								)TOTAL
							GROUP BY UA01005
                     ", strWhere);
                }
                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 根据条件导出客户类型/By Customer Type 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportByCustomerType(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@"  SELECT CB01002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        COUNT(DISTINCT CASE WHEN OA01044=1 THEN CA01001 ELSE 0 END)-1  as OA01044 
                        FROM (
		                        SELECT CB01002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*OA01016 AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*OA01018 AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                        ,OA01002
			                        ,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
                        ) ABCD 
                        GROUP BY CB01002 ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT CB01002, CAST(SUM(OA01020) AS DECIMAL(18,2)) OA01020,
                            CAST(SUM(OA01022) AS DECIMAL(18,2)) OA01022 ,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        COUNT(DISTINCT CASE WHEN OA01044=1 THEN CA01001 ELSE 0 END)-1  as OA01044 
                        FROM (
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*OA01016 AS OA01020
			                        ,OA01022*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*OA01018 AS OA01020
			                        ,OA01022*OA01018 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*(1-OA01016-OA01018) AS OA01020
			                        ,OA01022*(1-OA01016-OA01018) AS OA01022
			                        ,OA01002
			                        ,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
	                        ) A 
	                        WHERE A.NUM = 1 
                        ) ABCD 
                        GROUP BY CB01002   ", strWhere);
                }

                

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出行业报告/Industry report
        /// </summary>
        /// <returns></returns>
        public DataSet ExportIndustryReportPage(string strWhereAdd,bool blInvoice)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Empty;
                if (blInvoice)
                {
                    strsql = string.Format(@" SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2) OA01022
                                                    ,CB02002,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240,GA06003 get220
                                                    ,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND  (OA01015 = '' or OA01015 is null)  
                                                        AND  (OA01017 = '' or OA01017 is null)
                                                        AND  OA01016 = 0 AND OA01018 = 0   
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01015 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}   
			                                            AND (OA01015 <> '' and OA01015 is not null) 
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT  OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01017 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND (OA01017 <> '' and OA01017 is not null)
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3   {0}
			                                             AND  (OA01016 <> 0 OR OA01018<>0) 
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010 ", strWhereAdd);
                }
                else
                {
                    strsql = string.Format(@" SELECT *
                        FROM (
	                        SELECT OA01002,CA01003,UA01001,UA01005,OA01022,CB02002,CB04002,OB02002,OA01045
				                        ,P.GA03002 Province ,GA05003 get240,GA06003 get220,OA01028,CA01009
				                        ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                        FROM OA01
	                        INNER JOIN UA01 ON UA01001 = OA01013 
	                        INNER JOIN CA01 ON CA01001 = OA01038 	
	                        LEFT JOIN CB02 ON CA01018 = CB02001
	                        LEFT JOIN CB04 ON CA01020 = CB04001
	                        LEFT JOIN OB02 ON OB02001 = OA01025
	                        INNER JOIN OB01 ON OB01002 = OA01999 
	                        INNER JOIN OC01 ON OC01003 = OB01999
	                        INNER JOIN GA03 ON GA03001 = CA01013
	                        INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                        LEFT JOIN GA05 ON GA05001 = OA01042
	                        LEFT JOIN GA06 ON GA06001 = OA01043
	                        WHERE OA01997 = 0 AND OA01003 <> 3  
			                        AND  (OA01015 = '' or OA01015 is null) {0}
                                    AND  (OA01017 = '' or OA01017 is null)
                                    AND  OA01016 = 0 AND OA01018 = 0   
                        ) A 
                        WHERE NUM =1 
                        UNION ALL
                        SELECT *
                        FROM (
	                        SELECT OA01002,CA01003,UA01001,UA01005,OA01022*OA01016 OA01022,CB02002
				                        ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                        ,GA06003 get220,OA01028,CA01009
				                        ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                        FROM OA01
	                        INNER JOIN UA01 ON UA01004 = OA01015 
	                        INNER JOIN CA01 ON CA01001 = OA01038 	
	                        LEFT JOIN CB02 ON CA01018 = CB02001
	                        LEFT JOIN CB04 ON CA01020 = CB04001
	                        LEFT JOIN OB02 ON OB02001 = OA01025
	                        INNER JOIN OB01 ON OB01002 = OA01999 
	                        INNER JOIN OC01 ON OC01003 = OB01999
	                        INNER JOIN GA03 ON GA03001 = CA01013
	                        INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                        LEFT JOIN GA05 ON GA05001 = OA01042
	                        LEFT JOIN GA06 ON GA06001 = OA01043
	                        WHERE OA01997 = 0 AND OA01003 <> 3  
			                        AND (OA01015 <> '' and OA01015 is not null)  {0}
                        ) A 
                        WHERE NUM =1 
                        UNION ALL
                        SELECT *
                        FROM (
	                        SELECT OA01002,CA01003,UA01001,UA01005,OA01022*OA01018 OA01022,CB02002
				                        ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                        ,GA06003 get220,OA01028,CA01009
				                        ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                        FROM OA01
	                        INNER JOIN UA01 ON UA01004 = OA01017 
	                        INNER JOIN CA01 ON CA01001 = OA01038 	
	                        LEFT JOIN CB02 ON CA01018 = CB02001
	                        LEFT JOIN CB04 ON CA01020 = CB04001
	                        LEFT JOIN OB02 ON OB02001 = OA01025
	                        INNER JOIN OB01 ON OB01002 = OA01999 
	                        INNER JOIN OC01 ON OC01003 = OB01999
	                        INNER JOIN GA03 ON GA03001 = CA01013
	                        INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                        LEFT JOIN GA05 ON GA05001 = OA01042
	                        LEFT JOIN GA06 ON GA06001 = OA01043
	                        WHERE OA01997 = 0 AND OA01003 <> 3  
			                        AND (OA01017 <> '' and OA01017 is not null) {0}
                        ) A 
                        WHERE NUM =1 
                        UNION ALL
                        SELECT *
                        FROM (
	                        SELECT OA01002,CA01003,UA01001,UA01005,OA01022*(1-OA01016-OA01018) OA01022,CB02002
				                        ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                        ,GA06003 get220,OA01028,CA01009
				                        ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                        FROM OA01
	                        INNER JOIN UA01 ON UA01001 = OA01013 
	                        INNER JOIN CA01 ON CA01001 = OA01038 	
	                        LEFT JOIN CB02 ON CA01018 = CB02001
	                        LEFT JOIN CB04 ON CA01020 = CB04001
	                        LEFT JOIN OB02 ON OB02001 = OA01025
	                        INNER JOIN OB01 ON OB01002 = OA01999 
	                        INNER JOIN OC01 ON OC01003 = OB01999
	                        INNER JOIN GA03 ON GA03001 = CA01013
	                        INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                        LEFT JOIN GA05 ON GA05001 = OA01042
	                        LEFT JOIN GA06 ON GA06001 = OA01043
	                        WHERE OA01997 = 0 AND OA01003 <> 3  
			                         AND  (OA01016 <> 0 OR OA01018<>0) {0}
                        ) A 
                        WHERE NUM =1  ", strWhereAdd);
                }
               


                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出按开发票金额统计(统计总金额)
        /// </summary>
        /// <returns></returns>
        public DataSet ExportInvoicesAmountOfMoneyPage(string strWhereAdd, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strsql = string.Empty;
                object obj = null;
                if (blInvoice)
                {
                    strsql = string.Format(@" SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0
	                        ) A  
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01018,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01018,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                         AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005 ", strWhereAdd);
                }
                else
                {
                    strsql = string.Format(@" SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0   
	                        ) A  
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01016) as sumrmb,
			                        SUM(OA01022*OA01016) as sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01016,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01018) sumrmb,
			                        SUM(OA01022*OA01018) sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
		                        SUM(OA01020*(1-OA01016-OA01018))sumrmb,
		                        SUM(OA01022*(1-OA01016-OA01018))sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01016
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                        AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005", strWhereAdd);
                }
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        ///导出迟交货统计
        /// </summary>
        /// <returns></returns>
        public DataSet ExportLateDeliveryPage(string strWhereAdd)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@" SELECT  OA01002, CA01001,CA01003,OB01005,OC01008,
                                OB01007,OB01009,OB01011,OB01014,OA01009,OA01010,OC01009,
                                ROW_NUMBER() over(order by UA01004)AS RowNumber ,ISNULL(OB01014,0) PB01004
                                FROM OA01  
		                        INNER JOIN CA01 ON CA01001 = OA01038
                                INNER JOIN UA01 ON UA01001 = OA01013
                                INNER JOIN OB01 ON OB01002=OA01999 
                                INNER JOIN OC01 ON OC01003=OB01999 
                                WHERE OA01997=0  and  OA01010 < isnull(OC01009,GETDATE()) {0} ", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 睡眠客户
        /// </summary>
        /// <returns></returns>
        public DataSet ExportSleepCustomerPage(string strWhereAdd)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@" SELECT * ,ROW_NUMBER() OVER(ORDER BY OA01009 DESC) AS RowNumber
                                FROM (
	                                SELECT * 
	                                FROM 
		                                (SELECT CA01003,OA01002,OA01009,UA01004,UA01005,UA01001,UA01013,OA01025,CA01020
	                                            ,ROW_NUMBER() over(partition by CA01001 order by OA01009 DESC) as rownum
	                                            ,CB04002,CA01009,CA01010,GA03002,dbo.FX_GetProvinceByCityId(CA01013) Province
                                                FROM  CA01
                                                INNER JOIN OA01 ON CA01001 = OA01038 
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                                INNER JOIN GA03 ON GA03001 = CA01013
                                                WHERE OA01997 = 0
                                   ) SleepCustomer 
                                WHERE SleepCustomer.rownum = 1  AND  DATEDIFF(MONTH,OA01009,getdate())>6 {0} ) a  ", strWhereAdd);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 年销售额
        /// </summary>
        /// <returns></returns>
        public DataSet ExportSalesYearPage(string strWhereAdd, bool blInvoice)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Empty;
                if (blInvoice)
                {
                    strsql = string.Format(@"
                      SELECT '' YEARS,COUNT(distinct CustomerNum) CUSTOMERCOUNT
                                            ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) OA01044
                                            ,COUNT(distinct OrderNum) ORDERCOUNT  ,SUM(OA01022) OA01022
                                            ,SUM(OA01020) OA01020
                                        FROM (
			                                        SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS CustomerNum,
			                                               OA01044 AS NewCustomerNum,
			                                               OA01002 AS OrderNum,
			                                              round((OB01009/OA01021/1.17),2) OA01022 ,OB01007*OB01008 OA01020
		                                            FROM dbo.OA01 
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
					                                        AND  (OA01015 = '' or OA01015 is null)  {0}
					                                        AND  (OA01017 = '' or OA01017 is null) 
                                                            AND  OA01016 = 0 AND OA01018 = 0    
	                                          UNION ALL 
		                                            SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS  CustomerNum,
				                                           OA01044 AS NewCustomerNum,
				                                           OA01002 AS OrderNum,
				                                           round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,
				                                           (OB01007*OB01008*OA01016) AS OA01020
		                                            FROM dbo.OA01  
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
				                                        AND   (OA01015 <> '' AND OA01015 IS NOT NULL)  {0}
	                                         UNION ALL 
	                                              SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,
				                                         OB01007*OB01008*OA01018 AS OA01020
	                                              FROM dbo.OA01 
	                                              JOIN dbo.CA01 ON OA01038 = CA01001
	                                              JOIN dbo.UA01 ON UA01004 = OA01017 	
	                                              JOIN dbo.OB01 ON OA01999 = OB01002
	                                              JOIN dbo.OC01 ON OB01999 = OC01003
	                                              WHERE OA01997 = 0 AND OA01003 <> 3  
		                                                   AND   (OA01017 <> '' AND OA01017 IS NOT NULL)  {0}
	                                         UNION ALL 
		                                          SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,
				                                         OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
		                                          FROM dbo.OA01 
		                                          JOIN dbo.CA01 ON OA01038 = CA01001
		                                          JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                          JOIN dbo.OB01 ON OA01999 = OB01002
		                                          JOIN dbo.OC01 ON OB01999 = OC01003
		                                          WHERE OA01997 = 0 AND OA01003 <> 3  
			                                                 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                        ) ABCD", strWhereAdd);
                }
                else
                {
                    strsql = string.Format(@"
                      SELECT '' YEARS,COUNT(distinct CustomerNum) CUSTOMERCOUNT
                                            ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) OA01044
                                            ,COUNT(distinct OrderNum) ORDERCOUNT  , CAST(SUM(OA01022) AS DECIMAL(18,2)) OA01022
                                            , CAST(SUM(OA01020) AS DECIMAL(18,2)) OA01020
                                        FROM (
	                                         SELECT * 
	                                         FROM (
			                                        SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS CustomerNum,
			                                               OA01044 AS NewCustomerNum,
			                                               OA01002 AS OrderNum,
			                                               OA01022 ,OA01020,
			                                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                            FROM dbo.OA01 
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
					                                        AND  (OA01015 = '' or OA01015 is null)  {0}
					                                        AND  (OA01017 = '' or OA01017 is null) 
                                                            AND  OA01016 = 0 AND OA01018 = 0    
	                                          ) A WHERE NUM =1
	                                          UNION ALL
	                                          SELECT * 
	                                          FROM (
		                                            SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS  CustomerNum,
				                                           OA01044 AS NewCustomerNum,
				                                           OA01002 AS OrderNum,
				                                           (OA01022*OA01016) AS OA01022,
				                                           (OA01020*OA01016) AS OA01020,
					                                        ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                            FROM dbo.OA01  
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
				                                        AND   (OA01015 <> '' AND OA01015 IS NOT NULL)  {0}
	                                         ) B WHERE NUM =1
	                                         UNION ALL
	                                         SELECT * 
	                                         FROM (
	                                              SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         OA01022*OA01018 AS OA01022,
				                                         OA01020*OA01018 AS OA01020,
				                                         ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
	                                              FROM dbo.OA01 
	                                              JOIN dbo.CA01 ON OA01038 = CA01001
	                                              JOIN dbo.UA01 ON UA01004 = OA01017 	
	                                              JOIN dbo.OB01 ON OA01999 = OB01002
	                                              JOIN dbo.OC01 ON OB01999 = OC01003
	                                              WHERE OA01997 = 0 AND OA01003 <> 3  
		                                                   AND   (OA01017 <> '' AND OA01017 IS NOT NULL)  {0}
	                                         ) C WHERE NUM =1
	                                         UNION ALL
	                                         SELECT * 
	                                         FROM (
		                                          SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         OA01022*(1-OA01016-OA01018) AS OA01022,
				                                         OA01020*(1-OA01016-OA01018) AS OA01020,
				                                         ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                          FROM dbo.OA01 
		                                          JOIN dbo.CA01 ON OA01038 = CA01001
		                                          JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                          JOIN dbo.OB01 ON OA01999 = OB01002
		                                          JOIN dbo.OC01 ON OB01999 = OC01003
		                                          WHERE OA01997 = 0 AND OA01003 <> 3  
			                                                  AND  (OA01016 <> 0 OR OA01018<>0)  {0}
	                                         ) D WHERE NUM =1
                                        ) ABCD", strWhereAdd);
                }

                
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 未付款先发货
        /// </summary>
        /// <returns></returns>
        public DataSet ExportUnpaidFirstShipmentPage(string strWhereAdd)
        {
            DataSet ds;
            object obj = null;
            try
            {
                string strsql = string.Format(@" SELECT * ,ROW_NUMBER() OVER(ORDER BY OA01009 DESC) AS RowNumber
                                   FROM (
                                    SELECT UA01004,UA01005,CA01003,CA01014,SUM(OA01020) OA01020, SUM(OP01016) OP01016,MAX(OA01009) OA01009,CB02002 
                                    FROM (SELECT UA01004,UA01005
                                    	 			 ,CA01003 
                                    	 			 ,CA01014 
                                    	 			 ,OA01020 
                                    	 			 ,OP01016
                                    	 			 ,DATEDIFF(DAY,OA01051,GETDATE()) OA01009
                                    	 			 ,CB02002
                                    	 			 ,ROW_NUMBER () OVER(PARTITION BY OA01038,OA01013,OA01002 ORDER BY OA01009) NUM
                                    	  FROM  OA01
                                    	  INNER JOIN CA01 ON CA01001 = OA01038 
                                    	  INNER JOIN UA01 ON UA01001 = OA01013
                                    	  INNER JOIN CB02 ON CB02001 = CA01018
                                          INNER JOIN GA03 ON GA03001 = CA01013
                                    	  INNER JOIN OP01 ON OA01999 = OP01003
                                    	  INNER JOIN OB01 on OB01002=OA01999 
                                    	  INNER JOIN OC01 on OC01003=OB01999
                                    	  WHERE OA01997 = 0 AND OA01003 <> 3 AND OP01016 > 0 AND OC01010 =1 
                                            AND OA01009 BETWEEN DATEADD(MONTH,-12,GETDATE())AND GETDATE() {0} ) B 
                                    WHERE NUM=1
                                    GROUP BY CA01003,UA01004,UA01005,CA01014,CB02002
                                    ) UnpaidFirstShipment ", strWhereAdd);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 按行业分类
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportsalesbyMDT(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" SELECT CB03002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
		                            SELECT CB03002, OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                   
	                            UNION ALL 
	                 
		                            SELECT CB03002,OB01007*OB01008*OA01016 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL 
		                            SELECT CB03002,OB01007*OB01008*OA01018 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL 
		                            SELECT CB03002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002 ,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
                            ) ABCD 
                            GROUP BY CB03002", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT CB03002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,CAST(OA01020 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022 AS DECIMAL(18,2)) OA01022 ,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002, CAST( OA01020*OA01016 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*OA01016 AS DECIMAL(18,2)) OA01022 
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,CAST( OA01020*OA01018 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*OA01018 AS DECIMAL(18,2)) OA01022 
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,CAST( OA01020*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01022 ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
	                            ) A 
	                            WHERE A.NUM = 1 
                            ) ABCD 
                            GROUP BY CB03002", strWhere);
                }
                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 按客户代码
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportsalesbyCTC(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@"
                                SELECT CB02002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
		                            SELECT CB02002, OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                          
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*OA01016 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*OA01018 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
                            ) ABCD 
                            GROUP BY CB02002", strWhere);
                }
                else
                {
                    strSql = string.Format(@"
                               SELECT CB02002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,CAST(OA01020 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022 AS DECIMAL(18,2)) OA01022,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,CAST(OA01020*OA01016 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*OA01016 AS DECIMAL(18,2)) OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,CAST(OA01020*OA01018 AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*OA01018 AS DECIMAL(18,2)) OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,CAST(OA01020*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01020 
                                        , CAST(OA01022*(1-OA01016-OA01018) AS DECIMAL(18,2)) OA01022
			                            ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
	                            ) A 
	                            WHERE A.NUM = 1 
                            ) ABCD 
                            GROUP BY CB02002", strWhere);
                }

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 按所属省份
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportSalesbyProvince(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" 
                            SELECT GA03001,GA03002 ProvinceName,COUNT(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum  ,SUM(Amout) Amout
                            FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               round((OB01009/OA01021/1.17),2) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
			                            AND  (OA01015 = '' or OA01015 is null) 
			                            AND  (OA01017 = '' or OA01017 is null) 
                                        AND  OA01016 = 0 AND OA01018 = 0 
	                            UNION ALL
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01016 AS Amout
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            UNION ALL
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01018 AS Amout
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) 
	                            UNION ALL
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
					                               OA01044 AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND  (OA01016 <> 0 OR OA01018<>0) 
                            ) ABCD
                            GROUP BY GA03001,GA03002 ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" 
                            SELECT GA03001,GA03002 ProvinceName,COUNT(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum  ,CAST(SUM(Amout) AS DECIMAL(18,2)) Amout
                            FROM (
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               OA01022 AS Amout,
			                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
			                            AND  (OA01015 = '' or OA01015 is null) 
			                            AND  (OA01017 = '' or OA01017 is null) 
                                        AND  OA01016 = 0 AND OA01018 = 0 
	                            ) A WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               (OA01022*OA01016) AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            ) B WHERE NUM =1
	                            UNION ALL
	                            SELECT * 
	                            FROM (
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               OA01022*OA01018 AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) 
	                            ) C WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM 
	                            (
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
					                               OA01044 AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               OA01022*(1-OA01016-OA01018) AS Amout,
					                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND  (OA01016 <> 0 OR OA01018<>0) 
	                            ) D WHERE NUM =1
                            ) ABCD
                            GROUP BY GA03001,GA03002 ", strWhere);
                }


                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出重点客户统计2/Top Customer2
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet ExportTopCustomerTwoPage(string[] strWhereAdd, bool blInvoice)
        {
            DataSet ds;
            object obj = new object();
            try
            {
                if (blInvoice)
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwoCountInvoice", 1, ref obj, new SqlParameter[]{
                 new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String}});
                }
                else
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwoCount", 1, ref obj, new SqlParameter[]{
                 new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String}});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 按所属城市
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportsalesbyCity(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@"SELECT GA03001,GA03002 ProvinceName,CityName,count(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum ,SUM(Amout) Amout
                            FROM (
		                            SELECT P.GA03001,P.GA03002, OA01038 AS  CustomerNum,
										   GA03.GA03002 CityName,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               round((OB01009/OA01021/1.17),2) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01015 = '' or OA01015 is null)  
			                            AND  (OA01017 = '' or OA01017 is null)  {0}
                                        AND  OA01016 = 0 AND OA01018 = 0    
	                            UNION ALL 
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											  GA03.GA03002 CityName,
				                                (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01016 AS Amout
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            UNION ALL 
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											 GA03.GA03002 CityName,
				                              (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01018 AS Amout
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3  
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) {0}
	                            UNION ALL 
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
													GA03.GA03002 CityName,
					                               (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01016 <> 0 OR OA01018<>0) {0}
                            ) ABCD
                            GROUP BY GA03001,GA03002,CityName
                                         ", strWhere);
                }
                else
                {
                    strSql = string.Format(@"SELECT GA03001,GA03002 ProvinceName,CityName,count(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum ,CAST(SUM(Amout) AS DECIMAL(18,2)) Amout
                            FROM (
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002, OA01038 AS  CustomerNum,
										   GA03.GA03002 CityName,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               OA01022 AS Amout,
			                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01015 = '' or OA01015 is null)  
			                            AND  (OA01017 = '' or OA01017 is null)  {0}
                                        AND  OA01016 = 0 AND OA01018 = 0    
	                            ) A WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											  GA03.GA03002 CityName,
				                                (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               (OA01022*OA01016) AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            ) B WHERE NUM =1
	                            UNION ALL
	                            SELECT * 
	                            FROM (
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											 GA03.GA03002 CityName,
				                              (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               OA01022*OA01018 AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3  
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) {0}
	                            ) C WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM 
	                            (
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
													GA03.GA03002 CityName,
					                               (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               OA01022*(1-OA01016-OA01018) AS Amout,
					                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01016 <> 0 OR OA01018<>0) {0}
	                            ) D WHERE NUM =1
                            ) ABCD
                            GROUP BY GA03001,GA03002,CityName
                                         ", strWhere);
                }



                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 重点客户
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportKeyCustomer(string strWhere)
        {
            DataSet ds;
            object obj = null;
            try
            {
                string strsql = string.Format(@" 
                    SELECT YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
	                                ,CAST(SUM(SUMRMB) AS DECIMAL(18,2)) SUMRMB
                                    ,CAST(SUM(SUMUD) AS DECIMAL(18,2)) SUMUD
	                                ,SUM(DISTINCT ORDERCOUNT) ORDERCOUNT
	                                ,CAST(SUM(DISTINCT OLDSUMUD) AS DECIMAL(18,2)) OLDSUMUD  
	                                ,SUM(DISTINCT OLDORDERCOUNT) OLDORDERCOUNT
	                                ,CAST(SUM(DISTINCT OLDSUMRMB) AS DECIMAL(18,2)) OLDSUMRMB
                                FROM 
                                (
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                 AND  (OA01015 = '' or OA01015 is null)
									                                 AND  (OA01017 = '' or OA01017 is null) {0}
                                                                     AND  OA01016 = 0 AND OA01018 = 0
								                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                 AND  (OA01015 = '' or OA01015 is null)
									                                 AND  (OA01017 = '' or OA01017 is null)  {0}
                                                                     AND  OA01016 = 0 AND OA01018 = 0
								                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                UNION ALL
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*OA01016) SUMRMB,SUM(OA01022*OA01016) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01020,OA01022,OA01002, OA01003,OA01016,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01015
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                AND  (OA01015 <> '' and OA01015 is not null)     {0}
									                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
								                                SUM(OA01022*OA01016) SUMUD,SUM(OA01020*OA01016) SUMRMB,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,OA01016,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01015
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                AND  (OA01015 <> '' and OA01015 is not null)    {0} 
									                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                 UNION ALL               
	                                 SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*OA01018) SUMRMB,SUM(OA01022*OA01018) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01018
									                                ,OA01020,OA01022,OA01002, OA01003,DATEPART(YEAR,OA01009) YEARS 
									                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01017
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
									                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
									                                AND (OA01017 <> '' and OA01017 is not null) 
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020*OA01018) SUMRMB,
								                                SUM(OA01022*OA01018) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,OA01018,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01017
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
									                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
									                                AND (OA01017 <> '' and OA01017 is not null) 
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                UNION ALL                
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*(1-OA01016-OA01018)) SUMRMB,SUM(OA01022*(1-OA01016-OA01018)) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01016,OA01018,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
								                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
								                                AND  (OA01016 <> 0 OR OA01018<>0)
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020*(1-OA01016-OA01018)) SUMRMB,
								                                SUM(OA01020*(1-OA01016-OA01018)) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01018,OA01016,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
								                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
								                                AND  (OA01016 <> 0 OR OA01018<>0)
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001            
                                 ) KeyCustomer
                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ", strWhere);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 按客户名称/By Customer Name
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportByCustomerName(string strWhere, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" SELECT CA01001,CA01003,SUM(OrderMoney) OrderMoney,
	                               COUNT(DISTINCT OrderCount) OrderCount,MAX(InvoicedCount) InvoicedCount
                            FROM (SELECT CA01001 , CA01003 ,
                                         SUM(round((OB01009/OA01021/1.17),2)) OrderMoney,
                                          OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021 
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01001 = OA01013
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1
			                                   AND  (OA01015 = '' or OA01015 is null)
                                               AND  (OA01017 = '' or OA01017 is null) 
                                               AND  OA01016 = 0 AND OA01018 = 0  {0}
                                       ) A
                                  GROUP BY CA01001 , CA01003 ,OA01002
                                  UNION ALL 
                                  SELECT CA01001 , CA01003 ,
                                         SUM(round((OB01009/OA01021/1.17),2)*OA01016) OrderMoney,
                                         OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01016
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01004 = OA01015
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1 
                                         AND (OA01015 <> '' and OA01015 is not null)  {0}
                                        ) B
                                   GROUP BY CA01001 , CA01003  ,OA01002   
	                               UNION ALL
                                   SELECT CA01001 , CA01003 ,
                                          SUM(round((OB01009/OA01021/1.17),2)*OA01018) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01018
                                          FROM	OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01004 = OA01017
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999 
                                          WHERE OA01997 = 0 AND OA01003 = 1
                                            AND (OA01017 <> '' and OA01017 is not null)   {0}
                                         ) C
                                   GROUP BY CA01001 , CA01003 ,OA01002   
                                   UNION ALL                        
                                   SELECT CA01001 , CA01003 ,
                                          SUM(round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018)) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01016,OA01018
                                          FROM      OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01001 = OA01013
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999
                                          WHERE OA01997 = 0 AND OA01003 = 1  {0}
                                            AND  (OA01016 <> 0 OR OA01018<>0)
                                         ) D
                                   GROUP BY CA01001 , CA01003 ,OA01002 
                             )A 
                             GROUP BY CA01001,CA01003  ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT CA01001,CA01003,SUM(OrderMoney) OrderMoney,
	                               MAX(OrderCount) OrderCount,MAX(InvoicedCount) InvoicedCount
                            FROM (SELECT CA01001 , CA01003 ,
                                         SUM( CASE WHEN vid = 1 THEN OA01022 ELSE 0 END) OrderMoney,
                                         COUNT(DISTINCT OA01002) OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01001 = OA01013
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1
			                                   AND  (OA01015 = '' or OA01015 is null)
                                               AND  (OA01017 = '' or OA01017 is null) 
                                               AND  OA01016 = 0 AND OA01018 = 0   
                                          {0}
                                       ) A
                                  GROUP BY CA01001 , CA01003 
                                  UNION ALL
                                  SELECT CA01001 , CA01003 ,
                                         SUM( CASE WHEN vid = 1 THEN OA01022*isnull(OA01016,0) ELSE 0 END) OrderMoney,
                                         COUNT(DISTINCT OA01002) OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01004 = OA01015
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1 AND (OA01015 <> '' and OA01015 is not null)
                                         {0}
                                        ) B
                                   GROUP BY CA01001 , CA01003     
	                               UNION
                                   SELECT CA01001 , CA01003 ,
                                          SUM( CASE WHEN vid = 1 THEN OA01022*isnull(OA01018,0) ELSE 0 END) OrderMoney,
                                          COUNT(DISTINCT OA01002) OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                          FROM	OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01004 = OA01017
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999
                                          WHERE OA01997 = 0 AND OA01003 = 1 AND (OA01017 <> '' and OA01017 is not null) 
                                          {0}
                                         ) C
                                   GROUP BY CA01001 , CA01003    
                                   UNION ALL                       
                                   SELECT CA01001 , CA01003 ,
                                          SUM( CASE WHEN vid = 1 THEN OA01022*(1-isnull(OA01016,0)-isnull(OA01018,0)) ELSE 0 END) OrderMoney,
                                          COUNT(DISTINCT OA01002) OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                          FROM      OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01001 = OA01013
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999
                                          WHERE OA01997 = 0 AND OA01003 = 1   AND  (OA01016 <> 0 OR OA01018<>0)
                                          {0}
                                         ) D
                                   GROUP BY CA01001 , CA01003  --有分享比例1 销售员金额 
                             )A 
                             GROUP BY CA01001,CA01003 ", strWhere);
                }
                 

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        /// <summary>
        /// 睡眠客户2
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportDormantCustomersTwoPage(string strWhere, bool blInvoice)
        {

            DataSet ds;
            try
            {
                string strSql = string.Empty;
                if (blInvoice)
                {
                    strSql = string.Format(@" SELECT UA01005,SUM(OA01022)OA01022,SUM(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2) OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) and OA01054 = 1 {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A 
										GROUP BY UA01005
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null) and OA01054 = 1 {0}
										) B 
										GROUP BY UA01005	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) and OA01054 = 1 {0}
										) C 
										GROUP BY UA01005
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
											FROM (
												SELECT  UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01054
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) and OA01054 = 1 {0}
											) D
										GROUP BY UA01005	
									) D
								)TOTAL
							GROUP BY UA01005 ", strWhere);
                }
                else
                {
                    strSql = string.Format(@" SELECT UA01005,SUM(OA01022)OA01022,sum(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005,SUM(OA01022) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) and OA01054 = 1 {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A WHERE NUM = 1 
										GROUP BY UA01005	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*OA01016) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054,OA01016
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null) and OA01054 = 1 {0}
										) B WHERE NUM = 1 
										GROUP BY UA01005	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*OA01018) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,OA01054,OA01018
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) and OA01054 = 1 {0}
										) C WHERE NUM = 1 
										GROUP BY UA01005
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*(1-OA01016-OA01018)) OA01022,SUM(OA01054) OA01054
											FROM (
												SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054,OA01016,OA01018
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) and OA01054 = 1 {0}
											) D WHERE NUM = 1 
										GROUP BY UA01005	
									) D
								)TOTAL
							GROUP BY UA01005 ", strWhere);
                }

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region 周报

        /// <summary>
        /// 导出周销售统计/Weekly Sales
        /// </summary>
        /// <param name="strWhereAdd">查询条件</param>
        /// <returns></returns>
        public DataSet ExportWeeklySalesPage(string strWhereAdd,bool blInvoice)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Empty;
                if (blInvoice)
                {
                    strsql = string.Format(@"  select OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                             dbo.FX_GetProvinceIdByCityId(ProvinceId) ProvinceId,dbo.FX_GetProvinceByCityId(ProvinceName) ProvinceName ,
                                             CB04002,OA01021,OA01015,OA01017,OA01044,sum(OA01020) OA01020,sum(OA01022) OA01022
                                       from (
                                        SELECT *  FROM
                                        (   SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                    CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                    OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022
                                            FROM OA01 
                                                   INNER JOIN OB01 ON OA01999 = OB01002
                                                   INNER JOIN OC01 ON OB01999 = OC01003
                                                   INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                   INNER JOIN UA01 ON UA01001 = OA01013
                                                   LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0  AND  (OA01015 = '' or OA01015 is null)
                                                AND  (OA01017 = '' or OA01017 is null)  AND  OA01016 = 0 AND OA01018 = 0 {0}
                                        ) A
                                        UNION ALL 
                                        SELECT *  FROM
                                        (   SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01004 = OA01015
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01015 <> '' and OA01015 is not null) {0}
                                        )  B  
                                        UNION ALL 
                                        SELECT *  FROM
                                        (    SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
                                            FROM OA01 
                                            INNER JOIN OB01 ON OA01999 = OB01002
                                            INNER JOIN OC01 ON OB01999 = OC01003
                                            INNER JOIN CA01 ON CA01001 = OA01038                                       
                                            INNER JOIN UA01 ON UA01004 = OA01017
                                            LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01017 <> '' and OA01017 is not null) {0}
                                         ) C  
                                         UNION ALL 
                                         SELECT *  FROM
                                         (    SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                          ) D 
                            ) GP  group by OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                  ProvinceId , ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044  ", strWhereAdd);
                }
                else
                {
                    strsql = string.Format(@" 
                                      select * from (
                                        SELECT *  FROM
                                        (   SELECT OA01001,OA01002,OA01009,OA01013 , 
                                                   CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                   dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                   CB04002, 
                                                   OA01020,
                                                   OA01021, OA01015,OA01017,
                                                   CAST(OA01022 as decimal(18,2)) OA01022,OA01044 ,
                                                   ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                                   INNER JOIN OB01 ON OA01999 = OB01002
                                                   INNER JOIN OC01 ON OB01999 = OC01003
                                                   INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                   INNER JOIN UA01 ON UA01001 = OA01013
                                                   LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0  AND  (OA01015 = '' or OA01015 is null)
                                                AND  (OA01017 = '' or OA01017 is null)  AND  OA01016 = 0 AND OA01018 = 0 {0}
                                     )  GP  WHERE NUM=1 
                                     UNION ALL
                                     SELECT    *
                                          FROM     ( SELECT    OA01001 ,
                                                                OA01002 ,
                                                                OA01009 ,
                                                                OA01013 ,
                                                                CA01001 ,
                                                                CA01003 ,
                                                                UA01004 ,
                                                                OA01015 AS UA01005 ,
                                                                UA01013 ,
                                                                dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                                dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                                CB04002 ,
                                                               ( OA01020 * ISNULL(OA01016, 0) ) OA01020 ,
                                                                OA01021 ,
                                                                UA01005 AS OA01015 ,
                                                                OA01017 ,
                                                                ( OA01022 * ISNULL(OA01016, 0) ) OA01022 ,
                                                                OA01044 ,
                                                                ROW_NUMBER() OVER ( PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                                      FROM      OA01
                                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                                INNER JOIN CA01 ON CA01001 = OA01038
                                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                                      WHERE     OA01997 = 0
                                                                AND ( OA01015 <> ''  AND OA01015 IS NOT NULL )
                                                                AND OA01018 = 0 {0} 
                                                    ) GP
                                          WHERE     NUM = 1                                    
                                     UNION ALL 

                                        SELECT    *
                                              FROM      ( SELECT    OA01001 ,
                                                            OA01002 ,
                                                            OA01009 ,
                                                            OA01013 ,
                                                            CA01001 ,
                                                            CA01003 ,
                                                            UA01004 ,
                                                            OA01017 AS UA01005 ,
                                                            UA01013 ,
                                                            dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                            dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                            CB04002 ,
                                                            ( OA01020 * ISNULL(OA01018, 0) ) OA01020 ,
                                                            OA01021 ,
                                                            UA01005 AS OA01015 ,
                                                            OA01017 ,
                                                            ( OA01022 * ISNULL(OA01018, 0) ) OA01022 ,
                                                            OA01044 ,
                                                            ROW_NUMBER() OVER ( PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                                    FROM      OA01
                                                            INNER JOIN OB01 ON OA01999 = OB01002
                                                            INNER JOIN OC01 ON OB01999 = OC01003
                                                            INNER JOIN CA01 ON CA01001 = OA01038
                                                            INNER JOIN UA01 ON UA01004 = OA01017
                                                            LEFT JOIN CB04 ON CB04001 = CA01020
                                                    WHERE     OA01997 = 0
                                                            AND ( OA01017 <> ''
                                                                    AND OA01017 IS NOT NULL
                                                                ) {0}
                                                ) GP
                                        WHERE     NUM = 1
                                  
                                     UNION ALL
                                    
                                    SELECT *  FROM
                                     (      SELECT  OA01001,OA01002,OA01009,OA01013 , 
                                                CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                CB04002,(OA01020* (1-isnull(OA01016,0)-isnull(OA01018,0))) OA01020 ,
                                                OA01021,OA01015,OA01017,
                                                CAST((OA01022*(1-isnull(OA01016,0)-isnull(OA01018,0))) AS DECIMAL(18,2)) OA01022,
                                                OA01044 ,
                                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                    )   GP  WHERE NUM=1 
                            ) GP 
                             ", strWhereAdd);
                }
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出欠款提醒统计/Debts Alert
        /// </summary>
        /// <returns></returns>
        public DataSet ExportDebtsAlertPage(string strWhereAdd, bool blInvoice)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@" SELECT OA01001,OA01002,OA01009,OA01013,CA01001,CA01003, 
	                                               OA01020,OA01021,OA01022,OA01044, OA01051 ,CA01025,
	                                                DATEDIFF(day,b.DebtsDays,getdate()) DebtsDays,
	                                               b.OC01015,b.OC01011,OP01005,
	                                               OP01016,UA01004,UA01013,UA01005,
	                                               ROW_NUMBER() OVER( ORDER BY OC01015 DESC ) NUM
                                            FROM OA01 a
                                            INNER JOIN CA01 ON CA01001 = OA01038                                       
                                            INNER JOIN UA01 ON UA01001 = OA01013 
                                            INNER JOIN OP01 ON OA01999 = OP01003 
                                            INNER JOIN ( SELECT OA01999,MAX(OC01015) OC01015,MAX(OC01011) OC01011,MIN(OC01009) OC01009
			                                            , CASE WHEN MIN(OC01015)>= MIN(OC01011) 
				                                               THEN MIN(OC01011) 
				                                               WHEN MIN(OC01015)< MIN(OC01011) 
				                                               THEN MIN(OC01011) 
				                                               WHEN MIN(OC01015) IS NULL AND MIN(OC01011) IS NOT NULL
				                                               THEN MIN(OC01011) 
				                                               WHEN MIN(OC01015) IS NOT NULL AND MIN(OC01011) IS NULL
				                                               THEN MIN(OC01015) END DebtsDays
                                                    FROM OC01 
                                                    INNER JOIN OB01 ON OB01999 = OC01003 
                                                    INNER JOIN OA01 ON OA01999 = OB01002 
                                                    GROUP BY oa01999
                                            )b ON a.oa01999=b.oa01999
                                            WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0   {0} ", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (!blInvoice) return ds;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                DataTable dtMoney = GetOA01020andOA01022ByOA01002(item["OA01002"].ToString());
                item["OA01020"] = dtMoney.Rows[0]["OA01020"];
                item["OA01022"] = dtMoney.Rows[0]["OA01022"];
            }
            return ds;
        }

        /// <summary>
        ///导出库存货物提醒/Pending Alert
        /// </summary>
        /// <returns></returns>
        public DataSet ExportPendingAlertPage(string strWhereAdd)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@" SELECT
                                OA01002,OB01005,OB01007,CAST(OB01008 AS DECIMAL(18,2)) OB01008,
                                CAST(OB01009 AS DECIMAL(18,2)) OB01009 ,OC01009,UA01005,
                                OA01010, CA01001,CA01003, UA01004,UA01013,OC01015,
                                ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber,
                                DATEDIFF(day,OA01010,getdate()) Pendingdays 
                                FROM OB01 
                                INNER JOIN OA01 ON(OB01002=OA01999)
                                INNER JOIN CA01 ON CA01001 = OA01038 
                                INNER JOIN UA01 ON UA01001 = OA01013
                                INNER JOIN OC01 ON(OC01003=OB01999)
                                WHERE OB01997 = 0  AND NOT EXISTS(
	                                	SELECT TOP 1 1
	                                	FROM dbo.OC01  
	                                	JOIN dbo.OB01 ON (OC01003=OB01999) 
	                                	JOIN OA01 t1 ON(OB01002=OA01999) 
	                                	WHERE (OC01010<>0 or oc01007<>1) 
	                                	AND oa01999=oa01.OA01999
	                                    )
                                AND (OA01003 = 1 OR OA01003 =3) {0} ", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        ///导出周欠款统计/Weekly Debts
        /// </summary>
        /// <returns></returns>
        public DataSet ExportWeeklyDebtsPage(string strWhereAdd)
        {
            DataSet ds;
            try
            {

                object obj = null;
                string strsql = string.Format(@" SELECT OA01001,OA01002,OA01009,OA01013,CA01001,CA01003, 
                                      OA01020,OA01021,OA01022,OA01044,OA01051 ,UA01005,
	                                    DATEDIFF(day,b.DebtsDays,getdate()) DebtsDays,
	                                  OP01005,OP01016,  b.OC01011,b.OC01009,b.OC01015,
                                      UA01004,UA01013,OA01053,
	                                   ROW_NUMBER() OVER( ORDER BY OC01015 DESC ) NUM
                                FROM OA01 a
                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                INNER JOIN UA01 ON UA01001 = OA01013 
                                INNER JOIN OP01 ON OA01999 = OP01003 
                                INNER JOIN ( SELECT OA01999,MAX(OC01015) OC01015,MIN(OC01011) OC01011, MIN(OC01009) OC01009
			                                , CASE WHEN MIN(OC01015)>= MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015)< MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NULL AND MIN(OC01011) IS NOT NULL
				                                  THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NOT NULL AND MIN(OC01011) IS NULL
				                                   THEN MIN(OC01015) END DebtsDays
                                        FROM OC01 
                                        INNER JOIN OB01 ON OB01999 = OC01003 
                                        INNER JOIN OA01 ON OA01999 = OB01002 
                                        GROUP BY oa01999
                                        )b ON a.oa01999=b.oa01999
                                        WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0   {0}", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        ///导出周库存货物统计/Weekly Pending
        /// </summary>
        /// <returns></returns>
        public DataSet ExportWeeklyPendingPage(string strWhereAdd)
        {
            DataSet ds;
            try
            {

                object obj = null;
                string strsql = string.Format(@" SELECT  OA01002,OB01005,OB01007,CAST(OB01008 AS DECIMAL(18,2)) OB01008,OA01053,
                                       CAST(OB01009 AS DECIMAL(18,2)) OB01009 ,OC01009,OC01015,
                                       OA01009,OA01010, CA01001,CA01003, UA01004,UA01013,UA01005,
                                       ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber,
                                       DATEDIFF(day,OA01010,getdate()) Pendingdays 
                                       FROM   OB01 
                                       INNER JOIN OA01 ON(OB01002=OA01999)
                                       INNER JOIN CA01 ON CA01001 = OA01038 
                                       INNER JOIN UA01 ON UA01001 = OA01013
                                       INNER JOIN OC01 ON(OC01003=OB01999) 
                                      WHERE OB01997 = 0  AND (OA01003 = 1 OR OA01003 =3) 
                                 AND NOT EXISTS(
		                            SELECT TOP 1 1
		                            FROM dbo.OC01  
		                            JOIN dbo.OB01 ON (OC01003=OB01999) 
		                            JOIN OA01 t1 ON(OB01002=OA01999) 
                                    INNER JOIN CA01 ON CA01001 = OA01038 
                                    INNER JOIN UA01 ON UA01001 = OA01013
		                            WHERE (OC01010<>0 or oc01007<>1) 
		                            AND oa01999=oa01.OA01999 {0} ) 
                                  AND NOT EXISTS(
		                              SELECT  TOP 1 1
		                              FROM dbo.OC01  
		                              JOIN dbo.OB01 ON (OC01003=OB01999) 
		                              JOIN OA01 t1 ON(OB01002=OA01999) 
		                              WHERE t1.OA01999=OA01.OA01999 
		                              AND OC01010 = 0 AND oc01007 = 0) {0} ", strWhereAdd);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion

        #region 日报

        /// <summary>
        ///  到款统计/Daliy Collection
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportDaliyCollection(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT OA01009 , OA01002 , CA01003 , OA01020 ,
                                                      UA01004 , UA01005, CAST(OP01007 as decimal(18,2)) OP01007 , 
                                                      OP01008 , CAST(OP01009 as decimal(18,2)) OP01009 ,
                                                      OP01010 ,CAST(OP01011 as decimal(18,2)) OP01011   , OP01012 , 
                                                      CAST(OP01013 as decimal(18,2)) OP01013  ,  OP01014 ,
                                                      CAST(OP01015 as decimal(18,2)) OP01015 
                                                FROM  dbo.OA01
                                                        INNER JOIN dbo.OP01 ON OA01999 = OP01003
                                                        INNER JOIN dbo.CA01 ON CA01001 = OA01038
                                                        INNER JOIN dbo.UA01 ON UA01001 = OA01013 
                                                WHERE   OA01003 = 1  {0} ORDER BY OA01009 DESC", strWhere);

                //                string strSql = string.Format(@"SELECT  CONVERT(varchar(100), OA01009, 23) AS OA01009 , OA01002 , CA01003 , OA01020 ,
                //                                                        UA01004 , OP01007 , CONVERT(varchar(100), OP01008, 23) AS OP01008 , OP01009 ,
                //                                                        CONVERT(varchar(100), OP01010, 23) AS OP01010 , OP01011 , CONVERT(varchar(100), OP01012, 23) AS OP01012 , OP01013 , 
                //                                                        CONVERT(varchar(100), OP01014, 23) AS OP01014  , OP01016 
                //                                                FROM  dbo.OA01
                //                                                        INNER JOIN dbo.OP01 ON OA01999 = OP01003
                //                                                        INNER JOIN dbo.CA01 ON CA01001 = OA01038
                //                                                        INNER JOIN dbo.UA01 ON UA01001 = OA01013 
                //                                                WHERE   OA01003 = 1  {0} ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 发货统计/Daily Shipment
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportDailyShipment(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  OA01009,OA01002,CA01003,UA01004,UA01005,OB01005,
                                                        OB01007,OC01012,OC01013,OC01018,OC01017,
                                                        OA01028,OC01011,OC01015
                                                FROM OB01 
                                                     INNER JOIN dbo.OC01 ON OB01999=OC01003
                                                     INNER JOIN dbo.OA01 ON OA01999 = OB01002
                                                     INNER JOIN dbo.OP01 ON OP01003 = OA01999
                                                     INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                                     INNER JOIN dbo.UA01 ON UA01001 = OA01013  
                                                WHERE  OC01010 = 1  {0} 
                                                GROUP BY OA01009,OA01002,CA01003,UA01004,UA01005,OB01005,OB01007,OC01012,OC01013,OC01018,OC01017,OA01028,OC01011,OC01015
                                                HAVING dbo.FX_GetDeliveredState(COUNT(1),SUM(Case when OC01010 = 1 then 1 else 0 end)) ='true'
                                                ORDER BY OA01009 DESC ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 到货通知统计/Arrival Notice
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportArrivalNotice(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  OA01009,OA01002,CA01003,OA01020,UA01005,UA01001,
                                                        UA01004,OP01015,UA01013,OC01007,MAX(OC01009) AS OC01009
                                                FROM OB01 
                                         INNER JOIN  dbo.OC01 ON OB01999=OC01003
                                         INNER JOIN  dbo.OA01 ON OA01999 = OB01002
                                         INNER JOIN  dbo.OP01 ON OP01003 = OA01999
                                         INNER JOIN  dbo.CA01 ON CA01001 = OA01038 
                                         INNER JOIN  dbo.UA01 ON UA01001 = OA01013 
                                        WHERE  OA01003 = 1  AND OC01010 <> 1  AND OA01002 NOT IN(
		                    	            SELECT OA01002
		                    	            FROM dbo.OA01 
		                       	            JOIN dbo.OB01 ON OA01999 = OB01002
		                    	            JOIN dbo.OC01 ON OB01999 = OC01003
		                    	            WHERE OC01007 =0 and OC01010 = 1
		                                   ) 
                                        GROUP BY OB01002 ,OA01002,OA01009,CA01003,OA01020,UA01001,UA01004,UA01005,OP01015,OC01007,UA01013 
                                        HAVING 1=1 {0}", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 订单明细统计/Order List
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportOrderList(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"
                                          SELECT OA01009,OA01002,CA01002,CA01003,CA01009 
	                                                 ,UA01004,UA01005,OB01005,OB01007,CAST(OB01008 as decimal(18,2)) OB01008
                                                     ,CASE OA01003 WHEN 2 THEN  -( OB01007 * OB01008 ) ELSE ( OB01007 * OB01008 ) END  OA01020 
                                                     , CASE OA01003 WHEN 2 THEN  -ROUND(( OB01007 * OB01008
                                                    / OA01021 / 1.17 ), 2) ELSE ROUND(( OB01007 * OB01008
                                                    / OA01021 / 1.17 ), 2) END  OA01022 
                                                     ,OA01021,OA01041,CA01018,OA01025
	                                                 ,OA01040,OP01007,OP01008,CAST(OP01009 as decimal(18,2)) OP01009
	                                                 ,OP01010,CAST(OP01011 as decimal(18,2)) OP01011,OP01012
                                                     ,CAST(OP01013 as decimal(18,2)) OP01013 ,OP01014
	                                                 ,CAST(OP01015 as decimal(18,2)) OP01015,OA01010,OC01009,OC01008,OC01011
	                                                 ,OC01015,OA01044,(CASE CA01024 WHEN 0 THEN 'N' WHEN 1 THEN 'Y' ELSE '' END) CA01024,OB02002,CB02002
	                                                 ,CB03002,CB04002,dbo.FX_GetProvinceByCityId(CA01013) AS CA01013
	                                                 ,dbo.FX_Get220(OA01043) AS OA01043,dbo.FX_Get240(OA01042) AS OA01042
                                                     ,OA01028,OA01015,OA01016,OA01017,OA01018,CB01002,GA03002
                                          FROM OA01
                                          INNER JOIN OB01 on OA01999 = OB01002
                                          INNER JOIN OC01 on OB01999=OC01003
                                          LEFT JOIN dbo.OP01 ON OP01003 = OA01999
                                          INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                          INNER JOIN dbo.CB01 ON CB01001=CA01016
                                          INNER JOIN dbo.GA03 ON GA03001=CA01013
                                          INNER JOIN dbo.UA01 ON UA01001 = OA01013 
                                          LEFT JOIN dbo.CB03 ON CB03001 = OA01040
                                          LEFT JOIN DBO.CB04 ON CB04001 = OA01041 
                                          LEFT JOIN DBO.OB02 ON OB02001 = OA01025
                                          LEFT JOIN DBO.CB02 ON CB02001 = CA01018  
                                          WHERE  1 = 1  {0}  
                                         ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 到货统计NewArrival
        /// </summary>
        /// <param name="strWhere">Where</param>
        /// <returns></returns>
        public DataSet ExportNewArrival(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  OA01009,OA01002,CA01003,OA01020,UA01001,
                                                        UA01004,UA01005,UA01013,CAST(OP01015 as decimal(18,2)) OP01015,OC01007,MAX(OC01009) AS OC01009,
                                                        ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber
                                                FROM OB01 
                                                     INNER JOIN dbo.OC01 ON OB01999=OC01003
                                                     INNER JOIN dbo.OA01 ON OA01999 = OB01002
                                                     INNER JOIN dbo.OP01 ON OP01003 = OA01999
                                                     INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                                     INNER JOIN dbo.UA01 ON UA01001 = OA01013 
                                               WHERE  OA01003 = 1  AND OA01002 NOT IN(
		                    	                    SELECT OA01002
		                    	                    FROM dbo.OA01 
		                       	                    JOIN dbo.OB01 ON OA01999 = OB01002
		                    	                    JOIN dbo.OC01 ON OB01999 = OC01003
		                    	                    WHERE OC01007 =0 ) 
                                              group by OB01002 ,OA01002,OA01009,CA01003,OA01020,UA01001,UA01004,UA01005,OP01015,OC01007,UA01013
                                                  HAVING 1=1 {0}
                                         ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        #endregion

        #region 订单导入DataTable转换List
        public List<OrderBase> ConvertOrderBaseToList(DataTable dt)
        {
            List<OrderBase> _listOrderBase = new List<OrderBase>();
            foreach (DataRow item in dt.Rows)
            {
                OrderBase _OrderBase = new OrderBase();
                _OrderBase.OA01002 = item["OA01002"].ToString();
                if (item["OA01003"] != DBNull.Value && item["OA01003"] != "")
                    _OrderBase.OA01003 = Convert.ToInt32(item["OA01003"]);
                if (item["OA01005"] != DBNull.Value && item["OA01005"] != "")
                    _OrderBase.OA01005 = Convert.ToInt32(item["OA01005"]);
                _OrderBase.OA01008 = item["OA01008"].ToString();
                if (item["OA01009"] != DBNull.Value && item["OA01009"] != "")
                    _OrderBase.OA01009 = Convert.ToDateTime(item["OA01009"]);
                if (item["OA01010"] != DBNull.Value && item["OA01010"] != "")
                    _OrderBase.OA01010 = Convert.ToDateTime(item["OA01010"]);
                if (item["OA01011"] != DBNull.Value && item["OA01011"] != "")
                    _OrderBase.OA01011 = Convert.ToDateTime(item["OA01011"]);
                _OrderBase.OA01012 = item["OA01012"].ToString();
                _OrderBase.OA01013 = item["OA01013"].ToString();
                _OrderBase.OA01014 = item["OA01014"].ToString();
                _OrderBase.OA01015 = item["OA01015"].ToString();
                if (item["OA01016"] != DBNull.Value && item["OA01016"] != "")
                    _OrderBase.OA01016 = Convert.ToDecimal(item["OA01016"]);
                _OrderBase.OA01017 = item["OA01017"].ToString();
                if (item["OA01018"] != DBNull.Value && item["OA01018"] != "")
                    _OrderBase.OA01018 = Convert.ToDecimal(item["OA01018"]);
                if (item["OA01019"] != DBNull.Value && item["OA01019"] != "")
                    _OrderBase.OA01019 = Convert.ToDecimal(item["OA01019"]);
                if (item["OA01020"] != DBNull.Value && item["OA01020"] != "")
                    _OrderBase.OA01020 = Convert.ToDecimal(item["OA01020"]);
                if (item["OA01021"] != DBNull.Value && item["OA01021"] != "")
                    _OrderBase.OA01021 = Convert.ToDecimal(item["OA01021"]);
                if (item["OA01022"] != DBNull.Value && item["OA01022"] != "")
                    _OrderBase.OA01022 = Convert.ToDecimal(item["OA01022"]);
                if (item["OA01023"] != DBNull.Value && item["OA01023"] != "")
                    _OrderBase.OA01023 = Convert.ToDecimal(item["OA01023"]);
                _OrderBase.OA01024 = item["OA01024"].ToString();
                if (item["OA01025"] != DBNull.Value && item["OA01025"] != "")
                    _OrderBase.OA01025 = Convert.ToInt32(item["OA01025"]);
                _OrderBase.OA01026 = item["OA01026"].ToString();
                _OrderBase.OA01027 = item["OA01027"].ToString();
                _OrderBase.OA01028 = item["OA01028"].ToString();
                _OrderBase.OA01029 = item["OA01029"].ToString();
                _OrderBase.OA01030 = item["OA01030"].ToString();
                _OrderBase.OA01031 = item["OA01031"].ToString();
                _OrderBase.OA01032 = item["OA01032"].ToString();
                _OrderBase.OA01033 = item["OA01033"].ToString();
                _OrderBase.OA01034 = item["OA01034"].ToString();
                _OrderBase.OA01035 = item["OA01035"].ToString();
                if (item["OA01036"] != DBNull.Value && item["OA01036"] != "")
                    _OrderBase.OA01038 = Convert.ToInt32(item["OA01036"]);
                if (item["OA01044"] != DBNull.Value && item["OA01044"] != "")
                    _OrderBase.OA01044 = Convert.ToInt32(item["OA01044"].ToString());
                if (item["OA01045"] != DBNull.Value && item["OA01045"] != "")
                    _OrderBase.OA01045 = Convert.ToInt32(item["OA01045"].ToString());
                _OrderBase.OA01046 = item["OA01046"].ToString();
                _OrderBase.OA01047 = item["OA01047"].ToString();
                _OrderBase.OA01048 = item["OA01048"].ToString();
                _OrderBase.OA01049 = item["OA01049"].ToString();
                _OrderBase.OA01050 = item["OA01050"].ToString();
                if (item["OA01051"] != DBNull.Value && item["OA01051"] != "")
                    _OrderBase.OA01051 = Convert.ToDateTime(item["OA01051"].ToString());
                if (item["OA01998"] != DBNull.Value && item["OA01998"] != "")
                    _OrderBase.OA01998 = Convert.ToDateTime(item["OA01998"].ToString());
                _OrderBase.OA01999 = Guid.NewGuid().ToString();
                _OrderBase.OA01997 = 0;
                _listOrderBase.Add(_OrderBase);
            }
            return _listOrderBase;
        }

        #endregion

        #region 商品导入DataTable转换List
        public List<OrderProduct> ConvertOrderProdustToList(ref DataTable dt)
        {
            List<OrderProduct> _listOrderProduct = new List<OrderProduct>();
            foreach (DataRow item in dt.Rows)
            {
                OrderProduct _OrderProduct = new OrderProduct();
                _OrderProduct.OB01002 = item["OA01002"].ToString();
                if (item["ProductId"] != DBNull.Value && item["ProductId"] != "")
                    _OrderProduct.OB01004 = Convert.ToInt32(item["ProductId"]);
                _OrderProduct.OB01005 = item["OB01005"].ToString();
                _OrderProduct.OB01006 = item["OB01006"].ToString();
                if (item["OB01007"] != DBNull.Value && item["OB01007"] != "")
                    _OrderProduct.OB01007 = Convert.ToInt32(item["OB01007"]);
                if (item["OB01008"] != DBNull.Value && item["OB01008"] != "")
                    _OrderProduct.OB01008 = Convert.ToDecimal(item["OB01008"]);
                if (item["OB01009"] != DBNull.Value && item["OB01009"] != "")
                    _OrderProduct.OB01009 = Convert.ToDecimal(item["OB01009"]);
                if (item["OB01010"] != DBNull.Value && item["OB01010"] != "")
                    _OrderProduct.OB01010 = Convert.ToDecimal(item["OB01010"]);
                if (item["OB01011"] != DBNull.Value && item["OB01011"] != "")
                    _OrderProduct.OB01011 = Convert.ToDecimal(item["OB01011"]);
                if (item["OB01012"] != DBNull.Value && item["OB01012"] != "")
                    _OrderProduct.OB01012 = Convert.ToDecimal(item["OB01012"]);
                if (item["OB01013"] != DBNull.Value && item["OB01013"] != "")
                    _OrderProduct.OB01013 = Convert.ToDecimal(item["OB01013"]);
                if (item["OB01014"] != DBNull.Value && item["OB01014"] != "")
                    _OrderProduct.OB01014 = Convert.ToDecimal(item["OB01014"]);
                if (item["OB01015"] != DBNull.Value && item["OB01015"] != "")
                    _OrderProduct.OB01015 = Convert.ToDateTime(item["OB01015"]);
                if (item["OB01016"] != DBNull.Value && item["OB01016"] != "")
                    _OrderProduct.OB01016 = Convert.ToDateTime(item["OB01016"]);
                if (item["OB01017"] != DBNull.Value && item["OB01017"] != "")
                    _OrderProduct.OB01017 = Convert.ToDateTime(item["OB01017"]);
                if (item["OB01018"] != DBNull.Value && item["OB01018"] != "")
                    _OrderProduct.OB01018 = Convert.ToDateTime(item["OB01018"]);
                if (item["OB01998"] != DBNull.Value && item["OB01998"] != "")
                    _OrderProduct.OB01998 = Convert.ToDateTime(item["OB01998"]);
                _OrderProduct.OB01999 = Guid.NewGuid().ToString();
                _OrderProduct.OB01997 = 0;
                item["OrderGUID"] = _OrderProduct.OB01999;
                _listOrderProduct.Add(_OrderProduct);
            }
            return _listOrderProduct;
        }

        #endregion

        #region 发票导入DataTable转换List
        public List<OrderInvoice> ConvertOrderInvoiceToList(DataTable dt)
        {
            List<OrderInvoice> _listOrderInvoice = new List<OrderInvoice>();
            foreach (DataRow item in dt.Rows)
            {
                OrderInvoice _OrderInvoice = new OrderInvoice();
                if (item["OC01002"] != DBNull.Value && item["OC01002"] != "")
                    _OrderInvoice.OC01002 = Convert.ToInt32(item["OC01002"]);
                _OrderInvoice.OC01003 = item["OC01003"].ToString();
                _OrderInvoice.OC01005 = item["OC01005"].ToString();
                if (item["OC01006"] != DBNull.Value && item["OC01006"] != "")
                    _OrderInvoice.OC01006 = Convert.ToInt32(item["OC01006"]);
                if (item["OC01007"] != DBNull.Value && item["OC01007"] != "")
                {
                    if (item["OC01007"].ToString() == "Y")
                    {
                        item["OC01007"] = 1;
                    }
                    else
                    {
                        item["OC01007"] = 0;
                    }
                    _OrderInvoice.OC01007 = Convert.ToInt32(item["OC01007"]);
                }
                _OrderInvoice.OC01008 = item["OC01008"].ToString();
                if (item["OC01009"] != DBNull.Value && item["OC01009"] != "")
                    _OrderInvoice.OC01009 = Convert.ToDateTime(item["OC01009"]);
                if (item["OC01010"] != DBNull.Value && item["OC01010"] != "")
                {
                    if (item["OC01010"].ToString() == "Y")
                    {
                        item["OC01010"] = 1;
                    }
                    else
                    {
                        item["OC01010"] = 0;
                    }
                    _OrderInvoice.OC01010 = Convert.ToInt32(item["OC01010"]);
                }
                if (item["OC01011"] != DBNull.Value && item["OC01011"] != "")
                    _OrderInvoice.OC01011 = Convert.ToDateTime(item["OC01011"]);
                _OrderInvoice.OC01012 = item["OC01012"].ToString();
                _OrderInvoice.OC01013 = item["OC01013"].ToString();
                if (item["OC01014"] != DBNull.Value && item["OC01014"] != "")
                {
                    if (item["OC01014"].ToString() == "Y")
                    {
                        item["OC01014"] = 1;
                    }
                    else
                    {
                        item["OC01014"] = 0;
                    }
                    _OrderInvoice.OC01014 = Convert.ToInt32(item["OC01014"]);
                }
                if (item["OC01015"] != DBNull.Value && item["OC01015"] != "")
                    _OrderInvoice.OC01015 = Convert.ToDateTime(item["OC01015"]);
                if (item["OC01016"] != DBNull.Value && item["OC01016"] != "")
                    _OrderInvoice.OC01016 = Convert.ToDateTime(item["OC01016"]);
                _OrderInvoice.OC01017 = item["OC01017"].ToString();
                if (item["OC01998"] != DBNull.Value && item["OC01998"] != "")
                    _OrderInvoice.OC01998 = Convert.ToDateTime(item["OC01998"]);
                _OrderInvoice.OC01997 = 0;
                _listOrderInvoice.Add(_OrderInvoice);
            }
            return _listOrderInvoice;
        }

        #endregion

        #region 付款信息导入DataTable转换List
        public List<OrderPayment> ConvertOrderPaymentToList(DataTable dt)
        {
            List<OrderPayment> _listOrderPayment = new List<OrderPayment>();
            foreach (DataRow item in dt.Rows)
            {
                OrderPayment _OrderPayment = new OrderPayment();
                _OrderPayment.OP01002 = item["OP01002"].ToString();
                _OrderPayment.OP01003 = item["OP01003"].ToString();
                if (item["OP01004"] != DBNull.Value && item["OP01004"] != "")
                    _OrderPayment.OP01004 = Convert.ToDateTime(item["OP01004"]);
                if (item["OP01005"] != DBNull.Value && item["OP01005"] != "")
                    _OrderPayment.OP01005 = Convert.ToDecimal(item["OP01005"]);
                if (item["OP01006"] != DBNull.Value && item["OP01006"] != "")
                    _OrderPayment.OP01006 = Convert.ToInt32(item["OP01006"]);
                if (item["OP01007"] != DBNull.Value && item["OP01007"] != "")
                    _OrderPayment.OP01007 = Convert.ToDecimal(item["OP01007"]);
                if (item["OP01008"] != DBNull.Value && item["OP01008"] != "")
                    _OrderPayment.OP01008 = item["OP01008"].ToString();
                if (item["OP01009"] != DBNull.Value && item["OP01009"] != "")
                    _OrderPayment.OP01009 = Convert.ToDecimal(item["OP01009"]);
                if (item["OP01010"] != DBNull.Value && item["OP01010"] != "")
                    _OrderPayment.OP01010 = item["OP01010"].ToString();
                if (item["OP01011"] != DBNull.Value && item["OP01011"] != "")
                    _OrderPayment.OP01011 = Convert.ToDecimal(item["OP01011"]);
                if (item["OP01012"] != DBNull.Value && item["OP01012"] != "")
                    _OrderPayment.OP01012 = item["OP01012"].ToString();
                if (item["OP01013"] != DBNull.Value && item["OP01013"] != "")
                    _OrderPayment.OP01013 = Convert.ToDecimal(item["OP01013"]);
                if (item["OP01014"] != DBNull.Value && item["OP01014"] != "")
                    _OrderPayment.OP01014 = item["OP01014"].ToString();
                if (item["OP01015"] != DBNull.Value && item["OP01015"] != "")
                    _OrderPayment.OP01015 = Convert.ToDecimal(item["OP01015"]);
                if (item["OP01016"] != DBNull.Value && item["OP01016"] != "")
                    _OrderPayment.OP01016 = Convert.ToDecimal(item["OP01016"]);
                if (item["OP01998"] != DBNull.Value && item["OP01998"] != "")
                    _OrderPayment.OP01998 = Convert.ToDateTime(item["OP01998"]);
                _listOrderPayment.Add(_OrderPayment);
            }
            return _listOrderPayment;
        }

        #endregion

        #region 根据订单号查询开票金额

        public DataTable GetOA01020andOA01022ByOA01002(string oa01002)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"
                    select SUM(OA01020) OA01020,SUM(OA01022) OA01022
                    from (
	                    SELECT OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01001 = OA01013
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND  (OA01015 = '' or OA01015 is null)
		                    AND  (OA01017 = '' or OA01017 is null) AND  OA01016 = 0 AND OA01018 = 0 and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01004 = OA01015
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01015 <> '' and OA01015 is not null)  and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01004 = OA01017
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01017 <> '' and OA01017 is not null)  and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01001 = OA01013
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01016 <> 0 OR OA01018 <> 0)  and OA01002 = '{0}' 
                    )tab", oa01002);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        #endregion
    }
}
