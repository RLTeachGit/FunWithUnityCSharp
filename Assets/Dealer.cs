using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour, Hello {



	// Use this for initialization
	void Start () {
        Card.NewDeck();
        StartCoroutine(FireCards());
	}

    IEnumerator FireCards() {  //Runs every second or so

        for(; ; ) {
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            var tCard = Card.Deal();
            if(tCard != null) {
                tCard.transform.SetParent(gameObject.transform, false);
                tCard.Position = new Vector3(Random.Range(-10f, 10f), 
                    Random.Range(10f, 30f), 
                    Random.Range(-10f, 10f)) + transform.position;
                tCard.Show = true;
            }
        }
    }

    public void Clear() {
        Card.NewDeck();
    }


    public void ListTest() {
        List<int> tMyList = new List<int>();
        for(int i=0;i<10;i++) {
            tMyList.Add(Random.Range(1, 100));
        }
        Debug.Log("Before");
        foreach (var tItem in tMyList) {
            Debug.Log(tItem);
        }
        for (int tI = tMyList.Count-1; tI>=0; tI--) {
            if(tMyList[tI]<50) { //Remove low numbers
                tMyList.Remove(tMyList[tI]);
            }
        }
        Debug.Log("After");
        foreach (var tItem in tMyList) {
            Debug.Log(tItem);
        }
    }

    public void DoHello() {
        Debug.Log("Dealer Do Hello");
    }
}
