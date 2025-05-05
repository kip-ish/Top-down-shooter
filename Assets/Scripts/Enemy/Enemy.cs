using UnityEngine;

public class Enemy : MonoBehaviour {

    public float Speed = 2f;
    protected Vector3 _moveDirection;

    void Update() {
        Move();
    }

    protected virtual void Move(){}

    public void SetMoveDirection(Vector3 direction) => _moveDirection = direction;
}
