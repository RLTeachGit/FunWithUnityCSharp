using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base class for ClickOn Items
public abstract class ClickObject : MonoBehaviour {

    private static DisplayList mDL; //Find Display List when used first time
    protected DisplayList DL {
        get {
            if(mDL == null) {
                mDL = FindObjectOfType<DisplayList>(); //May not be ready yet
            }
            return mDL;
        }
    }

    public virtual void OnClick() { //Handle if not overriden
        Debug.LogFormat("Default OnClick {0}",name);
    }

}
