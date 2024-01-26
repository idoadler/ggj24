using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour
{
    public GameObject dog;
    public GameObject[] absoluteDirs;

    private NavMeshAgent _agent;
    private GameObject _lastTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirToPlayer = transform.position - dog.transform.position;

        Vector3 maxPoint = Vector3.zero;
        GameObject maxObject = null;
        var run_away = transform.position + dirToPlayer;
        _agent.SetDestination(run_away);
        return;
        if (PointInRoom(run_away))
        {
            maxPoint = run_away;
            maxObject = dog;
        }
        else
        {
            float maxDist = 100000;
            foreach (var dir in absoluteDirs)
            {
                var pos = dir.transform.position;
                var newDist = Vector3.Distance(pos, dog.transform.position);
                if (maxDist > newDist)
                {
                    maxDist = newDist;
                    maxPoint = pos;
                    maxObject = dir;
                }
            }
        }

        if (maxObject != _lastTarget)
        {
            _lastTarget = maxObject;
            print("Target: "+_lastTarget.name);
        }
        _agent.SetDestination(maxPoint);
    }
    
    bool PointInRoom(Vector2 point)
    {
        const float range = 1f;
        NavMeshHit hit;
        var res = NavMesh.SamplePosition(point, out hit, range, NavMesh.AllAreas);
        print("hit("+point+"):" +res + "=" + hit.position);
        return res;
    }
}
