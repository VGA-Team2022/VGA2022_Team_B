using UnityEngine;

/// <summary>
///�V�[���J�� 
/// </summary>
public class AttachedSceneController : MonoBehaviour
{
    void Start()
    {
        FadeScript.StartFadeIn();
    }
    public void ChangeScene(string target)
    {
        SceneChangeScript.LoadScene(target);
    }

    public void Finish()
    {
        Application.Quit();
    }

}
