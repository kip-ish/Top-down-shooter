using UnityEngine;

public class Enemy : MonoBehaviour {
  

  HealthSystem _healthSystem;
  protected Vector3 _moveDirection;
  protected float _speedMultiplier = 1;
  public float Speed = 2f;
  [SerializeField] private float _enemyRadius;

  public static Enemy Instance {get; private set;}


  void Awake() {
    if(Instance == null) Instance = this;
  }

  void Start() {
    if(_healthSystem == null) {
        _healthSystem = gameObject.AddComponent<HealthSystem>();
    }
  }

  void Update() {
    HitPlayer();
    Move();
  }

  protected virtual void Move() {
    transform.position += Speed * Time.deltaTime * _speedMultiplier * _moveDirection;
  }

  public void SetMoveDirection(Vector3 direction, float speedMultiplier = 1) {
    _moveDirection = direction;
    _moveDirection.Normalize();
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

  public bool EnemyIsDead => Instance == null;

  void OnDrawGizmos() {
    Gizmos.DrawWireSphere(transform.position, _enemyRadius);
  }

}
