using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour
{
    public GameObject dog;

    private NavMeshAgent _agent;
    
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
        _agent.SetDestination(dog.transform.position);
    }
}
