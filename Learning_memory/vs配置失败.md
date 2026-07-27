# VS Code 配置 Unity 项目失败总结

## 问题背景

用 **Unity Hub** 创建了 RPG 项目在 `D:/unityProject/RPG/`，后来想把项目上传到 GitHub，就**在同一个位置重新创建了仓库**，导致目录结构变成：

```
D:/unityProject/RPG/      ← 原来是 Unity 项目根目录
  RPG/                    ← 把 Assets 等资源移到这下面（变成了新的 Unity 项目根目录）
    .git/
    Assets/
    ProjectSettings/
    Packages/
```

原本的 `.sln` 和 `.csproj` 文件（由 Unity 自动生成，供 IDE 识别项目结构）在迁移过程中**丢失了**。

结果：在 VS Code 里写 C# 脚本时，**语法错误不显示、没有智能提示、没有代码补全**。

---

## 问题诊断

| 检查项 | 结果 |
|--------|------|
| VS Code 扩展 | C# Dev Kit、C#、Unity 扩展均已安装 ✅ |
| .NET SDK | 9.0 已安装 ✅ |
| Unity 编辑器 | 2022.3.62f2c1 安装在 `E:\unity_download\Editor\Unity.exe` ✅ |
| Unity Hub 项目路径 | 已正确指向 `D:\unityProject\RPG\RPG` ✅ |
| `.sln` / `.csproj` 文件 | **不存在** ❌（核心原因） |
| 项目 `.gitignore` | 正确忽略了 `.sln` / `.csproj` ✅（是 Unity 自动生成的，本就不该进版本控制） |
| Unity 外部编辑器设置 | 未配置 VS Code ❌ |

### 根本原因

Unity 的 `.sln` / `.csproj` 文件**不是手动创建的**，而是 Unity 编辑器在打开项目时自动生成的。它们被 `.gitignore` 排除在版本控制之外（第 46-48 行），所以 git clone / 迁移后根本不会有这两个文件。

**缺少 `.sln` / `.csproj` → C# Dev Kit 语言服务器无法加载项目 → 没有语法分析和智能提示。**

---

## 解决方案

### 1. 添加 Unity VS Code 集成包

在 `Packages/manifest.json` 的 `dependencies` 中添加：

```json
"com.unity.ide.vscode": "1.2.5",
```

这个包让 Unity 知道如何正确生成供 VS Code 使用的项目文件。

**相关文件：** `Packages/manifest.json`

### 2. 在 Unity 中配置 VS Code 为外部编辑器

修改 `ProjectSettings/EditorSettings.asset`，添加字段：

```yaml
m_ExternalScriptEditor: C:\Users\Lenovo\AppData\Local\Programs\Microsoft VS Code\Code.exe
m_ExternalScriptEditorArgs:
m_TrackingEnabled: 1
```

或者手动在 Unity 中设置：**Edit → Preferences → External Tools → External Script Editor** 选择 VS Code。

### 3. 配置 VS Code 工作区

创建 `.vscode/settings.json`：

```json
{
    "dotnet.preferCSharpExtension": true,
    "files.exclude": {
        "**/[Ll]ibrary/": true,
        "**/[Tt]emp/": true,
        "**/[Oo]bj/": true,
        "**/[Bb]uild/": true,
        "**/[Ll]ogs/": true
    },
    "files.watcherExclude": {
        "**/[Ll]ibrary/**": true,
        "**/[Tt]emp/**": true,
        "**/[Oo]bj/**": true
    },
    "search.exclude": {
        "**/[Ll]ibrary/": true,
        "**/[Tt]emp/": true,
        "**/[Oo]bj/": true
    },
    "editor.formatOnSave": true,
    "[csharp]": {
        "editor.defaultFormatter": "ms-dotnettools.csharp",
        "editor.tabSize": 4
    }
}
```

创建 `.vscode/extensions.json`：

```json
{
    "recommendations": [
        "ms-dotnettools.csdevkit",
        "ms-dotnettools.csharp",
        "visualstudiotoolsforunity.vstuc"
    ]
}
```

---

## 恢复步骤（下次配置时照做）

### 第 1 步：确认项目路径
Unity Hub 中项目的路径必须是**包含 `Assets/` 文件夹的那一层**，即：
```
D:/unityProject/RPG/RPG/     ← 正确
D:/unityProject/RPG/         ← 错误（缺少 Assets）
```

> 项目文件夹和 git 仓库要在**同一层**。

### 第 2 步：添加 VS Code 包
编辑 `Packages/manifest.json`，在 `dependencies` 中加入：
```json
"com.unity.ide.vscode": "1.2.5",
```

### 第 3 步：设置外部编辑器（2选1）
**方式 A（推荐）：** 修改 `ProjectSettings/EditorSettings.asset`，添加：
```yaml
m_ExternalScriptEditor: C:\<你的路径>\Microsoft VS Code\Code.exe
```

**方式 B：** 在 Unity 里手动设置：
`Edit → Preferences → External Tools → External Script Editor → 选择 VS Code`

### 第 4 步：创建 VS Code 配置文件
新建 `.vscode/settings.json` 和 `.vscode/extensions.json`（内容见上方）。

### 第 5 步：打开 Unity 让它生成项目文件
在 Unity Hub 中打开项目 → Unity 会自动：
1. 下载 `com.unity.ide.vscode` 包
2. 重新生成 `RPG.sln` 和 `RPG.csproj`
3. 生成 `*.csproj` 到每个脚本文件夹

### 第 6 步：用 VS Code 打开正确目录
VS Code 里选择 **File → Open Folder** → 打开 `D:/unityProject/RPG/RPG/`（内部含 `.git` 的那个）

等待 C# Dev Kit 加载完成（右下角状态栏会显示 "Loading..." 约 10-30 秒），语法错误标红和智能提示就会恢复。

---

## 关键教训

| # | 教训 |
|---|------|
| 1 | **`.sln` / `.csproj` 是 Unity 自动生成的**，不需要手动创建，也不要放进 git |
| 2 | 克隆/迁移 Unity 项目后，**必须先用 Unity 编辑器打开一次**，才会生成 IDE 所需的项目文件 |
| 3 | `.gitignore` 里的 `*.sln` `*.csproj` 是**正常的标准配置**，不要删除它们 |
| 4 | VS Code 必须在**包含 `.git` 的那一层打开**（即 Unity 项目根目录），不是外层父文件夹 |
| 5 | 已经安装了 `visualstudiotoolsforunity.vstuc` 扩展就够用了，旧的 `com.unity.ide.vscode` 包也建议加上确保兼容 |
| 6 | C# Dev Kit 需要 `.sln` 文件才能提供语法分析 → **没有 `.sln` = 没有智能提示** |

---

## 所需软件清单

| 软件 | 用途 | 安装确认 |
|------|------|----------|
| Unity 2022.3.62f2c1 | 游戏引擎 | `E:\unity_download\Editor\Unity.exe` |
| VS Code | 代码编辑器 | `%LOCALAPPDATA%\Programs\Microsoft VS Code\Code.exe` |
| .NET SDK 9.0 | C# 编译/语言服务器 | `dotnet --version` → 9.0.312 |
| **C# Dev Kit** (VS Code 扩展) | 语法分析、智能提示 | `ms-dotnettools.csdevkit` |
| **C#** (VS Code 扩展) | C# 语言支持 | `ms-dotnettools.csharp` |
| **Unity Tools** (VS Code 扩展) | Unity 专用集成 | `visualstudiotoolsforunity.vstuc` |

> 最后编辑：2026-07-27
