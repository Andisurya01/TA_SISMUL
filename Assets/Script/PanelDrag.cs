using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform panelRect; // Panel yang akan di-drag
    public float smoothSpeed = 10f; // Kecepatan smooth scrolling

    private Vector2 targetPosition; // Posisi target untuk smooth scrolling
    private bool isDragging = false; // Apakah user sedang melakukan drag

    private float minY; // Batas bawah panel (dinamis)
    private float maxY; // Batas atas panel (dinamis)

    private void Start()
    {
        if (panelRect == null)
        {
            panelRect = GetComponent<RectTransform>();
        }

        // Hitung batas awal
        CalculateBounds();

        // Set posisi awal panel sebagai target posisi
        targetPosition = panelRect.anchoredPosition;
        Debug.Log($"minY: {minY}, maxY: {maxY}, screenHeight: {Screen.height}, panelHeight: {panelRect.rect.height}");
    }

    private void Update()
    {
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

    private void CalculateBounds()
    {
        float screenHeight = Screen.height; // Tinggi layar dalam piksel
        float panelHeight = panelRect.rect.height; // Tinggi panel dalam RectTransform

        // Perhitungan batas dinamis
        minY = -panelHeight + screenHeight * 0.2f; // Biarkan 20% panel terlihat di bawah
        maxY = screenHeight * 1.5f; // Biarkan panel ditarik maksimum hingga tengah layar
    }
}
