using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatermarkApp.Services;

namespace WatermarkApp.Controls
{
    /// <summary>
    /// 支持水印的Form基类
    /// </summary>
    public partial class WatermarkForm : Form
    {
        protected WatermarkRenderer? _renderer;
        protected bool _watermarkEnabled = true;

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
    }
}
