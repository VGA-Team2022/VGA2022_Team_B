using UnityEngine;

/// <summary>
/// UV�X�N���[���Ŕw�i�𓮂���
/// </summary>

public class BackGroundScroll : MonoBehaviour
{
    [Tooltip("UV�X�N���[���Ώۂ�material"),SerializeField]
    private Material _targetMaterial;

    [Tooltip("x�������ɓ�������"),SerializeField]
    private float _scrollX = 1;

    [Tooltip("y�������ɓ�������"), SerializeField]
    private float _scrollY = 0;

    [Tooltip("y�������ɓ������l"), SerializeField]
    private float _moveY = 0;

    [Tooltip("anim�Ԋu"), SerializeField]
    private float _durationTime = 0.5f;

    [HideInInspector]
    public bool isSea;

    private Vector2 offset;

    private float time;
    bool isFlipSeaAnim;
    private void Awake()
    {
        offset = _targetMaterial.mainTextureOffset;
        offset.y = 0;
        _targetMaterial.mainTextureOffset = offset;
        GetComponent<MeshRenderer>().enabled= false;
        this.enabled = false;
    }
    
    private void Update()
    {
        if (isSea)
        {
            time += Time.deltaTime;

            offset.x += _scrollX * Time.deltaTime;

            if (_durationTime <= time)
            {
                time = 0f;
                offset.y = (isFlipSeaAnim) ? offset.y += _moveY : offset.y -= _moveY;
            }
            _targetMaterial.mainTextureOffset = offset;
        }
        else
        {
            DefaultMove();
        }
    }

    private void DefaultMove()
    {
        offset.x += _scrollX * Time.deltaTime;
        offset.y += _scrollY * Time.deltaTime;
        _targetMaterial.mainTextureOffset = offset;
    }


}
