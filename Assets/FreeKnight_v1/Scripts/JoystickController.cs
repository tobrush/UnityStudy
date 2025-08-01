using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private GameObject backgroundUI;
    [SerializeField] private GameObject handlerUI;

    [SerializeField] private float joystickRadius = 50f;
    public Vector2 originPos , movePos;

    public Transform ButtonsPanel;
    public bool UseJoystick = false;

    

    public void Start()
    {
        backgroundUI.SetActive(false);
    }

    public void Update()
    {
        if(!UseJoystick)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            ButtonsPanel.gameObject.SetActive(false);


        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            ButtonsPanel.gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundUI.SetActive(true);
        originPos = eventData.position;

        backgroundUI.transform.position = originPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - originPos;
        float distance = Mathf.Min(direction.magnitude, joystickRadius);
        Vector2 clampedDirection = direction.normalized * distance;

        handlerUI.transform.position = originPos + clampedDirection;
        movePos = clampedDirection / joystickRadius;

        // knightController.InputJoystick(dragDir.x, dragDir.y);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundUI.SetActive(false);
        handlerUI.transform.localPosition = Vector3.zero;
        movePos = Vector3.zero;
    }

}
