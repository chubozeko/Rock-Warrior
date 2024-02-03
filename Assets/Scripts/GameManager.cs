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
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay Objects")]
    public GameObject player;
    // TODO: Add Animal GameObjects
    public List<Image> lives;
    public Text pointsText;
    private int points = 0;
    public Text GO_pointText;
    public Text GO_LivesText;
    public Text LC_pointText;
    public Text LC_LivesText;

    [Header("Gameplay Menus")]
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;
    public GameObject gameOverMenu;

    [Header("Audio Settings")]
    public GameObject soundOffBtn;
    public GameObject soundOnBtn;
    public GameObject musicOffBtn;
    public GameObject musicOnBtn;
    public Slider volumeSlider;

    public bool isGamePaused = false;
    private bool playMusic = true;

    public GameObject optionsMenu;
    public GameObject helpMenu;

    private void Awake()
    {
        PlayerPrefs.SetInt("Music", 1);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (PlayerPrefs.GetInt("Music", -1) == 1)
            {
                playMusic = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameSound;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (PlayerPrefs.GetInt("Music", -1) == 0)
                playMusic = false;
            else
                PlayerPrefs.SetInt("Music", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (PlayerPrefs.GetInt("Music", -1) == 1)
            {
                playMusic = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameSound;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (PlayerPrefs.GetInt("Music", -1) == 0)
                playMusic = false;
            else
                PlayerPrefs.SetInt("Music", 1);

            // POINTS
            pointsText.text = points.ToString();
        }
        else
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
        }

        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = false;
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            playMusic = true;
        }
        else
        {
            playMusic = false;
        }

    }

    // Menu Management
    public void StartGame()
    {
        // AudioManager.Instance.PlayOneShot(AudioManager.Instance.gameSound);
        SceneManager.LoadScene("Game");
    }

    public void PauseCurrentGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
            // Disable scripts that still work while timescale is set to 0
            player.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            // Disable scripts that still work while timescale is set to 0
            player.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void ShowHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ViewLevelComplete()
    {
        player.SetActive(false);
        // TODO: Add Animal Objects

        levelCompleteMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    // Sound Settings
    public void SoundToggleButton()
    {
        AudioManager.Instance.soundEffectAudio.mute = !AudioManager.Instance.soundEffectAudio.mute;
        if (AudioManager.Instance.soundEffectAudio.mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
            // soundOffBtn.SetActive(false);
            // soundOnBtn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            // soundOffBtn.SetActive(true);
            // soundOnBtn.SetActive(false);
        }
    }

    public void MusicToggleButton()
    {
        playMusic = !playMusic;
        if (playMusic)
        {
            PlayerPrefs.SetInt("Music", 1);
            // musicOffBtn.SetActive(true);
            // musicOnBtn.SetActive(false);
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameMusic;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameMusic;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
            }

        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            // musicOffBtn.SetActive(false);
            // musicOnBtn.SetActive(true);
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
        }
    }

    public void CheckSoundSettings()
    {
        if (AudioManager.Instance.soundEffectAudio.mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
            soundOffBtn.SetActive(false);
            soundOnBtn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            soundOffBtn.SetActive(true);
            soundOnBtn.SetActive(false);
        }
    }

    public void ChangeVolume()
    {
        AudioManager.Instance.soundEffectAudio.volume = volumeSlider.value;
    }

    public void RemovePlayerLife(int health)
    {
        lives[health].enabled = false;
    }

    public void AddPlayerLife(int health)
    {
        lives[health-1].enabled = true;
    }

    public void RemovePoints(int removedPoints)
    {
        points -= removedPoints;
        pointsText.text = points.ToString();
    }

    public void AddPoints(int addedPoints)
    {
        points += addedPoints;
        pointsText.text = points.ToString();
    }

    public void ShowGameOver(int health)
    {
        GO_pointText.text = "Points: " + points.ToString();
        GO_LivesText.text = "Lives: " + health;
        gameOverMenu.SetActive(true);
    }

    public void ShowLevelComplete(int health)
    {
        LC_pointText.text = "Points: " + points.ToString();
        LC_LivesText.text = "Lives: " + health;
        levelCompleteMenu.SetActive(true);
    }
}
