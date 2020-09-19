using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame
{
    public Vector3 postion;
    public Quaternion rotation;
    public PlayerAction action;

    public Frame nextFrame;
}

public enum PlayerAction
{
    IDLE,
    WALK,
    RUN,
    ATTACK,
}
