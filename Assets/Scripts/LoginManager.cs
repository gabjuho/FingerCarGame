using UnityEngine;
using TMPro;
using PacketPackage;
using System.Threading;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField loginIDInputField;
    public TMP_InputField loginPasswordInputField;

    public TMP_InputField signupIDInputField;
    public TMP_InputField signupPasswordInputField;
    private static string id = "", password = "";

    public static bool gotoLobby = false;

    private void Update()
    {
    }
    public static void GoLobby()
    {
        PacketManager.Instance.ID = id;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
    public void SendLoginPacket()
    {
        Debug.Log("id: " + loginIDInputField.text + " password: " + loginPasswordInputField.text);

        id = RSAEncoder.RSAEncrypt(loginIDInputField.text, Dispatcher.publicKey);
        password = RSAEncoder.RSAEncrypt(loginPasswordInputField.text, Dispatcher.publicKey);

        Debug.Log("encrypted id: " + id + " encrypted password: " + password);

        PacketManager.Instance.SendPacket(new LoginPacket(id, password));
    }
    public void SendSignupPacket()
    {
        Debug.Log("id: " + signupIDInputField.text + " password: " + signupPasswordInputField.text);

        id = RSAEncoder.RSAEncrypt(signupIDInputField.text, Dispatcher.publicKey);
        password = RSAEncoder.RSAEncrypt(signupPasswordInputField.text, Dispatcher.publicKey);

        Debug.Log("encrypted id: " + id + " encrypted password: " + password);

        PacketManager.Instance.SendPacket(new SignupPacket(id, password));
    }
}
