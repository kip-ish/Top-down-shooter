using UnityEngine;

public class Chaser : Enemy {


    protected override void Move() {
        
        Vector2 moveTowardsPlayer = Vector2.MoveTowards (
            transform.position, 
            Player.Instance.transform.position,
            Speed * Time.deltaTime
        );
        
        transform.position = moveTowardsPlayer;
        base.Move();
    }
}