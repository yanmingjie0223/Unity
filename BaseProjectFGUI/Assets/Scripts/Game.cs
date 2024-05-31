using UnityEngine;

public class Game : MonoBehaviour
{

    void Start()
    {
        InitializeManager();
        InitializeModel();
        InitializeStart();
    }

    private void InitializeManager()
    {
        StageManager.GetInstance().Initialize();
        FguiManager.GetInstance().Initialize();
        LayerManager.GetInstance().Initialize();
        ModelManager.GetInstance().Initialize();
    }

    private void InitializeModel()
    { }

    private void InitializeStart()
    {
        CSVManager.GetInstance().Initialize((bool isError) =>
        {
            if (!isError)
            {
                ViewManager.GetInstance().Show(typeof(MainView));
            }
        });
    }

}