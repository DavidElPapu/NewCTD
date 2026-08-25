using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SetupSelectionManager : MonoBehaviour
{
    public event Action SelectionDone;
    [SerializeField] private LayerMask setupSelectableLayer;
    private List<SetupSelectable> selectablesOnMap = new List<SetupSelectable>();
    private SetupSelectable currentHoverSelectable, currentSelectedSelectable;
    private int setSelectables;

    public void AddMapSelectable(SetupSelectable selectable)
    {
        //This is called by the Map Manager when it creates a station, if it has a selectable component, pass it here to add it to a list of selectables on map
        selectablesOnMap.Add(selectable);
    }

    public void StartSelection()
    {
        //Activates this manager, initializes values and activates all selectable objects in map
        gameObject.SetActive(true);
        currentHoverSelectable = null;
        currentSelectedSelectable = null;
        foreach (SetupSelectable selectable in selectablesOnMap)
        {
            selectable.EnableSetupSelectable();
        }
        setSelectables = 0;

        //Shows UI
        UIManager.singleton.ShowUI(UIType.SetupSelectionUI);
    }

    private void StopSelection()
    {
        //No need to hide ready button since hideUI hides both
        UIManager.singleton.HideUI(UIType.SetupSelectionUI);

        //Disables all selectables and hides the ingredientSelectionUI if there was one active
        if (currentSelectedSelectable != null)
        {
            UnselectSelectedSelectableAndHideUI();
        }
        foreach (SetupSelectable selectable in selectablesOnMap)
        {
            selectable.DisableSetupSelectable();
        }

        //Disables this object since it won't be used in the rest of the level (for now)
        gameObject.SetActive(false);
    }

    public void OnSelectInput(InputAction.CallbackContext context)
    {
        //This avoids the input to affect the world if the mouse is over UI
        if (IsPointerOverUI()) return;

        if (context.phase == InputActionPhase.Performed)
        {
            if (currentHoverSelectable != null)
            {
                //If a hovered object is selected, marks it as selected and opens the selection UI, and unselects the last selected object if there was one
                if (currentSelectedSelectable != null && currentHoverSelectable != currentSelectedSelectable)
                {
                    currentSelectedSelectable.UnSelect();
                }
                currentSelectedSelectable = currentHoverSelectable;
                currentSelectedSelectable.OnSelect();
                UIManager.singleton.ShowObjectUI(currentSelectedSelectable.uiType, currentSelectedSelectable.transform.parent.gameObject);
            }
            else if (currentSelectedSelectable != null)
            {
                //If selects on nothing and there was a selected object, unselects that object and hides the selection UI 
                UnselectSelectedSelectableAndHideUI();
            }
        }
    }

    public void OnPointInput(InputAction.CallbackContext context)
    {
        //This avoids the input to affect the world if the mouse is over UI
        if (IsPointerOverUI())
        {
            if (currentHoverSelectable != null && currentHoverSelectable != currentSelectedSelectable)
                currentHoverSelectable.UnHover();
            currentHoverSelectable = null;
            return;
        }

        //This shoots a ray from the camera to the mouse position
        Ray pointRay = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        if (Physics.Raycast(pointRay, out RaycastHit hit, 100f, setupSelectableLayer, QueryTriggerInteraction.Collide))
        {
            //If the raycast hits a selectable object, marks it as hovered and saves it, if there was already a hovered object, unhovers that one
            if (currentHoverSelectable != null && hit.collider.gameObject == currentHoverSelectable.gameObject) return;
            if (hit.collider.gameObject.TryGetComponent(out SetupSelectable selectable))
            {
                if (currentHoverSelectable != null && currentHoverSelectable != currentSelectedSelectable)
                    currentHoverSelectable.UnHover();
                currentHoverSelectable = selectable;
                currentHoverSelectable.OnHover();
            }
        }
        else
        {
            //If there was a hovered object but is currently hovering over nothing, unhovers the last hovered object
            if (currentHoverSelectable != null && currentHoverSelectable != currentSelectedSelectable)
                currentHoverSelectable.UnHover();
            currentHoverSelectable = null;
        }
    }

    private bool IsPointerOverUI()
    {
        //This whole method checks if the mouse is currently over any UI (this might get changed in the future to support gamepads)

        if (EventSystem.current == null)
            return false;

        if (EventSystem.current.currentInputModule is InputSystemUIInputModule uiModule)
        {
            if (Mouse.current == null)
                return false;
            //This is only valid if the mouse was on top of an UI element
            return uiModule.GetLastRaycastResult(Mouse.current.deviceId).isValid;
        }

        return false;
    }

    public void OnSelectableSet()
    {
        if (setSelectables < selectablesOnMap.Count && !currentSelectedSelectable.isSet)
        {
            //Only do this if all selectables are not set already and the selected selectable is not set already
            currentSelectedSelectable.isSet = true;
            setSelectables++;

            //If all selectables are set, shows the ready button to exit selection
            if (setSelectables >= selectablesOnMap.Count)
            {
                UIManager.singleton.ShowUI(UIType.SetupSelectionReadyUI);
            }
        }
        //We don't need to check if currentSelectedSelectable is not null since this method is called only by UI which hides when currentSelectedSelectable is null
        UnselectSelectedSelectableAndHideUI();
    }

    public void OnSelectionReady()
    {
        StopSelection();
        SelectionDone?.Invoke();
    }

    private void UnselectSelectedSelectableAndHideUI()
    {
        UIManager.singleton.HideUI(currentSelectedSelectable.uiType);
        currentSelectedSelectable.UnSelect();
        currentSelectedSelectable = null;
    }
}
