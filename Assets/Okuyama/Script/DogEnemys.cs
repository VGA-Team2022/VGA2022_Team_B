using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
class DogPrefabs
{
    [Tooltip("���錢Prefab"), SerializeField] public GameObject[] _runDogPrefabs;
    [Tooltip("�~�܂錢Prefab"), SerializeField] public GameObject[] _stopDogPrefabs;
}
public class DogEnemys : MonoBehaviour
{
    [Header("Scecn�ԍ��Ɠ���Element�ԍ���Scene�ɍ�����Prefab������\n���~(��)��0�ԂȂ�Element�ԍ�0�Ԃɉ��~(��)�Ɏg���M�~�b�N������")]
    [SerializeField] private DogPrefabs[] _sceneDogGimmick;
    [SerializeField, Tooltip("Player")] Player _player = null;
    [Tooltip("���Ō����Ƒ����")] GameObject[] _runObj = default;
    [Tooltip("���Ō����ƕ�����")] GameObject[] _stopObj = default;

    [Header("�ʒu")]
    [SerializeField, Tooltip("�����ʒu")] Transform[] _enemyspoint = default;
    [SerializeField, Tooltip("�O�������ʒu")] Transform[] _enemysForwardPoint = default;

    [Header("����")]
    [SerializeField, Tooltip("�����C���^�[�o��")] float _interval = 2f;
    [SerializeField, Tooltip("�Ăяo���܂ł̎���")] float spawnDelay = 1;
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
    /// ����
    /// </summary>
    void Instans()
    {
        var point = UnityEngine.Random.Range(0, _enemyspoint.Length);
        var runIndex = UnityEngine.Random.Range(0, _runObj.Length);
        var stopIndex = UnityEngine.Random.Range(0, _stopObj.Length);

        var type = UnityEngine.Random.Range(0, 2);
        if (type == 0)//����
        {
            Instantiate(_runObj[runIndex], _enemyspoint[point].position, Quaternion.identity);
        }
        else//�~�܂�
        {
            Instantiate(_stopObj[stopIndex], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }
}
