using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders.Stubs
{
    public class MySession : HttpSessionStateBase
    {
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

        public override object this[string name]
        {
            get => _items[name];
            set => base[name] = value;
        }

        public override void Add(string name, object value)
        {
            _items.Add(name, value);
        }

        public override IEnumerator GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }
    }

}