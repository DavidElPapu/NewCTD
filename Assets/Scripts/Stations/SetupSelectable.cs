using UnityEngine;

public class SetupSelectable : MonoBehaviour
{
    public enum SetupSelectableUI
    {
        IngredientSelectionUI
    }
    public SetupSelectableUI uiType;
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
        outlineMeshRenderer.GetPropertyBlock(propBlock);
        UnSelect();
    }

    public void DisableSetupSelectable()
    {
        gameObject.SetActive(false);
    }

    public void OnHover()
    {
        propBlock.SetFloat("_Emission", 35f);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }

    public void OnSelect()
    {
        propBlock.SetColor("_OutlineColor", Color.yellow);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }

    public void UnHover()
    {
        if (isSet)
            propBlock.SetColor("_OutlineColor", Color.green);
        propBlock.SetFloat("_Emission", 10f);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }

    public void UnSelect()
    {
        if (!isSet)
            propBlock.SetColor("_OutlineColor", Color.white);
        else
            propBlock.SetColor("_OutlineColor", Color.green);
        propBlock.SetFloat("_Emission", 10f);
        outlineMeshRenderer.SetPropertyBlock(propBlock);
    }
}
