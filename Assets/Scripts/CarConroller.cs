using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 네트워크 기능을 위해 추가
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using PacketPackage;
using System.Threading;
using System.Threading.Tasks;
using System;

public class CarConroller : MonoBehaviour
{
    public GameDirector gameDirector;
    public TextMeshProUGUI chanceText;
    // 게임에서 적용하는 각종 필드값을 설정
    float speed = 0;
    Vector2 startPos;
    // 네트워크 기능을 위해 추가
    GameObject car;
    GameObject flag;
    float time = 0;
    float swipeLength = 0;

    int chance = 3;
    int speedFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 네트워크 기능을 위해 추가
        car = GameObject.Find("car");
        flag = GameObject.Find("flag");
        chanceText.text = "Chance: " + chance;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Translate(speed, 0, 0);
        speed *= 0.96f;

        if(speed <= 0.001f && speed > 0f)
        {
            speed = 0;
            speedFlag = 0;
        }

        if (gameDirector.IsGameOver() == true || speedFlag == 1)
        {
            return;
        }

        // 스와이프의 길이를 활용해 봅시다.
        if (Input.GetMouseButtonDown(0))
        {
            //this.speed = 0.3f;
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            speedFlag = 1;
            Vector2 endPos = Input.mousePosition;
            swipeLength = endPos.x - startPos.x;

            speed = swipeLength / 1300.0f;
            chance--;
            chanceText.text = "Chance: " + chance;

            if (chance <= 0)
            {
                gameDirector.OnGameOver();
            }
            PacketManager.Instance.SendPacket(new MovePacket(RoomManager.RoomID, gameDirector.Length, time, swipeLength));
            GetComponent<AudioSource>().Play();
        }
    }
}
