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

    [Tooltip("y軸方向に動かす値"), SerializeField]
    private float _moveY = 0;

    [Tooltip("anim間隔"), SerializeField]
    private float _durationTime = 0.5f;

    [Header("海ステージ"), SerializeField]
    private bool isSea;

    private Vector2 offset;

    private int count;

    private float time;
    bool isFlipSeaAnim;
    private void Awake()
    {
        offset = _targetMaterial.mainTextureOffset;
        offset.y = 0;
        _targetMaterial.mainTextureOffset = offset;
    }
    
    private void Update()
    {
        if (isSea)
        {
            time += Time.deltaTime;

            offset.x += _scrollX * Time.deltaTime;

            if (_durationTime <= time)
            {
                //count++;
                time = 0f;

                //if (count % 5 == 4)
                //{
                //   //isFlipSeaAnim = (isFlipSeaAnim) ? !isFlipSeaAnim : isFlipSeaAnim;
                //   if(isFlipSeaAnim)
                //    {
                //        isFlipSeaAnim = false;
                //    }
                //   else
                //    {
                //        isFlipSeaAnim = true;
                //    }
                //}

                Debug.Log($"{_targetMaterial.mainTextureOffset}:{count}:余り{count % 5}：反転{isFlipSeaAnim}");

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
