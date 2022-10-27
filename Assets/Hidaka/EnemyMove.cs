using UnityEngine;

/// <summary>
///エネミー移動
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// エネミーのスピード
    /// </summary>
    [Tooltip("エネミーにアタッチする"), SerializeField] private float _speed = 2.0f;

    /// <summary>エネミーの軸移動方向</summary>
    private int x = 0;

    private void Start()
    {
        GetComponent<Transform>().position = this.gameObject.transform.position;
    }

    /// <summary>
    /// エネミーの移動
    /// </summary>
    void Update()
    {
        if(this.gameObject.name == "dog") x += 1;//背景スピード*方向
        else  x-= 1;
        transform.Translate(x, 0, 0);

        if (x <= 30 || x >= -30)
        {
            Destroy(this.gameObject);
        }
    }
}
