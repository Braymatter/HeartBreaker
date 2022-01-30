using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public AudioSource breakerMusic;

    public AudioSource introMusic;

    public Transform breakerCameraPosition;
    public Transform smsCameraPosition;

    public GameObject breakerGame;
    public StoryController smsGame;
    public VideoPlayer player;
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded!");
        breakerGame.SetActive(true);

        smsGame.NewGame();
        
        Test();
    }
    
    public void SwitchToSMS()
    {
        Debug.Log("Switching to SMS");
        //smsGame.Play();
        SwitchToSMSView();
    }

    public void SwitchToBreaker(String levelName)
    {
        breakerMusic.playOnAwake = false;
        breakerMusic.time = 0;
        breakerMusic.Play();
        breakerGame.GetComponent<BreakerController>().BeginNewGame(levelName);
        SwitchToBreakerView();
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
        SwitchToBreaker("DemoLevel");
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
        // move camera for breaker
        Camera.main.transform.position = breakerCameraPosition.position;
        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 60;
    }

    void SwitchToSMSView()
    {
        // move camera for sms
        Camera.main.transform.position = smsCameraPosition.position;
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 50;
    }

    private void Test()
    {
        StartCoroutine(WaitUntil(20, SwitchToSMS));
    }
    
    private IEnumerator WaitUntil(float seconds, Action lambda)
    {
        yield return new WaitForSeconds(seconds);
        lambda.Invoke();
    }
}
