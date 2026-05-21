using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using WatermarkApp.Models;

namespace WatermarkApp.Services
{
    /// <summary>
    /// 配置服务 - 负责加载和解析JSON配置文件
    /// </summary>
    public class ConfigService
    {
        private readonly string _configPath;

        public ConfigService(string configPath)
        {
            _configPath = configPath;
        }

        /// <summary>
        /// 加载应用配置
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
        /// 保存应用配置
        /// </summary>
        /// <param name="config">配置对象</param>
        public void SaveConfig(AppConfig config)
        {
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(_configPath, json);
        }

        /// <summary>
        /// 从十六进制颜色字符串转换为Color对象
        /// </summary>
        /// <param name="hexColor">十六进制颜色字符串（如 #D3D3D3）</param>
        /// <returns>Color对象</returns>
        public static Color ParseColor(string hexColor)
        {
            if (string.IsNullOrWhiteSpace(hexColor))
            {
                return Color.LightGray;
            }

            hexColor = hexColor.TrimStart('#');

            if (hexColor.Length == 6)
            {
                int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
                int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
                int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
                return Color.FromArgb(r, g, b);
            }

            return Color.LightGray;
        }
    }
}

