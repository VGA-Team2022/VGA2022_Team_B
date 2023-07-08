using UnityEngine;

enum Movement
{
    FirstMove,
    SecondMove,
    ThirdMove,
    No
}

public enum AnimType
{
    None,
    Idle,
    SideMove,
    FrontMove,
}

public class MovingArmor : GimmickBase
{
    [Tooltip("“®‚­‚Ì‚É‚©‚©‚éŽžŠÔ")]
    [SerializeField]
    private float _moveSpeed;

    private int _random = 0;

    private float _moveTime = 0;//“®‚¢‚Ä‚éŽžŠÔ
    private bool _isMove = false;

    //ŠZ‚ªŽ~‚Ü‚éˆÊ’u
   private float[] _stopPos = new float[] { -5f, -3f, -1f};

    private Vector3 _pos = default;
    private SpriteRenderer _spriteRenderer = default;
    private Animator _animator = default;

    private Movement _movement = Movement.FirstMove;
    private AnimType _currentAnimType = AnimType.None;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _animator = GetComponentInChildren<Animator>();
        _currentAnimType = AnimType.SideMove;

        _pos = transform.position;
    }

    private void FixedUpdate()
    {
        switch (_movement)
        {
            //‰¡‚©‚ço‚Ä‚­‚é
            case Movement.FirstMove:
                transform.position += new Vector3(Time.deltaTime * 2 * StageMovement.MoveSpeed, 0);
                if (transform.position.x >= 20)
                {
                    _movement = Movement.SecondMove;
                    ChangeAnim(AnimType.FrontMove);
                }
                break;

            //ƒŒ[ƒ“‚ÉˆÚ“®‚·‚é
            case Movement.SecondMove:
                _moveTime += Time.deltaTime;
                transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);
                _random = MakeRandom();

                transform.position =
                    Vector3.Lerp(transform.position,
                                 new Vector3(transform.position.x, transform.position.y, _stopPos[_random]),
                                 _moveTime / _moveSpeed);

                _spriteRenderer.sortingOrder = _random switch
                    {
                        0 => 15,
                        1 => 8,
                        _ => 1,
                    };

                if (_moveTime >= _moveSpeed)
                {
                    _movement = Movement.ThirdMove;
                    ChangeAnim(AnimType.Idle);
                }
                break;

            case Movement.ThirdMove:
                gameObject.transform.position -= new Vector3(Time.deltaTime * StageMovement.MoveSpeed, 0);
                if (transform.position.x <= _pos.x)
                {
                    Destroy(gameObject);
                }
                break;

            case Movement.No:
                if (GetComponent<BoxCollider>().enabled)
                {
                    GetComponent<BoxCollider>().enabled = false;
                }
                break;
        }
    }

    private void ChangeAnim(AnimType next)
    {
        if (_currentAnimType == next) return;

        switch (next)
        {
            case AnimType.Idle:
                _animator.SetBool("isSideMove", false);
                _animator.SetBool("isFrontMove", false);
                break;

            case AnimType.SideMove:
                _animator.SetBool("isSideMove", true);
                _animator.SetBool("isFrontMove", false);
                break;

            case AnimType.FrontMove:
                _animator.SetBool("isSideMove", false);
                _animator.SetBool("isFrontMove", true);
                break;
        }
        _currentAnimType = next;
    }

    private int MakeRandom()
    {
        if (!_isMove)
        {
            _isMove = true;
            return Random.Range(0, 3);
        }
        else return _random;
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
