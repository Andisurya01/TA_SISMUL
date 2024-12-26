using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // Cek apakah instance sudah ada
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Hancurkan duplicate
            return;
        }

        // Tetapkan instance dan jangan hancurkan
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
