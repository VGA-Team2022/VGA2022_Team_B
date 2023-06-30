using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneName : MonoBehaviour
{
    [Tooltip("シーン遷移先")]
    [SerializeField] private SceneNames _sceneNames = default;
    [SerializeField] FadeButton _fadeButton = default;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private NextScene _nextScene = NextScene.None;

    public void PassNextScene()
    {
        _fadeButton.Next = _nextScene;
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(Define.Scenes[_sceneNames]);
    }
}
