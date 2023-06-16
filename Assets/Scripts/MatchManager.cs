using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketPackage;
using System.Threading;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public static int threadFlag = 0;

    public void SendMatchMakingPacket()
    {
        PacketManager.Instance.SendPacket(new MatchMakingPacket(PacketManager.Instance.ID));
    }

    public void SendMatchCancelingPacket()
    {
        PacketManager.Instance.SendPacket(new MatchCancelingPacket(PacketManager.Instance.ID));
    }
}
