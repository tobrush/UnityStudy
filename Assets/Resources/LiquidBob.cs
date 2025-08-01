using System.Collections.Generic;
using UnityEngine;

public class LiquidBob : MonoBehaviour
{
    Renderer render;

    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;

    [Range(0f, 0.1f)]
    public float maxBob = 0.03f;

    [Range(0f, 2f)]
    public float bobSpeed = 1f;

    [Range(0f, 2f)]
    public float recovery = 1f;

    float bobAmountX;
    float bobAmountZ;
    float bobAmountAddX;
    float bobAmountAddZ;

    float pulse;

    [Range(0f, 1f)]
    public float time = 0.5f;

    private void OnValidate()
    {
        render = GetComponent<Renderer>();
    }

    void OnDrawGizmos()
    {
        time += Time.deltaTime;

        bobAmountAddX = Mathf.Lerp(bobAmountAddX, 0, Time.deltaTime * (recovery));
        bobAmountAddZ = Mathf.Lerp(bobAmountAddZ, 0, Time.deltaTime * (recovery));

        // make a sine wave of the decreasing bob
        pulse = 2 * Mathf.PI * bobSpeed;
        bobAmountX = bobAmountAddX * Mathf.Sin(pulse * time);
        bobAmountZ = bobAmountAddZ * Mathf.Sin(pulse * time);

        // assign material properties
        render.sharedMaterial.SetFloat("_BobX", bobAmountX);
        render.sharedMaterial.SetFloat("_BobZ", bobAmountZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;

        // add velocity to bob variables
        bobAmountAddX += Mathf.Clamp(velocity.x + (angularVelocity.z * 0.2f), -maxBob, maxBob);
        bobAmountAddZ += Mathf.Clamp(velocity.z + (angularVelocity.x * 0.2f), -maxBob, maxBob);

        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }
}