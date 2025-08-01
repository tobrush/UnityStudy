using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public Transform eventCheckOrigin;
    public float eventCheckRadius = 0.1f;
    public LayerMask eventLayer;

    public bool evented;
    public bool checkOneTime;
    public bool checkOneTime2;
    public enum InteractionType { SIGN, DOOR, NPC }
    public InteractionType type;

    public GameObject signPopup;

    public Transform doorCheckOrigin;
    public float doorCheckRadius = 0.1f;
    public bool inDoor;



    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interaction(other.transform);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<TownMovement>().enabled = false;

            StartCoroutine(DoorRoutine(other.transform));
        }
    }*/

    bool CheckEvent()
    {
        return Physics2D.OverlapCircle(eventCheckOrigin.position, eventCheckRadius, eventLayer);
    }

    bool CheckDoor()
    {
        return Physics2D.OverlapCircle(doorCheckOrigin.position, doorCheckRadius, eventLayer);
    }

    private void Start()
    {
        signPopup.SetActive(false);
    }

    private void Update()
    {
        evented = CheckEvent();
        if (evented && !checkOneTime)
        {
            signPopup.SetActive(true);
            this.gameObject.GetComponent<TypingText>().EventStart();
            checkOneTime = true;
        }

        if (!evented)
        {
            checkOneTime = false;
        }

        if (!inDoor)
        {
            checkOneTime2 = false;
        }

        inDoor = CheckDoor();
        if (inDoor && !checkOneTime2)
        {
            StartCoroutine(DoorRoutine());
            Debug.Log("wh");
             checkOneTime2 = true;
        }


    }

    /*
    void Interaction(Transform player)
    {
        switch (type)
        {
            case InteractionType.SIGN:
                signPopup.SetActive(true);
                break;
            case InteractionType.DOOR:

                break;
            case InteractionType.NPC:

                break;
        }
    }
  */
    public FadeRoutine fade;
    public Vector3 houseInPos;
    public Vector3 houseOutPos;

    public GameObject house;
    public GameObject env;

    public bool isHouse = true;


    IEnumerator DoorRoutine()
    {
       // yield return StartCoroutine(fade.Fade(3f, Color.black, true));
        /*
        if (isHouse)
            other.transform.position = houseInPos;
        else
            other.transform.position = houseOutPos;
        */
        house.SetActive(!house.activeSelf);
      //  env.SetActive(!env.activeSelf);
        isHouse = !isHouse;
        yield return null;
      //  yield return StartCoroutine(fade.Fade(3f, Color.clear, false));
       // other.GetComponent<TownMovement>().enabled = true;
    }
    
    void OnDrawGizmos()
    {
        if (eventCheckOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(eventCheckOrigin.position, eventCheckRadius);
        }

        if (doorCheckOrigin != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(doorCheckOrigin.position, doorCheckRadius);
        }
    }
}