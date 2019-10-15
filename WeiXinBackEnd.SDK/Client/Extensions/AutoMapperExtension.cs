using System;
using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class AutoMapperExtension
    {
        public static TDestination Map<TSource1, TSource2, TDestination>(this Mapper mapper, TSource1 src1, TSource2 src2)
        {
            mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            var dest = mapper.Map<TSource1, TDestination>(src1);
            dest = mapper.Map(src2, dest);
            return dest;
        }

        public static TDestination Map<TSource1, TSource2, TSource3, TDestination>(this Mapper mapper, TSource1 src1, TSource2 src2, TSource3 src3)
        {
            var dest = mapper.Map<TSource1, TSource2, TDestination>(src1, src2);
            dest = mapper.Map(src3, dest);
            return dest;
        }

        public static TDestination Map<TSource1, TSource2, TSource3, TSource4, TDestination>(this Mapper mapper, TSource1 src1, TSource2 src2, TSource3 src3, TSource4 src4)
        {
            var dest = mapper.Map<TSource1, TSource2, TSource3, TDestination>(src1, src2, src3);
            dest = mapper.Map(src4, dest);
            return dest;
        }
    }
}