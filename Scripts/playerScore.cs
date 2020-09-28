﻿using UnityEngine;
using UnityEngine.UI;

public class playerScore : MonoBehaviour {

	public delegate void hitCertainPoint();
	public static event hitCertainPoint OnHitCertainPoint;

	public delegate void secondCertainPoint();
	public static event secondCertainPoint SecretEnding;
	public Transform player;
	public Text scoreText;

	// Update is called once per frame
	void Update () {
		scoreText.text = player.position.z.ToString("0");

	}

	void FixedUpdate()
    {
		if (player.position.z > 200)
		{
			Debug.Log("Hello");
			OnHitCertainPoint?.Invoke();
		}
		if (player.position.z > 10000)
        {
			SecretEnding?.Invoke();
        }
	}
}
