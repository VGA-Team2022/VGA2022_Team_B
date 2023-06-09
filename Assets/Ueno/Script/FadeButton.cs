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
        float clearScale = 1f;//���l

        Color currentColor = _fadeColor;

        while (clearScale > 0f)//clearScale���O�ɂȂ�܂ŉ�
        {
            clearScale -= _fadeDuration * Time.deltaTime;//1�b���ƂɃ��l��������

            if (clearScale <= 0f)//�l��0�����ɂȂ邱�Ƃ�����Ă���
            {
                clearScale = 0f;
            }

            currentColor.a = clearScale;//���l��color�ɑ��

            foreach (var i in _buttons)
            {
                i.GetComponent<Image>().color = currentColor;//color�����ۂ�Image��color�ɑ��
            }
            yield return null;
        }
    }
}
