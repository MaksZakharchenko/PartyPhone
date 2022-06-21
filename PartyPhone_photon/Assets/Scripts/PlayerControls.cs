using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerControls : MonoBehaviour, IPunObservable
{
    public PhotonView photonView;
    private SpriteRenderer spr;

    public Vector2Int dir;
    public Vector2Int GamePos;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(dir);
        }
        else
        {
            dir = (Vector2Int)stream.ReceiveNext();
        }
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        spr = GetComponent<SpriteRenderer>();

        GamePos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        //FindObjectOfType<MapController>().AddPlayer(this);
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
        }

        if (dir == Vector2Int.left) spr.flipX = false;
        if (dir == Vector2Int.right) spr.flipX = true;

        transform.position = Vector3.Lerp(transform.position, (Vector2)GamePos, Time.deltaTime * 3);

    }
}
