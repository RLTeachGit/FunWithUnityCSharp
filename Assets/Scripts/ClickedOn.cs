using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedOn : MonoBehaviour {


    Camera mCam;
    public  Camera Cam {
        get {
            if(mCam == null) {
                mCam = transform.root.GetComponent<Camera>();
            }
            return mCam;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                ClickObject tCO = hit.transform.gameObject.GetComponent<ClickObject>();


                tCO?.OnClick(); //New way to avoid writing if(not null) {}

            }
        }
    }
}
