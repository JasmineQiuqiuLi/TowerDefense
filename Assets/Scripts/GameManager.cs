using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager: MonoBehaviour
{
    public GameObject gameResultPanel;
    public Text gameResultText;
    public GameObject replayButton;
    public GameObject menuButton;

    bool isGamOver;
    public bool IsGameOver { get { return isGamOver; } }

    public static GameManager instance;
 

    private void Awake()
    {
       
        isGamOver = false;
        instance = this;
    }

    private void Start()
    {
        Tweener replayTweener = replayButton.transform.DOLocalMove(new Vector3(-73, 0, 0), 0.5f).SetAutoKill(false); ;
        Tweener menuTweener = menuButton.transform.DOLocalMove(new Vector3(89, 0, 0), 0.5f).SetAutoKill(false); ;
        Tweener gameResultTextTweener = gameResultText.transform.DOLocalMove(new Vector3(-3,70, 0), 0.5f).SetAutoKill(false); ;
        replayTweener.Pause();
        menuTweener.Pause();
        gameResultTextTweener.Pause();
    }

    public void WinGame()
    {

        ShowGameResultPanel();
        gameResultText.text = "You Win";
    }

    public void LoseGame()
    {
        setGameOver();
        ShowGameResultPanel();
        gameResultText.text = "Game Over";
        EnemySpawner.instance.STOPAllCoroutine();
    }

    public void setGameOver()
    {
        isGamOver = true;
    }

    public void Replay()
    {
        DoTweenPlayBackwardAnimation();
        Invoke("ReloadCurrentScene", 0.5f);
    }

    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMenu()
    {
        DoTweenPlayBackwardAnimation();
        Invoke("LoadStartScene", 0.5f);
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }


    void ShowGameResultPanel()
    {
        gameResultPanel.SetActive(true);
        DoTweenPlayForwardAnimation();
    }
    void DoTweenPlayForwardAnimation()
    {
        replayButton.transform.DOPlayForward();
        menuButton.transform.DOPlayForward();
        gameResultText.transform.DOPlayForward();
    }

    void DoTweenPlayBackwardAnimation()
    {
        replayButton.transform.DOPlayBackwards();
        menuButton.transform.DOPlayBackwards();
        gameResultText.transform.DOPlayBackwards();

    }
}
