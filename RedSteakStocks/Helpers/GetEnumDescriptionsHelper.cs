using System;
using System.Collections.Generic;
using static AlphaVantageApiWrapper.AlphaVantageApiWrapper;

namespace RedSteakStocks.Helpers
{
    public static class GetEnumDescriptionsHelper
    {
        public static IEnumerable<Tuple<string, string>> GetDescriptions(Type type)
        {
            var descs = new List<Tuple<string, string>>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(EnumDescription), true);
                var ix = 0;
                foreach (var fd in fds)
                {
                    descs.Add(new Tuple<string, string>((fd as EnumDescription).Text, name));
                    ix++;
                }
            }
            return descs;
        }

        public static IEnumerable<Tuple<string, string>> GetInfos(Type type)
        {
            var descs = new List<Tuple<string, string>>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(EnumInfo), true);
                var ix = 0;
                foreach (var fd in fds)
                {
                    descs.Add(new Tuple<string, string>((fd as EnumInfo).Text, name));
                    ix++;
                }
            }
            return descs;
        }

        public static IEnumerable<Tuple<bool, string>> GetTriggers(Type type)
        {
            var descs = new List<Tuple<bool, string>>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(EnumTrigger), true);
                var ix = 0;
                foreach (var fd in fds)
                {
                    descs.Add(new Tuple<bool, string>((fd as EnumTrigger).IsIt, name));
                    ix++;
                }
            }
            return descs;
        }
    }
}