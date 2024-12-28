using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public Transform sun; // Referensi ke objek matahari
    public float orbitSpeed = 10f; // Kecepatan orbit planet
    public float distanceFromSun = 1f; // Jarak dari matahari

    void Start()
    {
        // Mengatur posisi awal planet sesuai jarak dari matahari
        transform.position = sun.position + new Vector3(distanceFromSun, 0, 0);
    }

    void Update()
    {
        // Melakukan rotasi mengelilingi matahari
        transform.RotateAround(sun.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

}
