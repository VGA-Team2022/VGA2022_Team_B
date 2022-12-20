using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarbanGarl : MonoBehaviour
{
    [SerializeField] GameObject _tarban;
    StageMove _stageMove;

    public StageMove StageMove { get => _stageMove; set => _stageMove = value; }

    private void Start()
    {
        StageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
    }
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
            Instantiate(_tarban, gameObject.transform);
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_kaiga_turban");
            //obon.Hit(this.transform.position.x);
        }
    }
}
