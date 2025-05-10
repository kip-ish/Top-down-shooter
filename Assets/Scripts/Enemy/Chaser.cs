using UnityEngine;

public class Chaser : Enemy {


    protected override void Move() {
        
        if(GameManager.Instance.PlayerIsDead) {
            base.Move();
            return;
        }
        
        Vector2 moveTowardsPlayer = Vector2.MoveTowards (
            transform.position, 
            Player.Instance.transform.position,
            Speed * Time.deltaTime * _speedMultiplier
        );
        
        transform.position = moveTowardsPlayer;
        
    }
}