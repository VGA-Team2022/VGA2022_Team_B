using Common;
using UnityEngine.SceneManagement;

public class SceneChangeScript
{
    private static bool roadNow = false;

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

    private static void Load(string sceneName)
    {
        roadNow = false;
        SceneManager.LoadScene(sceneName);
    }

    public static bool StageUp()
    {
        if (!GameManager.IsGameClear) return false;

        if (GameManager.GameState.Time == StageTime.DAYTIME)
        {
            GameManager.GameState.Time = StageTime.NIGHT;
        }
        else
        {
            GameManager.GameState.Time = StageTime.DAYTIME;
            GameManager.GameState.Stage++;
        }

        return true;
    }
}
