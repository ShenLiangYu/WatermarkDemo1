using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatermarkApp.Controls;
using WatermarkApp.Models;
using WatermarkApp.Services;

namespace WatermarkApp
{
    public partial class FormChild : WatermarkForm
    {
        public FormChild(WatermarkConfig config)
        {
            // 设置水印渲染器
            Renderer = new WatermarkRenderer(config);

            InitializeComponent();
        }
    }
}
