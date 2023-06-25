using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemys : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;
    [SerializeField, Tooltip("犬で言うと走る方")] GameObject[] _runObj = default;
    [SerializeField, Tooltip("犬で言うと歩く方")] GameObject[] _stopObj = default;

    [Header("位置")]
    [SerializeField, Tooltip("生成位置")] Transform[] _enemyspoint = default;
    [SerializeField, Tooltip("前方生成位置")] Transform[] _enemysForwardPoint = default;

    [Header("時間")]
    [SerializeField, Tooltip("生成インターバル")] float _interval = 2f;
    [SerializeField, Tooltip("呼び出すまでの時間")] float spawnDelay = 1;
    int _nowStarg = 0;

    void Start()
    {
        _nowStarg = GameManager.StageLevelNum;
        InvokeRepeating("Instans", spawnDelay, _interval);
    }

    private void Update()
    {
        if (GameManager.IsAppearDoorObj)
        {
            CancelInvoke("Instans");
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Instans()
    {
        var point = Random.Range(0, _enemyspoint.Length);
        var type = Random.Range(0, 2);

        if (type == 0)//走る
        {
            Instantiate(_runObj[_nowStarg], _enemyspoint[point].position, Quaternion.identity);
        }
        else//止まる
        {
            Instantiate(_stopObj[_nowStarg], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }
}
