using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public Action<string> AnimationEventAction;

    public void InvokeCustomEvent(string tag)
    {
        AnimationEventAction?.Invoke(tag);
    }
}
