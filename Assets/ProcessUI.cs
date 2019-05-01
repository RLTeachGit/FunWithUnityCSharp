using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TestGeneric() {
        GenericTest tGT = new GenericTest();

        tGT.Test();
    }
}
