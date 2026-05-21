using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatermarkApp.Services;

namespace WatermarkApp
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 配置应用
            //ApplicationConfiguration.Initialize();

            // 加载配置
            var configPath = Path.Combine(AppContext.BaseDirectory, "watermark.config.json");
            var configService = new ConfigService(configPath);
            var appConfig = configService.LoadConfig();

            // 创建主窗体并设置水印
            using var mainForm = new MainForm(appConfig.Watermark);
            Application.Run(mainForm);
        }
    }
}
