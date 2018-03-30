using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{


    /// <summary>
    /// 订单商品表(OB01)
    /// </summary>
    [Serializable]
    public partial class OrderProduct
    {
        public OrderProduct()
        { }
        #region Model
        private int? _ob01001;
        private string _ob01002;
        private int? _ob01003;
        private int? _ob01004;
        private string _ob01005;
        private string _ob01006;
        private int? _ob01007;
        private decimal? _ob01008;
        private decimal? _ob01009;
        private decimal? _ob01010;
        private decimal? _ob01011;
        private decimal? _ob01012;
        private decimal? _ob01013;
        private decimal? _ob01014;
        private DateTime? _ob01015;
        private DateTime? _ob01016;
        private DateTime? _ob01017;
        private DateTime? _ob01018;
        private string _ob01019;
        private int? _ob01997;
        private DateTime? _ob01998;
        private string _ob01999;
        /// <summary>
        /// 订单商品表ID
        /// </summary>
        public int? OB01001
        {
            set { _ob01001 = value; }
            get { return _ob01001; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OB01002
        {
            set { _ob01002 = value; }
            get { return _ob01002; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? OB01003
        {
            set { _ob01003 = value; }
            get { return _ob01003; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int? OB01004
        {
            set { _ob01004 = value; }
            get { return _ob01004; }
        }
        /// <summary>
        /// 商品编号(型号)
        /// </summary>
        public string OB01005
        {
            set { _ob01005 = value; }
            get { return _ob01005; }
        }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string OB01006
        {
            set { _ob01006 = value; }
            get { return _ob01006; }
        }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int? OB01007
        {
            set { _ob01007 = value; }
            get { return _ob01007; }
        }
        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal? OB01008
        {
            set { _ob01008 = value; }
            get { return _ob01008; }
        }
        /// <summary>
        /// 含税总价
        /// </summary>
        public decimal? OB01009
        {
            set { _ob01009 = value; }
            get { return _ob01009; }
        }
        /// <summary>
        /// 不含税单价
        /// </summary>
        public decimal? OB01010
        {
            set { _ob01010 = value; }
            get { return _ob01010; }
        }
        /// <summary>
        /// 不含税总价
        /// </summary>
        public decimal? OB01011
        {
            set { _ob01011 = value; }
            get { return _ob01011; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? OB01012
        {
            set { _ob01012 = value; }
            get { return _ob01012; }
        }
        /// <summary>
        /// 单位成本
        /// </summary>
        public decimal? OB01013
        {
            set { _ob01013 = value; }
            get { return _ob01013; }
        }
        /// <summary>
        /// 合计成本
        /// </summary>
        public decimal? OB01014
        {
            set { _ob01014 = value; }
            get { return _ob01014; }
        }
        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime? OB01015
        {
            set { _ob01015 = value; }
            get { return _ob01015; }
        }
        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime? OB01016
        {
            set { _ob01016 = value; }
            get { return _ob01016; }
        }
        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime? OB01017
        {
            set { _ob01017 = value; }
            get { return _ob01017; }
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime? OB01018
        {
            set { _ob01018 = value; }
            get { return _ob01018; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string OB01019
        {
            set { _ob01019 = value; }
            get { return _ob01019; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int? OB01997
        {
            set { _ob01997 = value; }
            get { return _ob01997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? OB01998
        {
            set { _ob01998 = value; }
            get { return _ob01998; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string OB01999
        {
            set { _ob01999 = value; }
            get { return _ob01999; }
        }
        #endregion Model

    }
}
