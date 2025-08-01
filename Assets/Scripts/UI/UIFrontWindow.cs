using UnityEngine;
using UnityEngine.EventSystems;

public class UIFrontWindow : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
