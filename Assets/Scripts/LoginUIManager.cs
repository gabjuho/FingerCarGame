using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject loginUI;
    public GameObject signupUI;
    public static GameObject loginErrorPopUpUI;
    public static GameObject signupErrorPopUpUI;
    public static GameObject signupSuccessPopUpUI;

    public static int signupErrorFlag = 0;
    public static int signupSuccessFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        loginErrorPopUpUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        signupErrorPopUpUI = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        signupSuccessPopUpUI = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
    }

    public void OnClickLoginButton()
    {
        ShowLoginUI();
    }

    public void OnClickExitButtonInLoginUI()
    {
        HideLoginUI();
    }

    public void OnClickSignupButton()
    {
        ShowSignupUI();
    }

    public void OnClickExitButtonInSignupUI()
    {
        HideSignupUI();
    }

    public void OnClickExitButtonInLoginErrorPopUpUI()
    {
        HideLoginErrorPopUpUI();
    }
    public void OnClickExitButtonInSignupErrorPopUpUI()
    {
        HideSignupErrorPopUpUI();
    }
    public void OnClickExitButtonInSignupSuccessPopUpUI()
    {
        HideSignupSuccessPopUpUI();
    }

    private void ShowLoginUI()
    {
        loginUI.SetActive(true);
    }
    private void HideLoginUI()
    {
        loginUI.SetActive(false);
    }

    private void ShowSignupUI()
    {
        signupUI.SetActive(true);
    }
    private void HideSignupUI()
    {
        signupUI.SetActive(false);
    }

    public static void ShowLoginErrorPopUpUI()
    {
        loginErrorPopUpUI.SetActive(true);
    }
    private void HideLoginErrorPopUpUI()
    {
        loginErrorPopUpUI.SetActive(false);
    }
    public static void ShowSignupErrorPopUpUI()
    {
        signupErrorPopUpUI.SetActive(true);
    }
    private void HideSignupErrorPopUpUI()
    {
        signupErrorPopUpUI.SetActive(false);
    }
    public static void ShowSignupSuccessPopUpUI()
    {
        signupSuccessPopUpUI.SetActive(true);
    }
    private void HideSignupSuccessPopUpUI()
    {
        signupSuccessPopUpUI.SetActive(false);
    }
}
