using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Heart;
using Ink.Runtime;
using UnityEngine;
using Random = System.Random;

public class StoryController : MonoBehaviour
{
    private Stack<String> levels = new Stack<String>(new [] {"Intro", "DemoLevel", "HardLevel" });
    // the ink story
    public TextAsset inkJSONAsset = null;
    
    // the phone text messaging system to interface to
    public TextMessaging messagingInterface;
    private GameManager _gameManager;
    
    private Story story;
    private bool _isGameInProgress = false;

    private void Start()
    {
        _gameManager = FindObjectOfType (typeof (GameManager)) as GameManager;
    }

    public void Play()
    {
        if (!_isGameInProgress)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        if (story == null)
        {
            story = new Story (inkJSONAsset.text);
        }
        else
        {
            story.ResetState();
        }
        
        RefreshView();
        _isGameInProgress = true;
    }

    private void RefreshView()
    {
        if (story.canContinue)
        {
            // Continue gets the next line of the story
            string currentTextChunk = story.Continue();
            List<string> currentTags = story.currentTags;
            
            // Create a blank line of dialogue
            string line = "";
            string speaker = currentTags.FirstOrDefault();

            // Concatenate the current text chunk
            line += currentTextChunk;

            var waitTime = 0f;
            if (speaker != "Player")
            {
                waitTime = UnityEngine.Random.Range(10f, 14f);
            }
            
            if (speaker != null && speaker != "Player")
            {
                messagingInterface.SetName(speaker);
                messagingInterface.SetTyping(true);
            }
            
            StartCoroutine(WaitUntil(waitTime, () =>
            {
                CreateContentView(speaker, line);
            }));
        } 
        else
        {
            // Display all the choices, if there are any!
            // creates a reply option for each
            var actions = new List<ReplyAndAction>();
            
            if(story.currentChoices.Count > 0) 
            {
                for (int i = 0; i < story.currentChoices.Count; i++) 
                {
                    Choice choice = story.currentChoices [i];
                    
                    actions.Add(new ReplyAndAction(
                        choice.text.Trim(),
                        () =>
                        {
                            story.ChooseChoiceIndex(choice.index);
                            RefreshView();
                        }
                    ));
                }
            }

            if (actions.Count > 0)
            {
                messagingInterface.CreateOptions(actions);
            }
        }
    }

    private IEnumerator WaitUntil(float seconds, Action lambda)
    {
        yield return new WaitForSeconds(seconds);
        lambda.Invoke();
    }
    
    void CreateContentView (string speaker, string text) {
        // skip the scene on "..."
        if (text.Trim() == "")
        {
            return;
        }
        
        if (speaker == "Player")
        {
            messagingInterface.CreateTextMessage(text, Speaker.Sender, () =>
            {
                StartCoroutine(WaitUntil(3, () =>
                {
                    RefreshView();
                    String level = null;
                    if (levels.Count > 0){
                        level = levels.Pop();
                    }
                    if(level == null)
                    {
                        level = "HardLevel";
                    }
                    _gameManager.SwitchToBreaker(level);
                }));
            }); 
        }
        else
        {
            messagingInterface.CreateTextMessage(text, Speaker.Receiver, () =>
            {
                messagingInterface.SetTyping(false);
                RefreshView();
                EventManager.TriggerEvent("NewTextMessage");
            });   
        }
    }
}
