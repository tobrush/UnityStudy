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
        RaycastHit hit; // ������ ���� ���

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

    // shootPos.forward�� �������� ������ ���� ���� ����� �Լ�
    Vector3 GetRandomDirectionWithinSpread(Vector3 forward, float spreadAngle)
    {
        // ���� ������ ���� -> ���� ��ȯ
        float spreadRad = spreadAngle * Mathf.Deg2Rad;

        // ���� �ȿ����� ���� ���� �����
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;

        // forward�� ������ ���� �ʵ��� ����
        Quaternion rotationToForward = Quaternion.FromToRotation(Vector3.forward, forward);

        // ���� ���� �� ȸ��
        return rotationToForward * Vector3.Slerp(Vector3.forward, randomDirection, Mathf.Tan(spreadRad));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPos.position, shootPos.forward * 100f);
    }
}
