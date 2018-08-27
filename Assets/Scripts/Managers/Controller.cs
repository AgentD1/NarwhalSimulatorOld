using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    public AudioMixer master;
    public Slider musicVolSlider;
    public GameObject pauseMenuRoot;
    public Camera blurCamera;
    public GameObject ShopUI;

    private void Start() {
        if (blurCamera == null) {
            blurCamera = Camera.main;
        }
    }
    // TODO: Change this when I have a wifi connection into something like add listener for slider's on value changed function.
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!ShopUI.activeSelf) {
                TogglePauseState();
            }
        }
        OnMusicVolumeChange();
    }

    public void ResumeButtonPressed() {
        TogglePauseState();
    }

    public void QuitButtonPressed() {
        Application.Quit();
    }

    public void ResetButtonPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShopButtonPressed() {
        ShopUI.SetActive(true);
        GameObject.Find("PauseUI").SetActive(false);
    }

    void OnMusicVolumeChange() {
        if (musicVolSlider.value == 0) {
            master.SetFloat("musicVol", -80);
        } else {
            master.SetFloat("musicVol", Mathf.Log(musicVolSlider.value) * 20);
        }
    }

    void TogglePauseState() {
        pauseMenuRoot.SetActive(!pauseMenuRoot.activeSelf);
        blurCamera.GetComponent<BoxBlur>().enabled = pauseMenuRoot.activeSelf;
        if (pauseMenuRoot.activeSelf) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }

}
