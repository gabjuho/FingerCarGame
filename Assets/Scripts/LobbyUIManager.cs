using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketPackage;
using System;
using UnityEngine.UI;
using TMPro;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject matchPanel;
    public GameObject rankPanel;
    public static GameObject rankContent;
    public static GameObject rankTextPrefab;
    public static int threadFlag = 0;
    public static List<Tuple<string, float>> rankList;
    private void Awake()
    {
        rankTextPrefab = Resources.Load<GameObject>("Rank");
        rankContent = GameObject.Find("Content");
        rankPanel.SetActive(false);
    }
    public void OnClickPlayButton()
    {
        ShowMatchUI();
    }

    public void OnClickExitButtonInMatchUI()
    {
        HideMatchUI();
    }

    public void OnClickRankButton()
    {
        ShowRankUI();
    }
    public void OnClickExitButtonInRankUI()
    {
        HideRankUI();
    }

    private void ShowMatchUI()
    {
        matchPanel.SetActive(true);
    }
    private void HideMatchUI()
    {
        matchPanel.SetActive(false);
    }

    private void ShowRankUI()
    {
        PacketManager.Instance.SendPacket(new RankRequestPacket());
        rankPanel.SetActive(true);
    }
    private void HideRankUI()
    {
        Transform[] childList = rankContent.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        rankPanel.SetActive(false);
    }

    public static void UpdateRankUI()
    {
        int num = 1;

        if (rankList == null)
            return;

        foreach(var item in rankList)
        {
            rankTextPrefab.GetComponent<TextMeshProUGUI>().text = $"{num++}. {item.Item1}: {item.Item2}m";
            Instantiate(rankTextPrefab, rankContent.transform);
        }
    }
}
