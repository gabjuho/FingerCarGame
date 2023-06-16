using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� �κ��� �߰��Ǿ� �մϴ�.
using UnityEngine.UI;
using TMPro;
using PacketPackage;


public class GameDirector : MonoBehaviour
{
    // ���Ǹ� ���� �߰�
    GameObject car;
    GameObject flag;
    GameObject distance;

    bool isGameOver = false;
    float length;
    public float Length => length;

    // Start is called before the first frame update
    void Start()
    {
        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.distance = GameObject.Find("DistanceText");
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == true)
        {
            return;
        }

        // ȭ�� �� �츮�� �ʿ�� �ϴ� ������ ��� �ɴϴ�.
        length = this.flag.transform.position.x - this.car.transform.position.x;

        // UI �� ǥ���� ���ϴ�.
        if (length >= 0)
        {
            this.distance.GetComponent<TextMeshProUGUI>().text
                = "Distance from flag is  " + length.ToString("F2") + "m";
        } 
        else
        {
            OnGameOver();
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
        if(length < 0)
        {
            length = -1f;
            this.distance.GetComponent<TextMeshProUGUI>().text = "Game Over~~~\nResult: Over";
        }
        else
        {
            this.distance.GetComponent<TextMeshProUGUI>().text = $"Game Over~~~\nResult: {length.ToString("F2")}m";
        }

        PacketManager.Instance.SendPacket(new GameOverPacket(RoomManager.RoomID, length));
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
