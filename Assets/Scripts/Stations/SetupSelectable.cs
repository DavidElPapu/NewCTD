using UnityEngine;

public class SetupSelectable : MonoBehaviour
{
    public UIType uiType;
    public bool isSet;
    private MaterialPropertyBlock propBlock;
    private MeshRenderer outlineMeshRenderer;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        TryGetComponent(out outlineMeshRenderer);
    }

    public void EnableSetupSelectable()
    {
        isSet = false;
        gameObject.SetActive(true);
        UnSelect();
    }

    public void DisableSetupSelectable()
    {
        gameObject.SetActive(false);
    }

    public void OnHover()
    {
        if (isSet)
        {
            outlineMeshRenderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_OutlineColor", Color.white);
            outlineMeshRenderer.SetPropertyBlock(propBlock);
        }
        else
            outlineMeshRenderer.enabled = true;
    }

    public void OnSelect()
    {
        outlineMeshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_OutlineColor", Color.yellow);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }

    public void UnHover()
    {
        if (isSet)
        {
            outlineMeshRenderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_OutlineColor", Color.green);
            outlineMeshRenderer.SetPropertyBlock(propBlock);
        }
        else
            outlineMeshRenderer.enabled = false;
    }

    public void UnSelect()
    {
        outlineMeshRenderer.GetPropertyBlock(propBlock);
        if (!isSet)
        {
            propBlock.SetColor("_OutlineColor", Color.white);
            outlineMeshRenderer.enabled = false;
        }
        else
            propBlock.SetColor("_OutlineColor", Color.green);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }
}
