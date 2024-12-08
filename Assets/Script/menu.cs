using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelSetting;
    public GameObject planet;
    
    void Start()
    {
        panelSetting.SetActive(false);
        panelMenu.SetActive(true);
        planet.SetActive(true);
    }

    void Update()
    {
        
    }

    public void activeSetting(){
        panelMenu.SetActive(false);
        panelSetting.SetActive(true);
    }

    public void desactiveSetting(){
        panelSetting.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void exitGame(){
        Application.Quit();
    }
}