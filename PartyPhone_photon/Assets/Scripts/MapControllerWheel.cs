using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllerWheel : MonoBehaviour
{

    private List<WheelControl> players = new List<WheelControl>();

    public void AddPlayer(WheelControl player)
    {
        players.Add(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
