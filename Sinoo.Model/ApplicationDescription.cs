using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Model
{
    /// <summary>
    /// ApplicationDescription:实体类(应用描述OD01)
    /// </summary>
    [Serializable]
    public partial class ApplicationDescription
    {
        public ApplicationDescription()
        { }
        #region Model
        private int? _od01001;
        private int? _od01002;
        private string _od01003;
        private int? _od01997;
        private DateTime? _od01998;
        /// <summary>
        /// 
        /// </summary>
        public int? OD01001
        {
            set { _od01001 = value; }
            get { return _od01001; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OD01002
        {
            set { _od01002 = value; }
            get { return _od01002; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OD01003
        {
            set { _od01003 = value; }
            get { return _od01003; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OD01997
        {
            set { _od01997 = value; }
            get { return _od01997; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OD01998
        {
            set { _od01998 = value; }
            get { return _od01998; }
        }
        #endregion Model
    }
}
