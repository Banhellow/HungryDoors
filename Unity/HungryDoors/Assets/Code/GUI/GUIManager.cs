using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [Header("Player life")]
    public Image lifeFillImage;

    [Header("Player item")]
    public Image playerItemIconImage;

    [Header("Dialog")]
    public CanvasGroup dialogCG;
    public Image dialogPortraitImage;
    public TMP_Text dialogText;
    private bool dialogAnimationInProgress = false;


    public void UpdatePlayerLife()
    {

    }

    public void UpdatePlayerItem()
    {

    }


    public void ShowDialogBox(Sprite portraitSprite, string dialogFraze)
    {
        dialogPortraitImage.sprite = portraitSprite;
        dialogText.text = dialogFraze;
        ShowDialogBox();
    }


    [Button]
    private void ShowDialogBox()
    {
        if (dialogAnimationInProgress == true)
            return;

        dialogAnimationInProgress = true;
        dialogText.maxVisibleCharacters = 0;

        StartCoroutine(ShowDialogAnimation());
    }

    [Button]
    private void HideDialogBox()
    {
        if (dialogAnimationInProgress == true)
            return;

        dialogAnimationInProgress = true;
        StartCoroutine(HideDialogAnimation());
    }

    IEnumerator ShowDialogAnimation()
    {
        float alphaAnimTime = 0.5f;
        float timer = 0;
        while (timer < alphaAnimTime)
        {
            timer += Time.deltaTime;
            dialogCG.alpha = Mathf.Lerp(0, 1, timer / alphaAnimTime);
            yield return null;
        }
        dialogCG.alpha = 1;

        // text
        int charCount = dialogText.textInfo.characterCount;
        int counter = 0;

        while (counter < charCount)
        {
            counter++;
            dialogText.maxVisibleCharacters = counter;
            yield return new WaitForFixedUpdate();
        }
        dialogAnimationInProgress = false;
    }

    IEnumerator HideDialogAnimation()
    {
        float alphaAnimTime = 0.5f;
        float timer = 0;
        while (timer < alphaAnimTime)
        {
            timer += Time.deltaTime;
            dialogCG.alpha = Mathf.Lerp(1, 0, timer / alphaAnimTime);
            yield return null;
        }
        dialogCG.alpha = 0;
        dialogAnimationInProgress = false;
    }
}
