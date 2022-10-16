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
    }
    private static void Load(string sceneName)
    {
        roadNow = false;
        SceneManager.LoadScene(sceneName);
    }
}
