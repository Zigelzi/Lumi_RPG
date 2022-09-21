﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Spell : MonoBehaviour
    {
        public UnityEvent onSpellHit;

        public void OnHit()
        {
            onSpellHit?.Invoke();
        }
    }

}