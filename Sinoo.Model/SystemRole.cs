using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// 系统角色(SystemRole)
    /// </summary>
    [Serializable]
    public partial class SystemRole
    {
        public SystemRole()
        { }
        #region Model
        private int? _GA02001;
        private string _GA02002;
        private DateTime? _GA02998;
        private string _GA02003;
        private int? _GA02997;
        private string _menuid;
        private string _powerid;

        /// <summary>
        /// 权限Id
        /// </summary>
        public string PowerId
        {
            get { return _powerid; }
            set { _powerid = value; }
        }

        /// <summary>
        /// 菜单
        /// </summary>
        public string MenuId
        {
            get { return _menuid; }
            set { _menuid = value; }
        }

        /// <summary>
        /// 角色主键
        /// </summary>
        public int? GA02001
        {
            set { _GA02001 = value; }
            get { return _GA02001; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string GA02002
        {
            set { _GA02002 = value; }
            get { return _GA02002; }
        }
        /// <summary>
        /// 角色排序
        /// </summary>
        public DateTime? GA02998
        {
            set { _GA02998 = value; }
            get { return _GA02998; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string GA02003
        {
            set { _GA02003 = value; }
            get { return _GA02003; }
        }
        /// <summary>
        /// 是否删除。0未删除 1 已删除
        /// </summary>
        public int? GA02997
        {
            set { _GA02997 = value; }
            get { return _GA02997; }
        }
        #endregion Model

    }
}
