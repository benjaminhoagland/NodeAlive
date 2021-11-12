using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Display_PulseWarning : MonoBehaviour
{
	[SerializeField] Image image;
	[SerializeField] TMPro.TMP_Text text;
	private Color redHard = new Color(1f,0f,0f,0.5f);
	private Color reddull = new Color(1f,0f,0f,0.25f);
		private Color softwhite = new Color(1f,1f,1f,0.5f);

	[SerializeField]
	AnimationCurve _curve;
	void Update()
	{
		var t = _curve.Evaluate(Time.time);
		text.color = Color.Lerp(softwhite, Color.white, t);
		image.color = Color.Lerp(reddull, redHard, t);
	}
}
