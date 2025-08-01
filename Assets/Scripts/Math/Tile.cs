using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] turretPrefabs;

    public bool BuildTurret = false;


    void OnMouseDown()
    {
        if (!BuildTurret)
        {
            GameObject myTurret = Instantiate(turretPrefabs[SetTile.turretIndex], transform.position, Quaternion.identity);
            BuildTurret = true;
        }
        else
        {
            Debug.Log("Aleady Build");
        }

    }
}