using UnityEngine.SceneManagement;

public class SceneChangeScript
{
    private static bool roadNow = false;

    /// <summary>
    /// 指定シーンに移行する
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        if (roadNow) return;

        roadNow = true;
        FadeScript.StartFadeOut(() => Load(sceneName));
        //  GameManager.isFindScenemng = false;
    }
    public static void NoFadeLoadScene(string sceneName)
    {
        if (roadNow) return;

        roadNow = true;
        Load(sceneName);
    }
    static void Load(string sceneName)
    {
        roadNow = false;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary> 次のステージに進むとき、GameManagerの値を更新する </summary>
    /// <return> 挑戦するステージがあればtrue, なければfalse </return>
    public static bool StageUp()
    {
        if (!GameManager.IsGameClear) return true;

        GameManager.IsGameClear = false;

        //ここカクニンする
        //屋敷ステージ
        if (GameManager.GameStageNum == 0)
        {
            if (GameManager.StageLevelNum == 0)
            {
                GameManager.StageLevelNum = 1;
            }
            else
            {
                GameManager.GameStageNum = 1;
                GameManager.StageLevelNum = 0;
            }
        }
        //海ステージ
        else
        {
            if (GameManager.StageLevelNum == 0)
            {
                GameManager.StageLevelNum = 1;
            }
            else
            {
                //最後までいったらリセット
                GameManager.GameStageNum = 0;
                GameManager.StageLevelNum = 0;
                return false;
            }
        }
        return true;
    }
}
