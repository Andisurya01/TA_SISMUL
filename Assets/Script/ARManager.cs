using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    public static ARManager instance;

    private void Awake()
    {
        instance = this;
    }

    public TrackableAr[] tr;

    //UI Variabel
    public GameObject UIPlanetUtuh;
    public GameObject UILapisanPlanet;
    public GameObject nonTracking;

    //variabel detect active UI
    public bool isUIPlanetActive;
    public bool isUILapisanActive;

    private int counterMarker;
    private bool[] checkMarker;

    private void Start()
    {
        checkMarker = new bool[tr.Length];

        isUIPlanetActive = true;

        UIPlanetUtuh.SetActive(false);
        UILapisanPlanet.SetActive(false);
        nonTracking.SetActive(true);
    }

    private void Update()
    {
        for (int i = 0; i < tr.Length; i++)
        {
            if (tr[i].GetMarker())
            {
                UIPlanetUtuh.SetActive (isUIPlanetActive);
                UILapisanPlanet.SetActive(isUILapisanActive);
                nonTracking.SetActive(false);

                if (!checkMarker[i])
                {
                    counterMarker++;
                    checkMarker[i] = true;
                }
            }
            else
            {
                UIPlanetUtuh.SetActive(false);
                UILapisanPlanet.SetActive(false);
                nonTracking.SetActive(true);
                if (checkMarker[i])
                {
                    counterMarker--;
                    checkMarker[i] = false;
                }
            }
        }
        
    }

}
