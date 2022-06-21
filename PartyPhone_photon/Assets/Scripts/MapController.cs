using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour, IOnEventCallback
{

   // public GameObject CellPrefab;
   // public GameObject BottlePrefab;

    private GameObject bottle;
    private GameObject[,] cells;
    private List<PlayControls> players = new List<PlayControls>();

    private double lastTickTime;

    public void AddPlayer(PlayControls player) {
        players.Add(player);

        //cells[player.GamePos.x, player.GamePos.y].SetActive(false);
    }

    public void OnQuestionClicked() {
        SceneManager.LoadScene("Question");
    }

    public void OnBottleClicked()
    {
        SceneManager.LoadScene("Bottle");
    }

    public void OnWheelClicked()
    {
        SceneManager.LoadScene("Wheel");
    }

    // Start is called before the first frame update
    void Start()
    {
        //cells = new GameObject[20, 10];

       // for (int i = 0; i < cells.GetLength(0); i++) {
        //    for (int j = 0; j < cells.GetLength(1); j++)
        //    {
        //        cells[i, j] = Instantiate(CellPrefab, new Vector3(i, j), Quaternion.identity);
         //   }
      //  }
   
       // bottle = Instantiate(BottlePrefab, new Vector3(5, 5), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.Time > lastTickTime + 1 &&
            PhotonNetwork.IsMasterClient &&
            PhotonNetwork.CurrentRoom.PlayerCount == 2) {

            //разослать события
            Vector2Int[] directions = players
                .OrderBy(p => p.photonView.Owner.ActorNumber)
                .Select(p => p.dir)
                .ToArray();

            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            SendOptions sendOptions = new SendOptions { Reliability = true };

            PhotonNetwork.RaiseEvent(42,directions, options, sendOptions);
            //сделать шаг
            PerformTick(directions);
        }
    }


    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code) {
            case 42:
                Vector2Int[] directions = (Vector2Int[])photonEvent.CustomData;
                PerformTick(directions);
                break;
        }
    }



    private void PerformTick(Vector2Int[] directions)
    {
        if (players.Count != directions.Length) return;
        /*
        int i = 0;
        foreach (var player in players.OrderBy(p => p.photonView.Owner.ActorNumber)) {
            player.dir = directions[i++];

            player.GamePos += player.dir;

            if (player.GamePos.x < 0) player.GamePos.x = 0;
            if (player.GamePos.y < 0) player.GamePos.y = 0;
            if (player.GamePos.x >= cells.GetLength(0)) player.GamePos.x = cells.GetLength(0)-1;
            if (player.GamePos.y >= cells.GetLength(1)) player.GamePos.y = cells.GetLength(1) - 1;

            cells[player.GamePos.x, player.GamePos.y].SetActive(false);
        }

        lastTickTime = PhotonNetwork.Time;
        */
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
