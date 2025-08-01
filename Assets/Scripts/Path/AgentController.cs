using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public Camera camera;

    private Transform player;
    private NavMeshAgent agent;

    public Transform[] points;
    public int index;


    public NavMeshSurface surface;

    public bool RandomMove = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = Random.Range(0, points.Length);

        surface.transform.position = agent.transform.position;
        surface.BuildNavMesh();
    }

    void Update()
    {
        if (RandomMove)
        {
            SetRandomPoint();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    agent.SetDestination(hit.point);
                }
            }

            if (Vector3.Distance(transform.position, surface.transform.position) > 20f)
            {
                surface.transform.position = agent.transform.position;
                surface.BuildNavMesh();
            }
        }




    }


    void SetRandomPoint()
    {
        agent.SetDestination(points[index].position);

        if (agent.remainingDistance <= 1.5f) // 목적지와의 거리가 1.5 이하일 경우
        {
            Debug.Log("목적지 변경");

            int temp = index;
            index = Random.Range(0, points.Length);

            if (temp == index)
                index = Random.Range(0, points.Length);
        }
    }
}