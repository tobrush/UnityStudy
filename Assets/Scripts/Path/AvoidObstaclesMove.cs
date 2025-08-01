using UnityEngine;

public class AvoidObstaclesMove : MonoBehaviour
{
    public float speed = 10f;
    public float mass = 5f;
    public float force = 50f;
    public float minDistToAvoid = 5f;

    private float curSpeed;
    private Vector3 targetPoint;
    public float steeringForce = 10f;

    void Start()
    {
        targetPoint = Vector3.zero;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                targetPoint = hit.point; // 마우스 클릭한 곳을 목표 지점으로 설정
        }

        Vector3 dir = targetPoint - transform.position;
        dir.Normalize();

        dir = GetAvoidanceDirection(dir); // 장애물이 없으면 그대로 / 있으면 변경된 방향이 적용

        if (Vector3.Distance(targetPoint, transform.position) < 1f)
            return;

        curSpeed = speed * Time.deltaTime;
        transform.position += transform.forward * curSpeed;

        Quaternion rot = Quaternion.LookRotation(dir); // 방향을 알려주는 벡터를 넣으면 그 방향을 보는 값을 설정
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, steeringForce * Time.deltaTime);


    }

    // 이동 방향에 장애물이 있을 경우 이동하려는 방향을 바꾸는 기능
    public Vector3 GetAvoidanceDirection(Vector3 dir)
    {
        RaycastHit hit;
        int layerMask = 1 << 15;
        if (Physics.Raycast(transform.position, transform.forward, out hit, minDistToAvoid, layerMask))
        {
            Vector3 hitNormal = hit.normal;
            hitNormal.y = 0f;

            dir = transform.forward + hitNormal * force;
            dir.Normalize();
        }

        return dir;
    }
}