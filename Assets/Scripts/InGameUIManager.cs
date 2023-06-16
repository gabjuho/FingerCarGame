using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static GameObject matchResultUI;
    public static int threadFlag = 0;

    private void Start()
    {
        matchResultUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        Debug.Log(matchResultUI.name);
    }

    public static void SetMatchResultUI(string winner, float distance)
    {
        matchResultUI.transform.GetChild(0).
            GetComponent<TextMeshProUGUI>().text = $"Winner is {winner}\nRecord: {distance.ToString("F2")}";
    }

    public static void ShowMatchResultUI()
    {
        matchResultUI.SetActive(true);
    }
    public void OnClickExitButtonInGame()
    {
        SceneManager.LoadScene("Lobby");
    }
}
