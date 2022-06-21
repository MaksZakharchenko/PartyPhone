using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

public class GameManQuest : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public List<Vector3> pos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

        Vector3 pos1 = new Vector3(-6f, 2f, 0f);
        Vector3 pos2 = new Vector3(0f, 2f, 0f);
        Vector3 pos3 = new Vector3(6f, 2f, 0f);
        Vector3 pos4 = new Vector3(-4f, -3f, 0f);
        Vector3 pos5 = new Vector3(4f, -3f, 0f);

        pos.Add(pos1);
        pos.Add(pos2);
        pos.Add(pos3);
        pos.Add(pos4);
        pos.Add(pos5);


        int rnd = UnityEngine.Random.Range(0, pos.Count);
        Debug.Log(rnd);
        PhotonNetwork.Instantiate(playerPrefab.name, pos[rnd], Quaternion.identity);
        pos.RemoveAt(rnd);

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Game");
    }

    public void Leave()
{
    PhotonNetwork.LeaveRoom();
}

public override void OnLeftRoom()
{
    //текущий игрок покидает комнату
    SceneManager.LoadScene(0);
}

public override void OnPlayerEnteredRoom(Player newPlayer)
{
    Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
}

public override void OnPlayerLeftRoom(Player otherPlayer)
{
    Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
}

public static object DeserializeVector2Int(byte[] data)
{
    Vector2Int result = new Vector2Int();

    result.x = BitConverter.ToInt32(data, 0);
    result.y = BitConverter.ToInt32(data, 4);

    return result;
}

public static byte[] SerializeVector2Int(object obj)
{
    Vector2Int vector = (Vector2Int)obj;
    byte[] result = new byte[8];

    BitConverter.GetBytes(vector.x).CopyTo(result, 0);
    BitConverter.GetBytes(vector.y).CopyTo(result, 4);

    return result;
}

}
