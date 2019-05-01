using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : ClickObject {

    private MeshRenderer mMR; //Lacy Initialisation
    public  Color Colour {
        get {
            if(mMR==null) {
                mMR = GetComponent<MeshRenderer>();
                Debug.Assert(mMR != null, "No Mesh Renderer found");
            }
            return mMR.material.color;
        }
        set {
            if (mMR == null) {
                mMR = GetComponent<MeshRenderer>();
                Debug.Assert(mMR != null, "No Mesh Renderer found");
                mMR.material.color = value;
            }
        }
    }

    private Rigidbody mRB;
    public Rigidbody RB {   //Lazy Initialisation
        get {
            if (mRB == null) { //If not initalised
                mRB = GetComponent<Rigidbody>(); //Initalise it
                Debug.Assert(mRB != null, "No RigidBody found"); //Error
            }
            return mRB;
        }
    } 



	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision vCollision) {
        Colour = Random.ColorHSV(); //Created as its used the first time
        RB.AddForce(Quaternion.Euler(Random.Range(-45,45), //Add a bit of random to the bounce
            0,
            Random.Range(-45,45))*Vector3.up,
            ForceMode.Impulse);
    }

    DisplayList.ClickItem mListItem;

    public override void OnClick() {
        Debug.Log(name+ ":Clicked");
        if(DL!=null) {
            if(mListItem==null) {
                mListItem = DL.AddObject(this);
            } else {
                DL.RemoveObject(mListItem);
                mListItem = null;
            }
        }
    }
}
