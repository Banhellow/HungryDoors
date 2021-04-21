using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button lvl1Btn;
    public Button lvl2Btn;
    public Button lvl3Btn;

    public void OnLevelClick(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
