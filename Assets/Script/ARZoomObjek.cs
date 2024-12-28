using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZoomObjek : MonoBehaviour
{
    public Camera arCamera; // Kamera AR (Vuforia ARCamera)
    public float moveSpeed = 5f; // Kecepatan memindahkan objek ke tengah
    public float scaleFactor = 2f; // Faktor pembesaran objek

    //public bool isCentering = false; // Apakah sedang memindahkan objek ke tengah
    //public bool isZooming = false; // Untuk Mengetahui apa sedang di zoom
    private Vector3 targetPosition; // Posisi target untuk objek
    //private Vector3 originalScale; // Skala awal objek
    //private GameObject targetObject; // Objek yang dipilih untuk di-zoom

    //GameObject planetLapisan;

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
        if (Pengaturan.instance.isZooming) { return; }

        Pengaturan.instance.planetLapisanObjek = GameObject.FindWithTag("PlanetLapisan");
        // Raycast untuk mendeteksi apakah sentuhan mengenai objek
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Zoomable"))
            {
                Debug.Log("Objek diklik: " + gameObject.name);
                // Tetapkan objek yang diklik sebagai target
                Pengaturan.instance.targetObject = hit.collider.gameObject;

                Pengaturan.instance.isUILapisanActive = true;
                Pengaturan.instance.isUIPlanetActive = false;
                Debug.Log("Objek diklik: " + Pengaturan.instance.targetObject.name);

                Pengaturan.instance.namaLapisan = Pengaturan.instance.targetObject.GetComponent<LapisanPlanet>().namaLapisan;
                Pengaturan.instance.informasiLapisan = Pengaturan.instance.targetObject.GetComponent<LapisanPlanet>().informasiLapisan;

                //untuk menghapus collider dari lapisan planet
                Collider planetLapisanCollider = Pengaturan.instance.planetLapisanObjek.GetComponent<Collider>();
                Destroy(planetLapisanCollider);

                // Simpan skala awal objek
                Pengaturan.instance.originalScale = Pengaturan.instance.targetObject.transform.localScale;

                // Hitung posisi target di tengah layar (depan kamera)
                targetPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f;

                // Sembunyikan semua objek bertag "Zoomable" kecuali target
                foreach (GameObject obj in Pengaturan.instance.zoomableObjects)
                {
                    if (obj != Pengaturan.instance.targetObject)
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
                Pengaturan.instance.isCentering = true;
                Pengaturan.instance.isZooming = true;
            }
        }
        // Proses memindahkan objek ke tengah
        if (Pengaturan.instance.isCentering && Pengaturan.instance.targetObject != null)
        {
            // Pindahkan objek ke target posisi secara halus
            Pengaturan.instance.targetObject.transform.position = Vector3.Lerp(Pengaturan.instance.targetObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Perbesar objek secara proporsional
            Pengaturan.instance.targetObject.transform.localScale = Vector3.Lerp(Pengaturan.instance.targetObject.transform.localScale, Pengaturan.instance.originalScale * scaleFactor, Time.deltaTime * moveSpeed);

            // Periksa jika objek sudah di posisi target
            if (Vector3.Distance(Pengaturan.instance.targetObject.transform.position, targetPosition) < 0.01f)
            {
                Pengaturan.instance.isCentering = false; // Hentikan centering
            }
        }
    }

    /*void Update()
    {
        if (Pengaturan.instance.isZooming) { return; }

        Pengaturan.instance.planetLapisanObjek = GameObject.FindWithTag("PlanetLapisan");

        // Periksa klik pada layar
        


    }*/

    
}
