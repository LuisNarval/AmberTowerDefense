using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// The Loader System is responsable for building the levels
/// It takes the información from the Loader Info subscribed to the Service Locator.
/// This search for the scene configuration that was edited by the developers and with that information, this script
/// will beggin to build the levels, wich are conformed by a Gameplay Scene & the a Level Scene.
/// </summary>

public class LoaderSystem : MonoBehaviour
{
    [SerializeField] GameObject loaderContent;
    [SerializeField] private Image loadBar;
   
    SceneConfiguration sceneConfiguration;
    List<AsyncOperation> operations = new List<AsyncOperation>();
    public int currentLevel = 0;

    private void Start()
    {
        GetLoadInfo();
        Load();
    }

    public void GetLoadInfo()
    {
       sceneConfiguration = ServiceLocator.GetService<LoaderInfo>().sceneConfiguration;
       currentLevel = ServiceLocator.GetService<LoaderInfo>().nextLevel;
    }

    public void Load()
    {
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        for (int i = 0; i < sceneConfiguration.Level[currentLevel].Scene.Length; i++)
        {
            string sceneName = sceneConfiguration.Level[currentLevel].Scene[i].name;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;
            operations.Add(asyncLoad);
        }

        while (GeneralProgress() < 0.89f)
        {
            loadBar.fillAmount = GeneralProgress();
            yield return new WaitForEndOfFrame();
        }

        loadBar.fillAmount = GeneralProgress();
        yield return new WaitForSeconds(2.0f);
        loadBar.fillAmount = 1.0f;

        loaderContent.SetActive(false);
        SceneManager.UnloadSceneAsync(1);
        
        foreach (var operation in operations)
        {
            operation.allowSceneActivation = true;
        }

    }

    float GeneralProgress()
    {
        float total = 0;
        float currentProgress = 0;

        foreach (var op in operations)
        {
            total++;
            currentProgress += op.progress;
        }

        return currentProgress / total;
    }


}