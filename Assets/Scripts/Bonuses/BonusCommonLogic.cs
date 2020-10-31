using UnityEngine;

public abstract class BonusCommonLogic : MonoBehaviour, IBonus
{
    protected bool isAlife = true;

    public void Use(GameObject user)
    {
        if (!isAlife)
            return;
        UseImplementation(user);
    }

    public abstract void UseImplementation(GameObject user);
}
