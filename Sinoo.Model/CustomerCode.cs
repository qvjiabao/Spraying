using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 客户代码(CustomerCode)
    /// </summary>
    [Serializable]
    public partial class CustomerCode
    {
        public CustomerCode()
        { }
        #region Model
        private int? _cb02001;
        private string _cb02002;
        private string _cb02003;
        private int? _cb02997;
        private DateTime? _cb02998;
        /// <summary>
        /// 客户代码ID
        /// </summary>
        public int? CB02001
        {
            set { _cb02001 = value; }
            get { return _cb02001; }
        }
        /// <summary>
        /// 客户代码名字
        /// </summary>
        public string CB02002
        {
            set { _cb02002 = value; }
            get { return _cb02002; }
        }
        /// <summary>
        /// 客户代码名字
        /// </summary>
        public string CB02003
        {
            set { _cb02003 = value; }
            get { return _cb02003; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? CB02997
        {
            set { _cb02997 = value; }
            get { return _cb02997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CB02998
        {
            set { _cb02998 = value; }
            get { return _cb02998; }
        }
        #endregion Model
    }
}
