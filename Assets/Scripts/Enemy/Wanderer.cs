using UnityEngine;

public class Wanderer : Enemy {


    
    protected override void Move() {
        transform.position += Speed * _speedMultiplier * Time.deltaTime * _moveDirection;
        base.Move();
    }

}