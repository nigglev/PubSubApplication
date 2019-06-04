using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public struct SChannelInfo
{
    [JsonProperty("data")]
    private SChannelInfoData[] _data;

    public string GetChannelID(int in_index) { return _data[in_index].GetChannelID();}
}

[JsonObject(MemberSerialization.Fields)]
public struct SChannelInfoData
{
    [JsonProperty("id")]
    private string _id;
    [JsonProperty("login")]
    private string _login;
    [JsonProperty("display_name")]
    private string _display_name;
    [JsonProperty("staff")]
    private string _staff;
    [JsonProperty("broadcaster_type")]
    private string _broadcaster_type;
    [JsonProperty("description")]
    private string _description;
    [JsonProperty("profile_image_url")]
    private string _profile_image_url;
    [JsonProperty("offline_image_url")]
    private string _offline_image_url;
    [JsonProperty("view_count")]
    private string _view_count;
    [JsonProperty("email")]
    private string _email;

    public string GetChannelID()
    {
        return _id;
    }
}
