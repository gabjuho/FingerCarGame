using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 이 부분이 추가되야 합니다.
using UnityEngine.UI;
using TMPro;
using PacketPackage;


public class GameDirector : MonoBehaviour
{
    // 편의를 위해 추가
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

        // 화면 상에 우리가 필요로 하는 정보를 얻어 옵니다.
        length = this.flag.transform.position.x - this.car.transform.position.x;

        // UI 에 표시해 봅니다.
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
