using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI _waveUIText;
    [SerializeField] TextMeshProUGUI _restartText;
    void Start() {
        WaveManager.Instance.OnWaveUIChanged += UpdateWaveUI;
        _restartText.gameObject.SetActive(false);
    }

    void Update() {
        if(GameManager.Instance.PlayerIsDead) {
            _restartText.gameObject.SetActive(true);
        }
    }

    void UpdateWaveUI(int currentWave) {
        if(GameManager.Instance.PlayerIsDead) return;
        _waveUIText.text = $"Wave : {currentWave}";
    }
}