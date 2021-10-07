using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The Loader System is responsable for building the levels
/// It takes the información from the Loader Info subscribed to the Service Locator.
/// This search for the scene configuration that was edited by the developers and with that information, this script
/// will beggin to build the levels, wich are conformed by a Gameplay Scene & the a Level Scene.
/// </summary>

public class LoaderSystem : MonoBehaviour
{
    SceneConfiguration sceneConfiguration;
    public int currentLevel = 0;

    List<AsyncOperation> operations = new List<AsyncOperation>();

    private void Awake()
    {
        GetLoadInfo();

        Load();
    }

    private void Start()
    {
    }

    public void GetLoadInfo()
    {
       sceneConfiguration = ServiceLocator.GetService<LoaderInfo>().sceneConfiguration;
       currentLevel = ServiceLocator.GetService<LoaderInfo>().nextLevel;
    }

    public void Load()
    {
        StartCoroutine(LoadLevelScenes());
    }

    IEnumerator LoadLevelScenes()
    {
        for (int i = 0; i < sceneConfiguration.Level[currentLevel].Scene.Length; i++)
        {
            string sceneName = sceneConfiguration.Level[currentLevel].Scene[i].name;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;
            operations.Add(asyncLoad);
        }

        bool allScenesLoaded = false;
        while (!allScenesLoaded)
        {
            allScenesLoaded = true;
            foreach (var operation in operations)
            {
                Debug.Log("Progres: " + operation.progress);
                if (operation.progress<0.9f)
                {
                    allScenesLoaded = false;
                    break;
                }
            }
           
            yield return null;
        }

        foreach (var operation in operations)
        {
            operation.allowSceneActivation = true;
        }

        SceneManager.UnloadSceneAsync(1);

    }

}