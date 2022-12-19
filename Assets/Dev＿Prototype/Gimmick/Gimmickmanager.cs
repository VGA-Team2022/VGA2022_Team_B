using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �M�~�b�N����Manager
/// </summary>
public class Gimmickmanager : MonoBehaviour
{ 
    [Header("�o�����ɐݒ�")]
    [Tooltip("�M�~�b�NPrefab"),SerializeField] private GameObject[] _gimmickPrefabs;
    [Tooltip("�o�����郌�[��"), SerializeField] private Transform[] _appearLane;
    [Tooltip("�o�����鎞��"), SerializeField] private float[] _appearTime;
  //  [Tooltip("GameManager"), SerializeField] private GameManager _gameManager;


    private int _appearGimmickNum = 0;
    private float _time = Define.GAME_TIME;
    private bool isAppearGimmick = false;
    private bool employee = false;

    private void Update()
    {
        _time -= Time.deltaTime;
        isAppearGimmick = false;

        //GameManager�̎��Ԃ��K�{
        //if (_gameTime <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        //{
        //    var index = Random.Range(0, _gimmickPrefabs.Length -1);
        //    GenerateGimmick(index, 3);
        //    if (_appearGimmickNum == _appearTime.Length) { return; }
        //    _appearGimmickNum++;
        //    isAppearGimmick = true;
        //}
        //if (_gameTime <= _gameTime/2 && !employee)
        //{
        //    //�P�[�L��ςގg�p�l  
        //    GenerateGimmick(3, 3);
        //    employee = true;
        //}
        if (_time <= _appearTime[_appearGimmickNum] && !isAppearGimmick)
        {
            var index = Random.Range(0, 2);
            GenerateGimmick(index, 3);
            if (_appearGimmickNum == _appearTime.Length) { return; }
            _appearGimmickNum++;
            isAppearGimmick = true;
        }
        if (_time < Define.GAME_TIME/2 && !employee)
        {
            GenerateGimmick(3, 3);
            employee = true;
        }
    }

    private void GenerateGimmick(int gimmickPrefabNum,int laneNum)
    {
        Instantiate(_gimmickPrefabs[gimmickPrefabNum], _appearLane[laneNum]);
        Debug.Log($"{_gimmickPrefabs[gimmickPrefabNum]}���o������");
    }

}
