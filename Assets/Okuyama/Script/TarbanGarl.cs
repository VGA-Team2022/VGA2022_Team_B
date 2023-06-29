using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarbanGarl : MonoBehaviour
{
    [SerializeField] GameObject _tarban;
    [SerializeField] float _tarbanOff = 3;
    Gimmickmanager _gimmickmanager;
    StageMove _stageMove;
    float _ratio;
    const float STAGEMOVE_ADJUSTMENT_EIGHT = 8.0f;

    public Gimmickmanager Gimmickmanager { get => _gimmickmanager; set => _gimmickmanager = value; }
    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }

    private void Start()
    {
        //_stageMove = _gimmickmanager.StageMove;
        _ratio = _stageMove.SpeedRatio * STAGEMOVE_ADJUSTMENT_EIGHT;
    }

    private void FixedUpdate()
    {
        if(transform.position.x <= -47) 
        {
            gameObject.SetActive(false);
            return; 
        }
        gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed * _ratio, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TarbanStart());
           // AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_kaiga_turban");
        }
        if (_tarban.activeInHierarchy && other.gameObject.CompareTag("Obon"))
        {
            if (other.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }
        }
    }

    IEnumerator TarbanStart()
    {
        _tarban.SetActive(true);
        yield return new WaitForSeconds(_tarbanOff);
        _tarban.SetActive(false);
    }
}
