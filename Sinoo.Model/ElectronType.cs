using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 电子类别204(ElectronType)
    /// </summary>
    [Serializable]
    public partial class ElectronType
    {
        public ElectronType()
        { }
        #region Model
        private int? _ga05001;
        private string _ga05002;
        private string _ga05003;
        private string _ga05004 = "0";
        private int? _ga05997 = 0;
        private DateTime? _ga05998 = DateTime.Now;
        /// <summary>
        /// 电子分类ID
        /// </summary>
        public int? GA05001
        {
            set { _ga05001 = value; }
            get { return _ga05001; }
        }
        /// <summary>
        /// 电子分类ID
        /// </summary>
        public string GA05002
        {
            set { _ga05002 = value; }
            get { return _ga05002; }
        }
        /// <summary>
        /// 电子分类名字
        /// </summary>
        public string GA05003
        {
            set { _ga05003 = value; }
            get { return _ga05003; }
        }
        /// <summary>
        /// 父级分类
        /// </summary>
        public string GA05004
        {
            set { _ga05004 = value; }
            get { return _ga05004; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? GA05997
        {
            set { _ga05997 = value; }
            get { return _ga05997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? GA05998
        {
            set { _ga05998 = value; }
            get { return _ga05998; }
        }
        #endregion Model
    }
}
