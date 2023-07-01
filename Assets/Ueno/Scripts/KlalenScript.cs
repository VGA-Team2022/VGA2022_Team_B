using UnityEngine;

public class KlalenScript : GimmickBase
{
    /// <summary>視界妨害用UIのCanvas、GameObject</summary>
    private GameObject _klakenCanvas;
    private Animator _anim;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }
    private StageMove _stageMove;

    private string _actionAnimName = "SplashTrigger";

    /// <summary>アクションさせたかどうか</summary>
    private bool _isActionDone = false;

    public Vector3 AppeairPos { get => _appaeirPos; set => _appaeirPos = value; }
    private Vector3 _appaeirPos;
    void Start()
    {
        transform.position = _appaeirPos;

        _klakenCanvas = this.transform.GetChild(0).gameObject;
        //ミスが無いように最初にfalseにしておく
        _klakenCanvas.SetActive(false);

        _anim = GetComponent<Animator>();
   
        _stageMove = GetComponent<StageMove>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (this.transform.position.x <= 0 && !_isActionDone)
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
        this.gameObject.transform.position -= new Vector3(Time.deltaTime * StageMove.MoveSpeed, 0);
    }
}
