using UnityEngine;

public class Game : MonoBehaviour
{

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
    }

    private void Start()
    {
        FguiManager.GetInstance().Initialize();
        StageManager.GetInstance().Initialize();
        LayerManager.GetInstance().Initialize();
        LoadManager.GetInstance().Initialize();

        var playMode = PathUtils.GetPlayMode();
        LoadManager.GetInstance().LoadPackage(
            GameConfig.yooPackageName,
            playMode,
            null,
            null,
            (bool isError) =>
            {
                if (isError)
                {
                    InitializeError();
                }
                else
                {
                    LoadingView loadingView = ViewManager.GetInstance().Show(typeof(LoadingView)) as LoadingView;
                    LoadManager.GetInstance().LoadGroup(
                        GameConfig.yooPackageName,
                        GroupType.Config,
                        null,
                        (float progress) =>
                        {
                            loadingView.SetProgress(progress);
                        },
                        (bool isError) =>
                        {
                            if (isError)
                            {
                                InitializeError();
                            }
                            else
                            {
                                InitializeModel();
                                InitializeManager();
                                InitializeStart();
                            }
                        }
                    );
                }
            }
        );
    }

    private void InitializeManager()
    {
        ModelManager.GetInstance().Initialize();
        ConfigManager.GetInstance().Initialize();
    }

    private void InitializeModel()
    {
        var modelManager = ModelManager.GetInstance();
        modelManager.Register<SettingOptionModel>();
    }

    private void InitializeStart()
    {
        ResManager.GetInstance().GetResAssetAsync(
            GameConfig.yooPackageName,
            new() { "main" },
            (string hint) => { },
            (float progress) => { },
            (bool isError) =>
            {
                var viewManager = ViewManager.GetInstance();
                viewManager.Close(typeof(LoadingView));
                viewManager.Show(typeof(MainView));
            }
         );
    }

    private void InitializeError()
    {
        Debug.LogError("initialize error! ");
    }

}