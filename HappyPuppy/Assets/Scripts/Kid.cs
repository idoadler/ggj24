using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour
{
    public static Kid Instance;
    
    const int DIR_NUM = 8;
    const float STEP_LEN = 10;
    const float TARGET_DIST = 2;

    private CharScript _dog;
    private NavMeshAgent _agent;
    private Vector3 _target;
    private float _lastCatchTime; 
    [HideInInspector] public bool running = true;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _dog = CharScript.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _target = RunAwayPosition();
        _agent.SetDestination(_target);
    }
    
    // Update is called once per frame
    void Update()
    {
        var elapsed = Time.time - _lastCatchTime;
        GetComponent<AudioSource>().volume = running ? 0.1f : 0.8f;
        if (running)
        {
            if (Vector2.Distance(_dog.transform.position, transform.position) < 15)
                GetComponent<SpriteRenderer>().flipX = _dog.transform.position.x > transform.position.x;
            else
                GetComponent<SpriteRenderer>().flipX = _dog.transform.position.x < transform.position.x;

            if (Vector2.Distance(transform.position, _dog.transform.position) < TARGET_DIST ||
                !PointInRoom(_target) || Vector2.Distance(transform.position, _target) < TARGET_DIST)
            {
                _target = RunAwayPosition();
                _agent.SetDestination(_target);
            }

            if (elapsed > GameManager.SLOW_WAIT_TIME && GetComponent<NavMeshAgent>().speed > 2)
            {
                GetComponent<NavMeshAgent>().speed *= 0.8f;
                _lastCatchTime = Time.time;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = _dog.transform.position.x < transform.position.x;
            if (elapsed > GameManager.WAIT_TIME)
            {
                _lastCatchTime = Time.time;
                running = true;
                _target = RunAwayPosition();
            }
            else if (elapsed > GameManager.WAIT_TIME - 1)
            {
                GetComponent<Animator>().SetBool("running",true);
                _target = RunAwayPosition();
            }
            else
            {
                GetComponent<Animator>().SetBool("running",false);
                _target = _dog.transform.position;
            }
            _agent.SetDestination(_target);
        }
    }

    Vector3 RunAwayPosition()
    {
        Vector3 dirToPlayer = transform.position - _dog.transform.position;
        var run_away = transform.position + dirToPlayer.normalized*STEP_LEN;
        if (PointInRoom(run_away)) return run_away;
        for (int i = 1; i < DIR_NUM; i++)
        {
            run_away = transform.position + Quaternion.AngleAxis(180f/DIR_NUM*i, Vector3.forward) * dirToPlayer.normalized*STEP_LEN;
            if (PointInRoom(run_away)) return run_away;

            run_away = transform.position + Quaternion.AngleAxis(-180f/DIR_NUM*i, Vector3.forward) * dirToPlayer.normalized*STEP_LEN;
            if (PointInRoom(run_away)) return run_away;
        }

        return (transform.position + dirToPlayer.normalized * STEP_LEN);
    }
    
    bool PointInRoom(Vector2 point)
    {
        return NavMesh.SamplePosition(point, out _, 1, NavMesh.AllAreas);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (running)
        {
            GetComponent<NavMeshAgent>().speed = _dog.runSpeed * 1.3f;
            running = false;
            _lastCatchTime = Time.time;
        }
    }
}
