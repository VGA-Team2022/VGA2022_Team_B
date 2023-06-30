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

    int _random = 0;

    private float _moveTime = 0;//“®‚¢‚Ä‚éŽžŠÔ

    private bool _isMove = false;

    private StageMove _stageMove;

    private Gimmickmanager _gimmickManager;

    private Movement _movement = Movement.FirstMove;

    private Vector3 _pos = default;
    
    void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _gimmickManager = GameObject.Find("GimmickManager").GetComponent<Gimmickmanager>();

        //this.transform.position = new Vector3(-24, this.transform.position.y, this.transform.position.z);

        _pos = transform.position;
    }

    private void FixedUpdate()
    {
        switch (_movement)
        {
            case Movement.FirstMove:
                transform.position += new Vector3(Time.deltaTime * 2 * _stageMove.MoveSpeed, 0);
                if (transform.position.x >= 20)
                {
                    _movement = Movement.SecondMove;
                }
                break;
            case Movement.SecondMove:
                _moveTime += Time.deltaTime;
                transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
                _random = MakeRandom();

                transform.position =
                    Vector3.Lerp(transform.position,
                                 new Vector3(transform.position.x, transform.position.y, _gimmickManager.Lanes[_random].position.z),
                                 _moveTime / _moveSpeed);

                if (_random == 0) { gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15; }
                else if (_random == 1) { gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 8; }
                else { gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1; }

                if (_moveTime >= _moveSpeed)
                {
                    _movement = Movement.ThirdMove;
                }
                break;

            case Movement.ThirdMove:
                gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
                if (transform.position.x <= _pos.x)
                {
                    Destroy(gameObject);
                }
                break;

            case Movement.No:
                if(GetComponent<BoxCollider>().enabled)
                {
                    GetComponent<BoxCollider>().enabled = false;
                }
                break;
        }
    }

    int MakeRandom()
    {
        if (!_isMove)
        {
            _isMove = true;
            return Random.Range(0, 3);
        }
        else { return _random; }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            if (gameObject.GetComponent<BoxCollider>().enabled)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            if (collision.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(transform.position.x);
            }
        }
    }
}
