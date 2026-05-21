using System;
using System.Windows.Forms;
using WatermarkApp.Controls;
using WatermarkApp.Services;

namespace WatermarkApp.Forms
{
    /// <summary>
    /// 主窗体 - 演示水印功能
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly WatermarkService _watermarkService;
        private WatermarkPanel _mainPanel = null!;
        private WatermarkUserControl _demoUserControl = null!;
        private Label _titleLabel = null!;
        private Button _refreshButton = null!;
        private Panel _contentPanel = null!;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="watermarkService">水印服务</param>
        public MainForm(WatermarkService watermarkService)
        {
            _watermarkService = watermarkService;
            InitializeComponent();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            // 窗体基本设置
            Text = "水印演示程序";
            Size = new System.Drawing.Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);

            // 创建主面板（带水印）
            _mainPanel = new WatermarkPanel
            {
                Dock = DockStyle.Fill,
                WatermarkService = _watermarkService,
                ShowWatermark = true,
                Padding = new Padding(20)
            };

            // 创建标题标签
            _titleLabel = new Label
            {
                Text = "欢迎使用水印演示程序",
                Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20),
                BackColor = System.Drawing.Color.Transparent
            };

            // 创建左侧内容面板
            _contentPanel = new Panel
            {
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(500, 550),
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var contentLabel = new Label
            {
                Text = "左侧示例区域\r\n\r\n所有界面元素（Form 和 UserControl）都可以显示一致的自定义水印。\r\n水印内容、文字排列角度以及透明度等参数都可以在 JSON 配置文件中自定义。\r\n\r\n右侧展示了如何在自定义 UserControl 中集成水印功能。",
                Font = new System.Drawing.Font("Microsoft YaHei UI", 11F),
                Location = new System.Drawing.Point(30, 30),
                Size = new System.Drawing.Size(440, 250),
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = false
            };

            _contentPanel.Controls.Add(contentLabel);

            // 创建右侧演示用户控件
            _demoUserControl = new WatermarkUserControl
            {
                WatermarkService = _watermarkService,
                Location = new System.Drawing.Point(540, 80),
                Size = new System.Drawing.Size(400, 300)
            };

            // 创建刷新按钮
            _refreshButton = new Button
            {
                Text = "刷新水印",
                Location = new System.Drawing.Point(20, 640),
                Size = new System.Drawing.Size(120, 40),
                FlatStyle = FlatStyle.Standard
            };
            _refreshButton.Click += RefreshButton_Click;

            // 添加控件到主面板
            _mainPanel.Controls.Add(_titleLabel);
            _mainPanel.Controls.Add(_contentPanel);
            _mainPanel.Controls.Add(_demoUserControl);
            _mainPanel.Controls.Add(_refreshButton);

            // 添加主面板到窗体
            Controls.Add(_mainPanel);
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void RefreshButton_Click(object? sender, EventArgs e)
        {
            _watermarkService.ClearCache();
            _mainPanel.Invalidate();
            MessageBox.Show("水印已刷新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object? sender, EventArgs e)
        {
            // 可以在这里进行额外的初始化
        }
    }
}
