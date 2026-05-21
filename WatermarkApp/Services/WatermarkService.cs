using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WatermarkApp.Models;

namespace WatermarkApp.Services
{
    /// <summary>
    /// 水印渲染服务类
    /// </summary>
    public class WatermarkService
    {
        private readonly WatermarkConfig _config;
        private Bitmap? _cachedWatermark;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">水印配置</param>
        public WatermarkService(WatermarkConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// 获取水印配置
        /// </summary>
        public WatermarkConfig Config => _config;

        /// <summary>
        /// 生成水印图片（带缓存）
        /// </summary>
        /// <param name="controlWidth">控件宽度</param>
        /// <param name="controlHeight">控件高度</param>
        /// <returns>水印位图</returns>
        public Bitmap GetWatermark(int controlWidth, int controlHeight)
        {
            // 如果缓存存在且尺寸匹配，直接返回缓存
            if (_cachedWatermark != null)
            {
                return _cachedWatermark;
            }

            // 创建位图
            var bitmap = new Bitmap(controlWidth, controlHeight);
            
            using (var g = Graphics.FromImage(bitmap))
            {
                // 设置高质量渲染
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 清空背景（透明）
                g.Clear(Color.Transparent);

                // 使用配置的透明度颜色
                var color = _config.GetTransparentColor();
                
                using (var font = _config.GetFont())
                {
                    // 测量单个文本的大小
                    var textSize = g.MeasureString(_config.Text, font);
                    var textWidth = textSize.Width;
                    var textHeight = textSize.Height;

                    // 计算对角线长度，确保旋转后能覆盖整个区域
                    var diagonal = Math.Sqrt(Math.Pow(controlWidth, 2) + Math.Pow(controlHeight, 2));
                    
                    // 计算需要的文本数量
                    var horizontalCount = (int)(diagonal / _config.HorizontalSpacing) + 2;
                    var verticalCount = (int)(diagonal / _config.VerticalSpacing) + 2;

                    // 移动到中心进行旋转
                    g.TranslateTransform(controlWidth / 2f, controlHeight / 2f);
                    g.RotateTransform(_config.Angle);
                    g.TranslateTransform(-controlWidth / 2f, -controlHeight / 2f);

                    // 计算起始位置（确保覆盖整个区域）
                    var startX = -(horizontalCount * _config.HorizontalSpacing) / 2f + controlWidth / 2f;
                    var startY = -(verticalCount * _config.VerticalSpacing) / 2f + controlHeight / 2f;

                    // 绘制多行多列的水印
                    for (var i = 0; i < horizontalCount; i++)
                    {
                        for (var j = 0; j < verticalCount; j++)
                        {
                            var x = startX + i * _config.HorizontalSpacing;
                            var y = startY + j * _config.VerticalSpacing;

                            // 交错排列（可选，让水印更均匀）
                            if (i % 2 == 1)
                            {
                                y += _config.VerticalSpacing / 2f;
                            }

                            g.DrawString(_config.Text, font, new SolidBrush(color), x, y);
                        }
                    }
                }
            }

            _cachedWatermark = bitmap;
            return bitmap;
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            _cachedWatermark?.Dispose();
            _cachedWatermark = null;
        }

        /// <summary>
        /// 在指定 Graphics 上绘制水印
        /// </summary>
        /// <param name="g">Graphics 对象</param>
        /// <param name="width">绘制区域宽度</param>
        /// <param name="height">绘制区域高度</param>
        public void DrawWatermark(Graphics g, int width, int height)
        {
            var watermark = GetWatermark(width, height);
            g.DrawImageUnscaled(watermark, 0, 0);
        }
    }
}
