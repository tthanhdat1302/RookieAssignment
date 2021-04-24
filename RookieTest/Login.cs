using NUnit.Framework;
using CustomerSite.Models;

namespace RookieTest
{
    [TestFixture]
    public class Login
    {
        private Item _item;

        [SetUp]
        public void SetUp()
        {
            _item=new Item();
        }

    }
}