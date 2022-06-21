using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlerQuestion : MonoBehaviour
{
    public List<PlayQuestionControl> players = new List<PlayQuestionControl>();

    public void AddPlayer(PlayQuestionControl player)
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
