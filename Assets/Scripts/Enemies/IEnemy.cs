using UnityEngine;

public interface IEnemy
{
    void Hit(GameObject hitter);
    bool IsAlife { get; }    
}
