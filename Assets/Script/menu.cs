using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelInfo;

    void Start()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
    }

    void Update()
    {
        
    }

    public void activeInfo(){
        panelMenu.SetActive(false);
        panelInfo.SetActive(true);
    }

    public void desactiveInfo(){
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void exitGame(){
        Application.Quit();
    }
}