using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
/// <summary> 水溜り </summary>
public class WaterPuddle : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50f;

    [Header("以下の2値はカメラ内に収まる値にしてください")]
    [SerializeField] private float _minPosX = -10f;
    [SerializeField] private float _maxPosX = 10f;

    private Rigidbody2D _rb2d = default;
    /// <summary> 自分がどのレーンに存在するか </summary>
    private int _myRane = 0;

    private void Start()
    {
        //レーンを一定範囲内からランダムで決める
        var posX = Random.Range(_minPosX, _maxPosX);
        var posZ = Random.Range(1, 4);
        var pos = transform.position;

        pos.x = posX;
        pos.z = posZ;
        transform.position = pos;

        Debug.Log(posX);

        _myRane = posZ;

        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0f;
        //設定忘れ防止
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    //private void Update()
    //{
    //    transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0f);
    //}

    private void FixedUpdate()
    {
        //水溜りの移動(当たり判定をとるため、Rigidbody2D)
        _rb2d.velocity = new Vector2(-Time.deltaTime * _moveSpeed, 0f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;

        if (hit.TryGetComponent(out Obon obon) &&
            hit.transform.parent.gameObject.TryGetComponent(out Player player))
        {
            //自分（水溜り）が存在するレーンとPlayerがいるレーンが一致したら
            if (player.NowPos == _myRane)
            {
                //揺らす処理を呼び出す→自身を消す
                obon.Hit(transform.position.x);
                Debug.Log("滑った");
                gameObject.SetActive(false);
            }
        }
    }
}
