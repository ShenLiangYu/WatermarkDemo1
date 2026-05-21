using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WatermarkApp.Services;

namespace WatermarkApp.Controls
{
    /// <summary>
    /// 带水印的面板控件
    /// 可应用于任何需要水印背景的容器
    /// </summary>
    public class WatermarkPanel : Panel
    {
        private WatermarkService? _watermarkService;
        private bool _showWatermark = true;

        /// <summary>
        /// 水印服务
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WatermarkService? WatermarkService
        {
            get => _watermarkService;
            set
            {
                _watermarkService = value;
                if (value != null)
                {
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 是否显示水印
        /// </summary>
        [Category("Watermark")]
        [Description("是否显示水印")]
        [DefaultValue(true)]
        public bool ShowWatermark
        {
            get => _showWatermark;
            set
            {
                _showWatermark = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WatermarkPanel()
        {
            // 启用双缓冲，减少闪烁
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// 重写背景绘制方法
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            // 绘制水印
            if (_showWatermark && _watermarkService != null)
            {
                _watermarkService.DrawWatermark(e.Graphics, ClientRectangle.Width, ClientRectangle.Height);
            }
        }

        /// <summary>
        /// 当尺寸改变时清除缓存
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _watermarkService?.ClearCache();
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _watermarkService?.ClearCache();
            }
            base.Dispose(disposing);
        }
    }
}
