using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public List<CardAnimationController> cards;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if (Input.GetKeyDown(cards[i].key))
            {
                cards[i].SetCardSelected(!cards[i].selected);
            }
            if (Input.GetKeyDown(cards[i].useKey))
            {
                cards[i].UseCard();
            }
        }

    }
}
