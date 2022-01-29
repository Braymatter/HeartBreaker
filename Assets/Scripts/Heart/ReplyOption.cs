using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heart
{
	public class ReplyOption : MonoBehaviour
	{
		// Thing we draw on
		public TextMeshProUGUI textMesh;
		
		// Callback to do things in response to a click
		public Action<ReplyOption> clickCallback;
		
		// Callback for post-construction of this object
		public Action<ReplyOption> postConstruct;
		
		public string text = "";
		
		private RectTransform _self;

		private void Start()
		{
			_self = gameObject.GetComponent<RectTransform>();
			textMesh.text = text;
			postConstruct?.Invoke(this);
		}

		public void OnButtonPress()
		{
			clickCallback?.Invoke(this);
		}

		public void Remove()
		{
			Destroy(gameObject);
		}
		
		public RectTransform GetRect()
		{
			return _self;
		}
	}
}
