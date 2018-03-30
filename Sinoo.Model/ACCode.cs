using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 应用代码(OB02)
    /// </summary>
    [Serializable]
    public partial class ACCode
    {
        public ACCode()
        { }
        #region Model
        private int? _ob02001;
        private string _ob02002;
        private DateTime? _ob02003 = DateTime.Now;
        /// <summary>
        /// ID
        /// </summary>
        public int? OB02001
        {
            set { _ob02001 = value; }
            get { return _ob02001; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string OB02002
        {
            set { _ob02002 = value; }
            get { return _ob02002; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? OB02003
        {
            set { _ob02003 = value; }
            get { return _ob02003; }
        }
        #endregion Model
    }
}
