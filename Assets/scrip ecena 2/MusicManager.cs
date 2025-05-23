using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip menuMusic;
    public AudioClip mapa1Music;     // Escena 1
    public AudioClip mapasMusic;     // Escena 4

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            audioSource.volume = savedVolume;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1: // Juego
                PlayMusic(mapa1Music);
                break;
            case 0: // Menu
                if (audioSource.clip != menuMusic)
                    PlayMusic(menuMusic);
                break;

            // Escenas donde no se cambia la música:
            case 2: // Opciones
            case 3: // Sonido
            case 4: // Mapas
            case 5: // Cuenta
            case 6: // Créditos
                // No cambiar música
                break;

            default:
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        // Siempre reinicia el clip, incluso si es el mismo
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }
}

