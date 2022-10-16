using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードアウト処理
/// </summary>
public class FadeScript : MonoBehaviour
{
    private static FadeScript instance = default;

    /// <summary>フェードさせる画像</summary>
    [Header("アタッチするもの"),
        Tooltip("フェードさせる画像"), SerializeField] private Image _fadePanel = default;
  
    /// <summary>フェードの時間</summary>
    [Header("各種変更値"), 
        Tooltip("フェードの時間"), SerializeField] private float _fadeSpeed = 1f;
    /// <summary>フェード開始時の色</summary>
    [Tooltip("フェード開始時の色"),SerializeField] private Color _fadeColor = Color.black;

    private void Awake()
    {
        instance = this;
        _fadePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// フェードイン後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeIn(Action action = null)
    {
        if (instance == null)
        {
            action?.Invoke();
            return;
        }
        instance.StartCoroutine(instance.FadeIn(action));
    }

    /// <summary>
    /// フェードアウト後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeOut(Action action = null)
    {
        if (instance == null)
        {
            action?.Invoke();
            return;
        }
        instance.StartCoroutine(instance.FadeOut(action));
    }
    public static void StartFadeOutIn(Action outAction = null, Action inAction = null)
    {
        if (instance == null)
        {
            outAction?.Invoke();
            inAction?.Invoke();
            return;
        }
        instance.StartCoroutine(instance.FadeOutIn(outAction, inAction));
    }

    

    private IEnumerator FadeOutIn(Action outAction, Action inAction)
    {
        yield return FadeOut(outAction);
        yield return FadeIn(inAction);
    }
    private IEnumerator FadeIn(Action action)
    {
        _fadePanel.gameObject.SetActive(true);
        yield return FadeIn();
        action?.Invoke();
        _fadePanel.gameObject.SetActive(false);
    }
    private IEnumerator FadeOut(Action action)
    {
        _fadePanel.gameObject.SetActive(true);
        yield return FadeOut();
        action?.Invoke();
    }
    private IEnumerator FadeIn()
    {
        float clearScale = 1f;
        Color currentColor = _fadeColor;
        while (clearScale > 0f)
        {
            clearScale -= _fadeSpeed * Time.deltaTime;
            if (clearScale <= 0f)
            {
                clearScale = 0f;
            }
            currentColor.a = clearScale;
            _fadePanel.color = currentColor;
            yield return null;
        }
    }
    private IEnumerator FadeOut()
    {
        float clearScale = 0f;
        Color currentColor = _fadeColor;
        while (clearScale < 1f)
        {
            clearScale += _fadeSpeed * Time.deltaTime;
            if (clearScale >= 1f)
            {
                clearScale = 1f;
            }
            currentColor.a = clearScale;
            _fadePanel.color = currentColor;
            yield return null;
        }
    }
}
