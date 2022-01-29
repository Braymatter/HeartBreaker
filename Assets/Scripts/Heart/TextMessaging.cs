using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Heart
{
    public class TextMessaging : MonoBehaviour
    {
        public GameObject bubblePrefab;
        public Canvas canvas;

        private List<Bubble> _sentMessages = new();

        private void Start()
        {
            // make some text messages
            StartCoroutine(WaitUntil(1, 
                () => CreateTextMessage("Hello there", Speaker.Receiver)));
            StartCoroutine(WaitUntil(5, 
                () => CreateTextMessage("Oh hey. How are you? Been a while!", Speaker.Sender)));
            StartCoroutine(WaitUntil(9, 
                () => CreateTextMessage("Hello???", Speaker.Sender)));
            StartCoroutine(WaitUntil(12, 
                () => CreateTextMessage("Sorry about that, I had to feed my dog; took longer than I anticipated.", Speaker.Receiver)));
        }

        private void Update()
        {

        }
        
        private IEnumerator WaitUntil(int seconds, Action lambda)
        {
            yield return new WaitForSeconds(seconds);
            lambda.Invoke();
        }

        private void CreateTextMessage(string text, Speaker speaker)
        {
            // message needs to render below others
            var yOffset = 20 + _sentMessages.Aggregate(0f, (acc, b) => acc + b.padding.y + b.imageMesh.rectTransform.rect.height);
            
            var go = Instantiate(bubblePrefab, canvas.transform);
            
            var bubble = go.GetComponent<Bubble>();
            bubble.text = text;
            bubble.padding = new Vector2(20, 20);
            bubble.speaker = speaker;
            bubble.offset = yOffset;
            bubble.moveFromOffset = canvas.GetComponent<RectTransform>().rect.height;
            
            _sentMessages.Add(bubble);
        }
    }
}