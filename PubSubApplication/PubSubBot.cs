using System;
using System.Net.Sockets;
using WebSocket4Net;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

class CPingMessage
{
    public string type;

    public CPingMessage()
    {
        type = "PING";
    }
}


class CPubSubBot
{
    WebSocket _ws;
    CPingMessage _ping;

    private int _pingTimerms = 6000;

    string _pingMessage;

    private SChannelInfo _channelInfo;

    public CPubSubBot()
    {
        _ws = new WebSocket("wss://pubsub-edge.twitch.tv");
        _ping = new CPingMessage();
        _pingMessage = JsonConvert.SerializeObject(_ping);

        RequestChannelInfo();
    }

    public void Connect()
    {
        _ws.Opened += OnOpened;
        _ws.Closed += OnClosed;
        _ws.MessageReceived += OnMessageReceived;
        _ws.Open();
    }

    public void Disconnect()
    {
        _ws.Close();
    }

    
    private void SendListenTopic()
    {
        //TimerCallback tm = new TimerCallback(Heartbeat);
        //Timer timer = new Timer(tm, null, 1000, _pingTimerms);
        
        SListenTopic topic = new SListenTopic(_channelInfo.GetChannelID(0), "122131");
        string message = JsonConvert.SerializeObject(topic, Formatting.Indented);
        _ws.Send(message);
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Console.WriteLine(e.Message);
    }
    private void OnOpened(object sender, EventArgs e)
    {   
        Heartbeat(null);
        Console.WriteLine("Connected");

        SendListenTopic();
    }

    private void OnClosed(object sender, EventArgs e)
    {   
        Console.WriteLine("Disconnected");
    }

    private void Heartbeat(object obj)
    {
        Console.WriteLine("Sending PING");
        _ws.Send(_pingMessage);
    }


    private async void RequestChannelInfo()
    {
        //NEW TWITCH API
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.twitch.tv/helix/users?login=russian_spirit"))
            {
                request.Headers.TryAddWithoutValidation("Client-ID", "ui8pt26wrq1gi1btna3djnnstcnnff");

                var response = await httpClient.SendAsync(request);

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    string info = await reader.ReadToEndAsync();
                    _channelInfo = JsonConvert.DeserializeObject<SChannelInfo>(info);
                }
            }

        }
    }
}






//{"data":[{"id":"205848344","login":"russian_spirit",
//    "display_name":"russian_spirit","type":"",
//    "broadcaster_type":"","description":"",
//    "profile_image_url":"https://static-cdn.jtvnw.net/user-default-pictures/49988c7b-57bc-4dee-bd4f-6df4ad215d3a-profile_image-300x300.jpg","offline_image_url":"","view_count":7}]}
