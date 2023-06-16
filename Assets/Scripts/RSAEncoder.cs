using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using System.Security.Cryptography;
using System.Text;

public class RSAEncoder
{
    // RSA ��ȣȭ 
    public static string RSAEncrypt(string getValue, string pubKey)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(pubKey);

        //��ȣȭ�� ���ڿ��� UFT8���ڵ�
        byte[] inbuf = (new UTF8Encoding()).GetBytes(getValue);

        //��ȣȭ
        byte[] encbuf = rsa.Encrypt(inbuf, false);

        //��ȣȭ�� ���ڿ� Base64���ڵ�
        return System.Convert.ToBase64String(encbuf);
    }
}
