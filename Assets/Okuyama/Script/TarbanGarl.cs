using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarbanGarl : MonoBehaviour
{
    [SerializeField] GameObject _tarban;
    [SerializeField] float _tarbanOff = 3;
    StageMove _stageMove;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }

    private void FixedUpdate()
    {
        if(transform.position.x <= -47) 
        {
            gameObject.SetActive(false);
            return; 
        }
        gameObject.transform.position -= new Vector3(Time.deltaTime * StageMove.MoveSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TarbanStart());
            if(_tarban.activeInHierarchy && other.gameObject.CompareTag("Obon"))
            {
                if (other.gameObject.TryGetComponent(out Obon obon))
                {
                    obon.Hit(this.transform.position.x);
                }
            }
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_kaiga_turban");
            //obon.Hit(this.transform.position.x);
        }
    }

    IEnumerator TarbanStart()
    {
        _tarban.SetActive(true);
        yield return new WaitForSeconds(_tarbanOff);
        _tarban.SetActive(false);
    }
}
