﻿using System.Collections;
using UnityEngine;

    public class NPC_Task_2_1 : NPC_Base
    {
        public Animation _animation = null;
        public override IEnumerator Data_Set()
        {
            _animation.Play();
            yield return null;
        }

        public void PlayAnimator(string key)
        {
            _animation.Play(key);
        }
    }