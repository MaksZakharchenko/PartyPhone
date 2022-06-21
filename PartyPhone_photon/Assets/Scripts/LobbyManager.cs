using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public Text LogText;

    public InputField InputNickName;

    private void Start()
    {
        string nickName = PlayerPrefs.GetString("NickName", "Player" + Random.Range(1000, 9999));
        PhotonNetwork.NickName = nickName;
        InputNickName.text = nickName;
        Log("Player's name is set to " + nickName);


        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Log("connected to master");
    }

    public void OnQuitGame() {
        Application.Quit();
    }


    public void CreateRoom() {

        PhotonNetwork.NickName = InputNickName.text;
        PlayerPrefs.SetString("NickName", InputNickName.text);
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }

    public void JoinRoom() {
        PhotonNetwork.NickName = InputNickName.text;
        PlayerPrefs.SetString("NickName", InputNickName.text);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");

        PhotonNetwork.LoadLevel("Game");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }

    private void Update()
    {
        
    }
}
