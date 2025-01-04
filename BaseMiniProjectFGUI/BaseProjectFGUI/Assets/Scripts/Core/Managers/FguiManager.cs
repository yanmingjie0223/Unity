using FairyGUI;

public class FguiManager : Singleton<FguiManager>
{
    public void Initialize()
    {
        InitializeConfig();
        BindComponent();
    }

    public void InitializeConfig()
    {

#if (UNITY_5 || UNITY_5_3_OR_NEWER)
        UIConfig.defaultFont = "Dream Han Serif CN";
#else
        //Need to put a ttf file into Resources folder. Here is the file name of the ttf file.
        UIConfig.defaultFont = "Dream Han Serif CN";
#endif

    }

    private void BindComponent()
    {

        UIObjectFactory.SetPackageItemExtension("ui://preload/GlobalModalWaiting", typeof(GlobalModalWaiting));


    }

}
