using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;

    public AudioSource breakerMusic;

    public AudioSource introMusic;

    public Transform breakerCameraPosition;
    public Transform smsCameraPosition;

    public GameObject breakerGame;
    public GameObject smsGame;
    public VideoPlayer player;
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded!");
        breakerGame.SetActive(true);
        
    }
    
    public void SwitchToSMS(bool didWin, String levelName)
    {
        
    }

    public void SwitchToBreaker(String levelName)
    {
        breakerMusic.playOnAwake = false;
        breakerMusic.time = 0;
        breakerMusic.Play();
        breakerGame.GetComponent<BreakerController>().BeginNewGame(levelName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.url = System.IO.Path.Combine (Application.streamingAssetsPath,"Spacey BG.mp4");
        player.Play();
        SwitchToBreaker("EasyLevel");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchToBreakerView()
    {
        
    }

    void SwitchToSMSView()
    {
        
    }
}
