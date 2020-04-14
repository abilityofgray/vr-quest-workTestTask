using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsData : MonoBehaviour {

    public TextMeshProUGUI itemsPorperties;

    Dictionary<string, GameObject> itemsData = new Dictionary<string, GameObject>();

    float power = 0;
    bool superPowerItemOnTable;

    private void Awake()
    {
        //Broadcaste.itemOnTable += this.PlaceItemOnTable;
        Broadcaste.itemRemoveFromTable += this.ItemRemoveFromTable;
        Broadcaste.itemPlaceOnTable += this.ItemPlaceOnTable;
    }
    // Use this for initialization
    void Start () {

        superPowerItemOnTable = false;

        GameObject[] go = GameObject.FindGameObjectsWithTag("item");

        for (int i = 0; i < go.Length; i++) {

                itemsData.Add(go[i].name, go[i]);
                //itemsPorperties.text = go[i].GetComponent<Items>().Power.ToString();
                //Debug.Log("<color = red>ITEMS</color>" + go[i].GetComponent<Items>().Power.ToString());
        }

        //Debug.Log("<color = red>ITEMS</color>");
        foreach (KeyValuePair<string, GameObject> kvp in itemsData) {

            //Debug.Log(kvp.Key);

            if (kvp.Value.GetComponent<Items>() != null) {

                //Debug.Log(kvp.Value.GetComponent<Items>().Power.ToString());
               

            }

        }

    }
	
	// Update is called once per frame
	void Update () {


		
	}

    void ItemPlaceOnTable(string s) {


        foreach (KeyValuePair<string, GameObject> kvp in itemsData)
        {

            if (s == kvp.Key)
            {
                if (kvp.Value.GetComponent<Items>() != null)
                {

                    if (kvp.Value.GetComponent<Items>().SuperPower != 0)
                    {

                        power += kvp.Value.GetComponent<Items>().Power * kvp.Value.GetComponent<Items>().SuperPower;
                        itemsPorperties.text = "SuperPower: " + power.ToString();
                        Debug.Log(power);
                        superPowerItemOnTable = true;
                        break;

                    }
                    else {

                        if (superPowerItemOnTable)
                        {

                            power += kvp.Value.GetComponent<Items>().Power;
                            itemsPorperties.text = "SuperPower: " + power.ToString();
                            Debug.Log(power);
                            break;

                        }
                        else if (!superPowerItemOnTable) {

                            power += kvp.Value.GetComponent<Items>().Power;
                            itemsPorperties.text = "Power: " + power.ToString();
                            Debug.Log(power);
                            break;


                        }
                        



                    }
                    
                    
                }


                break;
            }


        }


    }


    /*
    void PlaceItemOnTable(List<string> listOnTable) {

        Debug.Log("LLLLLLLLLLL");
        Debug.Log(listOnTable.Count);
        foreach (string s in listOnTable) {

            Debug.Log(s);
            foreach (KeyValuePair<string, GameObject> kvp in itemsData) {

                if (s == kvp.Key) {

                    //Debug.Log(kvp.Key);
                    if (kvp.Value.GetComponent<Items>() != null) {

                        
                        power += kvp.Value.GetComponent<Items>().Power;
                        itemsPorperties.text =power.ToString() ;
                        Debug.Log(power);
                        break;
                            
                    }
                    Debug.Log("Add Item");
               
                    break;
                }

                




            }

        }


    }
    */

    void ItemRemoveFromTable(string s) {

        Debug.Log("Item Remove from table");
        Debug.Log(s);
        foreach (KeyValuePair<string, GameObject> kvp in itemsData) {

            if (s == kvp.Key) {
                if (kvp.Value.GetComponent<Items>() != null)
                {


                    

                    if (kvp.Value.GetComponent<Items>().SuperPower != 0)
                    {

                        power -= kvp.Value.GetComponent<Items>().Power * kvp.Value.GetComponent<Items>().SuperPower;
                        
                        itemsPorperties.text = "Power: " + power.ToString();
                        superPowerItemOnTable = false;
                        Debug.Log(power);
                        break;

                    }
                    if (kvp.Value.GetComponent<Items>().SuperPower == 0)
                    {
                        if (superPowerItemOnTable)
                        {

                            power -= kvp.Value.GetComponent<Items>().Power;
                            itemsPorperties.text = "SuperPower: " + power.ToString();
                            Debug.Log(power);
                            break;


                        }
                        else if (!superPowerItemOnTable) {

                            power -= kvp.Value.GetComponent<Items>().Power;
                            itemsPorperties.text = "Power: " + power.ToString();
                            superPowerItemOnTable = false;
                            Debug.Log(power);
                            break;


                        }
                        



                    }
                }


                break;
            }


        }

    }
}
