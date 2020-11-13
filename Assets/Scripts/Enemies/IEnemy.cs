using UnityEngine;

public interface IEnemy
{
    void Hit(GameObject hitter);
    void DieFromSuperPlayer(GameObject hitter);
    bool IsAlife { get; }    
}
