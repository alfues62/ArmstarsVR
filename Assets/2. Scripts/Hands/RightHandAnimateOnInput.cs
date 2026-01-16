using System;
using System.Numerics;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandAnimateOnInput : MonoBehaviour
{
    public InputActionProperty slapAnimationAction;
    public InputActionProperty gripAnimationAction;
    public InputActionProperty pointAnimationAction;
    public Animator HandAnimator;
    public string triggerName = "Trigger";
    public string gripName = "Grip";
    public string pointName = "Point";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = slapAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat(triggerName,triggerValue);
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat(gripName, gripValue);
        float pointValue = pointAnimationAction.action.ReadValue<UnityEngine.Vector2>().y; // al intentar hacer tp: x=0 y=1
        HandAnimator.SetFloat(pointName, pointValue);
    }
}
