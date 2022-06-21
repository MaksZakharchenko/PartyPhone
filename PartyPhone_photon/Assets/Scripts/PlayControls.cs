using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayControls : MonoBehaviour, IPunObservable
{
    public PhotonView photonView;
    private SpriteRenderer spr;

    public Vector2Int dir;
    public TextMeshPro Nickname;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(dir);
        }
        else
        {
            dir = (Vector2Int)stream.ReceiveNext();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        spr = GetComponent<SpriteRenderer>();

        FindObjectOfType<MapController>().AddPlayer(this);

        Nickname.SetText(photonView.Owner.NickName);
        Debug.Log(photonView.Owner.NickName);
    }

    // Update is called once per frame
    void Update()
    {
            if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.A)) dir = Vector2Int.left;
            if (Input.GetKey(KeyCode.S)) dir = Vector2Int.down;
            if (Input.GetKey(KeyCode.D)) dir = Vector2Int.right;
            if (Input.GetKey(KeyCode.W)) dir = Vector2Int.up;


            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            transform.Translate(h * 10f * Time.deltaTime, v * 10f * Time.deltaTime, 0);
        }

        if (dir == Vector2Int.left) spr.flipX = false;
        if (dir == Vector2Int.right) spr.flipX = true;
        
    }
}
