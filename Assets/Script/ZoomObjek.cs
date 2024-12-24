using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomObjek : MonoBehaviour
{
    public Camera mainCamera; // Kamera utama
    public float zoomSpeed = 2f; // Kecepatan zoom
    public Vector3 zoomOffset = new Vector3(0, 0, -5); // Jarak zoom

    private bool isZooming = false; // Apakah sedang zoom
    private Vector3 originalPosition; // Posisi awal kamera
    private GameObject[] zoomableObjects; // Semua objek yang bisa di-zoom
    private GameObject[] hideableObjects; // Semua objek yang harus ikut menghilang
    private GameObject targetObject; // Objek yang dipilih untuk di-zoom

    void Start()
    {
        // Simpan posisi awal kamera
        originalPosition = mainCamera.transform.position;

        // Cari semua objek bertag "Zoomable"
        zoomableObjects = GameObject.FindGameObjectsWithTag("Zoomable");

        // Cari semua objek bertag "HideWithZoom"
        hideableObjects = GameObject.FindGameObjectsWithTag("HideWithZoom");
    }

    void Update()
    {
        // Periksa jika pengguna mengklik layar
        if (Input.GetMouseButtonDown(0))
        {
            // Buat Ray dari posisi kamera ke titik klik
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Periksa apakah Ray mengenai objek
            if (Physics.Raycast(ray, out hit))
            {
                // Jika objek memiliki tag "Zoomable"
                if (hit.collider.gameObject.CompareTag("Zoomable"))
                {
                    // Tetapkan objek yang diklik sebagai target
                    targetObject = hit.collider.gameObject;

                    // Sembunyikan semua objek bertag "Zoomable" kecuali target
                    foreach (GameObject obj in zoomableObjects)
                    {
                        if (obj != targetObject)
                        {
                            obj.SetActive(false);
                        }
                    }

                    // Sembunyikan semua objek bertag "HideWithZoom"
                    foreach (GameObject obj in hideableObjects)
                    {
                        obj.SetActive(false);
                    }

                    // Aktifkan zoom
                    isZooming = true;
                }
            }
        }

        // Proses zoom kamera
        if (isZooming && targetObject != null)
        {
            // Hitung posisi target kamera
            Vector3 targetPosition = targetObject.transform.position + zoomOffset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);

            // Periksa jika kamera sudah dekat dengan target
            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.1f)
            {
                isZooming = false; // Hentikan zoom
            }
        }
    }

    public void ResetCamera()
    {
        // Reset posisi kamera ke posisi awal
        mainCamera.transform.position = originalPosition;

        // Tampilkan kembali semua objek bertag "Zoomable"
        foreach (GameObject obj in zoomableObjects)
        {
            obj.SetActive(true);
        }

        // Tampilkan kembali semua objek bertag "HideWithZoom"
        foreach (GameObject obj in hideableObjects)
        {
            obj.SetActive(true);
        }

        // Hentikan zoom
        isZooming = false;
        targetObject = null;
    }
}
