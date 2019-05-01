using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepMove : ClickObject , Hello{

    
    [SerializeField]
    [Range(10,720)]
    float RotateSpeed = 360f;

    Rigidbody mRB;
    public  Rigidbody RB {  //Get RB on demand first time its used
        get {
            if(mRB==null) {
                mRB = GetComponent<Rigidbody>();
            }
            return mRB;
        }
    }

    public void DoHello() {
        throw new System.NotImplementedException();
    }

    private void FixedUpdate() {
        float tHorizontal = Input.GetAxis("Horizontal");
        float tVertical = Input.GetAxis("Vertical");
        RB.rotation *= Quaternion.Euler(0, tHorizontal * RotateSpeed * Time.deltaTime, 0);
        if(Mathf.Abs(tHorizontal)<0.1f) { //Stop rotational inertia if no input
            RB.angularVelocity = Vector3.zero;
        }
        RB.AddForce(transform.forward*tVertical, ForceMode.VelocityChange);
    }

}
