using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Conversation
{

    public Conversation()
    {
       
    }


    public List<T> GetAllPhrases<T>(string path) where T : ConversationModel

    {
        var asset = Resources.Load<TextAsset>(path);
        var content = asset.text.Replace("\n", "");
        string json = "{\"array\": " + content + "}";
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        List<T> phrases = new List<T>();
        for(int i = 0; i < wrapper.array.Length; i++)
        {
            phrases.Add(wrapper.array[i]);
        }
        return phrases;
    }

    public string GetPhraseByFoodType(FoodType foodType,bool isCorrect)
    {
        var phrases = GetAllPhrases<FoodModel>("Conversations/FoodConv");
        var correctPhrases = phrases.FindAll(x => x.isCorrect == isCorrect && x.type == foodType);
        return correctPhrases.RandomElement().phrase;
    }
    
    public string GetPhraseByWeaponType(WeaponType type)
    {
        var phrases = GetAllPhrases<WeaponModel>("Conversations/WeaponConv");
        var correctPhrases = phrases.FindAll(x => x.type == type);
        return correctPhrases.RandomElement().phrase;
    }

    public string GetCheatByHintAndLevel(int level, string hint)
    {
        var cheats = GetAllPhrases<CheatModel>("Conversations/Cheats");
        var resaultCheat = cheats.Find(x => x.level == level && x.hint == hint);
        string res = resaultCheat.cheatEN;
        return res;
    }
}
