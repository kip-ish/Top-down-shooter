using UnityEngine;

public class Camouflager : Enemy {

    [SerializeField] float _camouflageRadius = 5.0f;
    [SerializeField] Sprite _normalShape;
    [SerializeField] Sprite _camoShape;

    SpriteRenderer _spriteRenderer;

    void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Move() {

        if(GameManager.Instance.PlayerIsDead) {
            _spriteRenderer.sprite = _camoShape;
            base.Move();
            return;
        }
        
        if(Vector2.Distance(transform.position, Player.Instance.transform.position) <= _camouflageRadius) {
            _spriteRenderer.sprite = _normalShape;
            Vector2 moveTowardsPlayer = Vector2.MoveTowards(transform.position, 
                Player.Instance.transform.position,
                Speed * Time.deltaTime * _speedMultiplier
            );

            transform.position = moveTowardsPlayer;
        } else {
            _spriteRenderer.sprite = _camoShape;
            base.Move();
        }

        
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _camouflageRadius);
    }
}