using System;

namespace WeiXinBackEnd.SDK.Client.Message
{
    public class WeChatParameterNameAttribute : Attribute
    {
        public string WeChatParameterName { get; set; }

        public WeChatParameterNameAttribute(string name)
        {
            WeChatParameterName = name;
        }
    }
}