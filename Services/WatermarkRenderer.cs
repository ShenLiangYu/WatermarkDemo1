using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WatermarkApp.Models;

namespace WatermarkApp.Services
{
    /// <summary>
    /// 水印渲染服务 - 负责生成和绘制水印
    /// </summary>
    public class WatermarkRenderer
    {
        private readonly WatermarkConfig _config;
        private readonly Font _font;
        private readonly Color _color;
        private readonly float _opacity;

        public WatermarkRenderer(WatermarkConfig config)
        {
            _config = config;
            _font = new Font(config.FontFamily, config.FontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            _color = ConfigService.ParseColor(config.Color);
            _opacity = Math.Max(0f, Math.Min(1f, config.Opacity));
        }

        /// <summary>
        /// 在指定控件上绘制水印
        /// </summary>
        /// <param name="graphics">Graphics对象</param>
        /// <param name="clientRectangle">客户端区域</param>
        public void Draw(Graphics graphics, Rectangle clientRectangle)
        {
            // 保存原始状态
            var originalState = graphics.Save();

            try
            {
                // 设置高质量渲染
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                int horizontalSpacing = _config.HorizontalSpacing;
                int verticalSpacing = _config.VerticalSpacing;

                // 计算需要绘制的行列数（多绘制一些以确保覆盖旋转后的区域）
                int cols = (clientRectangle.Width / horizontalSpacing) + 4;
                int rows = (clientRectangle.Height / verticalSpacing) + 4;

                // 创建带透明度的画刷
                using var brush = new SolidBrush(Color.FromArgb((int)(_opacity * 255), _color));

                // 遍历所有位置绘制水印
                for (int row = -2; row < rows; row++)
                {
                    for (int col = -2; col < cols; col++)
                    {
                        float x = col * horizontalSpacing;
                        float y = row * verticalSpacing;

                        // 保存当前变换状态
                        var originalTransform = graphics.Transform;

                        try
                        {
                            // 创建旋转变换：先平移到水印中心，旋转，再平移回来
                            graphics.TranslateTransform(x + horizontalSpacing / 2f, y + verticalSpacing / 2f);
                            graphics.RotateTransform(_config.Angle);
                            graphics.TranslateTransform(-horizontalSpacing / 2f, -verticalSpacing / 2f);

                            // 绘制水印文本
                            graphics.DrawString(_config.Text, _font, brush, x, y);
                        }
                        finally
                        {
                            // 恢复变换状态
                            graphics.Transform = originalTransform;
                        }
                    }
                }
            }
            finally
            {
                // 恢复原始状态
                graphics.Restore(originalState);
            }
        }

        /// <summary>
        /// 测量单个水印的尺寸
        /// </summary>
        /// <param name="graphics">Graphics对象</param>
        /// <returns>水印尺寸</returns>
        private SizeF MeasureWatermarkSize(Graphics graphics)
        {
            return graphics.MeasureString(_config.Text, _font);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _font.Dispose();
        }
    }
}

