using UnityEngine;

public class Chaser : Enemy {


    protected override void Move() {
        
        if(!Player.Instance) return;
        
        Vector2 moveTowardsPlayer = Vector2.MoveTowards (
            transform.position, 
            Player.Instance.transform.position,
            Speed * Time.deltaTime * _speedMultiplier
        );
        
        transform.position = moveTowardsPlayer;
        base.Move();
    }
}