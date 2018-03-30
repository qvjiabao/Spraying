using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 系统菜单(GA04)
    /// </summary>
    [Serializable]
    public partial class SystemMenu
    {
        public SystemMenu()
        { }
        #region Model
        private string _ga04001;
        private string _ga04002;
        private string _ga04003;
        private string _ga04004;
        private string _ga04005;
        private bool _ga04006 = true;
        private int? _ga04997 = 0;
        private DateTime? _ga04998 = DateTime.Now;
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string GA04001
        {
            set { _ga04001 = value; }
            get { return _ga04001; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string GA04002
        {
            set { _ga04002 = value; }
            get { return _ga04002; }
        }
        /// <summary>
        /// 菜单父编码
        /// </summary>
        public string GA04003
        {
            set { _ga04003 = value; }
            get { return _ga04003; }
        }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string GA04004
        {
            set { _ga04004 = value; }
            get { return _ga04004; }
        }
        /// <summary>
        /// 菜单图片
        /// </summary>
        public string GA04005
        {
            set { _ga04005 = value; }
            get { return _ga04005; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool GA04006
        {
            set { _ga04006 = value; }
            get { return _ga04006; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? GA04997
        {
            set { _ga04997 = value; }
            get { return _ga04997; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? GA04998
        {
            set { _ga04998 = value; }
            get { return _ga04998; }
        }
        #endregion Model

    }
}
