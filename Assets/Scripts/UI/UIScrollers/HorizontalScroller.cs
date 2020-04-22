﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DN.SceneManagement.Data;
using DN.SceneManagement;

namespace DN.UI
{
    ///<summary>
    ///In this class i check wich button is the closest to the conter and snap it to that button.
    ///I also set the sizes of the buttons and the interactivability here.
    ///<summary>
    public class HorizontalScroller : MonoBehaviour
    {
        public int CurrentBtnIndex => currentBtnIndex;

        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform center;

        [SerializeField] private GameObject parent;

        [SerializeField] private LevelsData levelsData;
        [SerializeField] private HorizontalScrollerLevelLoader horizontalScrollerLevelLoader;

        private float[] distance;
        private bool dragging = false;

        private int btnDistance;
        private int minBtnNumb;
        private int currentBtnIndex = 0;

        private Vector2 minSize = new Vector2(150, 150);
        private Vector2 maxSize = new Vector2(250, 250);

        private const int ORIGIN_DISTANCE = 510;
        private const float LERP_SPEED = 10f;

        private List<RectTransform> currentRect = new List<RectTransform>();
        private List<Button> currentButton = new List<Button>();
        private List<Button> btn = new List<Button>();

        private void Start()
        {
            int btnLength = btn.Count;
            distance = new float[btnLength];

            for (int i = 0; i < btn.Count; i++)
            {
                currentRect.Add(btn[i].transform.GetChild(0).GetComponent<RectTransform>());
                currentButton.Add(btn[i].transform.GetChild(0).GetComponent<Button>());
            }
        }

        private void Update()
        {
            if (!dragging)
            {
                LerpToBtn(minBtnNumb * -btnDistance);
            }

            CheckDistanceCenterToButton();
            AnimateButtons();
        }

        private void AnimateButtons()
        {
            float minDistance = Mathf.Min(distance);

            for (int j = 0; j < btn.Count; j++)
            {
                if (Mathf.Approximately(minDistance, distance[j]))
                {
                    minBtnNumb = j;
                    currentBtnIndex = j;
                    LerpBetweenButtonsSize(j, maxSize);
                    SetButtonInteractive(j, !levelsData.Levels[j].IsLocked);
                }
                else
                {
                    LerpBetweenButtonsSize(j, minSize);
                    SetButtonInteractive(j, !levelsData.Levels[j].IsLocked);
                }
            }
        }

        private void CheckDistanceCenterToButton()
        {
            if (btnDistance != ORIGIN_DISTANCE)
            {
                CalculateDistance();
            }

            for (int i = 0; i < btn.Count; i++)
            {
                distance[i] = Mathf.Abs(center.transform.position.x - btn[i].transform.position.x);
            }
        }

        private void LerpToBtn(int position)
        {
            float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * LERP_SPEED);
            Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

            panel.anchoredPosition = newPosition;
        }

        private void CalculateDistance()
        {
            btnDistance = (int)Mathf.Abs(btn[0].GetComponent<RectTransform>().anchoredPosition.x - btn[1].GetComponent<RectTransform>().anchoredPosition.x);
        }

        private void SetButtonInteractive(int i, bool value)
        {
            currentButton[i].interactable = value;
        }

        private void LerpBetweenButtonsSize(int i, Vector2 size)
        {
            currentRect[i].sizeDelta = Vector2.Lerp(currentRect[i].sizeDelta, size, Time.deltaTime * LERP_SPEED);
        }

        public void StartDrag()
        {
            dragging = true;
        }

        public void EndDrag()
        {
            dragging = false;
        }

        public void AddButtons(Button buttons)
        {
            btn.Add(buttons);
        }
    }
}
