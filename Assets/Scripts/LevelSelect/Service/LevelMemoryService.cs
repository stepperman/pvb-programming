﻿using DN.LevelSelect.SceneManagment;
using DN.Service;
using DN.Tutorial;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DN.LevelSelect
{
	/// <summary>
	/// This is where the necessary data gets stored for the tutorials and scripts.
	/// </summary>
	[Service]
	public class LevelMemoryService
	{
		public BiomeController BiomeController => biomeController;
		public LevelLoader LevelLoader => levelLoader;
		public SetAudioListener SetAudioListener => setAudioListener;
 		public bool Assistant => assistant;
		public DN.LevelData LevelData { get; set; }
		public bool IsMazeTutorialDone => isMazeTutorialDone;
		public bool IsColorTutorialDone => isColorTutorialDone;
		public LevelDataEditor.SelectedPuzzle SelectedPuzzle => selectedPuzzle;
		public LevelDataEditor.SelectedAnimal SelectedAnimal => selectedAnimal;
		
		private SetAudioListener setAudioListener;
		private bool assistant;
		private LevelLoader levelLoader;
		private BiomeController biomeController;
		private bool isMazeTutorialDone;
		private bool isColorTutorialDone;
		private LevelDataEditor.SelectedPuzzle selectedPuzzle;
		private LevelDataEditor.SelectedAnimal selectedAnimal;

		public void SetBiomeAndLevelAndAudioController(BiomeController controller, LevelLoader loader, SetAudioListener listener)
		{
			biomeController = controller;
			levelLoader = loader;
			setAudioListener = listener;
		}
		public void SetSelectedPuzzle(LevelDataEditor.SelectedPuzzle puzzle, LevelDataEditor.SelectedAnimal animal)
		{
			selectedPuzzle = puzzle;
			selectedAnimal = animal;
		}
		public void SetDoneOnceTutorialMaze(bool value)
		{
			isMazeTutorialDone = value;
		}
		public void SetDoneOnceTutorialColor(bool value)
		{
			isColorTutorialDone = value;
		}

		public void SetTutorialStatus(bool _assistant)
		{
			assistant = _assistant;
		}
	}
}
