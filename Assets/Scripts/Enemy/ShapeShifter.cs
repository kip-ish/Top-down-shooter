using UnityEngine;

public class ShapeShifter : Enemy {


    protected override void Move() {
        transform.position += Speed * Time.deltaTime * _moveDirection;
        base.Move();
    }
}