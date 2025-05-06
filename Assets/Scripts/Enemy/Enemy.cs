using UnityEngine;

public class Enemy : MonoBehaviour {
  
  public float Speed = 2f;
  protected Vector3 _moveDirection;
  protected float _speedMultiplier = 1;
  [SerializeField] private float _enemyRadius;

  void Update() {
    HitPlayer();
    Move();
  }

  protected virtual void Move(){}

  public void SetMoveDirection(Vector3 direction, float speedMultiplier = 1) {
    _moveDirection = direction;
    _speedMultiplier = speedMultiplier;
  }

  void HitPlayer() {
    float distance = 0.15f;
    RaycastHit2D hitPlayer = Physics2D.CircleCast(transform.position, _enemyRadius, transform.up, distance);
    if(hitPlayer && hitPlayer.transform.TryGetComponent(out Player player)) {
        player.DestroyPlayer();
        Destroy(gameObject);
    }
  }

  void OnDrawGizmos() {
    Gizmos.DrawWireSphere(transform.position, _enemyRadius);
  }

}
