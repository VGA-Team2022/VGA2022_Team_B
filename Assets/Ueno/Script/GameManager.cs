using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int GameStageNum = 0;
    public static int StageLevelNum =  0;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PrefarenceStage(int i)
    {
        GameStageNum = i;
    }
    public void PrefarenceLevel(int i)
    {
        StageLevelNum = i;
    }


}