﻿using QuizSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSystem
{
    public class ChestManager : FloorManager
    {
        public LockPanel locks;
        public float waitTime = 2f;
        private float waitTimer = 0;
        private bool hasAnswered = false;
        private Animator anim;

        private QuizManager quiz;

        // Start is called before the first frame update
        void Start()
        {
            anim = transform.GetComponentInChildren<Animator>();
            quiz = QuizManager.quiz;
            quiz.SubscribeToOnQuestionAnswered(Unlock);
        }

        private void OnDestroy()
        {
            quiz.UnsubscribeFromOnQuestionAnswered(Unlock);
        }



        // Update is called once per frame
        void Update()
        {
            if (hasAnswered)
            {
                Tick();

                if(waitTimer > waitTime)
                {
                    hasAnswered = false;
                    waitTimer = 0;

                    if (locks.IsFinished())
                    {
                        DungeonManager.dungeon.FinishFloor();
                    }
                    else
                    {
                        quiz.AskQuestion();
                    }
                }
            }
        }


        public override void Initialize()
        {
            quiz.AskQuestion();
        }

        private void Unlock(bool correct)
        {
            if (correct)
            {
                locks.AnswerCorrectly();
            }
            else
            {
                locks.AnswerIncorrectly();
            }

            if (locks.IsFinished())
            {
                if (locks.IsUnlocked())
                {
                    UnlockChest();
                }
            }

            hasAnswered = true;
        }

        private void Tick()
        {
            waitTimer += Time.deltaTime;
        }

        private void ResetTimer()
        {
            waitTimer = 0;
        }

        private void UnlockChest()
        {
            anim.SetBool("isOpen", true);
        }
    }
}