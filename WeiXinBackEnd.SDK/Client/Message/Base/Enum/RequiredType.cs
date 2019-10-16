using System;
using System.ComponentModel;

namespace WeiXinBackEnd.SDK.Client.Message.Base.Enum
{
    [Flags]
    public enum RequiredType
    {
        /// <summary>
        ///  不必要
        /// </summary>
        [Description("不必要")]
        None = 1,

        /// <summary>
        ///  基础支持
        /// </summary>
        [Description("App基础支持")]
        App = 2,

        /// <summary>
        /// 商户支持
        /// </summary>
        [Description("商户支持")]
        Mch = 4

    }
}