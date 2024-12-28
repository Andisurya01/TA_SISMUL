using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotationSpeed;
    private Renderer planetRenderer;

    void Update()
    {

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
