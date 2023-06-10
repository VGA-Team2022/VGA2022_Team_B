using UnityEngine;

/// <summary> 水溜り </summary>
public class WaterPuddle : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [Range(0, 3)]
    [Tooltip("自分がどのレーンに存在するか")]
    [SerializeField] private int _myRane = 0;

    private BoxCollider2D _collider = default;

    private void Start()
    {
        //設定忘れ防止
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    private void Update()
    {
        //水溜りの移動(transformで動かす)
        transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0);
    }

    //以下2つの関数は、ゲーム開始時にオブジェクトがカメラ内にいる、
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
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            //自分（水溜り）が存在するレーンとPlayerがいるレーンが一致したら
            if (player.NowPos == _myRane)
            {
                //ここで揺らす等の処理を呼び出す
                Debug.Log("滑った");
            }
        }
    }
}
