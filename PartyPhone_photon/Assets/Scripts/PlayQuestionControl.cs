using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayQuestionControl : MonoBehaviour, IPunObservable
{

    
    public PhotonView photonView;

    public TextMeshPro Nickname;

    int pushes = 0;

    public List<Sprite> questions = new List<Sprite>();
    public SpriteRenderer spr;
    public GameObject vnutr;

    
    bool move = false;
    int rand = 0;
   

    // Start is called before the first frame update
    void Start(){
        spr = vnutr.GetComponent<SpriteRenderer>();

        photonView = GetComponent<PhotonView>();
        FindObjectOfType<MapControlerQuestion>().AddPlayer(this);

        Nickname.SetText(photonView.Owner.NickName);
    }

    private void Update()
    {
        
    }

    [PunRPC]
    public void loadSprite() {
        rand = Random.Range(0, questions.Count);
        spr.sprite = questions[rand];
        questions.RemoveAt(rand);
        
    }

    private void OnMouseDown()
    {
        if (photonView.IsMine)
        {
            if (pushes == 0)
            {
                pushes = 1;
                photonView.RPC("loadSprite", RpcTarget.AllBuffered, null);
                transform.Rotate(0f, 180f, 0f);
                Nickname.transform.Rotate(0f, 180f, 0f);


            }
            else if (pushes == 1)
            {
                pushes = 0;
                //photonView.RPC("loadSprite", RpcTarget.AllBuffered, null);
                transform.Rotate(0f, 180f, 0f);
                Nickname.transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rand);
        }
        else
        {
            rand = (int)stream.ReceiveNext();
        }
    }
}

