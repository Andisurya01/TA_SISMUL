using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pengaturan : MonoBehaviour
{
    public static Pengaturan instance;

    private void Awake()
    {
        instance = this;
    }

    public TrackableAr[] tr;
    public string[] nama;
    public string[] information;
    public AudioSource[] audioSources;

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

    void Start()
    {
        checkMarker = new bool[tr.Length];

        isUILapisanActive = false;
        isUIPlanetActive = true;

        planetUtuh = true;
        planetLapisan = false;

        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    void Update()
    {
        for (int i = 0; i < tr.Length; i++)
        {
            if (tr[i].GetMarker())
            {
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

                    audioSources[i].Play();
                }
            }
            else
            {
                nameAndDescriptionTracked();
                if (checkMarker[i])
                {
                    counterMarker--;
                    checkMarker[i] = false;
                    audioSources[i].Stop();
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
            nonTracking.SetActive(true);
            activeMarkerIndex = -1;

            
        }
        else
        {   
            uiPlanetUtuh.SetActive(isUIPlanetActive);
            uiLapisanPlanet.SetActive(isUILapisanActive);
            nonTracking.SetActive(false);
        }
    }
}
