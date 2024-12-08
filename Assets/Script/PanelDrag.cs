using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform panelRect; // Panel yang akan di-drag
    public float smoothSpeed = 10f; // Kecepatan smooth scrolling
    public float minY = -1080f; // Batas bawah panel
    public float maxY = 1080f; // Batas atas panel

    private Vector2 targetPosition; // Posisi target untuk smooth scrolling
    private bool isDragging = false; // Apakah user sedang melakukan drag

    private void Start()
{
    if (panelRect == null)
    {
        panelRect = GetComponent<RectTransform>();
    }

    // Set posisi awal panel sebagai target posisi
    targetPosition = panelRect.anchoredPosition;
}

    private void Update()
    {
        // Jika tidak sedang drag, lakukan smooth transition ke target posisi
        if (!isDragging)
        {
            panelRect.anchoredPosition = Vector2.Lerp(panelRect.anchoredPosition, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true; // Mulai drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Hitung posisi baru berdasarkan input user
        Vector2 newPosition = panelRect.anchoredPosition + new Vector2(0, eventData.delta.y);

        // Batasi posisi panel dengan minY dan maxY
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Update posisi panel langsung (tanpa smooth)
        panelRect.anchoredPosition = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false; // Drag selesai
        // Set target posisi ke posisi terakhir saat drag selesai
        targetPosition = panelRect.anchoredPosition;
    }
}
