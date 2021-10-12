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

public class LoaderSystem : Singleton<LoaderSystem>
{
    [SerializeField] private SceneConfiguration sceneConfiguration;
    [SerializeField] private Canvas loaderContent;
    [SerializeField] private Image loadBar;
    private List<AsyncOperation> operations;

    [SerializeField] private int previousLevel;

    [SerializeField] private int _currentLevelz;
    [SerializeField] public int currentLevel {
        get { return _currentLevelz; }
        set
        {
            if (value >= sceneConfiguration.Level.Length)
                _currentLevelz = 1;
            else
                _currentLevelz = value;
        }
    } 
   

    private void Start()
    {
        previousLevel = 0;
        currentLevel = 1;
        loaderContent.enabled = true;
        StartCoroutine(LoadScenes());
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
    }


    public void GoToLevel(int _level)
    {
        StopAllCoroutines();
        previousLevel = currentLevel;
        currentLevel = _level;
        StartCoroutine(ChangeLevel());
    }

    public void GoToLevel()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        Time.timeScale = 1.0f;

        yield return StartCoroutine(UnLoadScenes());
        yield return StartCoroutine(LoadScenes());

        EventBus.Publish(GameEvent.SCENESLOADED);
    }


    private IEnumerator UnLoadScenes()
    {
        operations = new List<AsyncOperation>();

        for (int i = 0; i < sceneConfiguration.Level[previousLevel].Scene.Length; i++)
        {
            string sceneName = sceneConfiguration.Level[previousLevel].Scene[i].name;
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(sceneName), UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            operations.Add(asyncLoad);
        }

        for (int i = 0; i < operations.Count; i++)
        {
            yield return new WaitUntil(()=> operations[i].isDone);
        }

        loaderContent.enabled = false;


    }


    private IEnumerator LoadScenes()
    {
        loaderContent.enabled = true;
        operations = new List<AsyncOperation>();

        for (int i = 0; i < sceneConfiguration.Level[currentLevel].Scene.Length; i++)
        {
            string sceneName = sceneConfiguration.Level[currentLevel].Scene[i].name;

            /*if(i== 0)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else
            {*/
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                asyncLoad.allowSceneActivation = false;
                operations.Add(asyncLoad);
            //}

        }

        while (GeneralProgress() < 0.89f)
        {
            loadBar.fillAmount = GeneralProgress();
            yield return new WaitForEndOfFrame();
        }

        loadBar.fillAmount = 1.0f;

        yield return new WaitForSeconds(1.0f);

        foreach (var operation in operations)
        {
            operation.allowSceneActivation = true;
        }

        foreach (var operation in operations)
        {
            yield return new WaitUntil(() => operation.isDone);
        }

        loaderContent.enabled = false;

        Scene scene;
        string sceneNamae = sceneConfiguration.Level[currentLevel].Scene[0].name;
        scene = SceneManager.GetSceneByName(sceneNamae);
        yield return new WaitUntil(() => scene.isLoaded);
        SceneManager.SetActiveScene(scene);

        yield return new WaitForEndOfFrame();

    }



    private float GeneralProgress()
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