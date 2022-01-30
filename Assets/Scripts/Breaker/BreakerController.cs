using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class BreakerController : MonoBehaviour
{

    [ValueDropdown("_keyboardLayouts")]
    [SerializeField]
    [OnValueChanged("setPlayerPrefsForLayout")]
    private String _controlPreference = "QWERTY";
    
    private const String PADDLE_LEFT = "PADDLE_LEFT";
    private const String PADDLE_RIGHT = "PADDLE_RIGHT";
    private const String LAUNCH_BALL = "LAUNCH_BALL";
    private const String ABILITY_ONE = "AB1";
    private const String ABILITY_TWO = "AB2";
    private const String ABILITY_THREE = "AB3";
    private const String ABILITY_FOUR = "AB4";

    public float maxTravelDistanceFromCenter = 5;
    public float travelSpeedPerSecond = 5F;
    private Vector3 _initialPaddlePosition;
    
    public GameObject paddleGameObject;
    
    private Dictionary<String, KeyCode> StringKeyCodes = new Dictionary<string, KeyCode>()
    {
        {"A", KeyCode.A},
        {"S", KeyCode.S},
        {"D", KeyCode.D},
        {"Space", KeyCode.Space},
        {"1", KeyCode.Alpha1},
        {"2", KeyCode.Alpha2},
        {"3", KeyCode.Alpha3},
        {"4", KeyCode.Alpha4}
    };
    
    private void setPlayerPrefsForLayout()
    {
        Debug.Log("Changing Keyboard Layout To: " + _controlPreference);
        //Doesn't change between QWERTY/COLEMAK
        PlayerPrefs.SetString(LAUNCH_BALL, "Space");
        PlayerPrefs.SetString(ABILITY_ONE, "1");
        PlayerPrefs.SetString(ABILITY_TWO, "2");
        PlayerPrefs.SetString(ABILITY_THREE, "3");
        PlayerPrefs.SetString(ABILITY_FOUR, "4");
        
        switch(_controlPreference.ToUpper())
        {
            case "COLEMAK":
                PlayerPrefs.SetString(PADDLE_LEFT, "A");
                PlayerPrefs.SetString(PADDLE_RIGHT, "S");
                break;
            default:
                PlayerPrefs.SetString(PADDLE_LEFT, "A");
                PlayerPrefs.SetString(PADDLE_RIGHT, "D");
                break;
        }
        
        PlayerPrefs.Save();
    }
    
    private IEnumerable _keyboardLayouts = new ValueDropdownList<String>()
    {
        {"QWERTY", "QWERTY"},
        {"COLEMAK", "COLEMAK"}
    };

    public void Awake()
    {
        if (!PlayerPrefs.HasKey(PADDLE_LEFT))
        {
            setPlayerPrefsForLayout();
        }

        if (paddleGameObject == null)
        {
            Debug.Log("Paddle was null on awake, searching for Child with BreakerPaddle Component");
            paddleGameObject = GetComponentInChildren<BreakerPaddle>().gameObject;

            if (paddleGameObject == null)
            {
                Debug.LogError("Could not find Breaker Paddle in Child object of controller");
            }
            else
            {
                _initialPaddlePosition = paddleGameObject.transform.position;
            }
        }

    }

    public void Update()
    {
        HandlePlayerInputs();
    }

    private void HandlePlayerInputs()
    {
        float paddleTravelDistance = _initialPaddlePosition.x - paddleGameObject.transform.position.x;
        if (Input.GetKey(StringKeyCodes[PlayerPrefs.GetString(PADDLE_LEFT)]))
        {
            //If the paddle is not more than the max distance from it's initial position on object awake 
            if (paddleTravelDistance < maxTravelDistanceFromCenter)
            {
                paddleGameObject.transform.Translate(-travelSpeedPerSecond * Time.deltaTime,0, 0);
            }
        }

        if (Input.GetKey(StringKeyCodes[PlayerPrefs.GetString(PADDLE_RIGHT)]))
        { 
            //If the paddle is not more than the max distance from it's initial position on object awake 
            if (paddleTravelDistance > -maxTravelDistanceFromCenter)
            {
                paddleGameObject.transform.Translate(travelSpeedPerSecond * Time.deltaTime,0, 0);
            }
        }

        if (Input.GetKeyDown(StringKeyCodes[PlayerPrefs.GetString(LAUNCH_BALL)]))
        {
            Debug.Log("LaunchBall:"+ LAUNCH_BALL);
        }
        
        //Add Abilities here
    }
}
