using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class showByAction : MonoBehaviour
{
    public InputAction showHideAction;
    public List<GameObject> objsToShowHide = new List<GameObject>();
    public bool bShowWhilePressing = false;

    private void Start()
    {
        foreach (var obj in objsToShowHide)
        {
            obj.SetActive(false);
        }
    }
    private void OnEnable()
    {
        showHideAction.Enable();
        showHideAction.performed += OnExecute;
        if (bShowWhilePressing)
            showHideAction.canceled += OnExecute;
    }
    private void OnDisable()
    {
        showHideAction.Disable();
        showHideAction.performed += OnExecute;
        if (bShowWhilePressing)
            showHideAction.canceled += OnExecute;
    }
    private void OnExecute(InputAction.CallbackContext context)
    {
        foreach (var obj in objsToShowHide)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    public void removeListObjects()
    {
        objsToShowHide.Clear();
    }

    public void addOjcetToList(GameObject go)
    {
        objsToShowHide.Add(go);
    }
}