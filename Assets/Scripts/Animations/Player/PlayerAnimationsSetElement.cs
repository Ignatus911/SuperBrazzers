using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationsSet
{
    public PlayerAnimationsSetElement[] Clips;

    public AnimationClip GetClip(PlayerAnimationState clipState, bool isBig)
    {
        foreach (var clipSet in Clips)
        {
            if (clipSet.ClipValue == clipState)
            return isBig ? clipSet.BigClip : clipSet.LittleClip;
        }
        return null;
    }
}

[Serializable]
public class PlayerAnimationsSetElement
{
    public string ElementName;
    public AnimationClip LittleClip;
    public AnimationClip BigClip;
    public PlayerAnimationState ClipValue;
}

[Serializable]
public enum PlayerAnimationState
{
    Idle,
    Run,
    Stopping,
    Jump,
    Sit,
    BecomeBig,
    BecomeSmall,
    Dead,
    NoState = 9999
}
