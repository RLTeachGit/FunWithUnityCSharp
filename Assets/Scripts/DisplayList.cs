using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayList : MonoBehaviour {

    public class ClickItem { //Temp structure for when items put in list

        static  Font sFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        public ClickItem(Transform vParent, ClickObject vClickObject) {
            var tGO = new GameObject(); //New Object, which will be in panel
            tGO.name = vClickObject.name + "Text";
            mText =tGO.AddComponent<Text>(); //Add Text
            var tRT = tGO.GetComponent<RectTransform>(); //Need to set size in RectTransform
            tRT.localPosition = Vector3.zero;
            tRT.sizeDelta = new Vector2(100, 20);
            mText.font = sFont;
            tGO.transform.SetParent(vParent, false);
            mClickObject = vClickObject; //Link Object
            mText.text = mClickObject.name;
        }
        public  Text mText; //Object Text
        public ClickObject mClickObject; //Link to Object
    }

    readonly List<ClickItem> mListItems=new List<ClickItem>(); //A List of ClickItems
    public ClickItem AddObject(ClickObject vObject) {
        ClickItem tLI = new ClickItem(transform,vObject); //Parent it to panel
        mListItems.Add(tLI); //Add item to list
        return tLI;
    }

    public void RemoveObject(ClickItem vListItem) {
        if (vListItem != null) { //If found
            mListItems.Remove(vListItem); //Remove it
            Destroy(vListItem.mText.gameObject); //Delete Text GO
            vListItem.mText = null;
            vListItem.mClickObject = null; //Mark for Garbage collection
        }
    }
}
