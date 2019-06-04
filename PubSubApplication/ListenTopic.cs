using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public struct SListenTopic
{   
    private string type;
    private string nonce;
    private SData data;

    public SListenTopic(string in_channelID, string in_nonce)
    {
        type = "LISTEN";
        nonce = in_nonce;
        data = new SData();
        data.Init(in_channelID);
    }
}

[JsonObject(MemberSerialization.Fields)]
public struct SData
{
    private string[] topics;
    private string auth_token;

    public void Init(string in_channelID)
    {
        topics = new[] { "channel-bits-events-v2." + in_channelID };
        auth_token = "ymk18ug4dnpaf2igy8qc20xlnzaura";
    }
}

