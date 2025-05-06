using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI _currentWaveUI;

    void Start() {
        WaveManager.Instance.OnWaveChange += ChangeWaveUI;
    }

    void ChangeWaveUI(int currentWave) {
        Debug.Log(currentWave);
        _currentWaveUI.text = $"Wave : "+ currentWave.ToString();
    }
}
