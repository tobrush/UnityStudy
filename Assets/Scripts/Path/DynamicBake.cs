using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DynamicBake : MonoBehaviour
{
    public NavMeshSurface surface;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        surface.transform.position = transform.position;
        surface.BuildNavMesh();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        agent.SetDestination(transform.position + dir);

        if (Vector3.Distance(transform.position, surface.transform.position) > 13f)
        {
            surface.transform.position = transform.position;
            surface.BuildNavMesh();
        }
    }
}