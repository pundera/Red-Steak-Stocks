using System;

namespace RedSteakStocks.ViewModels.Base
{
    public class KeyValue : Tuple<string, string>
    {
        public KeyValue(string key, string value) : base(key, value)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}