using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 汽车分类220(AuteType)
    /// </summary>
    [Serializable]
    public partial class AuteType
    {
        public AuteType()
        { }
        #region Model
        private int? _ga06001;
        private string _ga06002;
        private string _ga06003;
        private string _ga06004;
        private int? _ga06997;
        private DateTime? _ga06998;
        /// <summary>
        /// 汽车分类ID
        /// </summary>
        public int? GA06001
        {
            set { _ga06001 = value; }
            get { return _ga06001; }
        }
        /// <summary>
        /// 汽车分类ID
        /// </summary>
        public string GA06002
        {
            set { _ga06002 = value; }
            get { return _ga06002; }
        }
        /// <summary>
        /// 汽车分类名字
        /// </summary>
        public string GA06003
        {
            set { _ga06003 = value; }
            get { return _ga06003; }
        }
        /// <summary>
        /// 父类父级
        /// </summary>
        public string GA06004
        {
            set { _ga06004 = value; }
            get { return _ga06004; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? GA06997
        {
            set { _ga06997 = value; }
            get { return _ga06997; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? GA06998
        {
            set { _ga06998 = value; }
            get { return _ga06998; }
        }
        #endregion Model
    }
}
