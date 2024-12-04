using UnityEngine;

public class gyroCamera : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion baseRotation;

    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            baseRotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported on this device.");
        }
    }

    void Update()
    {
        if (gyroSupported)
        {
            transform.localRotation = baseRotation * GyroToUnity(gyro.attitude);
        }
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
