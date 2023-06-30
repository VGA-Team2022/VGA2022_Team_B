using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    [SerializeField] private Button[] _buttons = default;
    [SerializeField] private float _fadeDuration;

    private NextScene _nextScene = NextScene.None;

    public NextScene Next { get => _nextScene; set => _nextScene = value; }

    private void Start()
    {
        foreach(var button in _buttons)
        {
            button.onClick.AddListener(button.gameObject.GetComponent<LoadSceneName>().PassNextScene);
            button.onClick.AddListener(ButtonFade);
        }
    }

    private void ButtonFade()
    {
        var title = _buttons[0].gameObject;
        var next = _buttons[1].gameObject;
        var retry = _buttons[2].gameObject;

        if (_nextScene == NextScene.Title)
        {
            title.GetComponent<Image>()
                .DOFade(0f, _fadeDuration)
                .OnComplete(() => title.GetComponent<LoadSceneName>().SceneLoad());

            next.GetComponent<Image>().DOFade(0f, _fadeDuration);
            retry.GetComponent<Image>().DOFade(0f, _fadeDuration);
        }
        else if (_nextScene == NextScene.NextStage)
        {
            title.GetComponent<Image>().DOFade(0f, _fadeDuration);

            next.GetComponent<Image>()
                .DOFade(0f, _fadeDuration)
                .OnComplete(() => next.GetComponent<LoadSceneName>().SceneLoad());

            retry.GetComponent<Image>().DOFade(0f, _fadeDuration);
        }
        else if (_nextScene == NextScene.SameStage)
        {
            title.GetComponent<Image>().DOFade(0f, _fadeDuration);
            next.GetComponent<Image>().DOFade(0f, _fadeDuration);

            retry.GetComponent<Image>()
                .DOFade(0f, _fadeDuration)
                .OnComplete(() => retry.GetComponent<LoadSceneName>().SceneLoad());
        }
    }
}

public enum NextScene
{
    None,
    Title,
    NextStage,
    SameStage,
}
