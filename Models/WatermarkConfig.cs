using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatermarkApp.Models
{
    /// <summary>
    /// 水印配置模型
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
        public float FontSize { get; set; } = 48f;

        /// <summary>
        /// 字体家族
        /// </summary>
        public string FontFamily { get; set; } = "Microsoft YaHei";

        /// <summary>
        /// 文字颜色（十六进制格式）
        /// </summary>
        public string Color { get; set; } = "#D3D3D3";

        /// <summary>
        /// 文字旋转角度（度）
        /// </summary>
        public float Angle { get; set; } = -45f;

        /// <summary>
        /// 透明度（0.0 - 1.0）
        /// </summary>
        public float Opacity { get; set; } = 0.3f;

        /// <summary>
        /// 水平间距（像素）
        /// </summary>
        public int HorizontalSpacing { get; set; } = 200;

        /// <summary>
        /// 垂直间距（像素）
        /// </summary>
        public int VerticalSpacing { get; set; } = 150;
    }

    /// <summary>
    /// 根配置模型
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 水印配置
        /// </summary>
        public WatermarkConfig Watermark { get; set; } = new WatermarkConfig();
    }
}
