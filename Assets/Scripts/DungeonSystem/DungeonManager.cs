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
        private FloorProjectionManager projection;

        public EventSO onVictory;
        public EventSO onDefeat;

        //private FloorProjection floorProjection;
        //private QuizUI quizUi;

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
            

            QuizManager.quiz.SetNewQuestions(WorldState.GetQuestionSheet());
            projection = FindObjectOfType<FloorProjectionManager>();

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
            ChangeState(DungeonState.SetPlayerPosition);
        }

        /// <summary>
        /// Attach to event OnPlayerReachedPositionInDungeon
        /// Causes next floor to activate if it exists
        /// Otherwise sets dungeon state to completed
        /// </summary>
        public void CheckDungeonStateAfterCharacterStops()
        {
            if (!DungeonIsFinished())
            {
                ChangeState(DungeonState.ActivateFloor);
            }
            else
            {
                ChangeState(DungeonState.DungeonComplete);
            }
        }


        /// <summary>
        /// Call this in the OnDeactivateFloor event
        /// </summary>
        public void ProgressFloor()
        {

            if (PlayerIsDead())
            {
                Debug.Log("Player is dead");
                ChangeState(DungeonState.GameOver);
                player.SetAnimationBool("isDead", true);
            }
            else
            {
                Debug.Log("Player is not dead");
                floors[currentFloor].UnloadLevel();
                currentFloor++;
                ChangeState(DungeonState.SetPlayerPosition);
            }
        }

        /// <summary>
        /// Sets the background music in the AudioManager to the one for the dungeon
        /// </summary>
        public void ChangeBackgroundMusic(){
            AudioClip backgroundMusic = data.GetBackgroundMusic();
            AudioManager.ChangeBackgroundMusic(backgroundMusic);
            
        }

        public AudioClip GetCombatMusic(){
            return data.GetCombatMusic();
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
            }
            else
            {
                player.MoveToPosition(GetFloorPosition());
            }
            ChangeState(DungeonState.WaitForPlayer);
        }

        private void WaitForPlayerState()
        {
            if (!player.IsMoving())
            {
                if (!DungeonIsFinished())
                {
                    ChangeState(DungeonState.ActivateFloor);
                }
                else
                {
                    ChangeState(DungeonState.DungeonComplete);
                }
            }
        }

        private void ActivateFloorState()
        {
            floors[currentFloor].LoadLevel();
            floorIsRunning = true;
            projection.ActivateProjection();
            ChangeState(DungeonState.WaitForFloor);
        }



        private void WaitForFloorState()
        {
            if (!floorIsRunning)
            {
                
            }
        }

        private void DungeonCompleteState()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                string hubName = WorldState.GetHubName();
                WorldState.LoadLevel(hubName);
            }
        }

        private void GameOverState()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)){
                string hubName = WorldState.GetHubName();
                WorldState.LoadLevel(hubName);
            }
        }
        #endregion


        private void ChangeState(DungeonState newState)
        {
            switch (newState)
            {
                case DungeonState.AwaitingDungeon:
                    break;
                case DungeonState.SetPlayerPosition:
                    break;
                case DungeonState.WaitForPlayer:
                    break;
                case DungeonState.ActivateFloor:
                    break;
                case DungeonState.WaitForFloor:
                    break;
                case DungeonState.DungeonComplete:
                    onVictory?.CallEvent();
                    break;
                case DungeonState.GameOver:
                    onDefeat?.CallEvent();
                    break;
            }
            state = newState;
        }


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
            
        }

        private bool PlayerIsDead()
        {
            return WorldState.GetPlayerData().currentHealth == 0;
        }
    }
}