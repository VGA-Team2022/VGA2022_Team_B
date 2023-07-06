using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image _fadePanel = default;
    [SerializeField] private float _fadeSpeed = 1f;

    private void Start()
    {
        _fadePanel.gameObject.SetActive(false);

        FadeIn();
    }

    public void FadeIn()
    {
        _fadePanel.gameObject.SetActive(true);

        _fadePanel.DOFade(0f, _fadeSpeed)
                  .OnComplete(() =>
                  {
                      _fadePanel.gameObject.SetActive(false);
                      Debug.Log("finished fadein");
                  });
    }

    public void FadeOut()
    {
        _fadePanel.gameObject.SetActive(true);

        _fadePanel.DOFade(1f, _fadeSpeed)
                  .OnComplete(() => Debug.Log("finished fadeout"));
    }
}
