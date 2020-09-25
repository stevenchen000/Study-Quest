using CombatSystem;
using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonSystem
{

    public enum DungeonState
    {
        SetPlayerPosition,
        WaitForPlayer,
        ActivateFloor,
        WaitForFloor,
        DungeonComplete,
        GameOver
    }



    public class DungeonManager : MonoBehaviour
    {
        public PlayerExplorer player;
        public int currentFloor = 0;

        public static DungeonManager dungeon;
        public DungeonData data;
        public DungeonDifficulty difficulty;
        public Vector3 playerOffset;
        //list of monsters in area

        public List<DungeonFloorPanel> floors = new List<DungeonFloorPanel>();
        private bool floorReached = false;
        public DungeonState state;

        private bool floorIsRunning = false;

        public delegate void FloorState();
        public event FloorState OnFloorStart;
        public event FloorState OnFloorEnd;

        private FloorProjection floorProjection;
        private QuizUI quizUi;

        public GameObject victoryPanel;
        public GameObject gameoverPanel;

        private void Awake()
        {
            if(dungeon == null)
            {
                dungeon = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            player = FindObjectOfType<PlayerExplorer>();
            player.HealCharacter();

            floorProjection = FindObjectOfType<FloorProjection>();
            quizUi = FindObjectOfType<QuizUI>();
            quizUi.gameObject.SetActive(false);

            //player.transform.position = WorldState.GetDungeonPosition();
            data = WorldState.GetDungeonData();
            difficulty = WorldState.GetDungeonDifficulty();
            

            QuizManager.quiz.SetNewQuestions(data.GetQuestionSheet(difficulty));

            floors.AddRange(transform.GetComponentsInChildren<DungeonFloorPanel>());
            SetupFloors();
        }



        private void Update()
        {
            DungeonLoop();
        }

        private void OnDestroy()
        {
            player.HealCharacter();
        }




        //States
        #region State

        private void DungeonLoop()
        {
            switch (state)
            {
                case DungeonState.SetPlayerPosition:
                    PlayerPositionState();
                    break;
                case DungeonState.WaitForPlayer:
                    WaitForPlayerState();
                    break;
                case DungeonState.ActivateFloor:
                    ActivateFloorState();
                    break;
                case DungeonState.WaitForFloor:
                    WaitForFloorState();
                    break;
                case DungeonState.DungeonComplete:
                    DungeonCompleteState();
                    break;
                case DungeonState.GameOver:
                    GameOverState();
                    break;
            }
        }

        private void PlayerPositionState()
        {
            player.MoveToPosition(GetFloorPosition());
            state = DungeonState.WaitForPlayer;
        }

        private void WaitForPlayerState()
        {
            if (!player.IsMoving())
            {
                state = DungeonState.ActivateFloor;
            }
        }

        private void ActivateFloorState()
        {
            floors[currentFloor].LoadLevel();
            floorIsRunning = true;
            state = DungeonState.WaitForFloor;

            floorProjection.gameObject.SetActive(true);
            floorProjection.ActivateProjection();
        }


        /// <summary>
        /// Call this at the end of transition in animation to initialize the event
        /// </summary>
        public void InitializeDungeonEvent()
        {
            FloorManager floor = FindObjectOfType<FloorManager>();
            floor.Initialize();
        }

        private void WaitForFloorState()
        {
            if (!floorIsRunning)
            {
                floorProjection.DeactivateProjection();
            }
        }

        /// <summary>
        /// Call this after transition out animation to move character to next floor
        /// </summary>
        public void ProgressFloor()
        {

            if (PlayerIsDead())
            {
                Debug.Log("Player is dead");
                state = DungeonState.GameOver;
                player.SetAnimationBool("isDead", true);
            }
            else
            {
                Debug.Log("Player is not dead");
                floorProjection.gameObject.SetActive(false);
                quizUi.gameObject.SetActive(false);
                floors[currentFloor].UnloadLevel();
                currentFloor++;

                if (DungeonIsFinished())
                {
                    state = DungeonState.DungeonComplete;
                }
                else
                {
                    state = DungeonState.SetPlayerPosition;
                }
            }
        }

        private void DungeonCompleteState()
        {
            victoryPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                string loadScreen = WorldState.GetHubName();
                UnityUtilities.LoadLevel(loadScreen);
            }
        }

        private void GameOverState()
        {
            ShowGameOverScreen();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)){
                UnityUtilities.LoadLevel(WorldState.GetHubName());
            }
        }
        #endregion





        private void SetupFloors()
        {
            List<DungeonFloorData> floorData = data.floors;

            for(int i = 0; i < floors.Count; i++)
            {
                if (i < floorData.Count)
                {
                    floors[i].SetData(floorData[i]);
                }
                else
                {
                    floors[i].gameObject.SetActive(false);
                }
            }
        }

        public bool DungeonIsFinished()
        {
            return currentFloor == data.floors.Count;
        }

        private void IncrementFloor()
        {
            currentFloor++;
        }

        public void MovePlayer()
        {
            Vector3 panelPosition = floors[currentFloor].transform.position;
            player.transform.position = Vector3.Lerp(player.transform.position, panelPosition, Time.deltaTime * 2);
            
        }

        private Vector3 GetFloorPosition()
        {
            DungeonFloorPanel panel = floors[currentFloor];
            return panel.transform.position + playerOffset;
        }

        public void FinishFloor()
        {
            floorIsRunning = false;
        }

        public void ShowGameOverScreen()
        {
            gameoverPanel.SetActive(true);
        }

        private bool PlayerIsDead()
        {
            return WorldState.GetPlayerData().currentHealth == 0;
        }
    }
}