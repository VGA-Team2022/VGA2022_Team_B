using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image _fadePanel = default;
    [SerializeField] private float _fadeSpeed = 1f;

    private void Start()
    {
        _fadePanel.enabled = false;

        FadeIn();
    }

    public void FadeIn()
    {
        _fadePanel.enabled = true;

        _fadePanel.DOFade(0f, _fadeSpeed)
                  .OnComplete(() =>
                  {
                      _fadePanel.enabled = false;
                      Debug.Log("end");
                  });
    }

    public void FadeOut()
    {
        _fadePanel.enabled = true;

        _fadePanel.DOFade(1f, _fadeSpeed)
                  .OnComplete(() => Debug.Log("end"));
    }
}
