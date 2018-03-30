using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{

    /// <summary>
    /// 产品信息(ProductBase)
    /// </summary>
    [Serializable]
    public class ProductBase
    {
        public ProductBase()
        { }
        #region Model
        private int _pa01001;
        private string _pa01002;
        private string _pa01003;
        private string _pa01004;
        private string _pa01005;
        private int? _pa01997;
        private DateTime? _pa01998;
        private string _pa01999;
        private string _priceone;
        private string _pricetwo;
        private string _pricethree;
        private string _pricefour;
        private string _netprice;
        /// <summary>
        /// 成本价
        /// </summary>
        public string Netprice
        {
            get { return _netprice; }
            set { _netprice = value; }
        }
        /// <summary>
        /// 价格4
        /// </summary>
        public string Pricefour
        {
            get { return _pricefour; }
            set { _pricefour = value; }
        }
        /// <summary>
        /// 价格3
        /// </summary>
        public string Pricethree
        {
            get { return _pricethree; }
            set { _pricethree = value; }
        }
        /// <summary>
        /// 价格2
        /// </summary>
        public string Pricetwo
        {
            get { return _pricetwo; }
            set { _pricetwo = value; }
        }
        /// <summary>
        /// 价格1
        /// </summary>
        public string Priceone
        {
            get { return _priceone; }
            set { _priceone = value; }
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int PA01001
        {
            set { _pa01001 = value; }
            get { return _pa01001; }
        }
        /// <summary>
        /// 产品名字(预留)
        /// </summary>
        public string PA01002
        {
            set { _pa01002 = value; }
            get { return _pa01002; }
        }
        /// <summary>
        /// 产品编号(型号)
        /// </summary>
        public string PA01003
        {
            set { _pa01003 = value; }
            get { return _pa01003; }
        }
        /// <summary>
        /// 产品类别(预留)
        /// </summary>
        public string PA01004
        {
            set { _pa01004 = value; }
            get { return _pa01004; }
        }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string PA01005
        {
            set { _pa01005 = value; }
            get { return _pa01005; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? PA01997
        {
            set { _pa01997 = value; }
            get { return _pa01997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? PA01998
        {
            set { _pa01998 = value; }
            get { return _pa01998; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string PA01999
        {
            set { _pa01999 = value; }
            get { return _pa01999; }
        }
        #endregion Model
    }

}
