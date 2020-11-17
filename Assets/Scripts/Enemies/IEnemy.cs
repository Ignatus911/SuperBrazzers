using UnityEngine;

public interface IEnemy
{
    void Hit(GameObject hitter, bool hitDirection);
    void DieFromSuperPlayer(GameObject hitter, bool hitterDirection);
    bool IsAlife { get; }    
}
