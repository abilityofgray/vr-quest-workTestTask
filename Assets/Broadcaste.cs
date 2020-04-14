using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaste : MonoBehaviour {

	public static Broadcaste Instanse {

		get { return instance; }


	}

	private static Broadcaste instance = null;


	private void Awake (){

		if (instance) {

			DestroyImmediate (gameObject);
			return;



		}

		instance = this;
		DestroyImmediate (gameObject);

	}

    public delegate void GameEvent();
	public delegate void Event(string s);
    public delegate void EventItemsOnTable(List<string> itemList);



	public static event Event itemsTake;
    public static event EventItemsOnTable itemOnTable;
    public static event Event itemRemoveFromTable;
    public static event Event itemPlaceOnTable;
    public static event GameEvent startTestEvent;
    public static event GameEvent stopTestEvent;


    public static void ItemsTake (string s){


		if (itemsTake != null)
			itemsTake (s);

	}


    public static void ItemOnTable(List<string> itemList) {

        if (itemOnTable != null) itemOnTable(itemList);


    }

    public static void ItemRemoveFromTable(string s)
    {

        if (itemRemoveFromTable != null) itemRemoveFromTable(s);


    }
    public static void ItemPlaceOnTable(string s)
    {

        if (itemPlaceOnTable != null) itemPlaceOnTable(s);


    }

    public static void StartEventTest() {

        if (startTestEvent != null) startTestEvent();

    }

    public static void StopEventTest()
    {

        if (stopTestEvent != null) stopTestEvent();

    }
}
