using System;
using System.Drawing;
using System.Windows.Forms;
using WatermarkApp.Controls;
using WatermarkApp.Services;

namespace WatermarkApp.Controls
{
    /// <summary>
    /// 带水印的用户控件示例
    /// 演示如何在自定义 UserControl 中集成水印功能
    /// </summary>
    public class WatermarkUserControl : UserControl
    {
        private WatermarkService? _watermarkService;
        private Label _titleLabel = null!;
        private TextBox _inputTextBox = null!;
        private Button _actionButton = null!;

        /// <summary>
        /// 水印服务
        /// </summary>
        public WatermarkService? WatermarkService
        {
            get => _watermarkService;
            set
            {
                _watermarkService = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WatermarkUserControl()
        {
            // 启用双缓冲
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);

            InitializeComponent();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            Size = new Size(400, 300);

            // 标题标签
            _titleLabel = new Label
            {
                Text = "用户信息录入",
                Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15),
                BackColor = Color.Transparent
            };

            // 输入框标签
            var nameLabel = new Label
            {
                Text = "姓名：",
                Font = new Font("Microsoft YaHei UI", 10F),
                AutoSize = true,
                Location = new Point(20, 60),
                BackColor = Color.Transparent
            };

            // 输入文本框
            _inputTextBox = new TextBox
            {
                Location = new Point(80, 57),
                Size = new Size(280, 25),
                Font = new Font("Microsoft YaHei UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };

            // 操作按钮
            _actionButton = new Button
            {
                Text = "提交",
                Location = new Point(20, 110),
                Size = new Size(100, 35),
                Font = new Font("Microsoft YaHei UI", 10F),
                FlatStyle = FlatStyle.Standard,
                BackColor = Color.FromArgb(0, 120, 215)
            };
            _actionButton.ForeColor = Color.White;
            _actionButton.Click += ActionButton_Click;

            // 信息标签
            var infoLabel = new Label
            {
                Text = "此控件演示了如何在自定义 UserControl 中\r\n使用统一的水印背景。所有控件都保持\r\n透明背景以显示水印。",
                Font = new Font("Microsoft YaHei UI", 9F),
                Location = new Point(20, 170),
                Size = new Size(350, 100),
                BackColor = Color.Transparent,
                AutoSize = false
            };

            // 添加控件
            Controls.Add(_titleLabel);
            Controls.Add(nameLabel);
            Controls.Add(_inputTextBox);
            Controls.Add(_actionButton);
            Controls.Add(infoLabel);
        }

        /// <summary>
        /// 重写背景绘制以显示水印
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (_watermarkService != null)
            {
                _watermarkService.DrawWatermark(e.Graphics, ClientRectangle.Width, ClientRectangle.Height);
            }
        }

        /// <summary>
        /// 尺寸改变时清除缓存
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _watermarkService?.ClearCache();
        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        private void ActionButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_inputTextBox.Text))
            {
                MessageBox.Show("请输入姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"您好，{_inputTextBox.Text}！\r\n数据已成功提交。", "成功", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
