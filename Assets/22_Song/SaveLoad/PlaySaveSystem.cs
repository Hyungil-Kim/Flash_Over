using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaySaveSystem
{
    public static PlaySave ps;
    public static void SaveInPlay(PlaySave ps, int slot)
    {
        MySaveLoadSystem<PlaySave>.Save(ps, SaveDataType.Play, slot);
    }
    public static void LoadInPlay(int slot)
    {
        ps = MySaveLoadSystem<PlaySave>.Load(SaveDataType.Play, slot);
        ps.Init();
    }
}
