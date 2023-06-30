using DG.Tweening;
using UnityEngine;

public class ImageFade : MonoBehaviour
{
    [SerializeField] private RectTransform _unMask = default;
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        _unMask.localScale = new Vector3(30f, 30f, 30f);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) Fade();
    }

    public void Fade()
    {
        _unMask
            .DOScale(0f, _duration).SetEase(Ease.Linear)
            .OnComplete(() => Debug.Log("finished mask"));
    }
}
