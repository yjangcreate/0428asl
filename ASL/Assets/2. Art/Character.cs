using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onIdle;

    public UnityEvent OnIdle
    {
        get => onIdle;
        set => onIdle = value;
    }
}
