using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{

    /// <summary>
    /// 发票表(OC01)
    /// </summary>
    [Serializable]
    public partial class OrderInvoice
    {
        public OrderInvoice()
        { }
        #region Model
        private int? _oc01001;
        private int? _oc01002;
        private string _oc01003;
        private int? _oc01004;
        private string _oc01005;
        private int? _oc01006;
        private int? _oc01007;
        private string _oc01008;
        private DateTime? _oc01009;
        private int? _oc01010;
        private DateTime? _oc01011;
        private string _oc01012;
        private string _oc01013;
        private int? _oc01014;
        private DateTime? _oc01015;
        private DateTime? _oc01016;
        private string _oc01017;
        private string _oc01018;
        private string _oc01019;
        private string _oc01020;
        private int? _oc01997;
        private DateTime? _oc01998;
        private string _oc01999;
        /// <summary>
        /// 发票Id
        /// </summary>
        public int? OC01001
        {
            set { _oc01001 = value; }
            get { return _oc01001; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? OC01002
        {
            set { _oc01002 = value; }
            get { return _oc01002; }
        }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string OC01003
        {
            set { _oc01003 = value; }
            get { return _oc01003; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int? OC01004
        {
            set { _oc01004 = value; }
            get { return _oc01004; }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string OC01005
        {
            set { _oc01005 = value; }
            get { return _oc01005; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int? OC01006
        {
            set { _oc01006 = value; }
            get { return _oc01006; }
        }
        /// <summary>
        /// 是否到货
        /// </summary>
        public int? OC01007
        {
            set { _oc01007 = value; }
            get { return _oc01007; }
        }
        /// <summary>
        /// 到货批次
        /// </summary>
        public string OC01008
        {
            set { _oc01008 = value; }
            get { return _oc01008; }
        }
        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime? OC01009
        {
            set { _oc01009 = value; }
            get { return _oc01009; }
        }
        /// <summary>
        /// 是否发货
        /// </summary>
        public int? OC01010
        {
            set { _oc01010 = value; }
            get { return _oc01010; }
        }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? OC01011
        {
            set { _oc01011 = value; }
            get { return _oc01011; }
        }
        /// <summary>
        /// 快递公司
        /// </summary>
        public string OC01012
        {
            set { _oc01012 = value; }
            get { return _oc01012; }
        }
        /// <summary>
        /// 快递包裹号
        /// </summary>
        public string OC01013
        {
            set { _oc01013 = value; }
            get { return _oc01013; }
        }
        /// <summary>
        /// 是否开发票
        /// </summary>
        public int? OC01014
        {
            set { _oc01014 = value; }
            get { return _oc01014; }
        }
        /// <summary>
        /// 开发票时间
        /// </summary>
        public DateTime? OC01015
        {
            set { _oc01015 = value; }
            get { return _oc01015; }
        }
        /// <summary>
        /// 退发票时间
        /// </summary>
        public DateTime? OC01016
        {
            set { _oc01016 = value; }
            get { return _oc01016; }
        }
        /// <summary>
        /// 跟踪包裹
        /// </summary>
        public string OC01017
        {
            set { _oc01017 = value; }
            get { return _oc01017; }
        }
        /// <summary>
        /// 快递公司（ExpressCo）
        /// </summary>
        public string OC01018
        {
            set { _oc01018 = value; }
            get { return _oc01018; }
        }
        /// <summary>
        /// 单号备注
        /// </summary>
        public string OC01019
        {
            set { _oc01019 = value; }
            get { return _oc01019; }
        }
        /// <summary>
        /// 发货方式备注
        /// </summary>
        public string OC01020
        {
            set { _oc01020 = value; }
            get { return _oc01020; }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int? OC01997
        {
            set { _oc01997 = value; }
            get { return _oc01997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? OC01998
        {
            set { _oc01998 = value; }
            get { return _oc01998; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string OC01999
        {
            set { _oc01999 = value; }
            get { return _oc01999; }
        }

        #endregion Model

    }
}
