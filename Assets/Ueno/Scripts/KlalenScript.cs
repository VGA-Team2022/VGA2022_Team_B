using UnityEngine;

/// <summary>
/// 海ステージ視界妨害ギミック
/// </summary>
public class KlalenScript : GimmickBase
{
    /// <summary>視界妨害用UIのCanvas、GameObject</summary>
    private GameObject _klakenCanvas;
    private Animator _anim;

    private string _actionAnimName = "SplashTrigger";

    /// <summary>アクションさせたかどうか</summary>
    private bool _isActionDone = false;

    private Vector3 _appearPos;
    public Vector3 AppearPos { get => _appearPos; set => _appearPos = value; }

    private void Start()
    {
        transform.position = _appearPos;

        _klakenCanvas = transform.GetChild(0).gameObject;
        //ミスが無いように最初にfalseにしておく
        _klakenCanvas.SetActive(false);

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
        /*Todo:とりがーやめる
 */       if (this.transform.position.x <= 0 && !_isActionDone)
        {
            _anim.SetTrigger(_actionAnimName);
            SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Enemy_ArtPaint_Milk);
            _isActionDone = true;
        }
    }

    //以下2つの関数は、オブジェクト出現時にオブジェクトがカメラ内にいる、
    //シーンビューでも非表示にならないと呼ばれない、等注意点有
    private void OnBecameVisible()
    {
        Debug.Log("見えた");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("画面外なので描画を終了します");
        gameObject.SetActive(false);
    }

    /// <summary>移動処理</summary>
    private void FixedUpdate()
    {
        transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);
    }
}
