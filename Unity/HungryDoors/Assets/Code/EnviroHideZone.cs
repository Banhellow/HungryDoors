using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroHideZone : MonoBehaviour
{
    public GameObject enviroVisuals;
    int counter = 0;

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
        if(counter > 0)
            enviroVisuals.SetActive(false);
        else
            enviroVisuals.SetActive(true);
    }
}
