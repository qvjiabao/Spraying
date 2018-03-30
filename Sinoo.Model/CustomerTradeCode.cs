using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{

    /// <summary>
    /// 客户行业代码(CustomerTradeCode)
    /// </summary>
    public class CustomerTradeCode
    {
        public CustomerTradeCode()
        { }
        #region Model
        private int? _cb04001;
        private string _cb04002;
        private int? _cb04003;
        private string _cb04004;
        private string _cb04005;
        private int? _cb04997;
        private DateTime? _cb04998;
        /// <summary>
        /// 代码ID
        /// </summary>
        public int? CB04001
        {
            set { _cb04001 = value; }
            get { return _cb04001; }
        }
        /// <summary>
        /// 代码名字
        /// </summary>
        public string CB04002
        {
            set { _cb04002 = value; }
            get { return _cb04002; }
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CB04003
        {
            set { _cb04003 = value; }
            get { return _cb04003; }
        }
        /// <summary>
        /// 应用代码
        /// </summary>
        public string CB04004
        {
            set { _cb04004 = value; }
            get { return _cb04004; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string CB04005
        {
            set { _cb04005 = value; }
            get { return _cb04005; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? CB04997
        {
            set { _cb04997 = value; }
            get { return _cb04997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CB04998
        {
            set { _cb04998 = value; }
            get { return _cb04998; }
        }
        #endregion Model

    }
}
