using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool IsInvincible { get; private set; }
    public PlayerStatus Status { get; private set; }
    public static Action<PlayerStatus> OnChangeStatus;

    [SerializeField] private PlayerSizeController playerSizeAspet;

    public void BecomeBig()
    {
        if (Status != PlayerStatus.Small)
            return;
        Status = PlayerStatus.Big;
        if (OnChangeStatus != null)
            OnChangeStatus.Invoke(Status);
        playerSizeAspet.setBigCollider();
    }


    public void Hit()
    {
        //Если большой то мелкий
        //если мелкий то умри
        if (Status == PlayerStatus.Small)
        {
            ScoreController.Instance.DecreaseLives();
            //показать количесвто жизней, начать с контрольной точки
            return;
        }
        Status = PlayerStatus.Small;
        OnChangeStatus.Invoke(Status);
        playerSizeAspet.setSmallCollider();
    }
}
