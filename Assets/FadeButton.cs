using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private float _fadeDuration;

    private Color _fadeColor;

    private void Start()
    {
        foreach(var b in _buttons)
        {
            b.gameObject.GetComponent<Button>();
            b.onClick.AddListener(ButtonFade);
        }

    }

    private void ButtonFade()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float clearScale = 1f;//α値

        Color currentColor = _fadeColor;

        while (clearScale > 0f)//clearScaleが０になるまで回す
        {
            clearScale -= _fadeDuration * Time.deltaTime;//1秒ごとにα値を下げる

            if (clearScale <= 0f)//値が0未満になることを避けている
            {
                clearScale = 0f;
            }

            currentColor.a = clearScale;//α値をcolorに代入

            foreach (var i in _buttons)
            {
                i.GetComponent<Image>().color = currentColor;//colorを実際のImageのcolorに代入
            }
            yield return null;
        }
    }
}
