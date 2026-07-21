using UnityEngine;

public abstract class ToolScript : CookingObject
{
    public override bool TryEnterItem(CookingObject item)
    {
        return false;
    }

    public override void OnPlayerInteraction(bool wasPicked)
    {
        //Nothing by default
    }
}
