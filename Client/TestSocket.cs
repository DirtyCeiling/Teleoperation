using UnityEngine;
using System.Collections;
using Net;

public class TestSocket : MonoBehaviour
{
    public ClientSocket mSocket = new ClientSocket();
    // Use this for initialization
    void Start()
    {
       
        mSocket.ConnectServer("127.0.0.3", 8088);
        mSocket.SendMessage("你好服务器！");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
