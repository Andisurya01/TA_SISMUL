using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    public GameObject planetUtuh; // Objek planet utuh
    public GameObject planetBerlapis; // Objek planet berlapis
    public Animator planetBerlapisAnimator; // Animator untuk planet berlapis

    bool isActive = false;

    // public void Start()
    // {
    //     planetBerlapis.SetActive(false);
    // }
    public void OnPlanetClicked()
    {
        Debug.Log("jancok");

        planetBerlapis.SetActive(true);
        planetUtuh.SetActive(false);

        planetBerlapisAnimator.SetBool("expanded", true);

    }

    public void OnPlanetClickedRealease()
    {
        Debug.Log("jancok");

        planetUtuh.SetActive(true);
        planetBerlapis.SetActive(false);

        planetBerlapisAnimator.SetBool("expanded", false);

    }

    // public void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (isActive == false)
    //         {
    //             isActive = true;
    //             OnPlanetClicked();
    //         }
    //         else
    //         {
    //             isActive = false;
    //             OnPlanetClickedRealease();
    //         }
    //     }
    // }



}
