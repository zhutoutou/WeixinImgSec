﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message.Base.Attributes;

namespace WeiXinBackEnd.SDK.Client.Message.Base
{
    public class WeChatRequest : ProtocolRequest
    {
        /// <summary>
        /// 获取业务参数
        /// </summary>
        /// <returns></returns>
        public virtual void ConstructParam()
        {
            var propertyInfos = GetType().GetProperties().Where(v => v.GetCustomAttributes().OfType<WeChatParameterNameAttribute>().Any()).ToList();
            propertyInfos.ForEach(v =>
            {
                var key = v.GetCustomAttribute<WeChatParameterNameAttribute>().WeChatParameterName;
                if (!Parameters.ContainsKey(key))
                    Parameters.Add(key, v.GetValue(this).ToString());
            });
        }

        /// <summary>
        /// 检查参数的合法性
        /// </summary>
        protected virtual void CheckValidation()
        {
            GetType().GetProperties().ToList().ForEach(prop =>
            {
                prop.GetCustomAttributes<ValidationAttribute>(true).ToList().ForEach(valid => valid.Validate(prop.GetValue(this), prop.Name));
            });
        }


        /// <summary>
        /// 准备
        /// </summary>
        public virtual void Prepare()
        {
            // 验证参数
            CheckValidation();
            // 构建参数
            ConstructParam();

            if (Method == HttpMethod.Get)
            {
                var queryString = Parameters.ToQueryString();
                RequestUri = new Uri($"{Address}?{queryString}");
            }
            else if (Method == HttpMethod.Post)
            {
                if (Address.IsPresent())
                {
                    RequestUri = new Uri(Address);
                }

                if (Parameters.Any())
                {
                    Content = new FormUrlEncodedContent(Parameters);
                }
            }
        }

 
    }
}