﻿using DN.LevelSelect.Player;
using DN.Service;
using System.Collections;
using System.ComponentModel;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DN.LevelSelect.SceneManagment
{
    /// <summary>
    /// This is the actual scene loader where you load thge selected scene
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        [HideInInspector] public bool isInBetweenFinished;

        public GameObject LevelObject => levelObject;
        public LevelData.SelectedPuzzle SelectedPuzzle => selectedPuzzle;
        public LevelData.SelectedAnimal SelectedAnimal => selectedAnimal;

        private GameObject levelObject;
        private LevelData.SelectedPuzzle selectedPuzzle;
        private LevelData.SelectedAnimal selectedAnimal;

        private const string LEVEL_SELECT_NAME = "LevelSelect";
        private const string DOG_IBS_NAME = "LevelOpenerDragonfly";
        private const string OWL_IBS_NAME = "LevelOpenerOwl";

        private string prevSceneLoaded;

        public void LoadInBetweenScene()
        {
            if (levelObject.GetComponent<LevelData>().PuzzleSelected == selectedPuzzle)
            {
                switch (selectedAnimal)
                {
                    case LevelData.SelectedAnimal.Dog:
                        GetAndSetScene(DOG_IBS_NAME);
                        break;

                    case LevelData.SelectedAnimal.Owl:
                        GetAndSetScene(OWL_IBS_NAME);
                        break;
                }
            }
        }

        private void GetAndSetScene(string sceneName)
        {
            prevSceneLoaded = SceneManager.GetActiveScene().name;

            Scene loadedLevel = SceneManager.GetSceneByName(sceneName);
            if (loadedLevel.isLoaded)
            {
                SceneManager.SetActiveScene(loadedLevel);
                SceneManager.UnloadScene(prevSceneLoaded);
                return;
            }

            StartCoroutine(LoadLevel(sceneName, prevSceneLoaded));
        }

        IEnumerator LoadLevel(string sceneName, string prevSceneName)
        {
            enabled = false;
            yield return SceneManager.LoadSceneAsync(
                sceneName, LoadSceneMode.Additive
            );

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            if (prevSceneName != LEVEL_SELECT_NAME)
            {
                SceneManager.UnloadScene(prevSceneName);
            }

            enabled = true;
        }

        public void LoadPuzzleScene()
        {
            GetAndSetScene(selectedPuzzle.ToString());
            isInBetweenFinished = false;
        }

        public void SetLoadingLevelData(GameObject other, LevelData.SelectedPuzzle puzzle, LevelData.SelectedAnimal animal)
        {
            levelObject = other;
            selectedPuzzle = puzzle;
            selectedAnimal = animal;
        }

        public void LoadLevelSelect()
        {
            SceneManager.LoadScene(LEVEL_SELECT_NAME);
        }

        public void LoadLevelSelectFromPuzzle(bool isGameWon)
        {
            GetAndSetScene(LEVEL_SELECT_NAME);
            levelObject.GetComponent<LevelData>().isCompleted = isGameWon;
            ServiceLocator.Locate<LevelMemoryService>().BiomeController.CompletedLevel();
        }
    }
}
