using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RayCheker : MonoBehaviour {


	public Image ring;

	public float replaceDelay = 1;

    public TextMeshProUGUI itemCount;

    Timer _gameTimer;

    int itemCounter;

    float timer  =1;

	float seconds;

    Dictionary<string, bool> positionPoint = new Dictionary<string, bool>();
    Dictionary<string, bool> itemsOnTable = new Dictionary<string, bool>();
    Dictionary<string, Vector3> itemsOnTablePreviousePos = new Dictionary<string, Vector3>();
    Dictionary<string, string> itemsOnPoint = new Dictionary<string, string>();

    List<string> itemPlaceOnTable = new List<string>();
    
	// Use this for initialization
	void Start () {

        itemCounter = 0;
        GameObject[] go = GameObject.FindGameObjectsWithTag("positionPoint");

        for (int i = 0; i < go.Length; i++) {

            //Debug.Log(go[i].name);
            positionPoint.Add(go[i].name, false);

        }

        GameObject[] goItems = GameObject.FindGameObjectsWithTag("item");

        for (int i = 0; i < goItems.Length; i++) {

            itemsOnTable.Add(goItems[i].name, false);
            itemsOnTablePreviousePos.Add(goItems[i].name, goItems[i].transform.position);

        }

        foreach (KeyValuePair<string, Vector3> kvp in itemsOnTablePreviousePos) {


            //Debug.Log( kvp.Key);
            //Debug.Log(kvp.Value);

        }

        _gameTimer = GameObject.FindGameObjectWithTag("TIMER").GetComponent<Timer>();
        


    }
	
	// Update is called once per frame
	void Update () {

        //так можно переключать состояния из любово класса 
        //_gameTimer.game_state = Timer.GAME_STATE.STOP_EVENT;
        //Debug.Log(_gameTimer.game_state);
        //Debug.Log("Ray Cheker");

        Ray ray = new Ray (this.transform.position, transform.forward);
		RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);


        if (Physics.Raycast (ray, out hit, 100) && _gameTimer.game_state == Timer.GAME_STATE.START_EVENT) {

			//Debug.Log (hit.collider.tag);

			ring.fillAmount += 0.1f;

			switch (hit.collider.tag) {

			case "item":

				timer += Time.deltaTime;
				seconds = Mathf.RoundToInt (timer % 60);
                if (seconds == replaceDelay) {

                        // items on table true;
                        foreach (KeyValuePair<string, bool> kvp in itemsOnTable)
                        {

                            
                            if (kvp.Key == hit.collider.name && kvp.Value == false)
                            {
                                Debug.Log("Place to item on table ");
                                PlaceItemToFreePoint(hit);
                                itemsOnTable[kvp.Key] = true;
                                
                                if (itemCounter <= 5) {
                                    itemCounter++;
                                    itemCount.text = itemCounter.ToString();
                                }
                                

                                break;

                            }

                            if (kvp.Key == hit.collider.name && kvp.Value == true)
                            {
                                Debug.Log("Items Already on table");
                                PlaceItemToPreviousePlace(hit);
                                itemsOnTable[kvp.Key] = false;
                                
                                if (itemCounter >= 0) {
                                    itemCounter--;
                                    itemCount.text = itemCounter.ToString();
                                }
                                

                                break;

                            }


                        }

                        

                        timer = 0;
                        seconds = 0;

                    }

				Broadcaste.ItemsTake (hit.collider.tag);
                    
				break;
                
			default:
				break;

			}
		
		
		
		
		
		} else {

			ring.fillAmount = 0;

			seconds = 0;
			timer = 0;
		}
		
	}


    void PlaceItemToFreePoint(RaycastHit hit) {

        //add items to table dictionary
        //check for free position point
        foreach (KeyValuePair<string, bool> kvp in positionPoint)
        {
            
            if (kvp.Value == false) {
                GameObject go = GameObject.Find(kvp.Key);
                hit.transform.position = go.transform.position;
                positionPoint[kvp.Key] = true;
                itemsOnPoint.Add(hit.collider.name, kvp.Key);
                itemPlaceOnTable.Add(hit.collider.name);

                //broadcaste message witch list items
                Broadcaste.ItemPlaceOnTable(hit.collider.name);
                Broadcaste.ItemOnTable(itemPlaceOnTable);
                break;
            }

        }

    }

    void PlaceItemToPreviousePlace(RaycastHit hit) {

        
        foreach (KeyValuePair<string, Vector3> kvp in itemsOnTablePreviousePos) {

            if (hit.collider.name == kvp.Key) {
                
                hit.transform.position = kvp.Value;
                

                foreach (KeyValuePair<string, string> kvp1 in itemsOnPoint)
                {
                    if (hit.collider.name == kvp1.Key) {

                        foreach (KeyValuePair<string, bool> kvp2 in positionPoint)
                        {

                            if (kvp1.Value == kvp2.Key)
                            {

                                positionPoint[kvp2.Key] = false;
                                itemsOnPoint.Remove(kvp1.Key);
                                itemPlaceOnTable.Remove(hit.collider.name);
                                Broadcaste.ItemRemoveFromTable(hit.collider.name);
                                //broadcaste message witch list items
                                Broadcaste.ItemOnTable(itemPlaceOnTable);
                                break;

                            }

                        }

                        break;

                    }



                }
                
                break;
                

            }

        }

    }
}
