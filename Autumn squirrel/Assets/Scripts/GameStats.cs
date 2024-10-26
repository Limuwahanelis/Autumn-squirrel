using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats 
{
    static List<LevelData> _levelDatas=new List<LevelData>() {new(),new (),new () };
    public static List<int> acornsInLevels = new List<int>() { 3, 3, 3 };
    public static void CollectAcorn(int levelIndex,int acornIndex)
    {
        _levelDatas[levelIndex].collectedAcornsIndex.Add(acornIndex);
    }
    public static void DigHole(int levelIndex)
    {
        _levelDatas[levelIndex].isHoleDigged = true;
    }
}
