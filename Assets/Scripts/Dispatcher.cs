using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PacketPackage;
using ProtoBuf;
using UnityEngine.SceneManagement;

public class Dispatcher : MonoBehaviour
{
    private static Dispatcher main = null;
    private readonly Queue<Tuple<int, Packet>> queue = new Queue<Tuple<int, Packet>>();
    private Dictionary<int, Action<Packet>> packetDictionary;
    public static string publicKey;

    private void Awake()
    {
        Init();
        packetDictionary = new Dictionary<int, Action<Packet>>();
        packetDictionary.Add(2, ExecuteCheckLoginInfoPacket);
        packetDictionary.Add(3, ExecuteCheckSignupInfoPacket);
        packetDictionary.Add(6, ExecuteMatchSuccessPacket);
        packetDictionary.Add(7, ExecuteRoomIDPacket);
        packetDictionary.Add(9, ExecuteUpdateMovePacket);
        packetDictionary.Add(11, ExecuteMatchResultPacket);
        packetDictionary.Add(13, ExecuteRankPacket);
        packetDictionary.Add(14, ExecutePublicKeyPacket);
    }

    private void Update()
    {
        lock(queue)
        {
            while(queue.Count > 0)
            {
                Tuple<int, Packet> tuple = queue.Dequeue();
                packetDictionary[tuple.Item1](tuple.Item2);
            }
        }
    }

    public static void Init()
    {
        if(main == null)
        {
            main = FindObjectOfType<Dispatcher>();
            if (main == null)
            {
                main = new GameObject("[Dispatcher]").AddComponent<Dispatcher>();
            }
        }

        DontDestroyOnLoad(main.gameObject);
    }

    public static void Register(int head, Packet packet)
    {
        lock(main.queue)
        {
            main.queue.Enqueue(new Tuple<int, Packet>(head, packet));
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void ResetDomain()
    {
        main = null;
    }

    private void ExecuteCheckLoginInfoPacket(Packet packet)
    {
        CheckLoginInfoPacket checkLoginInfoPacket = packet as CheckLoginInfoPacket;

        if (checkLoginInfoPacket.IsCorrect == true)
        {
            LoginManager.GoLobby();
        }
        else
        {
            LoginUIManager.ShowLoginErrorPopUpUI();
        }
    }

    private void ExecuteCheckSignupInfoPacket(Packet packet)
    {
        CheckSignupInfoPacket checkSignupInfoPacket = packet as CheckSignupInfoPacket;
        if (checkSignupInfoPacket.IsCorrect == true)
        {
            LoginUIManager.ShowSignupSuccessPopUpUI();
        }
        else
        {
            LoginUIManager.ShowSignupErrorPopUpUI();
        }
    }
    private void ExecuteMatchSuccessPacket(Packet packet)
    {
        SceneManager.LoadSceneAsync("Main");
    }

    private void ExecuteRoomIDPacket(Packet packet)
    {
        RoomIDPacket roomIDPacket = packet as RoomIDPacket;
        RoomManager.RoomID = roomIDPacket.RoomID;
    }

    private void ExecuteUpdateMovePacket(Packet packet)
    {
        UpdateMovePacket updateMovePacket = packet as UpdateMovePacket;

        EnemyCarController.MoveEnemyCar(updateMovePacket.SwipeLength);
    }

    private void ExecuteMatchResultPacket(Packet packet)
    {
        MatchResultPacket matchResultPacket = packet as MatchResultPacket;

        InGameUIManager.SetMatchResultUI(matchResultPacket.Winner, matchResultPacket.Distance);
        InGameUIManager.ShowMatchResultUI();
    }
    private void ExecuteRankPacket(Packet packet)
    {
        RankPacket rankPacket = packet as RankPacket;

        LobbyUIManager.rankList = rankPacket.RankList;
        LobbyUIManager.UpdateRankUI();
    }
    private void ExecutePublicKeyPacket(Packet packet)
    {
        PublicKeyPacket publicKeyPacket = packet as PublicKeyPacket;
        publicKey = publicKeyPacket.PublicKey;
        Debug.Log("publicKey = " + publicKey);
    }
}
