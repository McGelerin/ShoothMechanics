using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject endGamePanel;
    public Text percentText;
    float targetCountCache,percentCacher;
    
    private void Start()
    {
        inGamePanel.SetActive(true);
        endGamePanel.SetActive(false);
        percentCacher = 0;
        targetCountCache = GameManager.Instance.TargetController.TargetCount;
    }

    private void LateUpdate()
    {
        percentCalculate();
        if (GameManager.GameStatusCache == GameStatus.END)
        {
            StartCoroutine("gameFinnish");
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void percentCalculate()
    {
        percentCacher = (targetCountCache -GameManager.Instance.TargetController.TargetCount) / targetCountCache *100;
        percentText.text = "%" + (((int)percentCacher).ToString());
    }

    IEnumerator gameFinnish()
    {
        yield return new WaitForSeconds(0.8f);
        inGamePanel.SetActive(false);
        endGamePanel.SetActive(true);
    }
}
