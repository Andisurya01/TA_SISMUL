using UnityEngine;
using Lean.Touch;

public class ObjectClickHandler : MonoBehaviour
{
    // Referensi ke objek yang ingin diubah
    public GameObject newObject;

    // Fungsi yang dipanggil saat objek dipilih
    public void OnObjectClicked()
    {
        if (newObject != null)
        {
            // Ganti objek dengan yang baru
            Instantiate(newObject, transform.position, transform.rotation);
            Destroy(gameObject); // Hapus objek lama
        }
    }
}
