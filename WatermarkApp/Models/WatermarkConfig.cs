using System;
using System.Drawing;

namespace WatermarkApp.Models
{
    /// <summary>
    /// 水印配置模型类
    /// </summary>
    public class WatermarkConfig
    {
        /// <summary>
        /// 水印文本内容
        /// </summary>
        public string Text { get; set; } = "CONFIDENTIAL";

        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; } = 24f;

        /// <summary>
        /// 字体名称
        /// </summary>
        public string FontFamily { get; set; } = "Arial";

        /// <summary>
        /// 字体样式
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Bold;

        /// <summary>
        /// 文字颜色（十六进制格式，如 "#808080"）
        /// </summary>
        public string Color { get; set; } = "#808080";

        /// <summary>
        /// 透明度（0.0 - 1.0）
        /// </summary>
        public double Opacity { get; set; } = 0.3;

        /// <summary>
        /// 文字旋转角度（度）
        /// </summary>
        public float Angle { get; set; } = -45f;

        /// <summary>
        /// 水平间距（像素）
        /// </summary>
        public int HorizontalSpacing { get; set; } = 200;

        /// <summary>
        /// 垂直间距（像素）
        /// </summary>
        public int VerticalSpacing { get; set; } = 150;

        /// <summary>
        /// 获取转换后的 Color 对象
        /// </summary>
        /// <returns>System.Drawing.Color</returns>
        public Color GetColor()
        {
            return ColorTranslator.FromHtml(Color);
        }

        /// <summary>
        /// 获取带透明度的 Color 对象
        /// </summary>
        /// <returns>带透明度的 Color</returns>
        public Color GetTransparentColor()
        {
            var baseColor = GetColor();
            var alpha = (int)(Opacity * 255);
            return Color.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
        }

        /// <summary>
        /// 获取 Font 对象
        /// </summary>
        /// <returns>System.Drawing.Font</returns>
        public Font GetFont()
        {
            return new Font(FontFamily, FontSize, FontStyle, GraphicsUnit.Pixel);
        }
    }

    /// <summary>
    /// 配置文件根模型
    /// </summary>
    public class AppConfig
    {
        public WatermarkConfig Watermark { get; set; } = new();
    }
}
