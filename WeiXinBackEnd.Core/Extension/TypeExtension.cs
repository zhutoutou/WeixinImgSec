using System;
using System.Reflection;

namespace WeiXinBackEnd.Core.Extension
{
    public static class TypeExtension
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }
    }
}