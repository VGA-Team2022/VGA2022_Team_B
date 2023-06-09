using UnityEngine;

/// <summary>
/// UVスクロールで背景を動かす
/// </summary>

public class BackGroundScroll : MonoBehaviour
{
    [Tooltip("UVスクロール対象のmaterial")]
    public Material TargetMaterial;

    [Tooltip("x軸方向に動く速さ"),SerializeField]
    private float _scrollX = 1;

    [Tooltip("y軸方向に動く速さ"), SerializeField]
    private float _scrollY = 0;

    [Tooltip("y軸方向に動かす値"), SerializeField]
    private float _moveY = 0;

    [Tooltip("anim間隔"), SerializeField]
    private float _durationTime = 0.5f;

    //[HideInInspector]
    public bool isSea;

    private Vector2 offset;

    private float time;
    bool isFlipSeaAnim;
    private void Awake()
    {
        offset = TargetMaterial.mainTextureOffset;
        offset.y = 0;
        TargetMaterial.mainTextureOffset = offset;
        if (isSea)
        {
            GetComponent<MeshRenderer>().enabled = false;
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        isSea = true;
        Debug.Log(GetComponent<MeshRenderer>().enabled);

    }

    private void FixedUpdate()
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
            TargetMaterial.mainTextureOffset = offset;

            Debug.Log(offset);
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
        TargetMaterial.mainTextureOffset = offset;
    }


}
