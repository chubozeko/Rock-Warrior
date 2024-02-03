/*
 * Created by Chubo Zeko.
 * 
 * GitHub: https://github.com/chubozeko
 * LinkedIn: https://www.linkedin.com/in/chubo-zeko/
 * Game Catalog: https://chubozeko.itch.io/
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public AudioSource soundEffectAudio;
    public int a;  // flag for Game
    public int b;  // flag for Main Menu
    public bool vibration;
    public AudioClip gameSound;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    void Awake()
    {
        a = 0;
        b = 0;
        vibration = true;
        if (Instance == null)
        {
            Instance = this;    // makes sure this is the only AudioManager
        }
        else if (Instance != null)
        {
            Destroy(gameObject);    // if there are others, destroy them
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
	
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            soundEffectAudio.clip = mainMenuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }
	
        // TOGGLE MUTE
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            soundEffectAudio.mute = true;
        }
        else
        {
            soundEffectAudio.mute = false;
        }
	
        DontDestroyOnLoad(gameObject.transform);
    }

    private void Update()
    {
	/*
        if (SceneManager.GetActiveScene().name == "Game" && b == 0)
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            b++;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu" && a == 0)
        {
            soundEffectAudio.clip = mainMenuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            a++;
        }
	*/
    }

    /** PLAY SPECIFIC SOUND **/
    public void PlayGameSound(AudioClip clip)
    {
        soundEffectAudio.clip = clip;
        soundEffectAudio.loop = false;
        // soundEffectAudio.Play(0);
        soundEffectAudio.PlayOneShot(clip);
    }
}
