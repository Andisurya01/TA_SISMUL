using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform panelToDrag; // Panel yang akan digerakkan
    public float snapThreshold = 200f; // Jarak minimum untuk "snap" ke atas/bawah
    private Vector2 initialPosition;   // Posisi awal panel
    private Vector2 targetPosition;    // Posisi target setelah drag

    private void Start()
    {
        initialPosition = panelToDrag.anchoredPosition;
        targetPosition = initialPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Tidak perlu melakukan apa-apa saat drag dimulai
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update posisi panel selama drag
        Vector2 newPos = panelToDrag.anchoredPosition;
        newPos.y += eventData.delta.y; // Geser posisi berdasarkan input mouse/touch
        panelToDrag.anchoredPosition = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Tentukan apakah panel akan snap ke atas atau kembali ke posisi awal
        float distanceMoved = panelToDrag.anchoredPosition.y - initialPosition.y;

        if (Mathf.Abs(distanceMoved) > snapThreshold)
        {
            // Jika jarak drag cukup besar, snap ke posisi atas
            targetPosition = initialPosition + new Vector2(0, panelToDrag.sizeDelta.y);
        }
        else
        {
            // Kembali ke posisi awal
            targetPosition = initialPosition;
        }

        // Lerp untuk animasi transisi
        StartCoroutine(SmoothMove(panelToDrag.anchoredPosition, targetPosition, 0.25f));
    }

    private System.Collections.IEnumerator SmoothMove(Vector2 start, Vector2 end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            panelToDrag.anchoredPosition = Vector2.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panelToDrag.anchoredPosition = end;
    }
}
