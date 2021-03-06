﻿using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Data for which node is the finished, and what lines are connected
	/// to be used by the player.
	/// </summary>
	public class Node : MonoBehaviour
	{
		public NodeData Data { get; private set; }

		[SerializeField] private Image nodeRenderer;
		[SerializeField] private Sprite finishNode;

		public void InitializeNode(NodeData data)
		{
			if (Data is null)
			{
				Data = data;
				Data.SetOwner(this);

				if (data.IsFinish)
					nodeRenderer.sprite = finishNode;
			}
		}
	}
}
