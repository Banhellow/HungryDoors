using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationController : MonoBehaviour
{

    public Animator chillAnim;
    void Start()
    {
        chillAnim.SetTrigger("Chill");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
