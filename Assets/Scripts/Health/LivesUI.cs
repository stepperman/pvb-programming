﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
	/// <summary>
	/// creates the ui for the lives
	/// </summary>
	public class LivesUI
	{
		private float size = 50f;
		private Sprite heart;
		private List<GameObject> hearts;
		private RectTransform canvas;
		private GameObject canvasObject;
		private Lives lives;

		public LivesUI(GameObject canvas, Lives newLives, float heartSize)
		{
			canvasObject = canvas;
			lives = newLives;
			size = heartSize;
			Start();
		}

		public void Start()
		{
			canvas = canvasObject.GetComponent<RectTransform>();
			hearts = new List<GameObject>();
			heart = Resources.Load<Sprite>("Sprites/heart");

			for (int i = 0; i < lives.CurrentLives; i++)
			{
				GameObject life = new GameObject($"heart {i}");
				life.AddComponent<Image>().sprite = heart;
				RectTransform rTransform = life.GetComponent<RectTransform>();			
				life.transform.SetParent(canvasObject.transform);
				rTransform.sizeDelta = new Vector2(size, size);
				rTransform.position = new Vector2(rTransform.rect.width + (size / 4 + rTransform.rect.width) * i, Screen.height - size);
				hearts.Add(life);
			}
			lives.LifeLostEvent += OnLifeLostEvent;
			lives.AllLifeLost += OnAllLifeLostEvent;
		}

		private void OnAllLifeLostEvent()
		{
			lives.LifeLostEvent -= OnLifeLostEvent;
		}

		private void OnLifeLostEvent(int obj)
		{
			Object.Destroy(hearts[hearts.Count - 1]);
			hearts.RemoveAt(hearts.Count - 1);
		}

		public void LoseLife()
		{
			lives.LoseLife();
		}

		private void OnDestroy()
		{
			lives.LifeLostEvent -= OnLifeLostEvent;
			lives.AllLifeLost -= OnAllLifeLostEvent;
		}
	}
}