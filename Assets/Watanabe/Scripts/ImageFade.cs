using DG.Tweening;
using UnityEngine;

public class ImageFade : MonoBehaviour
{
    [SerializeField] private RectTransform _unMask = default;
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        _unMask.localScale = new Vector3(25f, 25f, 25f);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) Fade();
    }

    private void Fade()
    {
        _unMask
            .DOScale(0f, _duration).SetEase(Ease.Linear)
            .OnComplete(() => Debug.Log("finished mask"));
    }
}
