using UnityEngine;

/// <summary>
///エネミー移動
/// </summary>
public class EnemyMove1 : MonoBehaviour //これいらないね。大丈夫だよ。謝らないで。誰にでも間違いはあるよ！俺も入学したときはこんなコード書いてたよ☆♡
{
    /// <summary>
    /// エネミーのスピード
    /// </summary>
    [Tooltip("エネミーにアタッチする"), SerializeField] private float _speed = 2.0f;//これいらないね。大丈夫だよ。謝らないで。♤

    /// <summary>エネミーの軸移動方向</summary>　//summary付けれて偉いね！
    private int x = 0;

    private void Start()//これいらないね。大丈夫だよ。謝らないで。♤
    {
        GetComponent<Transform>().position = this.gameObject.transform.position;//これいらないね。大丈夫だよ。謝らないで。♤
    }

    /// <summary>
    /// エネミーの移動　//summary付けれて偉いね！
    /// </summary>
    void Update()
    {
        if (this.gameObject.name == "dog")　//ブロックは付けようね♡
        {
            x += 1;//背景スピード*方向 　//補足付けれて偉いね。♧
        }
        else
        {
            x -= 1;
        }
        transform.Translate(x, 0, 0);　//ポジションでいいよ！♧

        if (x <= 30 || x >= -30)
        {
            Destroy(this.gameObject);　//動体視力いいね！♤
        }
    }
}