using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace Sinoo.Common
{
    /// <summary>
    /// 获取制定应用程序域下面的配置文件
    /// </summary>
    public sealed class ConfigHandler
    {
         /// <summary>
        /// 定义变量
        /// </summary>
        private readonly string _configName = "config";

        #region 单例模式

        /// <summary>
        /// 阻止线程对象
        /// </summary>
        private static object _obj = new object();
        /// <summary>
        /// 配置文件
        /// </summary>
        private static ConfigHandler _Handle;

        /// <summary>
        /// 构造函数
        /// </summary>
        private ConfigHandler()
        { }
        /// <summary>
        /// 实例化类库
        /// </summary>
        public static ConfigHandler GetInstance()
        {
            if (_Handle == null)
            {
                lock (_obj)
                {
                    _Handle = new ConfigHandler();
                }
            }
            return _Handle;
        }

        #endregion

        #region 操作方法

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="configName">配置文件名称</param>
        /// <returns>配置文件路径</returns>
        protected string GetPathForConfig(string configName)
        {
            string ConfigPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, configName);
            return ConfigPath;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        /// <param name="Key">键</param>
        /// <param name="configName">配置文件名称</param>
        /// <param name="IsApp">true=AppSettings节点，false=ConnectionStrings节点</param>
        /// <returns>返回值</returns>
        public string GetValueForConfig(string Key,string configName)
        {
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = GetPathForConfig(configName) }, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[Key] != null)
                return config.AppSettings.Settings[Key].Value;
            else
                return "";
        }

        #endregion
    }
}
