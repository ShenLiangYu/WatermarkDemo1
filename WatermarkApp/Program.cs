using System;
using System.IO;
using System.Windows.Forms;
using WatermarkApp.Forms;
using WatermarkApp.Services;

namespace WatermarkApp
{
    /// <summary>
    /// 应用程序入口类
    /// </summary>
    internal static class Program
    {
        private static WatermarkService? _watermarkService;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 确定配置文件路径（与可执行文件同目录）
                var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "watermark.config.json");

                // 加载配置
                var configService = new ConfigService(configPath);
                var appConfig = configService.LoadConfig();

                // 创建水印服务
                _watermarkService = new WatermarkService(appConfig.Watermark);

                // 创建并运行主窗体
                var mainForm = new MainForm(_watermarkService);
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"应用程序启动失败：{ex.Message}\n\n详细信息：{ex.StackTrace}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
