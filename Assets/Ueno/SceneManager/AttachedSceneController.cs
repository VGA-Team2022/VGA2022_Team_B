using UnityEngine;
using Common;

/// <summary>
///ÉVÅ[ÉìëJà⁄ 
/// </summary>
public class AttachedSceneController : MonoBehaviour
{
    void Start()
    {
        FadeScript.StartFadeIn();
    }
    public void NoFadeChangeScene(string target)
    {
        SceneChangeScript.NoFadeLoadScene(target);
    }

    public void ChangeTitleScene()
    {
        SceneChangeScript.LoadScene(Define.SCENENAME_TITLE);
    }
    public void ChangeHomeScene()
    {
        SceneChangeScript.LoadScene(Define.SCENENAME_HOME);
    }
    public void ChangeGameScene()
    {
        SceneChangeScript.LoadScene(Define.SCENENAME_MASTERGAME);
    }
    public void ChangeResultScene()
    {
        SceneChangeScript.LoadScene(Define.SCENENAME_RESULT);
    }

    public void Finish()
    {
        Application.Quit();
    }

}
