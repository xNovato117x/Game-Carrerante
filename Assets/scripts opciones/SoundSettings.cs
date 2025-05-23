using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private float[] volumeLevels = { 0f, 0.25f, 0.5f, 0.75f, 1f };

    void Start()
    {
        // Obtener el índice de nivel guardado (0-4)
        int savedIndex = PlayerPrefs.GetInt("VolumeLevelIndex", 4);
        volumeSlider.value = savedIndex;

        SetVolume(savedIndex);

        // Listener para cuando se mueve el slider
        volumeSlider.onValueChanged.AddListener(delegate {
            SetVolume((int)volumeSlider.value);
        });
    }

    public void SetVolume(int levelIndex)
    {
        float volume = volumeLevels[Mathf.Clamp(levelIndex, 0, volumeLevels.Length - 1)];

        var music = FindFirstObjectByType<MusicManager>();
        if (music != null)
        {
            music.SetVolume(volume);
        }

        PlayerPrefs.SetInt("VolumeLevelIndex", levelIndex);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
