using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 脱琉（FGDCode）
    /// </summary>
    [Serializable]
    public partial class FGDCode
    {
        public FGDCode()
        { }
        #region Model
        private int? _cd01001;
        private string _cd01002;
        private string _cd01003;
        private DateTime? _cd01998;
        /// <summary>
        /// ID
        /// </summary>
        public int? CD01001
        {
            set { _cd01001 = value; }
            get { return _cd01001; }
        }
        /// <summary>
        /// Value值
        /// </summary>
        public string CD01002
        {
            set { _cd01002 = value; }
            get { return _cd01002; }
        }
        /// <summary>
        /// Text值
        /// </summary>
        public string CD01003
        {
            set { _cd01003 = value; }
            get { return _cd01003; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CD01998
        {
            set { _cd01998 = value; }
            get { return _cd01998; }
        }
        #endregion Model
    }
}
