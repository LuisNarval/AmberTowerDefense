using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a basic TitleManager
/// By the moment register de Loader Info to the Service Locater.
/// It has the Scene Configuration, responsable for building the levels using diferents scenes.
/// For this prototype, each level only has 2 scenes, the multiplayer scene & the selected level scene
/// Once it register the loader info, it filled with the data that the player choice.
/// Later call to the Load Scene, the one that is gonna be responsable for building the levels.
/// /// </summary>

public class TitleManager : MonoBehaviour
{
    [SerializeField] SceneConfiguration sceneConfiguration;

    private void Awake()
    {
        LoaderInfo loaderInfo = new LoaderInfo();
        ServiceLocator.RegisterService(loaderInfo);
    }

    public void SelectLevel(int _level)
    {
        ServiceLocator.GetService<LoaderInfo>().sceneConfiguration = sceneConfiguration;
        ServiceLocator.GetService<LoaderInfo>().nextLevel = _level;

        SceneManager.LoadScene(sceneConfiguration.Level[1].Scene[0].name);
    }

}
