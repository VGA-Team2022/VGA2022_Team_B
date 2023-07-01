using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
/// <summary> 水溜り </summary>
public class WaterPuddle : MonoBehaviour
{
    [Header("以下の2値はカメラ内に収まる値にしてください")]
    [SerializeField] private float _minPosX = -10f;
    [SerializeField] private float _maxPosX = 10f;

    private Rigidbody _rb = default;
    /// <summary> 自分がどのレーンに存在するか </summary>
    private int _myRane = 0;

    private StageMove _stage = default;

    private void Start()
    {
        //レーンを一定範囲内からランダムで決める
        var posX = Random.Range(_minPosX, _maxPosX);
        var posZ = Random.Range(1, 4);
        var pos = transform.position;

        pos.x = posX;
        pos.z = posZ;
        transform.position = pos;

        _myRane = posZ;

        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        //設定忘れ防止
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Init(StageMove stage)
    {
        //値が未設定だった場合のみ、自分で設定した値にする
        _stage = stage;
    }

    private void FixedUpdate()
    {
        //水溜りの移動(当たり判定をとるため、Rigidbody)
        _rb.velocity = new Vector2(-Time.deltaTime * _stage.MoveSpeed, 0f);
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

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;

        if (hit.TryGetComponent(out Obon obon) &&
            hit.transform.parent.gameObject.TryGetComponent(out Player player))
        {
            //自分（水溜り）が存在するレーンとPlayerがいるレーンが一致したら
            if (player.NowPos == _myRane)
            {
                SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Whale_WaterPaddle);
                //揺らす処理を呼び出す→自身を消す
                obon.Hit(transform.position.x);
                Debug.Log("滑った");
                gameObject.SetActive(false);
            }
        }
    }
}
