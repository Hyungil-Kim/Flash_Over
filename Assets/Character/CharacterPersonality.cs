using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Personality
{
    [Flags]
    public enum CharacterPersonality
    {
        Kind = 1 << 0,
        Angry = 1 << 1,
        Deliberation = 1 << 2,
        Honesty = 1 << 3,
        Mild = 1 << 4,
        Lively = 1 << 5,
    }
    public CharacterPersonality currentPersonality;

    public bool CheakPersonality (CharacterPersonality personality)
    {
        return currentPersonality.HasFlag(personality);
    }
    public void AddPersonality(CharacterPersonality personality)
    {
        currentPersonality |= personality;
    }
    public void AddPersonality(int index)
    {
        var personalityName = Enum.GetNames(typeof(CharacterPersonality))[index];
        currentPersonality |= StringToEnum.SToE<CharacterPersonality>(personalityName);
    }
    public void SetPersonality(int min, int max)
    {
        var psCount = UnityEngine.Random.Range(min, max);
        var psList = Enum.GetNames(typeof(CharacterPersonality)).ToList();
        for (int i = 0; i < psCount; i++)
        {
            var randomPs = UnityEngine.Random.Range(0, psList.Count);
            AddPersonality(randomPs);
            psList.RemoveAt(randomPs);
        }
    }
    public List<string> CheakAllPersonality()
    {
        List<string> list = new List<string>();
        foreach (var type in Enum.GetValues(typeof(CharacterPersonality)))
        {
            if(CheakPersonality((CharacterPersonality)type))
            {
                list.Add(type.ToString());
                Debug.Log(type);
            }
        }
        return list;
    }
}