using Common;
using UnityEngine;

public class AttachedSceneController : MonoBehaviour
{
    private void Start()
    {
        FadeScript.StartFadeIn();
    }

    public void NoFadeChangeScene(string target)
    {
        SceneChangeScript.NoFadeLoadScene(target);
    }

    public void NoFadeChangeScene(SceneNames target)
    {
        SceneChangeScript.NoFadeLoadScene(Define.Scenes[target]);
    }

    #region FadeèàóùÇçsÇ§ä÷êî
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
    #endregion

    public void Finish()
    {
        Application.Quit();
    }
}
