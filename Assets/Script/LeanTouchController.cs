using Lean.Touch;
using System.Collections;
using UnityEngine;
public class LeanTouchController : MonoBehaviour
{
    //public GameObject objectAR;
    public Animator animator;
    bool isclicked;
    //bool isAnimationActive;

    private void Start()
    {
        Pengaturan.instance.isAnimationActive = true;
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
        if (isclicked) { return; }
        isclicked = true;
        StartCoroutine(PlanetAnimationCoroutine());
        IEnumerator PlanetAnimationCoroutine()
        {
            animator.SetTrigger("back");

            Pengaturan.instance.isAnimationActive = false;
            yield return new WaitForSeconds(1.7f);

            isclicked = false;

            Pengaturan.instance.planetUtuh = true;
            Pengaturan.instance.planetLapisan = false;

            gameObject.SetActive(Pengaturan.instance.planetLapisan);

            Pengaturan.instance.planetUtuhObjek.SetActive(Pengaturan.instance.planetUtuh);
        }
        
    }
}
