using UnityEngine;
using UnityEngine.UI;

public class ColorTransitioner : MonoBehaviour
{
    [SerializeField]
    private Image _image = default; // ”wŒi‰æ‘œ

    [SerializeField]
    private Color _to = default; // ‚±‚ÌF‚É‘JˆÚ‚·‚é

    [SerializeField]
    private float _duration = 1; // ‘JˆÚŠÔi•bj

    private Color _from;
    private float _elapsed = 0;

    /// <summary>
    /// ”wŒiF‚Ì‘JˆÚ‚ªŠ®—¹‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
    /// </summary>
    public bool IsCompleted
        => _image is null ? false : _image.color == _to;
    void Start()
    {
        if (_image is null) { return; }
        _from = _image.color;
    }

    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed < _duration)
        {
            _image.color = Color.Lerp(_from, _to, _elapsed / _duration);
        }
        else { _image.color = _to; }
    }

    /// <summary>
    /// ”wŒi‚Ì‘JˆÚ‚ğˆ—‚ğŠJn
    /// </summary>
    /// <param name="color"></param>
    public void Play(Color color)
    {
        if (_image is null)
        {
            return;
        }

        _from = _image.color;
        _to = color;
        _elapsed = 0;
    }

    /// <summary>
    /// Œ»İ‚Ì”wŒi‚Ì‘JˆÚˆ—‚ğƒXƒLƒbƒv‚·‚é
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}