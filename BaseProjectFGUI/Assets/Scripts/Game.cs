using Steamworks;
using UnityEngine;

public class Game : MonoBehaviour
{

    void Start()
    {
        InitializeModel();
        InitializeManager();
        InitializeStart();
        InitializeSteam();
    }

    private void InitializeManager()
    {
        StageManager.GetInstance().Initialize();
        FguiManager.GetInstance().Initialize();
        LayerManager.GetInstance().Initialize();
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
        ViewManager.GetInstance().Show(typeof(MainView));
    }

    private void InitializeSteam()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log("steam user name: " + name);
            CSteamID id = SteamUser.GetSteamID();
            Debug.Log("steam user id: " + id);
        }
    }

}