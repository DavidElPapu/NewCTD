using UnityEngine;
using System.Collections;


public class WorldspaceUIControler : MonoBehaviour
{
    private Coroutine rotationUpdateCoroutine = null;

    public void ToggleUIRotationUpdate(bool updateRotation)
    {
        //This starts the coroutine so the UI can always face the camera while the coroutine is active or stops it when the object is placed so its static
        if (updateRotation)
        {
            if (rotationUpdateCoroutine != null)
                StopCoroutine(rotationUpdateCoroutine);
            rotationUpdateCoroutine = StartCoroutine(UIRotationUpdate());
        }
        else
        {
            if (rotationUpdateCoroutine == null) return;
            StopCoroutine(rotationUpdateCoroutine);
            rotationUpdateCoroutine = null;
        }
    }

    private IEnumerator UIRotationUpdate()
    {
        //This acts as an Update method that rotates the UI to face the camera every frame, but can be stoped at any time
        while (true)
        {
            RotateUIToCamera();
            yield return null;
        }
    }

    public void RotateUIToCamera()
    {
        Vector3 targetPosition = transform.position + Camera.main.transform.rotation * Vector3.forward;
        transform.LookAt(targetPosition, Camera.main.transform.rotation * Vector3.up);
    }
}
