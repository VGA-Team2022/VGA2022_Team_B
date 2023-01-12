using UnityEngine.SceneManagement;


public class SceneChangeScript
{
    private static bool roadNow = false;

    /// <summary>
    /// �w��V�[���Ɉڍs����
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeScript.StartFadeOut(() => Load(sceneName));
        //  GameManager.isFindScenemng = false;
    }
    public static void NoFadeLoadScene(string sceneName)
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        Load(sceneName);
    }
    static void Load(string sceneName)
    {
        roadNow = false;
        SceneManager.LoadScene(sceneName);
    }
}
