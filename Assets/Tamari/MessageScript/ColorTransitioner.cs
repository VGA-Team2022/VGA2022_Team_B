using UnityEngine;
using UnityEngine.UI;

public class ColorTransitioner : MonoBehaviour
{
    [SerializeField]
    private Image _image = default; // �w�i�摜

    [SerializeField]
    private Color _to = default; // ���̐F�ɑJ�ڂ���

    [SerializeField]
    private float _duration = 1; // �J�ڎ��ԁi�b�j

    private Color _from;
    private float _elapsed = 0;

    /// <summary>
    /// �w�i�F�̑J�ڂ��������Ă��邩�ǂ���
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
    /// �w�i�̑J�ڂ��������J�n
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
    /// ���݂̔w�i�̑J�ڏ������X�L�b�v����
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}