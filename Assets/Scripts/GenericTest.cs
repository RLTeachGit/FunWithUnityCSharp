using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface Hello {
    void DoHello();
}


public class GenericTest  {

    abstract public class Fruit : Hello {
        static int sCurrent=0;  //Give each a unique ID

        abstract public string Name { get;}

        virtual public void    DoHello() {
            Debug.Log("Base Hello");
        }

        public int ID { get; protected set; }
        protected Fruit() {
            ID = ++sCurrent;
        }
        public override string ToString() {
            return Name + ":" + ID;
        }
    }

    public  class Apple : Fruit {
        public override string Name {
            get {
                return "Apple";
            }
        }
    }

    public class Orange : Fruit{
        public override string Name {
            get {
                return "Orange";
            }
        }
    }


    //Make any fruit
    public static T MakeItem<T>() where T : Fruit, new() { //Make sure only items of type fruit are inserted
        T tItem = new T();
        Debug.LogFormat("Added :{0}",tItem.Name);
        return tItem;
    }


    public  void Test() {
        Test1();
        return;
        List<Fruit> tMyList = new List<Fruit>();
        tMyList.Add(MakeItem<Apple>()); //Add types of fruit
        tMyList.Add(MakeItem<Apple>());
        tMyList.Add(MakeItem<Orange>());
        tMyList.Add(MakeItem<Apple>());
        tMyList.Add(MakeItem<Orange>());
        foreach (var tItem in tMyList) {
            Debug.Log(tItem);
        }
    }

    Dictionary<int, object> mTest = new Dictionary<int, object>();
    
    //Checking types at runtime
    Dictionary<string, int> mDict = new Dictionary<string, int>();
    public void Test1() {
        mTest.Add(1, "richard");
        mTest.Add(2, 99);
        foreach(var tObj in mTest) {
            if(tObj.Value.GetType() == typeof(int)) { //Intellisense error, this is valid
                Debug.LogFormat("Method1 {0} is the Int {1}", tObj.Key, (int)tObj.Value);
            } else if (tObj.Value.GetType() == typeof(string)) {
                Debug.LogFormat("Method1 {0} is the string {1}", tObj.Key, (string)tObj.Value);
            }

            var tInt = tObj.Value as int?;  //Nullable int, if conversion fails its null
            var tString = tObj.Value as String; //Strings are nullable anyway

            if (tInt != null) Debug.LogFormat("Method2 {0} is the Int {1}", tObj.Key, tInt);
            if (tString != null) Debug.LogFormat("Method2 {0} is the string {1}", tObj.Key, tString);

        }



        mDict.Add("richard1", 10);
        mDict.Add("tom", 15);
        mDict.Add("sam", 17);
        mDict.Add("abby", 19);
        foreach(var tItem in mDict) {
            Debug.LogFormat("{0} {1}", tItem.Key, tItem.Value);
        }

        Stack<int> mQ = new Stack<int>();
        int tI = 0;

        mQ.Push(tI++);
        mQ.Push(tI++);
        mQ.Push(tI++);
        mQ.Push(tI++);
        mQ.Push(tI++);
        mQ.Push(tI++);
        while(mQ.Count>0) {
            Debug.LogFormat("Dequeue {0}", mQ.Pop());
        }
    }


    public void Test3 () {
        string[] tStrings = { "Hello", "Fred", "Tony" };
        var tIntersect = tStrings[0].Intersect(tStrings[1]);
        foreach(var tItem in tIntersect) {
            Debug.LogFormat("{0}",tItem);
        }
    }

}
