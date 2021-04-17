using DG.Tweening;
using NaughtyAttributes;
using System;
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
    public Sprite emptySlotSprite;
    public TMP_Text itemUsageLeftText;

    [Header("Dialog")]
    public CanvasGroup dialogCG;
    public Image dialogPortraitImage;
    public TMP_Text dialogText;
    private bool isDialogOpen = false;
    private bool dialogAnimationInProgress = false;
    private float dialogVisibilityTime;
    private bool forceEndTextAnim = false;


    private void Update()
    {
        if(isDialogOpen)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                forceEndTextAnim = true;
            }
        }
    }

    public void UpdatePlayerLife(float lifePercentage)
    {
        lifeFillImage.DOFillAmount(lifePercentage, 0.2f);
    }


    public void UpdatePlayerItem(Item item)
    {
        playerItemIconImage.sprite = item.data.icon;
        UpdateItemDurability(item);
    }

    internal void UpdateItemDurability(Item item)
    {
        itemUsageLeftText.text = (item.data.maxUsageCount - item.durability).ToString();
    }

    internal void ItemLost()
    {
        playerItemIconImage.sprite = emptySlotSprite;
        itemUsageLeftText.text = "";
    }

    /// <summary>
    /// Main method to call when dialog box should be shown.
    /// </summary>
    public void ShowDialogBox(Sprite portraitSprite, string dialogFraze, float visibleTime)
    {
        dialogPortraitImage.sprite = portraitSprite;
        dialogText.text = dialogFraze;
        dialogVisibilityTime = visibleTime;
        ShowDialogBox();
    }


    [Button]
    private void ShowDialogBox()
    {
        if (dialogAnimationInProgress == true)
            return;

        dialogAnimationInProgress = true;
        dialogText.maxVisibleCharacters = 0;
        isDialogOpen = true;

        StartCoroutine(ShowDialogAnimation());
    }

    [Button]
    private void HideDialogBox()
    {
        if (dialogAnimationInProgress == true)
            return;

        dialogAnimationInProgress = true;
        isDialogOpen = false;

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

        while (counter < charCount && forceEndTextAnim == false)
        {
            counter++;
            dialogText.maxVisibleCharacters = counter;
            yield return new WaitForFixedUpdate();
        }
        forceEndTextAnim = false;
        dialogText.maxVisibleCharacters = charCount;
        dialogAnimationInProgress = false;

        yield return new WaitForSeconds(dialogVisibilityTime);
        HideDialogBox();
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





    [Button]
    private void DEV_testDialog()
    {
        ShowDialogBox(dialogPortraitImage.sprite,
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            2);
    }
}
