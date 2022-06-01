using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wrapper<T> where T: ConversationModel
{
    public T[] array;
}
