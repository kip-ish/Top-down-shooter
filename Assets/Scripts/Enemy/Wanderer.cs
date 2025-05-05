using UnityEngine;

public class Wanderer : Enemy {


    
    protected override void Move() {
        transform.position += Speed * Time.deltaTime * _moveDirection;
        base.Move();
    }

}