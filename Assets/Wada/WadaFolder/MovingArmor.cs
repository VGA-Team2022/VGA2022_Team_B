using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Movement
{
    FirstMove,
    SecondMove,
    ThirdMove,
    No
}

public class MovingArmor : MonoBehaviour
{
    [Tooltip("“®‚­‚Ì‚É‚©‚©‚éŽžŠÔ"), SerializeField]
    float _moveSpeed;

    private Transform _startPos;
    private Transform _finishPos;

    private int _startTime = 0;

    int random = 0;

    private float _moveTime = 0;//“®‚¢‚Ä‚éŽžŠÔ

    private bool _move = false;

    private StageMove _stageMove;

    private Gimmickmanager _gimmickManager;

    private Movement _movement = Movement.FirstMove;


    public Transform StartPos
    {
        get
        {
            return _startPos;
        }
        set
        {
            _startPos = value;
        }
    }
    public Transform FinishPos
    {
        get
        {
            return _finishPos;
        }
        set
        {
            _finishPos = value;
        }
    }

    private Vector3 _pos = default;




    void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _gimmickManager = GameObject.Find("GimmickManager").GetComponent<Gimmickmanager>();

        //this.transform.position = new Vector3(-24, this.transform.position.y, this.transform.position.z);

        _pos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_movement)
        {
            case Movement.FirstMove:
                this.gameObject.transform.position += new Vector3(Time.deltaTime * 2 * _stageMove.MoveSpeed, 0);
                if (this.gameObject.transform.position.x >= 20)
                {
                    _movement = Movement.SecondMove;
                }
                break;
            case Movement.SecondMove:
                _moveTime += Time.deltaTime;
                this.gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
                random = MakeRandom();
                this.gameObject.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, _gimmickManager.Lanes[random].position.z), (_moveTime / _moveSpeed));
                if (random == 0) { this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15; }
                else if (random == 1) { this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 8; }
                else { this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1; }
                if (_moveTime >= _moveSpeed)
                {
                    _movement = Movement.ThirdMove;
                }
                break;
            case Movement.ThirdMove:
                this.gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
                if (this.transform.position.x <= _pos.x)
                {
                    Destroy(this.gameObject);
                }
                break;
            case Movement.No:
                if(this.gameObject.GetComponent<BoxCollider>().enabled)
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                break;
        }
    }

    int MakeRandom()
    {
        if (!_move)
        {
            _move = true;
            return Random.Range(0, 3);
        }
        else { return random; }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            if (this.gameObject.GetComponent<BoxCollider>().enabled)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            if (collision.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }
        }
    }
}
