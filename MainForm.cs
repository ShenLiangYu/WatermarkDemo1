using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WatermarkApp.Controls;
using WatermarkApp.Models;
using WatermarkApp.Services;

namespace WatermarkApp;

/// <summary>
/// 主窗体 - 演示水印功能
/// </summary>
public partial class MainForm : WatermarkForm
{
    private readonly WatermarkPanel _contentPanel;
    private readonly Button _refreshButton;
    private readonly Button _toggleButton;
    private readonly Button _testButton;
    private readonly Label _infoLabel;
    private WatermarkConfig _config;
    public MainForm(WatermarkConfig config)
    {
        _config = config;
        // 初始化窗体
        Text = "水印演示程序";
        Size = new Size(1024, 768);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.White;

        // 设置水印渲染器
        Renderer = new WatermarkRenderer(config);

        // 创建内容面板
        _contentPanel = new WatermarkPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent,
            Renderer = new WatermarkRenderer(config)
        };

        // 创建信息标签
        _infoLabel = new Label
        {
            Text = $"当前水印配置:\n文字：{config.Text}\n角度：{config.Angle}°\n透明度：{config.Opacity:P1}\n字体：{config.FontFamily} {config.FontSize}px",
            AutoSize = false,
            Size = new Size(300, 150),
            Location = new Point(20, 20),
            BackColor = Color.FromArgb(200, 255, 255, 255),
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10),
            Font = new Font("Microsoft YaHei", 10f)
        };

        // 创建刷新按钮
        _refreshButton = new Button
        {
            Text = "重新加载配置",
            Location = new Point(20, 180),
            Size = new Size(120, 35),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            Font = new Font("Microsoft YaHei", 9f)
        };
        _refreshButton.Click += RefreshButton_Click;

        // 创建切换按钮
        _toggleButton = new Button
        {
            Text = "隐藏水印",
            Location = new Point(150, 180),
            Size = new Size(120, 35),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            Font = new Font("Microsoft YaHei", 9f)
        };
        _toggleButton.Click += ToggleButton_Click;


        // 创建切换按钮
        _testButton = new Button
        {
            Text = "测试子窗体",
            Location = new Point(320, 180),
            Size = new Size(120, 35),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            Font = new Font("Microsoft YaHei", 9f)
        };
        _testButton.Click += testButton_Click;

        // 添加控件到内容面板
        _contentPanel.Controls.Add(_infoLabel);
        _contentPanel.Controls.Add(_refreshButton);
        _contentPanel.Controls.Add(_toggleButton);
        _contentPanel.Controls.Add(_testButton);

        // 添加内容面板到窗体
        Controls.Add(_contentPanel);
    }

    private void RefreshButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "watermark.config.json");
            var configService = new ConfigService(configPath);
            var appConfig = configService.LoadConfig();
            _config = appConfig.Watermark;

            // 更新水印渲染器
            Renderer?.Dispose();
            Renderer = new WatermarkRenderer(_config);

            _contentPanel.Renderer?.Dispose();
            _contentPanel.Renderer = new WatermarkRenderer(_config);

            // 更新信息显示
            var config = appConfig.Watermark;
            _infoLabel.Text = $"当前水印配置:\n文字：{config.Text}\n角度：{config.Angle}°\n透明度：{config.Opacity:P1}\n字体：{config.FontFamily} {config.FontSize}px";

            Invalidate();
            MessageBox.Show("配置已重新加载！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"加载配置失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ToggleButton_Click(object? sender, EventArgs e)
    {
        WatermarkEnabled = !WatermarkEnabled;
        _contentPanel.WatermarkEnabled = WatermarkEnabled;
        _toggleButton.Text = WatermarkEnabled ? "隐藏水印" : "显示水印";
        Invalidate();
    }

    private void testButton_Click(object? sender, EventArgs e)
    {
        FormChild form = new FormChild(_config);
        form.ShowDialog();
    }
}