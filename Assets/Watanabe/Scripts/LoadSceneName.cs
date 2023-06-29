using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneName : MonoBehaviour
{
    [Tooltip("シーン遷移先")]
    [SerializeField] private SceneNames _sceneNames = default;
    [SerializeField] FadeButton _fadeButton = default;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private bool _isTitle = false;

    public void PassFlag()
    {
        _fadeButton.IsTitle = _isTitle;
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(Define.Scenes[_sceneNames]);
    }
}
