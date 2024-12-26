using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZoomObjek : MonoBehaviour
{
    public Camera arCamera; // Kamera AR (Vuforia ARCamera)
    public float moveSpeed = 5f; // Kecepatan memindahkan objek ke tengah
    public float scaleFactor = 2f; // Faktor pembesaran objek

    private bool isCentering = false; // Apakah sedang memindahkan objek ke tengah
    private bool isZooming = false; // Untuk Mengetahui apa sedang di zoom
    private Vector3 targetPosition; // Posisi target untuk objek
    private Vector3 originalScale; // Skala awal objek
    private GameObject targetObject; // Objek yang dipilih untuk di-zoom
    //private GameObject[] zoomableObjects; // Objek yang bisa di-zoom
    //private GameObject[] hideableObjects; // Objek yang harus ikut menghilang

    

    void Update()
    {
        if (isZooming) { return; }
        // Periksa klik pada layar
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Periksa apakah Ray mengenai objek
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Zoomable"))
                {
                    // Tetapkan objek yang diklik sebagai target
                    targetObject = hit.collider.gameObject;

                    Pengaturan.instance.isUILapisanActive = true;
                    Pengaturan.instance.isUIPlanetActive = false;
                    Debug.Log("Objek diklik: " + targetObject.name);

                    Pengaturan.instance.namaLapisan = targetObject.GetComponent<LapisanPlanet>().namaLapisan;
                    Pengaturan.instance.informasiLapisan = targetObject.GetComponent<LapisanPlanet>().informasiLapisan;

                    // Simpan skala awal objek
                    originalScale = targetObject.transform.localScale;

                    // Hitung posisi target di tengah layar (depan kamera)
                    targetPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f;

                    // Sembunyikan semua objek bertag "Zoomable" kecuali target
                    foreach (GameObject obj in Pengaturan.instance.zoomableObjects)
                    {
                        if (obj != targetObject)
                        {
                            obj.SetActive(false);
                        }
                    }

                    // Sembunyikan semua objek bertag "HideWithZoom"
                    foreach (GameObject obj in Pengaturan.instance.hideableObjects)
                    {
                        obj.SetActive(false);
                    }

                    // Aktifkan proses centering
                    isCentering = true;
                    isZooming = true;
                }
            }
        }

        // Proses memindahkan objek ke tengah
        if (isCentering && targetObject != null)
        {
            // Pindahkan objek ke target posisi secara halus
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Perbesar objek secara proporsional
            targetObject.transform.localScale = Vector3.Lerp(targetObject.transform.localScale, originalScale * scaleFactor, Time.deltaTime * moveSpeed);

            // Periksa jika objek sudah di posisi target
            if (Vector3.Distance(targetObject.transform.position, targetPosition) < 0.01f)
            {
                isCentering = false; // Hentikan centering
            }
        }
    }

    public void ResetScene()
    {
        // Tampilkan kembali semua objek bertag "Zoomable"
        foreach (GameObject obj in Pengaturan.instance.zoomableObjects)
        {
            obj.SetActive(true);
        }

        // Tampilkan kembali semua objek bertag "HideWithZoom"
        foreach (GameObject obj in Pengaturan.instance.hideableObjects)
        {
            obj.SetActive(true);
        }

        // Reset skala objek target
        if (targetObject != null)
        {
            targetObject.transform.localScale = originalScale;
        }

        // Hentikan centering
        isCentering = false;
        targetObject = null;
        isZooming = false;

        Pengaturan.instance.isUILapisanActive = false;
        Pengaturan.instance.isUIPlanetActive = true;
    }
}
