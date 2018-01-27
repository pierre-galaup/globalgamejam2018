using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrice : MonoBehaviour {

    public Text priceValue;
    
	void Start ()
    {
        priceValue = GetComponent<Text>();	
	}

    public void priceUpdate(float value)
    {
        priceValue.text = value + "$";
    }
}
