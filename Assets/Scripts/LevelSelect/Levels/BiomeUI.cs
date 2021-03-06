﻿using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DN.UI
{
	/// <summary>
	/// Here the in game UI gets set in the level select.
	/// </summary>
	public class BiomeUI : MonoBehaviour
	{
		[SerializeField] private Text taskText;
		[SerializeField] private Text finishedBiomeText;

		private const string TASK_MANAGER_BEGIN = "Help nog ";
		private const string TASK_MANAGER_EIND = " dieren in dit gebied!";
		private const string TASK_MANAGER_EIND_SINGULAR = " dier in dit gebied!";

		public void SetTaskManager(bool biomeIsFinished, int robotsNeedFixing)
		{
			if (!biomeIsFinished)
			{
				taskText.gameObject.SetActive(!biomeIsFinished);
				finishedBiomeText.gameObject.SetActive(biomeIsFinished);
				if(Mathf.Approximately(robotsNeedFixing, 1))
				{
					taskText.text = TASK_MANAGER_BEGIN + robotsNeedFixing + TASK_MANAGER_EIND_SINGULAR;
				}
				else
				{
					taskText.text = TASK_MANAGER_BEGIN + robotsNeedFixing + TASK_MANAGER_EIND;
				}
			}
			else
			{
				taskText.gameObject.SetActive(!biomeIsFinished);
				finishedBiomeText.gameObject.SetActive(biomeIsFinished);
			}
		}
	}
}
