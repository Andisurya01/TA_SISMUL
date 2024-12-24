using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject objectAR;
    public Animator animator;

    private void Start()
    {
        
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
                gameObject.SetActive(false);
                objectAR.SetActive(true);
                animator.SetTrigger("move");
            }
        }
    }

    private void OnObjectClicked()
    {
        /*if (statusObject == true)
        {
            objectAR[0].SetActive(false);
            objectAR[1].SetActive(true);
            statusObject = false;
        }
        else if (statusObject == false)
        {
            objectAR[0].SetActive(true);
            objectAR[1].SetActive(false);
            statusObject = true;
        }*/
    }
}
