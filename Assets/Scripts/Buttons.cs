using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public DisplayMap displayMap;
    public GameObject loadingText;
    public void OnEthnicityButtonPress() {
        Debug.Log("test");
        displayMap.DisplayEthnicityColor();
    }

    public void OnOilButtonPress() {
        Debug.Log("test");
        displayMap.DisplayOilColor();
    }

    public void OnGoldButtonPress() {
        Debug.Log("test");
        displayMap.DisplayGoldColor();
    }

    public void OnWoodButtonPress() {
        Debug.Log("test");
        displayMap.DisplayWoodColor();
    }

    public void OnLandButtonPress() {
        Debug.Log("test");
        displayMap.DisplayLandColor();
    }

    public void ReloadScene() {
        loadingText.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClearButtonPress() {
        displayMap.ClearSelection();
    }

}
