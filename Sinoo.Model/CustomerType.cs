using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 客户类型（CustomerType）
    /// </summary>
    [Serializable]
    public partial class CustomerType
    {
        public CustomerType()
        { }
        #region Model
        private int? _cb01001;
        private string _cb01002;
        private int? _cb01997;
        private DateTime? _cb01998;
        /// <summary>
        /// 客户类型ID
        /// </summary>
        public int? CB01001
        {
            set { _cb01001 = value; }
            get { return _cb01001; }
        }
        /// <summary>
        /// 客户类型名字
        /// </summary>
        public string CB01002
        {
            set { _cb01002 = value; }
            get { return _cb01002; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? CB01997
        {
            set { _cb01997 = value; }
            get { return _cb01997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CB01998
        {
            set { _cb01998 = value; }
            get { return _cb01998; }
        }
        #endregion Model
    }
}
