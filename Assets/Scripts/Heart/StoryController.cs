using System;
using System.Collections.Generic;
using System.Linq;
using Heart;
using Ink.Runtime;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    // the ink story
    public TextAsset inkJSONAsset = null;
    
    // the phone text messaging system to interface to
    public TextMessaging messagingInterface;
    
    private Story story;

    private void Start()
    {
        story = new Story (inkJSONAsset.text);
        RefreshView();
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
            
            if (speaker != null && speaker != "Player")
            {
                messagingInterface.SetName(speaker);
            }
            
            // Concatenate the current text chunk
            line += currentTextChunk;
            
            CreateContentView(speaker, line);
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

    void CreateContentView (string speaker, string text) {
        // skip the scene on "..."
        if (text.Trim() == "")
        {
            return;
        }
        
        if (speaker == "Player")
        {
            messagingInterface.CreateTextMessage(text, Speaker.Sender, RefreshView); 
        }
        else
        {
            messagingInterface.CreateTextMessage(text, Speaker.Receiver, RefreshView);   
        }
    }
}
