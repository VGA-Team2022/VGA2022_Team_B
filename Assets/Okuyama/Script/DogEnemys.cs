using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemys : MonoBehaviour
{
    [SerializeField, Tooltip("Player")] Player _player = null;
    [SerializeField, Tooltip("���Ō����Ƒ����")] GameObject[] _runObj = default;
    [SerializeField, Tooltip("���Ō����ƕ�����")] GameObject[] _stopObj = default;

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
    /// ����
    /// </summary>
    void Instans()
    {
        var point = Random.Range(0, _enemyspoint.Length);
        var type = Random.Range(0, 2);

        if (type == 0)//����
        {
            Instantiate(_runObj[_nowStarg], _enemyspoint[point].position, Quaternion.identity);
        }
        else//�~�܂�
        {
            Instantiate(_stopObj[_nowStarg], _enemyspoint[_player.NowPos].position, Quaternion.identity);
        }
    }
}
