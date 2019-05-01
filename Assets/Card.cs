using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ClickObject {

    static readonly string[] sSuites = { "Spades", "Diamonds", "Hearts", "Clubs" };
    static readonly string[] sValues = { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };


    static Sprite[] sFrontSprites; //These will only be loaded once for all the cards
    static Sprite sBackSprite;
    static GameObject sCardPrefab;
    static Card[] sCards;
    static bool sHasLoaded = false;

    static List<Card> sDeck = new List<Card>();

    SpriteRenderer[] mSRs; //Keep key components cached
    Rigidbody mRB;
    Collider mCOL;


    public int ID { get; private set; }

    bool mShow;
    public bool Show {
        get {
            return mShow;
        }
        set {
            mShow = value;
            StartCoroutine(ShownWhenUpdated(mShow));
        }
    }

    IEnumerator ShownWhenUpdated(bool vShow) {
        yield return new WaitForFixedUpdate();
        mSRs[0].enabled = mSRs[1].enabled = mCOL.enabled = vShow; //Show it
        mRB.isKinematic = !vShow;   //Turn off Physics if not shown
    }

    public Vector3 Position {
        get {
            return mRB.position;
        }
        set {
            mRB.position = value;
        }
    }

    public Vector3 Throw {
        set {
            mRB.AddForce(value,ForceMode.Impulse);
        }
    }

    public float Twist {
        set {
            mRB.AddTorque(Random.rotation * Vector3.one * value, ForceMode.Impulse);
        }
    }

    // Use this for initialization
    void Start () {
        transform.position = mRB.position;
    }

    // Update is called once per frame
    void Update () {
		
    }

    static  public Card Deal() {
        Card tCard = null;
        if(sDeck.Count>0) {
            tCard = sDeck[sDeck.Count - 1]; //Get Last card]
            tCard.transform.position = Vector3.zero;
            tCard.transform.rotation = Quaternion.identity;
            tCard.mRB.velocity = Vector3.zero;
            tCard.mRB.angularVelocity = Vector3.zero;
            sDeck.Remove(tCard); //remove from deck
        }
        return tCard;
    }

    public static void NewDeck() {
        if (!sHasLoaded) { //Only need to load first time we run
            sFrontSprites = Resources.LoadAll<Sprite>("CardFront");
            Debug.Assert(sFrontSprites != null, "Cannot find: CardFront");
            sBackSprite = Resources.Load<Sprite>("CardBack");
            Debug.Assert(sBackSprite != null, "Cannot find: CardBack");
            sCardPrefab = Resources.Load<GameObject>("CardPrefab");
            Debug.Assert(sFrontSprites != null, "Cannot find: CardPrefab");
            sCards = new Card[sFrontSprites.Length];
            for (int i = 0; i < sFrontSprites.Length; i++) {
                Card tCard = Instantiate(sCardPrefab).GetComponent<Card>(); //Make a deck of cards into an array
                Debug.Assert(tCard != null, "Cannot make: Card");
                tCard.mSRs = tCard.GetComponentsInChildren<SpriteRenderer>();
                tCard.mSRs[0].sprite = sFrontSprites[i]; //Make front and back of sprites
                tCard.mSRs[1].sprite = sBackSprite;
                tCard.mRB = tCard.GetComponent<Rigidbody>();
                tCard.mCOL = tCard.GetComponent<Collider>();
                tCard.Show = false; //Hide them for now
                tCard.ID = i;   //Card ID
                tCard.name = string.Format("{0} of {1}", sValues[i % 13], sSuites[i / 13]);
                sCards[i] = tCard;
                sHasLoaded = true; //Only need to do this once
                Random.InitState(System.Environment.TickCount); //Init Random seed
            }
        }
        foreach(var tCard in sCards) {
            tCard.Show = false; //Hide all the cards
            tCard.transform.SetParent(null);
        }
        sDeck.Clear();

        List<Card> tCards = new List<Card>(sCards); //Make Array into List
        while(tCards.Count>0) {
            var tCard = tCards[Random.Range(0, tCards.Count)]; //Pick random item from list
            tCards.Remove(tCard); //Remove from card stack
            sDeck.Add(tCard);   //Add to Deck
        }
    }

    DisplayList.ClickItem mListItem;
    public override void OnClick() {
        Debug.Log(name + ":Clicked");
        if (DL != null) {
            if (mListItem == null) {
                mListItem = DL.AddObject(this); //Add object to list if not there
            } else {
                DL.RemoveObject(mListItem); //Remove if its there
                mListItem = null;
            }
            Throw = Quaternion.Euler(Random.Range(-10f,10f),0,Random.Range(-10f,10f)) * Vector3.up * Random.Range(1f,10f);
            Twist = Random.Range(1f, 10f); //Throw Card up and spin
        }
    }
}
