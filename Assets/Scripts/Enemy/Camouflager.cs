using UnityEngine;

public class Camouflager : Enemy {

    [SerializeField] float _camouflageRadius = 3.0f;
    [SerializeField] Sprite _normalShape;
    [SerializeField] Sprite _camoShape;

    SpriteRenderer _spriteRenderer;

    void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Move() {
        base.Move();
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _camouflageRadius);
    }
}