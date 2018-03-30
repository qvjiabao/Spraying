using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 系统区域表(SystemArea)
    /// </summary>
    [Serializable]
    public partial class SystemArea
    {
        public SystemArea()
        { }
        #region Model
        private string _ga03001;
        private string _ga03002;
        private string _ga03003;
        private string _ga03004;
        private int? _ga03997;
        private DateTime? _ga03998;
        /// <summary>
        /// 区域编码
        /// </summary>
        public string GA03001
        {
            set { _ga03001 = value; }
            get { return _ga03001; }
        }
        /// <summary>
        /// 区域名字
        /// </summary>
        public string GA03002
        {
            set { _ga03002 = value; }
            get { return _ga03002; }
        }
        /// <summary>
        /// 区域父级编码
        /// </summary>
        public string GA03003
        {
            set { _ga03003 = value; }
            get { return _ga03003; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string GA03004
        {
            set { _ga03004 = value; }
            get { return _ga03004; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? GA03997
        {
            set { _ga03997 = value; }
            get { return _ga03997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? GA03998
        {
            set { _ga03998 = value; }
            get { return _ga03998; }
        }
        #endregion Model
    }
}
