using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public DisplayMap displayMap;
    public void OnEthnicityButtonPress() {
        Debug.Log("test");
        displayMap.displayEthnicityColor();
    }

    public void OnOilButtonPress() {
        Debug.Log("test");
        displayMap.displayOilColor();
    }

    public void OnGoldButtonPress() {
        Debug.Log("test");
        displayMap.displayGoldColor();
    }

    public void OnWoodButtonPress() {
        Debug.Log("test");
        displayMap.displayWoodColor();
    }

    public void OnLandButtonPress() {
        Debug.Log("test");
        displayMap.displayLandColor();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
