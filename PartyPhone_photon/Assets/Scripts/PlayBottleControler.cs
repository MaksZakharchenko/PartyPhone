using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayBottleControler : MonoBehaviour //, IDragHandler, IBeginDragHandler
{

    public PhotonView photonView;

    public TextMeshPro Nickname;

    public float speed = 2f;
    public float timer = 0f;

    public bool AboveHour = true;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        FindObjectOfType<MapControllerBottle>().AddPlayer(this);

        Nickname.SetText(photonView.Owner.NickName);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0f)
        {
            timer += Time.deltaTime;
        }
        if (speed < 0f) {
            speed = 0f;
        }
        if (timer > 1.5f)
        {
            speed -= 0.15f;
            timer = 0f;
        }

        if (photonView.IsMine)
            Swipe();
        

    }

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log("x: " + firstPressPos.x + " y: " + firstPressPos.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            Debug.Log(currentSwipe);

            //normalize the 2d vector
            currentSwipe.Normalize();
            currentSwipe = currentSwipe  * Random.Range(1.5f,5f);
            
            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -5f && currentSwipe.x < 5f)
            {
                speed = currentSwipe.y;
                AboveHour = false;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -5f && currentSwipe.x < 5f)
            {
                speed = -currentSwipe.y;
                AboveHour = true;
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -5f && currentSwipe.y < 5f)
            {
                speed = -currentSwipe.x;
                AboveHour = true;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -5f && currentSwipe.y < 5f)
            {
                speed = currentSwipe.x;
                AboveHour = false;
            }
        }

    

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }


            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();
                currentSwipe = currentSwipe * Random.Range(1f, 3f);

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -5f && currentSwipe.x < 5f)
                {
                    speed = currentSwipe.y;
                    AboveHour = false;
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -5f && currentSwipe.x < 5f)
                {
                    speed = -currentSwipe.y;
                    AboveHour = true;
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -5f && currentSwipe.y < 5f)
                {
                    speed = -currentSwipe.x;
                    AboveHour = true;
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -5f && currentSwipe.y < 5f)
                {
                    speed = currentSwipe.x;
                    AboveHour = false;
                }
            }
        }

        if (AboveHour == true) this.transform.Rotate(0f, 0f, -speed);
        else this.transform.Rotate(0f, 0f, speed);
    }
}
