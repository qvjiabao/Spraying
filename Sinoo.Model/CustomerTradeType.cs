using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 客户行业分类(CustomerTradeType)
    /// </summary>
    [Serializable]
    public partial class CustomerTradeType
    {
        public CustomerTradeType()
        { }
        #region Model
        private int? _cb03001;
        private string _cb03002;
        private int? _cb03997;
        private DateTime? _cb03998;
        /// <summary>
        /// 行业分类ID
        /// </summary>
        public int? CB03001
        {
            set { _cb03001 = value; }
            get { return _cb03001; }
        }
        /// <summary>
        /// 行业分类名字
        /// </summary>
        public string CB03002
        {
            set { _cb03002 = value; }
            get { return _cb03002; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? CB03997
        {
            set { _cb03997 = value; }
            get { return _cb03997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CB03998
        {
            set { _cb03998 = value; }
            get { return _cb03998; }
        }
        #endregion Model
    }
}
