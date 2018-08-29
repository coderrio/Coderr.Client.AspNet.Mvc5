using System.Collections.Specialized;
using System.Web;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders.Stubs
{
    public class TestApplicationState : HttpApplicationStateBase
    {
        private readonly NameValueCollection _items = new NameValueCollection();

        public override int Count => _items.Count;

        public override object this[int index] => _items[index];

        public override object this[string name]
        {
            get { return _items[name]; }
            set { _items[name] = value.ToString(); }
        }

        public override string GetKey(int index)
        {
            return _items.AllKeys[index];
        }
    }
}