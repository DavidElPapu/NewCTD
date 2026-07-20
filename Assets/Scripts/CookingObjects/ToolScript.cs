using UnityEngine;

public abstract class ToolScript : CookingObject
{
    public override bool TryEnterItem(CookingObject item)
    {
        return false;
    }
}
