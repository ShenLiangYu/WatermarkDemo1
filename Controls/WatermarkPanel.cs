using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WatermarkApp.Services;

namespace WatermarkApp.Controls
{
    /// <summary>
    /// 支持水印的Panel控件
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(WatermarkPanel), "WatermarkPanel.bmp")]
    public partial class WatermarkPanel : Panel
    {
        private WatermarkRenderer? _renderer;
        private bool _watermarkEnabled = true;

        /// <summary>
        /// 获取或设置水印渲染器
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WatermarkRenderer? Renderer
        {
            get => _renderer;
            set
            {
                _renderer?.Dispose();
                _renderer = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置是否启用水印
        /// </summary>
        [Category("Watermark")]
        [Description("是否启用水印显示")]
        [DefaultValue(true)]
        public bool WatermarkEnabled
        {
            get => _watermarkEnabled;
            set
            {
                _watermarkEnabled = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (_watermarkEnabled && _renderer != null)
            {
                _renderer.Draw(e.Graphics, ClientRectangle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _renderer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

