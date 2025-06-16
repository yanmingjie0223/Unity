#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using YooAsset.Editor;
using YooAsset;
using WeChatWASM;
using static WeChatWASM.WXConvertCore;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Unity.EditorCoroutines.Editor;
using System.Collections;

public class WeChatMiniGameBuilder : EditorWindow
{
    private const string BuildVersionKey = "WeChatMiniGameBuilder_BuildVersion";
    private const string BundlesFolder = "Bundles";
    private const string ReleaseFolder = "Release";

    private string buildVersion = "";

    [MenuItem("Tools/一键微信小游戏打包")]
    public static void ShowWindow()
    {
        GetWindow<WeChatMiniGameBuilder>("微信小游戏打包");
    }

    private void OnEnable()
    {
        // 加载保存的版本号
        buildVersion = EditorPrefs.GetString(BuildVersionKey, "v0.1");
    }

    private void OnGUI()
    {
        buildVersion = EditorGUILayout.TextField("构建版本:", buildVersion);

        EditorGUILayout.Space();

        if (GUILayout.Button("构建并转换微信小游戏", GUILayout.Height(40)))
        {
            Build();
        }

        EditorGUILayout.HelpBox("请确保已安装:\n1. YooAsset最新版本\n2. 微信小游戏转换插件", MessageType.Info);
    }

    private void Build()
    {
        try
        {
            // 保存当前版本号
            EditorPrefs.SetString(BuildVersionKey, buildVersion);
            // 构建更换版本
            BuildChangeVersion();
            // 延迟打包
            EditorCoroutineUtility.StartCoroutineOwnerless(DelayedBuild());
        }
        catch (System.Exception e)
        {
            Debug.LogError("构建失败: " + e.Message);
            EditorUtility.DisplayDialog("错误", "构建失败: " + e.Message, "确定");
        }
    }

    private IEnumerator DelayedBuild()
    {
        // 清理Bundles文件夹（YooAsset资源）
        string bundlesPath = Path.Combine(Application.dataPath, "../", BundlesFolder);
        if (Directory.Exists(bundlesPath))
        {
            Directory.Delete(bundlesPath, true);
            Debug.Log($"已清理文件夹: {bundlesPath}");
        }
        yield return new EditorWaitForSeconds(3f);
        // 构建YooAsset资源
        if (!BuildYooAsset())
        {
            EditorUtility.DisplayDialog("错误", "YooAsset资源构建失败", "确定");
            yield break;
        }

        // 清理Release文件夹（微信小游戏输出）
        string releasePath = Path.Combine(Application.dataPath, "../", ReleaseFolder);
        if (Directory.Exists(releasePath))
        {
            Directory.Delete(releasePath, true);
            Debug.Log($"已清理文件夹: {releasePath}");
        }
        yield return new EditorWaitForSeconds(3f);
        // 构建微信小游戏
        if (!BuildWeChatMiniGame())
        {
            EditorUtility.DisplayDialog("错误", "微信小游戏构建失败", "确定");
            yield break;
        }

        BuildWechatMiniGameFinishBat();
    }

    private void BuildChangeVersion()
    {
        string filePath = Path.Combine(Application.dataPath, "Scripts/Configs/GameConfig.cs");

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);

            // 在这里进行你的修改
            // 例如替换特定文本
            string pattern = @"(appVersion\s*=\s*"")(v\d+\.\d+)("")";
            string replacement = $"appVersion = \"{buildVersion}\"";
            content = Regex.Replace(content, pattern, replacement);

            File.WriteAllText(filePath, content);
            Debug.Log("GameConfig.cs modified successfully.");

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        else
        {
            Debug.LogError("GameConfig.cs not found at: " + filePath);
        }
    }

    private bool BuildYooAsset()
    {
        Debug.Log("构建YooAsset");

        try
        {
            // 获取默认的构建参数
            BuiltinBuildParameters buildParameters = new();
            buildParameters.BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            buildParameters.BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
            buildParameters.BuildPipeline = "BuiltinBuildPipeline";
            buildParameters.BuildBundleType = (int)EBuildBundleType.AssetBundle;
            buildParameters.BuildTarget = BuildTarget.WebGL;
            buildParameters.PackageName = "DynamicAssets";
            buildParameters.PackageVersion = buildVersion;
            buildParameters.EnableSharePackRule = true;
            buildParameters.VerifyBuildingResult = true;
            buildParameters.FileNameStyle = EFileNameStyle.HashName;
            buildParameters.BuildinFileCopyOption = EBuildinFileCopyOption.None;
            buildParameters.BuildinFileCopyParams = "";
            buildParameters.CompressOption = ECompressOption.LZ4;
            buildParameters.ClearBuildCacheFiles = true;
            buildParameters.UseAssetDependencyDB = false;
            buildParameters.EncryptionServices = null;

            BuiltinBuildPipeline pipeline = new BuiltinBuildPipeline();
            var buildResult = pipeline.Run(buildParameters, true);
            if (buildResult.Success)
            {
                EditorUtility.RevealInFinder(buildResult.OutputPackageDirectory);
                Debug.Log("YooAsset构建成功");
                return true;
            }
            else
            {
                Debug.LogError($"YooAsset构建失败: {buildResult.ErrorInfo}");
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("YooAsset构建失败: " + e.Message);
            return false;
        }
    }

    private bool BuildWeChatMiniGame()
    {
        Debug.Log("开始转换为微信小游戏格式");

        try
        {
            WXExportError rt = WXEditorWin.DoExport();
            if (rt == WXExportError.SUCCEED)
            {
                Debug.Log("微信小游戏转换成功");
                return true;
            }
            else
            {
                Debug.LogError("微信小游戏转换失败");
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("微信小游戏转换失败: " + e.Message);
            throw;
        }
    }

    private void BuildWechatMiniGameFinishBat()
    {
        // 获取相对路径
        string relativePath = "../build_minigame.bat"; // 相对于Assets文件夹的路径
        string batFilePath = Path.Combine(Application.dataPath, relativePath);

        // 检查文件是否存在
        if (!File.Exists(batFilePath))
        {
            Debug.LogError("Batch file not found at: " + batFilePath);
            return;
        }

        // 创建进程启动信息
        ProcessStartInfo processStartInfo = new ProcessStartInfo(batFilePath)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // 启动进程
        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();

            // 读取输出
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // 等待进程结束
            process.WaitForExit();

            // 输出结果
            if (!string.IsNullOrEmpty(output))
            {
                Debug.Log("Output: " + output);
            }
        }
    }

}

#endif