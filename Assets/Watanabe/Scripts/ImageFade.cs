using DG.Tweening;
using UnityEngine;

public class ImageFade : MonoBehaviour
{
    [SerializeField] private Transform _mask = default;
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        _mask.localScale = new Vector3(25f, 25f, 25f);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) Fade();
    }

    private void Fade()
    {
        _mask.DOScale(0f, _duration);
    }
}
