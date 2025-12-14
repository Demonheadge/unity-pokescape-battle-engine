// 14/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class Music_Manager : MonoBehaviour
{
    public MusicList.Music_Area selectedAreaMusic;
    public MusicList.Music_Battle selectedBattleMusic;
    public MusicList.Music_Encounter selectedEncounterMusic;
    public MusicList.Music_Fanfare selectedFanfareMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            UpdateMusic();
        }
    }

    // Generic method to play music from any MusicCategories enum
    public void PlayMusic(Enum selectedMusic)
    {
        // Get the category name and enum value dynamically
        string categoryName = selectedMusic.GetType().Name;
        string musicName = selectedMusic.ToString();

        // Construct the path dynamically based on category and enum value
        string musicPath = AssetPathManager.GetMusicPath($"{musicName}");

        if (!string.IsNullOrEmpty(musicPath))
        {
            // Load the audio clip from Resources
            AudioClip audioClip = Resources.Load<AudioClip>(musicPath);

            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                Debug.Log($"Playing music: {musicName} from category: {categoryName}, path: {musicPath}");
            }
            else
            {
                Debug.LogError($"AudioClip not found at path: {musicPath}");
            }
        }
        else
        {
            Debug.LogError($"Music path not found for: music/{musicName}");
        }
    }

    // Method to update music dynamically when the enum value changes
    public void UpdateMusic()
    {
        // Example: Play music from Music_Area enum
        PlayMusic(selectedAreaMusic);
    }
}



