using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pengaturan : MonoBehaviour
{
    public TrackableAr[] tr;
    public string[] name;
    public string[] information;
    public AudioSource[] audioSources;

    public TextMeshProUGUI  PlanetName;
    public TextMeshProUGUI  PlanetInformation;

    public GameObject uiPlanetUtuh;
    public GameObject uiLapisanPlanet;
    public GameObject nonTracking;

    private bool[] checkMarker;
    private int counterMarker;
    private int activeMarkerIndex = -1;
    bool isActive = false;

    void Start()
    {
        checkMarker = new bool[tr.Length];
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
                PlanetName.text = name[i];
                PlanetInformation.text = information[i];
                activeMarkerIndex = i;

                if (!checkMarker[i])
                {
                    counterMarker++;
                    checkMarker[i] = true;

                    audioSources[i].Play();
                }
            }
            else
            {
                if (checkMarker[i])
                {
                    counterMarker--;
                    checkMarker[i] = false;
                    audioSources[i].Stop();
                }
            }
        }
        nameAndDescriptionTracked();
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
            uiPlanetUtuh.SetActive(true);
            uiLapisanPlanet.SetActive(false);
            nonTracking.SetActive(false);
        }
    }
}
