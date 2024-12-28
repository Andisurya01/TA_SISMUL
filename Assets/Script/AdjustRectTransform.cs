using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustRectTransform : MonoBehaviour
{
    public RectTransform targetRect; // RectTransform yang akan dimodifikasi

    float screenWidth = 1920;
    float screenHeight = 1080;

    private void Start()
    {
        if (targetRect == null)
        {
            Debug.LogError("RectTransform belum diatur!");
            return;
        }


        AdjustSize();
    }

    private void AdjustSize()
    {
        // Ambil resolusi layar saat ini
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Sesuaikan width dan height RectTransform
        targetRect.sizeDelta = new Vector2(screenWidth, screenHeight);

        targetRect.anchorMin = new Vector2(0.5f, 0.5f);
        targetRect.anchorMax = new Vector2(0.5f, 0.5f);
        targetRect.pivot = new Vector2(0.5f, 0.5f);

        Debug.Log($"New RectTransform Size: {targetRect.sizeDelta}");
    }

}
