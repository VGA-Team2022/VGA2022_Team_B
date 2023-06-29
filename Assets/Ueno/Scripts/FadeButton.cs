using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    [SerializeField] private Button[] _buttons = default;
    [SerializeField] private float _fadeDuration;

    private bool _isTitle = false;

    public bool IsTitle { get => _isTitle; set => _isTitle = value; }

    private void Start()
    {
        foreach(var button in _buttons)
        {
            button.onClick.AddListener(button.gameObject.GetComponent<LoadSceneName>().PassFlag);
            button.onClick.AddListener(ButtonFade);
        }
    }

    private void ButtonFade()
    {
        var title = _buttons[0].gameObject;
        var next = _buttons[1].gameObject;

        if (_isTitle)
        {
            title.GetComponent<Image>()
                .DOFade(0f, _fadeDuration)
                .OnComplete(() => title.GetComponent<LoadSceneName>().SceneLoad());


            next.GetComponent<Image>().DOFade(0f, _fadeDuration);
        }
        else
        {
            title.GetComponent<Image>().DOFade(0f, _fadeDuration);

            next.GetComponent<Image>()
                .DOFade(0f, _fadeDuration)
                .OnComplete(() => next.GetComponent<LoadSceneName>().SceneLoad());
        }

    }
}
