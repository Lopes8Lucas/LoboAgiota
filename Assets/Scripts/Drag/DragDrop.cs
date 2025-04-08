using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Outline outline;
    private RectTransform container;


    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
        if (canvas != null)
        {
            Debug.Log("Canvas found");
        }
        else
        {
            Debug.LogWarning("Canvas not found");
        }

        GameObject containerGO = GameObject.FindGameObjectWithTag("Container");
        if (containerGO != null)
        {
            container = containerGO.GetComponent<RectTransform>();
        }

        rectTransform = GetComponent<RectTransform>();

        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        if (outline != null)
        {
            outline.effectColor = Color.white;
        }

        outline.enabled = false;

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        Clamping();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointerdown");
    }

    private void Clamping()
    {
        Vector3[] containerCorners = new Vector3[4];
        container.GetWorldCorners(containerCorners);

        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        Vector3 offset = Vector3.zero;

        // Check each corner and compute how far it's outside the container
        for (int i = 0; i < 4; i++)
        {
            Vector3 objectCorner = objectCorners[i];
            Vector3 containerMin = containerCorners[0];
            Vector3 containerMax = containerCorners[2];

            float xDiff = 0f;
            float yDiff = 0f;

            if (objectCorner.x < containerMin.x)
                xDiff = containerMin.x - objectCorner.x;
            else if (objectCorner.x > containerMax.x)
                xDiff = containerMax.x - objectCorner.x;

            if (objectCorner.y < containerMin.y)
                yDiff = containerMin.y - objectCorner.y;
            else if (objectCorner.y > containerMax.y)
                yDiff = containerMax.y - objectCorner.y;

            offset.x = Mathf.Abs(xDiff) > Mathf.Abs(offset.x) ? xDiff : offset.x;
            offset.y = Mathf.Abs(yDiff) > Mathf.Abs(offset.y) ? yDiff : offset.y;
        }

        // Convert world offset to local position change
        Vector2 localOffset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            rectTransform.position + offset,
            canvas.worldCamera,
            out localOffset
        );

        rectTransform.anchoredPosition += localOffset - rectTransform.anchoredPosition;
    }
}
