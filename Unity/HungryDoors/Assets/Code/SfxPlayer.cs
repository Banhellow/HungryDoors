using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SfxPlayer : MonoBehaviour
{
    public SFX sfx;
    [Inject]
    public SoundManager soundManager;

    void Start()
    {
        soundManager.PlaySfx(sfx);
    }
}
