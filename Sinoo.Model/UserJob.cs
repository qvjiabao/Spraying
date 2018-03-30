using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 职位表(UB01)
    /// </summary>
    [Serializable]
    public partial class UserJob
    {
        public UserJob()
        { }
        #region Model
        private int? _ub01001;
        private string _ub01002;
        /// <summary>
        /// 职位ID
        /// </summary>
        public int? UB01001
        {
            set { _ub01001 = value; }
            get { return _ub01001; }
        }
        /// <summary>
        /// 职位名字
        /// </summary>
        public string UB01002
        {
            set { _ub01002 = value; }
            get { return _ub01002; }
        }
        #endregion Model
    }
}
