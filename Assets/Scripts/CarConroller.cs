using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ��Ʈ��ũ ����� ���� �߰�
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
    // ���ӿ��� �����ϴ� ���� �ʵ尪�� ����
    float speed = 0;
    Vector2 startPos;
    // ��Ʈ��ũ ����� ���� �߰�
    GameObject car;
    GameObject flag;
    float time = 0;
    float swipeLength = 0;

    int chance = 3;
    int speedFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ��Ʈ��ũ ����� ���� �߰�
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

        // ���������� ���̸� Ȱ���� ���ô�.
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
