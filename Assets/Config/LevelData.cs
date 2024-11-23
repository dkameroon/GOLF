using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelCountData : ScriptableObject
{
    [System.Serializable]
    public class LevelCountsData
    {
        public string levelName;
        public int counts;
        public int countTo3Stars;
        public int countTo2Stars;
    }

    public List<LevelCountsData> levelCountsData;
    
}