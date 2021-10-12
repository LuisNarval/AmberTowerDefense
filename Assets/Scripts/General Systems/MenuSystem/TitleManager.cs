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
    public void SelectLevel(int _level)
    {
        LoaderSystem.Instance.GoToLevel(_level);
    }

}
