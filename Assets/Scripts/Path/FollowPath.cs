using System.IO;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public RootPath path;
    public float speed = 5f;
    public float mass = 5f;
    public bool isLooping = true;

    public float curSpeed;
    public int curPathIndex;
    public int pathLength;
    public Vector3 targetPoint;

    public Vector3 velocity;

    void Start()
    {
        pathLength = path.points.Length;
        curPathIndex = 0;

        velocity = transform.forward;
    }

    void Update()
    {
        curSpeed = speed * Time.deltaTime;
        targetPoint = path.GetPoint(curPathIndex);

        // 목적지에 거의 도착하면 다음 목적지로 설정하는 기능
        if (Vector3.Distance(transform.position, targetPoint) < path.radius)
        {
            if (curPathIndex < pathLength - 1)
            {
                curPathIndex++;
                Debug.Log("Next Point");
            }
            else if (isLooping)
                curPathIndex = 0;
            else
                return;
        }

        if (curPathIndex >= pathLength)
            return;

        if (curPathIndex >= pathLength - 1 && !isLooping) // 방향 전환
            velocity += Steer(targetPoint, true);
        else
            velocity += Steer(targetPoint);

        transform.position += velocity;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    public Vector3 Steer(Vector3 target, bool isFinalPoint = false)
    {
        Vector3 targetDir = target - transform.position;
        float dist = targetDir.magnitude;

        // targetDir = targetDir.normalized;
        targetDir.Normalize();

        if (isFinalPoint && dist < 10f)
            targetDir *= curSpeed * (dist / 10f);
        else
            targetDir *= curSpeed;

        Vector3 steeringForce = targetDir - velocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }

}