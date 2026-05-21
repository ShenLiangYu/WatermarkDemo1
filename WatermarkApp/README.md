# 水印演示程序 (WatermarkApp)

## 项目简介
这是一个基于 .NET Framework 4.8 的 WinForm 应用程序，使用 C# 12.0 语言特性开发。程序实现了在所有界面（Form 和 UserControl）背景上显示一致的自定义水印功能。

## 功能特点

### 核心功能
- ✅ **统一水印背景**：所有界面元素都能显示一致的水印
- ✅ **JSON 配置**：水印参数完全可在 JSON 配置文件中自定义
- ✅ **高性能渲染**：使用缓存机制优化水印绘制性能
- ✅ **可复用控件**：提供 `WatermarkPanel` 控件，可轻松应用于任何容器

### 可配置参数
| 参数 | 说明 | 默认值 |
|------|------|--------|
| Text | 水印文本内容 | "CONFIDENTIAL" |
| FontSize | 字体大小 | 24 |
| FontFamily | 字体名称 | "Arial" |
| FontStyle | 字体样式 | Bold |
| Color | 文字颜色（十六进制） | "#808080" |
| Opacity | 透明度 (0.0-1.0) | 0.3 |
| Angle | 旋转角度（度） | -45 |
| HorizontalSpacing | 水平间距（像素） | 200 |
| VerticalSpacing | 垂直间距（像素） | 150 |

## 项目结构

```
WatermarkApp/
├── Controls/
│   └── WatermarkPanel.cs      # 带水印的面板控件
├── Forms/
│   ├── MainForm.cs            # 主窗体
│   └── MainForm.Designer.cs   # 主窗体设计器代码
├── Models/
│   └── WatermarkConfig.cs     # 配置模型类
├── Services/
│   ├── ConfigService.cs       # 配置加载/保存服务
│   └── WatermarkService.cs    # 水印渲染服务
├── Properties/
│   └── AssemblyInfo.cs        # 程序集信息
├── Program.cs                 # 程序入口
├── watermark.config.json      # 水印配置文件
├── app.config                 # 应用配置
├── packages.config            # NuGet 包引用
└── WatermarkApp.csproj        # 项目文件
```

## 技术栈

- **框架**: .NET Framework 4.8
- **语言**: C# 12.0
- **UI**: Windows Forms
- **JSON 处理**: Newtonsoft.Json 13.0.3
- **开发工具**: Visual Studio 2022/2026

## 快速开始

### 前置要求
1. Visual Studio 2022 或更高版本（推荐 VS 2026）
2. .NET Framework 4.8 SDK
3. NuGet 包管理器

### 安装步骤

1. **打开项目**
   ```
   在 Visual Studio 中打开 WatermarkApp.csproj
   ```

2. **还原 NuGet 包**
   ```
   右键项目 → 管理 NuGet 程序包 → 还原
   或使用命令行:
   nuget restore WatermarkApp.sln
   ```

3. **编译运行**
   ```
   按 F5 或点击"启动"按钮
   ```

### 自定义水印

编辑 `watermark.config.json` 文件：

```json
{
  "Watermark": {
    "Text": "公司内部资料",
    "FontSize": 28,
    "FontFamily": "微软雅黑",
    "FontStyle": "Bold",
    "Color": "#FF0000",
    "Opacity": 0.25,
    "Angle": -30,
    "HorizontalSpacing": 250,
    "VerticalSpacing": 180
  }
}
```

## 代码亮点

### 1. C# 12.0 特性应用
- 主构造函数（Primary Constructors）
- 集合表达式
- 模式匹配增强
- 可空引用类型

### 2. 架构设计
- **服务层分离**：配置服务和渲染服务独立
- **依赖注入**：通过构造函数传递服务
- **缓存机制**：水印位图缓存提升性能
- **资源管理**：正确实现 IDisposable 模式

### 3. 可扩展性
- `WatermarkPanel` 可轻松替换为标准 Panel
- 支持动态更新配置
- 可添加更多水印样式（图片水印等）

## 使用说明

### 在现有项目中集成

1. **复制核心文件**
   - `Controls/WatermarkPanel.cs`
   - `Services/WatermarkService.cs`
   - `Services/ConfigService.cs`
   - `Models/WatermarkConfig.cs`

2. **添加 NuGet 包**
   ```xml
   <package id="Newtonsoft.Json" version="13.0.3" targetFramework="net48" />
   ```

3. **使用示例**
   ```csharp
   // 加载配置
   var configService = new ConfigService("watermark.config.json");
   var appConfig = configService.LoadConfig();
   
   // 创建水印服务
   var watermarkService = new WatermarkService(appConfig.Watermark);
   
   // 创建带水印的面板
   var panel = new WatermarkPanel
   {
       WatermarkService = watermarkService,
       Dock = DockStyle.Fill
   };
   
   // 添加到窗体
   this.Controls.Add(panel);
   ```

## 许可证

MIT License

## 常见问题

**Q: 水印显示模糊？**
A: 确保启用了双缓冲（WatermarkPanel 已默认启用），并检查 DPI 设置。

**Q: 如何动态更新水印？**
A: 修改配置后调用 `watermarkService.ClearCache()` 然后 `panel.Invalidate()`。

**Q: 支持图片水印吗？**
A: 当前版本仅支持文字水印，可通过扩展 WatermarkService 添加图片支持。
