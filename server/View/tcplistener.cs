using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
 
namespace TcpSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) 로컬 포트 7000 을 Listen
            TcpListener listener = new TcpListener(IPAddress.Any, 7000); //make tcplistener object, any ip address but the port should be 7000
            listener.Start(); // server open!

            byte[] buff = new byte[1024]; //make buff

            while (true) //while the top value is true, yang dd
            {
                // (2) TcpClient Connection 요청을 받아들여
                //     서버에서 새 TcpClient 객체를 생성하여 리턴
                TcpClient tc = listener.AcceptTcpClient(); //when a aclient entered the server, the server accept the 

                // (3) TcpClient 객체에서 NetworkStream을 얻어옴 
                NetworkStream stream = tc.GetStream(); //make stream object

                // (4) 클라이언트가 연결을 끊을 때까지 데이타 수신
                int nbytes;
                while ((nbytes = stream.Read(buff, 0, buff.Length)) > 0) //after you read the stream, if the data is emtered
                {
                    // (5) 데이타 그대로 송신
                    stream.Write(buff, 0, nbytes); //write the stream 
                }

                // (6) 스트림과 TcpClient 객체 
                stream.Close();
                tc.Close();

                // (7) 계속 반복
            }
        }
    }
}