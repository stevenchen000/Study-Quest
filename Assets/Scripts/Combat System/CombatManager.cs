using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [Serializable]
    public class CombatState
    {
        public string stateName;
        public bool isDefault = false;

        public List<CombatStateAction> startActions = new List<CombatStateAction>();
        public List<CombatStateAction> runActions = new List<CombatStateAction>();
        public List<CombatStateAction> endActions = new List<CombatStateAction>();

        public List<CombatStateTransition> transitions = new List<CombatStateTransition>();

        public void StartState(CombatManager manager)
        {
            RunActions(startActions, manager);
        }

        public void RunState(CombatManager manager)
        {
            RunActions(runActions, manager);
            CheckTransitions(manager);
        }

        public void EndState(CombatManager manager)
        {
            RunActions(endActions, manager);
        }
        




        /* "Serialization" */

        public void UpdateData(CombatStateList list)
        {
            for(int i = 0; i < transitions.Count; i++)
            {
                transitions[i].UpdateData(list);
            }
        }
        


        /* Helper functions */

        private void RunActions(List<CombatStateAction> actions, CombatManager manager)
        {
            for(int i = 0; i < actions.Count; i++)
            {
                actions[i].CallAction(manager);
            }
        }

        private void CheckTransitions(CombatManager manager)
        {
            for(int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].ConditionsMet(manager))
                {
                    //manager.ChangeState(transitions[i].toStateIndex);
                }
            }
        }
    }

    [Serializable]
    public class CombatStateTransition
    {
        public string description;
        public List<CombatStateCondition> conditions = new List<CombatStateCondition>();
        public int toStateIndex;
        public string stateName;

        public bool ConditionsMet(CombatManager manager)
        {
            bool result = true;

            for(int i = 0; i < conditions.Count; i++)
            {
                if(conditions[i] != null)
                {
                    result &= conditions[i].ConditionMet(manager);

                    if (!result)
                    {
                        break;
                    }
                }
            }

            return result;
        }


        public void UpdateData(CombatStateList list)
        {
            List<CombatState> states = list.states;

            if (toStateIndex >= 0 && toStateIndex < states.Count)
            {
                stateName = list.states[toStateIndex].stateName;
                description = $"-> {stateName}";
            }
            else
            {
                stateName = "Error: Invalid Index";
                description = stateName;
            }
        }
    }

    [CreateAssetMenu(menuName = "Combat State/State List")]
    public class CombatStateList : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<CombatState> states = new List<CombatState>();

        public CombatState GetStartState()
        {
            CombatState result = null;

            for(int i = 0; i < states.Count; i++)
            {
                if (states[i].isDefault)
                {
                    result = states[i];
                    break;
                }else if(i == 0)
                {
                    result = states[0];
                }
            }

            return result;
        }



        public void OnAfterDeserialize()
        {
            for(int i = 0; i < states.Count; i++)
            {
                states[i].UpdateData(this);
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }

    public enum StateEnum
    {
        TransitionIn,
        SelectAction,
        AnswerQuestion,
        CharacterAttack,
        BattleOver,
        TransitionOut
    }









    public class CombatManager : MonoBehaviour
    {
        /*public CombatStateList states;
        public CombatState currState;*/
        public StateEnum currentState;
        public StateEnum newState;
        public float currStateDuration = 0;

        public Fighter player;
        public Fighter enemy;


        //public Action currentAction
        public Fighter currentAttacker;


        public bool isAwaitingInput = false;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            RunState();

            if(currentState == newState)
            {
                currStateDuration += Time.deltaTime;
            }
            else
            {
                currentState = newState;
                currStateDuration = 0;
            }
        }

        /* States */

        public void RunState()
        {
            switch (currentState)
            {
                case StateEnum.TransitionIn:
                    TransitionIn();
                    break;
                case StateEnum.SelectAction:
                    SelectAction();
                    break;
                case StateEnum.AnswerQuestion:
                    AnswerQuestion();
                    break;
                case StateEnum.CharacterAttack:
                    CharacterAttack();
                    break;
                case StateEnum.BattleOver:
                    BattleOver();
                    break;
                case StateEnum.TransitionOut:
                    TransitionOut();
                    break;
            }
        }

        private void TransitionIn()
        {
            ChangeState(StateEnum.AnswerQuestion);
        }

        private void SelectAction()
        {
            if (currStateDuration == 0)
            {
                //Enable select action UI
            }
            else
            {
                //await action
            }
        }

        private void AnswerQuestion()
        {
            if(currStateDuration == 0)
            {

            }
        }

        private void CharacterAttack()
        {

        }

        private void BattleOver()
        {

        }

        private void TransitionOut()
        {

        }

        private void ChangeState(StateEnum state)
        {
            newState = state;
        }


        /*public void ChangeState(int index)
        {
            
            if (currState != null)
            {
                currState.EndState(this);
            }
            currState = states.states[index];
            currState.StartState(this);
        }

        public void ChangeState(CombatState state)
        {
            if (currState != null)
            {
                currState.EndState(this);
            }
            currState = state;
            currState.StartState(this);
        }*/

    }
}