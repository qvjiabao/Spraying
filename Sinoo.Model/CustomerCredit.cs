using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 信誉客户(CustomerCredit)
    /// </summary>
    [Serializable]
    public partial class CustomerCredit
    {
        public CustomerCredit()
        { }
        #region Model
        private int? _cd02001;
        private string _cd02002;
        private DateTime? _cd02998=DateTime.MinValue;
        /// <summary>
        /// ID
        /// </summary>
        public int? CD02001
        {
            set { _cd02001 = value; }
            get { return _cd02001; }
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string CD02002
        {
            set { _cd02002 = value; }
            get { return _cd02002; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CD02998
        {
            set { _cd02998 = value; }
            get { return _cd02998; }
        }
        #endregion Model
    }
}
