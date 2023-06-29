using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
class DogPrefabs
{
    [Tooltip("走る犬Prefab"), SerializeField] public GameObject[] _runDogPrefabs;
    [Tooltip("止まる犬Prefab"), SerializeField] public GameObject[] _stopDogPrefabs;
}
public class DogEnemys : MonoBehaviour
{
    [Header("Scecn番号と同じElement番号にSceneに合ったPrefabを入れる\n屋敷(昼)が0番ならElement番号0番に屋敷(昼)に使うギミックを入れる")]
    [SerializeField] private DogPrefabs[] _sceneDogGimmick;
    [SerializeField, Tooltip("Player")] Player _player = null;
    [Tooltip("犬で言うと走る方")] GameObject[] _runObj = default;
    [Tooltip("犬で言うと歩く方")] GameObject[] _stopObj = default;

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
        _runObj = _sceneDogGimmick[_nowStarg]._runDogPrefabs;
        _stopObj = _sceneDogGimmick[_nowStarg]._stopDogPrefabs;
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
        var point = UnityEngine.Random.Range(0, _enemyspoint.Length);
        var runIndex = UnityEngine.Random.Range(0, _runObj.Length);
        var stopIndex = UnityEngine.Random.Range(0, _stopObj.Length);

        var type = UnityEngine.Random.Range(0, 2);
        if (type == 0)//走る
        {
            Instantiate(_runObj[runIndex], _enemyspoint[point].position, Quaternion.identity);
        }
        else//止まる
        {
            Instantiate(_stopObj[stopIndex], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }
}
