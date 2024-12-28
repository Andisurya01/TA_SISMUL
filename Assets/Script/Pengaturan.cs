using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;
public class Pengaturan : MonoBehaviour
{
    public static Pengaturan instance;

    private void Awake()
    {
        instance = this;
    }

    public Material spaceSkybox;

    public TrackableAr[] tr;
    public string[] nama;
    public string[] information;
    public GameObject[] planetUtuhObjekArray;
    public GameObject buttonBack;
    public GameObject buttonBack2;

    public TextMeshProUGUI  PlanetName;
    public TextMeshProUGUI  PlanetInformation;

    //Value Text Lapisan Planet
    public TextMeshProUGUI textNamaLapisan;
    public TextMeshProUGUI textInformasiLapisan;

    //Variabel yang menampung UI
    public GameObject uiPlanetUtuh;
    public GameObject uiLapisanPlanet;
    public GameObject nonTracking;

    private bool[] checkMarker;
    private int counterMarker;
    private int activeMarkerIndex = -1;
    bool isActive = false;

    //Variabel untuk mendeteksi UI yang aktif
    public bool isUILapisanActive;
    public bool isUIPlanetActive;

    //Value dari UI Lapisan Planet
    public string namaLapisan;
    public string informasiLapisan;

    //Untuk mencari semua objek yang bisa di hilangkan dan di zoom
    public GameObject[] zoomableObjects; // Objek yang bisa di-zoom
    public GameObject[] hideableObjects; // Objek yang harus ikut menghilang

    //Untuk deteksi planet utuh/lapisan planet
    public bool planetUtuh;
    public bool planetLapisan;

    //Variabel untuk ARZoomObjek Script
    public GameObject targetObject;
    public Vector3 originalScale;
    public bool isCentering = false; // Apakah sedang memindahkan objek ke tengah
    public bool isZooming = false; // Untuk Mengetahui apa sedang di zoom
    public GameObject planetLapisanObjek;
    SphereCollider sphereCollider;

    //Variabel untuk LeanTouchController
    public GameObject planetUtuhObjek;
    public Animator animator;
    public bool isAnimationActive;

    private bool isSpecialMarkerDetected = false;
    private DevicePoseBehaviour devicePoseBehaviour;

    void Start()
    {
        // Cari DevicePoseBehaviour dari AR Camera
        devicePoseBehaviour = FindObjectOfType<DevicePoseBehaviour>();

        if (devicePoseBehaviour == null)
        {
            Debug.LogError("DevicePoseBehaviour tidak ditemukan. Pastikan AR Camera memiliki komponen DevicePoseBehaviour.");
            return;
        }
        checkMarker = new bool[tr.Length];

        isUILapisanActive = false;
        isUIPlanetActive = true;

        planetUtuh = true;
        planetLapisan = false;
        buttonBack.SetActive(true);

    }

    void Update()
    {
        if (isSpecialMarkerDetected) { return; }
        planetLapisanObjek = GameObject.FindWithTag("PlanetLapisan");
        if (planetLapisanObjek != null )
        {
            animator = planetLapisanObjek.GetComponent<Animator>();
        }
        
        for (int i = 0; i < tr.Length; i++)
        {
            if (tr[i].GetMarker())
            {
                if (tr[9].GetMarker() == true)
                {
                    SetDevicePoseTracking(true);
                    isSpecialMarkerDetected = true;
                }
                instance.planetUtuhObjek = planetUtuhObjekArray[i];
                if (!planetUtuhObjek.activeSelf && planetLapisanObjek == null)
                {
                    planetUtuhObjek.SetActive(true);
                }
                Debug.Log("isi objek: " + Pengaturan.instance.planetUtuhObjek.name);
                if (!planetUtuhObjek.activeSelf )
                {
                    Debug.Log("Objek dinonaktifkan dengan SetActive(false).");
                }

                PlanetName.text = nama[i];
                PlanetInformation.text = information[i];
                activeMarkerIndex = i;

                textNamaLapisan.text = namaLapisan;
                textInformasiLapisan.text = informasiLapisan;

                nameAndDescriptionTracked();

                if (!checkMarker[i])
                {
                    counterMarker++;
                    checkMarker[i] = true;
                }
            }
            else
            {
                nameAndDescriptionTracked();
                if (checkMarker[i])
                {
                    counterMarker--;
                    checkMarker[i] = false;
                }
            }
        }
        
    }

    void nameAndDescriptionTracked()
    {
        if (counterMarker == 0)
        {

            uiPlanetUtuh.SetActive(false);
            uiLapisanPlanet.SetActive(false);
            buttonBack.SetActive(true);
            nonTracking.SetActive(true);
            activeMarkerIndex = -1;

            //Ini Script ResetScene() di ARZoomObjek Dipanggil disini lagi buat reset karna gabisa langsung
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

            planetLapisanObjek = GameObject.FindWithTag("PlanetLapisan");
            if (Pengaturan.instance.planetLapisanObjek != null)
            {
                SphereCollider sphereCollider = Pengaturan.instance.planetLapisanObjek.AddComponent<SphereCollider>();
                sphereCollider.radius = 1.0f;
            }
            //Sampe sini Script ResetScene() di ARZoomObjek Dipanggil disini lagi buat reset karna gabisa langsung

            //Ini scripted OnObjectClicked() dari LeanTouchController Script
            animator.SetTrigger("back");
            isAnimationActive = false;

            Pengaturan.instance.planetUtuh = true;
            Pengaturan.instance.planetLapisan = false;

            if (planetLapisanObjek != null)
            {
                planetLapisanObjek.SetActive(false);
            }

        }
        else
        {   
            uiPlanetUtuh.SetActive(isUIPlanetActive);
            uiLapisanPlanet.SetActive(isUILapisanActive);
            nonTracking.SetActive(false);
            if (isUILapisanActive)
            {
                buttonBack.SetActive(false);
                buttonBack2.SetActive(true);
            } else
            {
                buttonBack.SetActive(true);
                buttonBack2.SetActive(false);
            }
        }
    }

    public void SetDevicePoseTracking(bool enable)
    {
        if (devicePoseBehaviour != null)
        {
            devicePoseBehaviour.enabled = enable;
            Debug.Log($"Device Pose Tracking {(enable ? "diaktifkan" : "dinonaktifkan")}.");
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

        planetLapisanObjek = GameObject.FindWithTag("PlanetLapisan");
        if (Pengaturan.instance.planetLapisanObjek != null)
        {
            SphereCollider sphereCollider = Pengaturan.instance.planetLapisanObjek.AddComponent<SphereCollider>();
            sphereCollider.radius = 1.0f;
        }
        //Sampe sini Script ResetScene() di ARZoomObjek Dipanggil disini lagi buat reset karna gabisa langsung
        buttonBack.SetActive(true);
        buttonBack2.SetActive(false);
    }
}
