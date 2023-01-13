using UnityEngine;

/// <summary>
/// UVスクロールで背景を動かす
/// </summary>

public class BackGroundScroll : MonoBehaviour
{
    [Tooltip("UVスクロール対象のmaterial"),SerializeField]
    private Material _targetMaterial;

    [Tooltip("x軸方向に動く速さ"),SerializeField]
    private float _scrollX = 1;

    [Tooltip("y軸方向に動く速さ"), SerializeField]
    private float _scrollY = 0;

    private Vector2 offset;
    private void Awake()
    {
        offset = _targetMaterial.mainTextureOffset;
    }

    private void Update()
    {
        offset.x += _scrollX * Time.deltaTime;
        offset.y += _scrollY * Time.deltaTime;
        _targetMaterial.mainTextureOffset = offset;
    }

}
