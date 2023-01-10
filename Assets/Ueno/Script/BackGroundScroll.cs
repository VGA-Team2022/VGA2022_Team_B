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

    private Vector2 offset;
    private void Awake()
    {
        offset = _targetMaterial.mainTextureOffset;
    }

    private void Update()
    {
        offset.x += _scrollX * Time.deltaTime;
        _targetMaterial.mainTextureOffset = offset;
    }

}
