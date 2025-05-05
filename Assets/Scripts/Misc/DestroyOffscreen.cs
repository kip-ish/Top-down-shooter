using UnityEngine;

public class DestroyOffscreen : MonoBehaviour {
    Camera _mainCam;
    float _buffer = 1f;

    void Start() {
        _mainCam = Camera.main;
    }

    void Update() {
        Vector3 viewPos = _mainCam.WorldToViewportPoint(transform.position);

        // Offscreen if outside viewport (0..1 range), with some buffer
        if (viewPos.x < -_buffer || viewPos.x > 1 + _buffer ||
            viewPos.y < -_buffer || viewPos.y > 1 + _buffer)
        {
            Destroy(gameObject);
        }
    }
}