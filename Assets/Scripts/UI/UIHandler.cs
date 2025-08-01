using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform parentRect;

    private Vector2 baseRect;
    private Vector2 startRect;
    private Vector2 currentRect;

    private Vector2 minAnchor, maxAnchor, anchorPos, deltaSize;

    private float timer;
    private float doubleClickedTime = 0.3f;
    private bool isDoubleClicked = false;
    private bool isFullScreen = false;

    private void Awake()
    {
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isDoubleClicked)
        {
            timer += Time.deltaTime;
            if (timer >= doubleClickedTime)
            {
                timer = 0f;
                isDoubleClicked = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //parentRect.transform.SetAsFirstSibling(); // 맨뒤로 그려짐
        parentRect.transform.SetAsLastSibling(); // 맨앞으로 그려짐

        if (!isDoubleClicked)
            isDoubleClicked = true;
        else
            SetFullScreen();

        if (isFullScreen)
            return;

        baseRect = parentRect.anchoredPosition;
        startRect = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (isFullScreen)
            return;

        currentRect = eventData.position;
        var offset = currentRect - startRect;
        parentRect.anchoredPosition = baseRect + offset;
    }

    public void SetFullScreen()
    {
        if (!isFullScreen)
        {
            minAnchor = parentRect.anchorMin;
            maxAnchor = parentRect.anchorMax;
            anchorPos = parentRect.anchoredPosition;
            deltaSize = parentRect.sizeDelta;

            parentRect.anchorMin = Vector2.zero;
            parentRect.anchorMax = Vector2.one;
            parentRect.anchoredPosition = Vector2.zero;
            parentRect.sizeDelta = Vector2.zero;
        }
        else
        {
            parentRect.anchorMin = minAnchor;
            parentRect.anchorMax = maxAnchor;
            parentRect.anchoredPosition = anchorPos;
            parentRect.sizeDelta = deltaSize;
        }

        isFullScreen = !isFullScreen;
    }
}