/*
using UnityEngine;

/// <summary>
/// Script responsible for managing level, like spawning level, spawning balls, deciding game win/loss status and more
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab;           //reference to ball prefab
    public Vector3 ballSpawnPos;            //reference to spawn position of ball

    public LevelData[] levelDatas;          //list of all the available levels

    private int shotCount = 0;              //count to store available shots

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SpawnLevel(int levelIndex)
    {
        //we spawn the level prefab at required position
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;
        UIManager.Instance.CountOfShots.text = shotCount.ToString();
        GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
        CameraFollow.instance.SetTarget(ball);
    }


    public void ShotTaken()
    {
        if (shotCount > 0)
        {
            shotCount--;
            UIManager.Instance.CountOfShots.text = "" + shotCount;

            /*if (shotCount <= 0)
            {
                LevelFailed();
            }#1#
        }
    }


}
*/
