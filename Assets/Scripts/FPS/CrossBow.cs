using System;
using System.Collections;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPos;
    public bool isShoot;
    public float spreadAngle = 5f;

    void Update()
    {
        Ray ray = new Ray(shootPos.position, shootPos.forward);
        RaycastHit hit; // 레이저 닿은 대상

        bool isTargeting = Physics.Raycast(ray, out hit);

        if (isTargeting && !isShoot)
            StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        isShoot = true;

        Vector3 direction = GetRandomDirectionWithinSpread(shootPos.forward, spreadAngle);

        Quaternion baseRotation = Quaternion.LookRotation(direction);
        Quaternion modelCorrection = Quaternion.Euler(90f, 0f, 0f);

        Quaternion finalRot = baseRotation * modelCorrection;

        GameObject arrow = Instantiate(arrowPrefab, transform);
        arrow.transform.SetPositionAndRotation(shootPos.position, finalRot);

        yield return new WaitForSeconds(3f);
        isShoot = false;
    }

    // shootPos.forward를 기준으로 퍼지는 랜덤 방향 만들기 함수
    Vector3 GetRandomDirectionWithinSpread(Vector3 forward, float spreadAngle)
    {
        // 원뿔 반지름 각도 -> 라디안 변환
        float spreadRad = spreadAngle * Mathf.Deg2Rad;

        // 원뿔 안에서의 랜덤 방향 만들기
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;

        // forward와 완전히 같지 않도록 보정
        Quaternion rotationToForward = Quaternion.FromToRotation(Vector3.forward, forward);

        // 방향 보정 후 회전
        return rotationToForward * Vector3.Slerp(Vector3.forward, randomDirection, Mathf.Tan(spreadRad));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPos.position, shootPos.forward * 100f);
    }
}
