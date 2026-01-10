using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandAnimatorOnInput : MonoBehaviour
{
    public InputActionProperty gripAnimationAction;
    public Animator HandAnimator;
    public string gripName = "Grip";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        HandAnimator.SetFloat(gripName, gripValue);
    }
}
