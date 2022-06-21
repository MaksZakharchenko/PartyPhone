using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControllerBottle : MonoBehaviour
{
    
    private List<PlayBottleControler> players = new List<PlayBottleControler>();

    public Text t1;
    public Text t2;
    public Text t3;

    public float timer = 0f;

    public void AddPlayer(PlayBottleControler player)
    {
        players.Add(player);
    }
    
    void Start()
    {
        //Instantiate(btlPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10f) {
            t1.text = "";
            t2.text = "";
            t3.text = "";
        }
    }
}
