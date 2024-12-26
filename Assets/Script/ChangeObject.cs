using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject objectAR;
    public Animator animator;

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

                Pengaturan.instance.planetUtuh = false;
                Pengaturan.instance.planetLapisan = true;

                gameObject.SetActive(Pengaturan.instance.planetUtuh);
                objectAR.SetActive(Pengaturan.instance.planetLapisan);
                animator.SetTrigger("move");

                // Cari semua objek bertag "Zoomable"
                Pengaturan.instance.zoomableObjects = GameObject.FindGameObjectsWithTag("Zoomable");

                // Cari semua objek bertag "HideWithZoom"
                Pengaturan.instance.hideableObjects = GameObject.FindGameObjectsWithTag("HideWithZoom");
            }
        }
    }
}
