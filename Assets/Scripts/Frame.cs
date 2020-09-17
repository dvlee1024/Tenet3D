using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame
{
    public Vector3 postion;
    public Quaternion rotation;
    public ActorAction action;

    public Frame nextFrame;
}

public enum ActorAction
{
    IDLE,
    WALK,
    RUN,
    ATTACK,
}
