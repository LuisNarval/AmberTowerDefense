using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// This is the Game Manager.
/// The objective of this script is to be direct the game flow of each game.
/// He will know when the base has been destroyed or when the enemies waves have ended.
/// After that, it will call the necesary screen.
/// Also, his responsability is to start the WaveSystem.
/// </summary>

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas winScreen;
    [SerializeField] Canvas looseScreen;
    [SerializeField] Canvas countDownScreen;

    private void Awake()
    {
        winScreen.enabled = false;
        looseScreen.enabled = false;
        countDownScreen.enabled = false;
        EventBus.Subscribe(GameEvent.GAMEWINNED, GameWinned);
        EventBus.Subscribe(GameEvent.BASEDESTROYED, GameLoosed);
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        Text txt = countDownScreen.GetComponentInChildren<Text>();
        txt.text = "READY ? "; 
        countDownScreen.enabled = true;
        yield return new WaitForSeconds(1.0f);

        for (int i = 3; i > 0; i--)
        {
            txt.text = i.ToString();
            yield return StartCoroutine(Scale(txt.rectTransform));
        }
        txt.text = "START !";
        yield return StartCoroutine(Scale(txt.rectTransform));

        yield return new WaitForSeconds(1.0f);

        countDownScreen.enabled = false;
        StartGame();
    }

    IEnumerator Scale(RectTransform rTransform)
    {
        float scale = 0;
        rTransform.localScale = Vector3.zero;
        while (rTransform.localScale.x<1)
        {
            scale += Time.deltaTime;
            rTransform.localScale = Vector3.one * scale;
            yield return new WaitForEndOfFrame();
        }
    }

    void StartGame()
    {
        EventBus.Publish(GameEvent.STARTGAME);
    }

    void GameWinned()
    {
        winScreen.enabled = true;
    }

    void GameLoosed()
    {
        looseScreen.enabled = true;
    }


    public void NextLevel()
    {
        ServiceLocator.GetService<LoaderInfo>().nextLevel++;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        ServiceLocator.GetService<LoaderInfo>().nextLevel = 0;
        SceneManager.LoadScene(1);
    }


}