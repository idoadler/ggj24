using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour
{
    const int DIR_NUM = 8;
    const float STEP_LEN = 10;
    const float TARGET_DIST = 2;

    public CharScript dog;

    private NavMeshAgent _agent;
    private Vector3 _target;
    private float _lastCatchTime; 
    [HideInInspector] public bool running = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _target = RunAwayPosition();
        _agent.SetDestination(_target);
    }
    
    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = dog.transform.position.x < transform.position.x;
        var elapsed = Time.time - _lastCatchTime;
        if (running)
        {
            if (Vector2.Distance(transform.position, dog.transform.position) < TARGET_DIST ||
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
            if (elapsed > GameManager.WAIT_TIME)
            {
                _lastCatchTime = Time.time;
                running = true;
                _target = RunAwayPosition();
            }
            else if (elapsed > GameManager.WAIT_TIME - 1)
            {
                _target = RunAwayPosition();
            }
            else
            {
                _target = dog.transform.position;
            }
            _agent.SetDestination(_target);
        }
    }

    Vector3 RunAwayPosition()
    {
        Vector3 dirToPlayer = transform.position - dog.transform.position;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_target, 0.5f);
        Vector3 dirToPlayer = transform.position - dog.transform.position;
        var r = transform.position + Quaternion.AngleAxis(0, Vector3.up) * dirToPlayer.normalized*STEP_LEN;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, r);
        if (PointInRoom(r)) return;
        for (int i = 1; i < DIR_NUM; i++)
        {
            var run_away = transform.position + Quaternion.AngleAxis(180f/DIR_NUM*i, Vector3.forward) * dirToPlayer.normalized*STEP_LEN;
            Gizmos.color = PointInRoom(run_away) ? Color.blue : Color.red;
            Gizmos.DrawLine(transform.position, run_away);

            run_away = transform.position + Quaternion.AngleAxis(-180f/DIR_NUM*i, Vector3.forward) * dirToPlayer.normalized*STEP_LEN;
            Gizmos.color = PointInRoom(run_away) ? Color.cyan : Color.magenta;
            Gizmos.DrawLine(transform.position, run_away);

        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (running)
        {
            GetComponent<NavMeshAgent>().speed = dog.runSpeed * 1.3f;
            running = false;
            _lastCatchTime = Time.time;
        }
    }
}
