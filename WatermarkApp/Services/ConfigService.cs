using System;
using System.IO;
using Newtonsoft.Json;
using WatermarkApp.Models;

namespace WatermarkApp.Services
{
    /// <summary>
    /// 配置文件服务类
    /// </summary>
    public class ConfigService
    {
        private readonly string _configPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        public ConfigService(string configPath)
        {
            _configPath = configPath;
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns>应用配置对象</returns>
        public AppConfig LoadConfig()
        {
            if (!File.Exists(_configPath))
            {
                // 如果配置文件不存在，创建默认配置
                var defaultConfig = new AppConfig();
                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            var json = File.ReadAllText(_configPath);
            return JsonConvert.DeserializeObject<AppConfig>(json) ?? new AppConfig();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config">配置对象</param>
        public void SaveConfig(AppConfig config)
        {
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(_configPath, json);
        }
    }
}
