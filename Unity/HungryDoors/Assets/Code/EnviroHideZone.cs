using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroHideZone : MonoBehaviour
{
    public GameObject enviroVisuals;
    private MeshRenderer[] allRenderers;
    int counter = 0;

    private void Awake()
    {
        allRenderers = enviroVisuals.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            counter++;
            UpdateVisibility();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            counter--;
            UpdateVisibility();
        }
    }

    private void UpdateVisibility()
    {
        if (counter > 0)
        {
            //enviroVisuals.SetActive(false);
            for (int i = 0; i < allRenderers.Length; i++)
            {
                allRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        }
        else
        {
            //enviroVisuals.SetActive(true);
            for (int i = 0; i < allRenderers.Length; i++)
            {
                allRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }

    }
}
