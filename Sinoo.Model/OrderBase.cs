using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 订单基本信息(OA01)
    /// </summary>
    [Serializable]
    public partial class OrderBase
    {
        public OrderBase()
        { }
        #region Model
        private int? _oa01001;
        private string _oa01002;
        private int? _oa01003;
        private int? _oa01004;
        private int? _oa01005;
        private string _oa01006;
        private string _oa01007;
        private string _oa01008;
        private DateTime? _oa01009;
        private DateTime? _oa01010;
        private DateTime? _oa01011;
        private string _oa01012;
        private string _oa01013;
        private string _oa01014;
        private string _oa01015;
        private decimal? _oa01016;
        private string _oa01017;
        private decimal? _oa01018;
        private decimal? _oa01019;
        private decimal? _oa01020;
        private decimal? _oa01021;
        private decimal? _oa01022;
        private decimal? _oa01023;
        private string _oa01024;
        private int? _oa01025;
        private string _oa01026;
        private string _oa01027;
        private string _oa01028;
        private string _oa01029;
        private string _oa01030;
        private string _oa01031;
        private string _oa01032;
        private string _oa01033;
        private string _oa01034;
        private string _oa01035;
        private string _oa01036;
        private int? _oa01037;
        private int? _oa01038;
        private string _oa01039;
        private int? _oa01040;
        private int? _oa01041;
        private int? _oa01042;
        private int? _oa01043;
        private int? _oa01044;
        private int? _oa01045;
        private string _oa01046;
        private string _oa01047;
        private string _oa01048;
        private string _oa01049;
        private string _oa01050;
        private DateTime? _oa01051;
        private int? _oa01052;
        private string _oa01053;
        private int? _oa01054;
        private string _oa01055;
        private string _oa01056;
        private string _oa01057;
        private string _oa01058;
        private decimal _oa01060;
        private int? _oa01997;
        private DateTime? _oa01998;
        private string _oa01999;

        /// <summary>
        /// 税率
        /// </summary>
        public decimal OA01060
        {
            set { _oa01060 = value; }
            get { return _oa01060; }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int? OA01001
        {
            set { _oa01001 = value; }
            get { return _oa01001; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OA01002
        {
            set { _oa01002 = value; }
            get { return _oa01002; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int? OA01003
        {
            set { _oa01003 = value; }
            get { return _oa01003; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? OA01004
        {
            set { _oa01004 = value; }
            get { return _oa01004; }
        }
        /// <summary>
        /// 订单模式(系统订单)
        /// </summary>
        public int? OA01005
        {
            set { _oa01005 = value; }
            get { return _oa01005; }
        }
        /// <summary>
        /// 系统-订单编号
        /// </summary>
        public string OA01006
        {
            set { _oa01006 = value; }
            get { return _oa01006; }
        }
        /// <summary>
        /// 喷嘴-订单编号
        /// </summary>
        public string OA01007
        {
            set { _oa01007 = value; }
            get { return _oa01007; }
        }
        /// <summary>
        /// 发票号
        /// </summary>
        public string OA01008
        {
            set { _oa01008 = value; }
            get { return _oa01008; }
        }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime? OA01009
        {
            set { _oa01009 = value; }
            get { return _oa01009; }
        }
        /// <summary>
        /// 要求交货日期
        /// </summary>
        public DateTime? OA01010
        {
            set { _oa01010 = value; }
            get { return _oa01010; }
        }
        /// <summary>
        /// 承诺交货日期
        /// </summary>
        public DateTime? OA01011
        {
            set { _oa01011 = value; }
            get { return _oa01011; }
        }
        /// <summary>
        /// 报价号
        /// </summary>
        public string OA01012
        {
            set { _oa01012 = value; }
            get { return _oa01012; }
        }
        /// <summary>
        /// 销售员
        /// </summary>
        public string OA01013
        {
            set { _oa01013 = value; }
            get { return _oa01013; }
        }
        /// <summary>
        /// 特殊要求
        /// </summary>
        public string OA01014
        {
            set { _oa01014 = value; }
            get { return _oa01014; }
        }
        /// <summary>
        /// 分享人名称1
        /// </summary>
        public string OA01015
        {
            set { _oa01015 = value; }
            get { return _oa01015; }
        }
        /// <summary>
        /// 分享比例1
        /// </summary>
        public decimal? OA01016
        {
            set { _oa01016 = value; }
            get { return _oa01016; }
        }
        /// <summary>
        /// 分享人名称2
        /// </summary>
        public string OA01017
        {
            set { _oa01017 = value; }
            get { return _oa01017; }
        }
        /// <summary>
        /// 分享比例2
        /// </summary>
        public decimal? OA01018
        {
            set { _oa01018 = value; }
            get { return _oa01018; }
        }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal? OA01019
        {
            set { _oa01019 = value; }
            get { return _oa01019; }
        }
        /// <summary>
        /// 总金额RMB
        /// </summary>
        public decimal? OA01020
        {
            set { _oa01020 = value; }
            get { return _oa01020; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? OA01021
        {
            set { _oa01021 = value; }
            get { return _oa01021; }
        }
        /// <summary>
        /// 总金额US
        /// </summary>
        public decimal? OA01022
        {
            set { _oa01022 = value; }
            get { return _oa01022; }
        }
        /// <summary>
        /// 利润率
        /// </summary>
        public decimal? OA01023
        {
            set { _oa01023 = value; }
            get { return _oa01023; }
        }
        /// <summary>
        /// 新应用
        /// </summary>
        public string OA01024
        {
            set { _oa01024 = value; }
            get { return _oa01024; }
        }
        /// <summary>
        /// 应用代码
        /// </summary>
        public int? OA01025
        {
            set { _oa01025 = value; }
            get { return _oa01025; }
        }
        /// <summary>
        /// 付款条款
        /// </summary>
        public string OA01026
        {
            set { _oa01026 = value; }
            get { return _oa01026; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string OA01027
        {
            set { _oa01027 = value; }
            get { return _oa01027; }
        }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string OA01028
        {
            set { _oa01028 = value; }
            get { return _oa01028; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string OA01029
        {
            set { _oa01029 = value; }
            get { return _oa01029; }
        }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string OA01030
        {
            set { _oa01030 = value; }
            get { return _oa01030; }
        }
        /// <summary>
        /// 收货邮编
        /// </summary>
        public string OA01031
        {
            set { _oa01031 = value; }
            get { return _oa01031; }
        }
        /// <summary>
        /// 寄发票地址
        /// </summary>
        public string OA01032
        {
            set { _oa01032 = value; }
            get { return _oa01032; }
        }
        /// <summary>
        /// 收发票人
        /// </summary>
        public string OA01033
        {
            set { _oa01033 = value; }
            get { return _oa01033; }
        }
        /// <summary>
        /// 收发票人电话
        /// </summary>
        public string OA01034
        {
            set { _oa01034 = value; }
            get { return _oa01034; }
        }
        /// <summary>
        /// 收发票邮编
        /// </summary>
        public string OA01035
        {
            set { _oa01035 = value; }
            get { return _oa01035; }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string OA01036
        {
            set { _oa01036 = value; }
            get { return _oa01036; }
        }
        /// <summary>
        /// 下单用户ID
        /// </summary>
        public int? OA01037
        {
            set { _oa01037 = value; }
            get { return _oa01037; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? OA01038
        {
            set { _oa01038 = value; }
            get { return _oa01038; }
        }
        /// <summary>
        /// 主订单编号
        /// </summary>
        public string OA01039
        {
            set { _oa01039 = value; }
            get { return _oa01039; }
        }
        /// <summary>
        /// 行业分类
        /// </summary>
        public int? OA01040
        {
            set { _oa01040 = value; }
            get { return _oa01040; }
        }
        /// <summary>
        /// 行业代码
        /// </summary>
        public int? OA01041
        {
            set { _oa01041 = value; }
            get { return _oa01041; }
        }
        /// <summary>
        /// 240电子分类
        /// </summary>
        public int? OA01042
        {
            set { _oa01042 = value; }
            get { return _oa01042; }
        }
        /// <summary>
        /// 220汽车分类
        /// </summary>
        public int? OA01043
        {
            set { _oa01043 = value; }
            get { return _oa01043; }
        }
        /// <summary>
        /// 是否为新客户
        /// </summary>
        public int? OA01044
        {
            set { _oa01044 = value; }
            get { return _oa01044; }
        }
        /// <summary>
        /// Minifogger/是否
        /// </summary>
        public int? OA01045
        {
            set { _oa01045 = value; }
            get { return _oa01045; }
        }
        /// <summary>
        /// 最终用户
        /// </summary>
        public string OA01046
        {
            set { _oa01046 = value; }
            get { return _oa01046; }
        }
        /// <summary>
        /// 应用描述
        /// </summary>
        public string OA01047
        {
            set { _oa01047 = value; }
            get { return _oa01047; }
        }
        /// <summary>
        /// 是否应用分享
        /// </summary>
        public string OA01048
        {
            set { _oa01048 = value; }
            get { return _oa01048; }
        }
        /// <summary>
        /// 其他人1
        /// </summary>
        public string OA01049
        {
            set { _oa01049 = value; }
            get { return _oa01049; }
        }
        /// <summary>
        /// 其他人2
        /// </summary>
        public string OA01050
        {
            set { _oa01050 = value; }
            get { return _oa01050; }
        }
        /// <summary>
        /// 客户承诺付款日期
        /// </summary>
        public DateTime? OA01051
        {
            set { _oa01051 = value; }
            get { return _oa01051; }
        }
        /// <summary>
        /// 下单人ID
        /// </summary>
        public int? OA01052
        {
            set { _oa01052 = value; }
            get { return _oa01052; }
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string OA01053
        {
            set { _oa01053 = value; }
            get { return _oa01053; }
        }

        /// <summary>
        /// 是否为睡眠客户
        /// </summary>
        public int? OA01054
        {
            set { _oa01054 = value; }
            get { return _oa01054; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? OA01997
        {
            set { _oa01997 = value; }
            get { return _oa01997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? OA01998
        {
            set { _oa01998 = value; }
            get { return _oa01998; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string OA01999
        {
            set { _oa01999 = value; }
            get { return _oa01999; }
        }
        /// <summary>
        /// 分享省份1
        /// </summary>
        public string OA01055
        {
            set { _oa01055 = value; }
            get { return _oa01055; }
        } /// <summary>
          /// 分享省份2
          /// </summary>
        public string OA01056
        {
            set { _oa01056 = value; }
            get { return _oa01056; }
        }
        /// <summary>
        /// 分享省份1
        /// </summary>
        public string OA01057
        {
            set { _oa01057 = value; }
            get { return _oa01057; }
        } /// <summary>
          /// 分享省份2
          /// </summary>
        public string OA01058
        {
            set { _oa01058 = value; }
            get { return _oa01058; }
        }

        #endregion Model

    }
}
