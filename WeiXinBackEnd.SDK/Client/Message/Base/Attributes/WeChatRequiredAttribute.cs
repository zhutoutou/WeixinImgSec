using System;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;

namespace WeiXinBackEnd.SDK.Client.Message.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class WeChatRequiredAttribute : Attribute
    {
        public WeChatRequiredAttribute(RequiredType weChatRequiredType = RequiredType.App)
        {
            WeChatRequiredType = weChatRequiredType;
        }

        public RequiredType WeChatRequiredType { get; set; }

        public void Valid(object value)
        {
            if(!IsValid(value))
                throw new ArgumentNullException(nameof(value));
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            // only check string length if empty strings are not allowed
            return !(value is string stringValue) || stringValue.Trim().Length != 0;
        }
    }
}