using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public CanvasGroup PanelCG;

    public Button lvl1Btn;
    public Button lvl2Btn;
    public Button lvl3Btn;

    private void Awake()
    {
        PanelCG.alpha = 1;
    }

    private void Start()
    {
        PanelCG.DOFade(0,1);
    }

    public void OnLevelClick(int levelIndex)
    {
        Debug.Log($"OnLevelClick {levelIndex}");
        SceneManager.LoadScene(levelIndex);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
