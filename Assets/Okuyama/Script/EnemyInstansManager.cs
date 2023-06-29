using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エネミー生成
/// </summary>
public class EnemyInstansManager : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;

    [Header("prefab")]
    [SerializeField, Tooltip("走る犬エネミー")] GameObject[] _runDogEnemyPrefab = default;
    [SerializeField, Tooltip("止まる犬エネミー")] GameObject[] _stopDogEnemyPrefab = default;

    [Header("位置")]
    [SerializeField, Tooltip("生成位置")] Transform[] _enemyspoint = default;
    [SerializeField, Tooltip("前方生成位置")] Transform[] _enemysForwardPoint = default;

    [Header("時間")]
    [SerializeField, Tooltip("生成インターバル")] float _interval = 2f;
    [SerializeField, Tooltip("呼び出すまでの時間")] float spawnDelay = 1;

    void Start()
    {
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj)
        {
            CancelInvoke("Instans");
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Instans()
    { 
        var point = Random.Range(0,_enemyspoint.Length);
        var index = Random.Range(0, _runDogEnemyPrefab.Length);

        var dogType = Random.Range(0, 2);//0→走る犬、１→止まる犬
        Debug.Log($"dogType={dogType}");

        if (dogType == 1) //走る犬
        {
            var obj = Instantiate(_runDogEnemyPrefab[index], _enemyspoint[point].position, Quaternion.identity);
            obj.GetComponent<EnemyDogScript>().EnemyInstansManager = this;

        }
        else //止まる犬
        {
            Instantiate(_stopDogEnemyPrefab[index], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }

    /// <summary>
    /// 後ろからくる犬の時
    /// </summary>
    public void Dog(GameObject dogs)
    {
        //Player players = new Player();　//Playerスクリプトからインスタンスを精製 Playerの現在のNowPosを使用
        Debug.Log("犬");
        Instantiate(dogs, _enemysForwardPoint[_player.NowPos].transform.position, Quaternion.identity);
    }

}
