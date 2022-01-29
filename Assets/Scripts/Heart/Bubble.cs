using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heart
{
	public class Bubble : MonoBehaviour
	{
		// Thing we draw on
		public TextMeshProUGUI textMesh;
		public Image imageMesh;

		// The Y-offset to render at and to move from
		public float offset = 0f;
		public float moveFromOffset = 800f;
		
		private RectTransform _self;
		private BubbleAnimation _animation = BubbleAnimation.New;
		private float _maxWidth = 270f;
		
		// callback to happen after bubble is done
		public Action doAfterComplete;
		
		// our parent text messaging interface -- we need to notify
		// them when we finish adding a text so repositioning
		// can occur
		private TextMessaging _messaging;
		
		// length of animations
		private float _animationTime = 0.3f;

		// Text to show
		public string text;
	
		// Padding around the bubble
		public Vector2 padding = new Vector2(0, 0);
	
		// Is this the sender? If so, we can use a different style 
		// and pin to a certain side of the parent.
		public Speaker speaker = Speaker.Receiver;

		private bool _doOnce = false;
		
		// track if we are animating so we don't do things
		// while that is happening.
		private bool _isAnimating = false;

		private void Start()
		{
			_self = gameObject.GetComponent<RectTransform>();
			_messaging = gameObject.GetComponentInParent<TextMessaging>();
			textMesh.text = text;
			
			if (speaker == Speaker.Receiver)
			{
				imageMesh.color = Color.gray;
				_self.pivot = new Vector2(0, 1);
				_self.anchorMin = new Vector2(0, 1);
				_self.anchorMax = new Vector2(0, 1);
				_self.localPosition += new Vector3(16, 0, 0);
				imageMesh.rectTransform.pivot = new Vector2(0, 1);
				imageMesh.rectTransform.anchorMin = new Vector2(0, 1);
				imageMesh.rectTransform.anchorMax = new Vector2(0, 1);
				textMesh.rectTransform.pivot = new Vector2(0, 1);
				textMesh.rectTransform.anchorMin = new Vector2(0, 1);
				textMesh.rectTransform.anchorMax = new Vector2(0, 1);
			}
			else
			{
				imageMesh.color = Color.blue;
				_self.pivot = new Vector2(1, 1);
				_self.anchorMin = new Vector2(1, 1);
				_self.anchorMax = new Vector2(1, 1);
				_self.localPosition += new Vector3(-16, 0, 0);
				imageMesh.rectTransform.pivot = new Vector2(1, 1);
				imageMesh.rectTransform.anchorMin = new Vector2(1, 1);
				imageMesh.rectTransform.anchorMax = new Vector2(1, 1);
				textMesh.rectTransform.pivot = new Vector2(1, 1);
				textMesh.rectTransform.anchorMin = new Vector2(1, 1);
				textMesh.rectTransform.anchorMax = new Vector2(1, 1);
			}
			
			_self.localPosition = new Vector3(_self.localPosition.x, -moveFromOffset, _self.localPosition.z);
			
			// Fixed width for the text to force text wrapping.
			// Needed to resize the surrounding bubbles.
			SetMaxTextWidth(_maxWidth);
			
			// Stays hidden at first
			Hide();

			_animation = BubbleAnimation.Resizing;
		}

		void Update ()
		{
			// only do this on the first time
			if (!_doOnce)
			{
				// wait for load
				if (imageMesh == null || imageMesh.rectTransform == null || textMesh == null || textMesh.rectTransform == null)
				{
					return;
				}
				
				_doOnce = true;
			}
			
			if (_doOnce)
			{
				// don't go farther if we are actively animating
				// or if we are finished
				if (_isAnimating || _animation == BubbleAnimation.Done)
				{
					return;
				}
				
				// animation can occur now
				switch (_animation)
				{
					case BubbleAnimation.Resizing:
					{
						Resize();
						
						_isAnimating = true;
						StartCoroutine(WaitUntil(0.3f, () =>
						{
							_isAnimating = false;
							_animation = BubbleAnimation.Revealing;
						}));
						
						break;
					}
					case BubbleAnimation.Revealing:
					{
						// reveal resized component
						Reveal();
						
						_animation = BubbleAnimation.Moving;

						break;
					}
					case BubbleAnimation.Moving:
					{
						// Slide up into position
						_isAnimating = true;
						StartCoroutine(MoveTo(
							new Vector3(_self.localPosition.x, moveFromOffset - offset, _self.localPosition.z), 
							_animationTime, 
							() =>
						{
							_isAnimating = false;
							_animation = BubbleAnimation.Done;
							_messaging.OnBubblePositioned(this); 
							doAfterComplete?.Invoke();
						}));
						
						break;
					}
				}
			}
		}

		private void Hide()
		{
			textMesh.alpha = 0.0f;
			imageMesh.color = new Color(imageMesh.color.r, imageMesh.color.g, imageMesh.color.b, 0);
		}

		private void Reveal()
		{
			textMesh.alpha = 1.0f;
			imageMesh.color = new Color(imageMesh.color.r, imageMesh.color.g, imageMesh.color.b, 1);
		}
		
		private IEnumerator WaitUntil(float seconds, Action lambda)
		{
			yield return new WaitForSeconds(seconds);
			lambda.Invoke();
		}

		public IEnumerator MoveTo(Vector3 newPosition, float animationLength, Action onDone = null)
		{
			var delta = Time.deltaTime;
			var time = animationLength;
			var initialPosition = _self.localPosition;
			var finalPosition = newPosition;

			while (time > 0)
			{
				var interpolate = (_animationTime - time) / _animationTime;

				_self.localPosition = Vector3.Lerp(initialPosition, finalPosition, interpolate);
				
				time -= delta;
				yield return null;
			}

			_self.localPosition = finalPosition;

			onDone?.Invoke();
		}
		
		private void Resize()
		{
			var maxWidth = textMesh.rectTransform.sizeDelta.x;
			var desiredWidth = textMesh.preferredWidth;
			var desiredHeight = textMesh.preferredHeight;

			// left-right offset depends on who is texting
			if (speaker == Speaker.Receiver)
			{
				textMesh.rectTransform.localPosition = new Vector3(padding.x / 2, -padding.y / 2, 0);
			}
			else
			{
				textMesh.rectTransform.localPosition = new Vector3(-padding.x / 2, -padding.y / 2, 0);
			}
			
			// we can size down the text box if it isn't overflowing its container.
			// necessary for text bubbles on the right-side to anchor.
			if (desiredWidth < maxWidth)
			{
				imageMesh.rectTransform.sizeDelta = new Vector2(desiredWidth + padding.x, desiredHeight + padding.y);
				textMesh.rectTransform.sizeDelta = new Vector2(desiredWidth, desiredHeight);
			}
			else
			{
				imageMesh.rectTransform.sizeDelta = new Vector2(maxWidth + padding.x, desiredHeight + padding.y);
				textMesh.rectTransform.sizeDelta = new Vector2(maxWidth, desiredHeight);
			}
		}

		private void SetMaxTextWidth(float width)
		{
			textMesh.rectTransform.sizeDelta = new Vector2(width, textMesh.rectTransform.sizeDelta.y);
		}

		public RectTransform GetRect()
		{
			return _self;
		}

		public float RenderedHeight()
		{
			// if height == 40...
			// 966 - 800 => 40 - 166 => -126
			// 820 - 800 => 40 - 20  => 20
			// 780 - 800 => 40 -- 20 => 60
			// if < 0; 0; else min(height, difference)

			var height = padding.y + imageMesh.rectTransform.rect.height;
			var yDiff = height - (_self.localPosition.y - moveFromOffset);
			
			if (yDiff <= 0)
			{
				return 0;
			}

			return Math.Min(height, yDiff);
		}

		public float PositionY()
		{
			return (moveFromOffset - _self.localPosition.y) + RenderedHeight();
		}
	}
	
	internal enum BubbleAnimation {
		New,
		Resizing,
		Revealing,
		Moving,
		Done
	}
}
