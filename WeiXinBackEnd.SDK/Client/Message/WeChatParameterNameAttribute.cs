using System;

namespace WeiXinBackEnd.SDK.Client.Message
{
    /// <summary>
    /// 标记需要作为传输参数的属性或字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class WeChatParameterNameAttribute : Attribute
    {
        public string WeChatParameterName { get; set; }

        public WeChatParameterNameAttribute(string name)
        {
            WeChatParameterName = name;
        }
    }
}