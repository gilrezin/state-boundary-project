using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public DisplayMap displayMap;
    public GameObject loadingText;
    public GameManager gameManager;
    public void OnEthnicityButtonPress() {
        displayMap.DisplayEthnicityColor();
    }

    public void OnOilButtonPress() {
        displayMap.DisplayOilColor();
    }

    public void OnGoldButtonPress() {
        displayMap.DisplayGoldColor();
    }

    public void OnWoodButtonPress() {
        displayMap.DisplayWoodColor();
    }

    public void OnLandButtonPress() {
        displayMap.DisplayLandColor();
    }

    public void ReloadScene() {
        loadingText.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClearButtonPress() {
        displayMap.ClearSelection();
    }

    public void CalculateButtonPress() {
        gameManager.CalculateStability();
    }

}
