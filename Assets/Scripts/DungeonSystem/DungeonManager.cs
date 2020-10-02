using CombatSystem;
using QuizSystem;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DungeonSystem
{

    public enum DungeonState
    {
        AwaitingDungeon,
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

        public DungeonFloorPanel startPanel;
        public DungeonFloorPanel endPanel;
        public List<DungeonFloorPanel> floors = new List<DungeonFloorPanel>();
        private bool floorReached = false;
        public DungeonState state;

        private bool floorIsRunning = false;

        public EventSO OnActivateFloor;
        public EventSO OnFloorStart;
        public EventSO OnFloorEnd;

        //private FloorProjection floorProjection;
        //private QuizUI quizUi;

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

            //floorProjection = FindObjectOfType<FloorProjection>();
            

            data = WorldState.GetDungeonData();
            difficulty = WorldState.GetDungeonDifficulty();
            

            QuizManager.quiz.SetNewQuestions(data.GetQuestionSheet(difficulty));

            floors.AddRange(transform.GetComponentsInChildren<DungeonFloorPanel>());
            SetupFloors();
            player.transform.position = startPanel.GetMarkerPosition();
        }



        private void Update()
        {
            DungeonLoop();
        }

        private void OnDestroy()
        {
            player.HealCharacter();
        }







        //Public State Changing Functions
            //Use these outside of the class for changing the state

        public void StartDungeon()
        {
            state = DungeonState.SetPlayerPosition;
        }


        /// <summary>
        /// Call this at the end of transition in animation to initialize the event
        /// </summary>
        public void InitializeDungeonEvent()
        {
            OnFloorStart.CallEvent();
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
                floors[currentFloor].UnloadLevel();
                currentFloor++;
                state = DungeonState.SetPlayerPosition;
            }
        }








        //States
        #region State

        private void DungeonLoop()
        {
            switch (state)
            {
                case DungeonState.AwaitingDungeon:
                    AwaitingDungeonState();
                    break;
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

        private void AwaitingDungeonState()
        {

        }


        private void PlayerPositionState()
        {
            if (DungeonIsFinished())
            {
                player.MoveToPosition(endPanel.GetMarkerPosition());
                state = DungeonState.WaitForPlayer;
            }
            else
            {
                player.MoveToPosition(GetFloorPosition());
                state = DungeonState.WaitForPlayer;
            }
        }

        private void WaitForPlayerState()
        {
            if (!player.IsMoving())
            {
                if (!DungeonIsFinished())
                {
                    state = DungeonState.ActivateFloor;
                }
                else
                {
                    state = DungeonState.DungeonComplete;
                }
            }
        }

        private void ActivateFloorState()
        {
            floors[currentFloor].LoadLevel();
            floorIsRunning = true;
            OnActivateFloor.CallEvent();
            state = DungeonState.WaitForFloor;
        }



        private void WaitForFloorState()
        {
            if (!floorIsRunning)
            {
                OnFloorEnd.CallEvent();
            }
        }

        private void DungeonCompleteState()
        {
            if (!player.IsMoving())
            {
                victoryPanel.SetActive(true);
            }
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
            for(int i = 0; i < floors.Count; i++)
            {
                DungeonFloorData newFloor = data.GetRandomFloor();
                floors[i].SetData(newFloor);
            }
        }

        private bool DungeonIsFinished()
        {
            return currentFloor == floors.Count;
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
            return panel.GetMarkerPosition();
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