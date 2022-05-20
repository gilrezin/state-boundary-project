using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public GameObject SelectedPixels;
    // Start is called before the first frame update
    void Start() {
        Screen.SetResolution(1077, 606, false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void CalculateStability() 
    {
        SelectedPixels.AddComponent<CompositeCollider2D>();
        Debug.Log("done");
    }
}
