using Lean.Touch;
using System.Collections;
using UnityEngine;
public class LeanTouchController : MonoBehaviour
{
    public GameObject objectAR;
    public Animator animator;
    bool isAnimationActive;

    private void Start()
    {
        isAnimationActive = true;
    }
    private void OnEnable()
    {
        // Menambahkan event untuk mendeteksi klik pada objek
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    private void OnDisable()
    {
        // Menghapus event saat objek dinonaktifkan
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        // Raycast untuk mendeteksi apakah sentuhan mengenai objek
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Objek diklik: " + gameObject.name);
                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        StartCoroutine(PlanetAnimationCoroutine());
        IEnumerator PlanetAnimationCoroutine()
        {
            animator.SetTrigger("back");
            isAnimationActive = false;
            yield return new WaitForSeconds(0.7f);

            Pengaturan.instance.planetUtuh = true;
            Pengaturan.instance.planetLapisan = false;

            gameObject.SetActive(Pengaturan.instance.planetLapisan);

            objectAR.SetActive(Pengaturan.instance.planetUtuh);
        }
        
    }
}
