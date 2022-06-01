using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FoodModel : ConversationModel
{
    public string phrase;
    public FoodType type;
    public bool isCorrect;
}
