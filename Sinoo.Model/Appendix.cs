using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
	/// 文件附件(GA07)
	/// </summary>
    [Serializable]
    public partial class Appendix
    {
        public Appendix()
        { }
        #region Model
        private int? _ga07001;
        private string _ga07002;
        private string _ga07003;
        private int? _ga07004;
        private string _ga07005;
        private string _ga07006;
        private string _ga07007;
        private int _ga07008;
        private int? _ga07997;
        private DateTime? _ga07998;
        /// <summary>
        /// 文件ID
        /// </summary>
        public int? GA07001
        {
            set { _ga07001 = value; }
            get { return _ga07001; }
        }
        /// <summary>
        /// 对象编号
        /// </summary>
        public string GA07002
        {
            set { _ga07002 = value; }
            get { return _ga07002; }
        }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string GA07003
        {
            set { _ga07003 = value; }
            get { return _ga07003; }
        }
        /// <summary>
        /// 文件大小(KB)
        /// </summary>
        public int? GA07004
        {
            set { _ga07004 = value; }
            get { return _ga07004; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string GA07005
        {
            set { _ga07005 = value; }
            get { return _ga07005; }
        }
        /// <summary>
        /// 文件名字
        /// </summary>
        public string GA07006
        {
            set { _ga07006 = value; }
            get { return _ga07006; }
        }
        /// <summary>
        /// 文件内容
        /// </summary>
        public string GA07007
        {
            set { _ga07007 = value; }
            get { return _ga07007; }
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public int GA07008
        {
            set { _ga07008 = value; }
            get { return _ga07008; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? GA07997
        {
            set { _ga07997 = value; }
            get { return _ga07997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? GA07998
        {
            set { _ga07998 = value; }
            get { return _ga07998; }
        }
        #endregion Model
    }
}
