using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
public class LeanTouchController : MonoBehaviour
{
    public Animator animator;
    bool isAnimationActive;

    private void Start()
    {
        isAnimationActive = false;
    }
    private void OnEnable()
    {
        // Menambahkan event untuk mendeteksi klik pada objek
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    private void OnDisable()
    {
        // Menghapus event saat objek dinonaktifkan
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        // Raycast untuk mendeteksi apakah sentuhan mengenai objek
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Objek diklik: " + gameObject.name);
                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        if (!isAnimationActive)
        {
            animator.SetTrigger("move");
            isAnimationActive = true;
        }
        else if (isAnimationActive)
        {
            animator.SetTrigger("back");
            isAnimationActive = false;
        }
    }
}
