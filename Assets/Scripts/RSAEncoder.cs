using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using System.Security.Cryptography;
using System.Text;

public class RSAEncoder
{
    // RSA 암호화 
    public static string RSAEncrypt(string getValue, string pubKey)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(pubKey);

        //암호화할 문자열을 UFT8인코딩
        byte[] inbuf = (new UTF8Encoding()).GetBytes(getValue);

        //암호화
        byte[] encbuf = rsa.Encrypt(inbuf, false);

        //암호화된 문자열 Base64인코딩
        return System.Convert.ToBase64String(encbuf);
    }
}
