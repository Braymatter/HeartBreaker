using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Heart
{
    public class TextMessaging : MonoBehaviour
    {
        // the prefab that represents a texting bubble
        public GameObject bubblePrefab;
        
        // the prefab that represents a reply option
        public GameObject replyPrefab;
        
        // the UI panel to attach text messages to
        public GameObject messagingPanel;
        
        // the UI panel to attach reply options to
        public GameObject replyPanel;
        
        // the text field with the recipient's name
        public TextMeshProUGUI textMesh;

        // messages need to process in a queue because we rely on their
        // positioning and sizing to layout future items
        private List<Tuple<string, Speaker>> _pendingMessages = new();
        private List<Bubble> _sentMessages = new();
        private List<ReplyOption> _currentOptions = new();
        private HashSet<Bubble> _toRemove = new ();
        private int _isAnimating = 0;

        private void Start()
        {
            SetName("Mom");
            
            // make some text messages
            StartCoroutine(WaitUntil(1, 
                () => CreateTextMessage("Hello there", Speaker.Receiver)));

            StartCoroutine(WaitUntil(2, 
                () => CreateOptions(new List<string> {"Who are you?", "Hey", "😊"})));
            
            StartCoroutine(WaitUntil(5, 
                () => CreateTextMessage("How are you doing tonight?", Speaker.Receiver)));
            
            StartCoroutine(WaitUntil(6, 
                () => CreateTextMessage("I'm doing well, thank you.", Speaker.Sender)));
            
            StartCoroutine(WaitUntil(7, 
                () => CreateTextMessage("OK good, just checking up on you. You never send me any texts or even call!", Speaker.Receiver)));
            
            StartCoroutine(WaitUntil(8, 
                () => CreateTextMessage("Yeah I know. It's been busy. Lots happening at work, you know how it is.", Speaker.Sender)));
            
            StartCoroutine(WaitUntil(9, 
                () => CreateTextMessage("You ought to visit soon, its been years.", Speaker.Receiver)));
            
            StartCoroutine(WaitUntil(10, 
                () => CreateTextMessage("OK.", Speaker.Sender)));
            
            StartCoroutine(WaitUntil(11, 
                () => CreateTextMessage("I'll try.", Speaker.Sender)));
            
        }

        private void Update()
        {
            if (_isAnimating == 0)
            {
                // process a pending message
                if (_pendingMessages.Count > 0)
                {
                    var message = _pendingMessages.First();
                    var bubble = ProcessPendingMessage(message.Item1, message.Item2);
                    
                    _pendingMessages.Remove(message);
                    _sentMessages.Add(bubble);
                }

                if (_toRemove.Count > 0)
                {
                    CleanUp();
                }
            }
        }
        
        private IEnumerator WaitUntil(int seconds, Action lambda)
        {
            yield return new WaitForSeconds(seconds);
            lambda.Invoke();
        }

        private ReplyOption CreateReply(string reply, Action<ReplyOption> onClick = null)
        {
            var go = Instantiate(replyPrefab, replyPanel.transform);
            var replyOption = go.GetComponent<ReplyOption>();
            replyOption.text = reply;
            replyOption.clickCallback = onClick;
            return replyOption;
        }

        private Bubble ProcessPendingMessage(string text, Speaker speaker)
        {
            // message needs to render below others
            var yOffset = 10 + _sentMessages.Aggregate(0f, (acc, b) => Math.Max(acc, b.PositionY()));
            
            var go = Instantiate(bubblePrefab, messagingPanel.transform);

            _isAnimating++;
            
            var bubble = go.GetComponent<Bubble>();
            bubble.text = text;
            bubble.padding = new Vector2(20, 20);
            bubble.speaker = speaker;
            bubble.offset = yOffset;
            bubble.moveFromOffset = messagingPanel.GetComponent<RectTransform>().rect.height;

            return bubble;
        }

        private void CreateTextMessage(string text, Speaker speaker)
        {
            _pendingMessages.Add(new Tuple<string, Speaker>(text, speaker));
        }

        // Occurs when a child bubble has finished being positioned
        public void OnBubblePositioned(Bubble bubble)
        {
            var distFromBottom = bubble.GetRect().localPosition.y - bubble.imageMesh.rectTransform.sizeDelta.y;

            // Need to reposition elements because we overflowed
            if (distFromBottom <= 0)
            {
                var scrollUpAmount = -distFromBottom + 20;
                
                RepositionAll(scrollUpAmount);
            }

            _isAnimating--;
        }

        private void CleanUp()
        {
            _sentMessages.RemoveAll(_toRemove.Contains);

            foreach (var bubble in _toRemove)
            {
                Destroy(bubble.gameObject);
            }

            _toRemove = new();
        }

        private void RepositionAll(float offset)
        {
            _isAnimating++;
            
            foreach (var bubble in _sentMessages)
            {
                var currentPosition = bubble.GetRect().localPosition;

                _isAnimating++;

                StartCoroutine(bubble.MoveTo(
                    currentPosition + new Vector3(0, offset, 0),
                    0.3f,
                    () =>
                    {
                        // delete if we're OOB
                        if (bubble.RenderedHeight() <= 0)
                        {
                            _toRemove.Add(bubble);
                        }
                        
                        _isAnimating--;
                    }
                ));
            }
            
            _isAnimating--;
        }

        private void CreateOptions(List<string> options)
        {
            if (options.Count == 0)
            {
                return;
            }

            if (options.Count > 3)
            {
                throw new Exception("Cannot have more than 3 reply options.");
            }

            var replies = options.Select(s =>
            {
                return CreateReply(s,
                    (reply) =>
                    {
                        CreateTextMessage(reply.text, Speaker.Sender);
                        ClearReplies();
                    });
            }).ToList();

            // need to lay stuff out depending on # of options
            if (replies.Count == 1)
            {
                replies[0].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                };
            }
            else if (replies.Count == 2)
            {
                replies[0].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    r.GetRect().localPosition -= new Vector3(-100, 0, 0);
                };
                replies[1].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
                    r.GetRect().localPosition -= new Vector3(100, 0, 0);
                };
                
            }
            else if (replies.Count == 3)
            {
                replies[0].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 133);
                    r.GetRect().localPosition -= new Vector3(-133, 0, 0);
                };
                replies[1].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 133);
                };
                replies[2].postConstruct = r =>
                {
                    r.GetRect().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 133);
                    r.GetRect().localPosition -= new Vector3(133, 0, 0);
                };
            }

            _currentOptions = replies;
        }

        private void ClearReplies()
        {
            foreach (var currentOption in _currentOptions)
            {
                currentOption.Remove();
            }

            _currentOptions = new();
        }

        public void SetName(string recipientName)
        {
            textMesh.text = recipientName;
        }
    }
}