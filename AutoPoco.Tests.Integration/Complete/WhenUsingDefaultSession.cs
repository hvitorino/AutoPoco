using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    // This is the Scott Hanselman Class
    public class WhenUsingDefaultSession
    {
        [Test]
        public void Next_Returns_Valid_Object()
        {
            var session = AutoPocoContainer.CreateDefaultSession();
            var user = session.Next<SimpleUser>();

            Assert.NotNull(user, "User was not created");
            Assert.NotNull(user.FirstName, "User did not get first name");
        }

        [Test]
        public void Next_WithAction_Allows_Configuration_Of_Object()
        {
            var session = AutoPocoContainer.CreateDefaultSession();
            var user = session.Next<SimpleUser>(all =>
            {
                all.Impose(x => x.FirstName, "Scott");
                all.Impose(x => x.LastName, "Hanselman");
            });

            Assert.AreEqual("Scott", user.FirstName);
            Assert.AreEqual("Hanselman", user.LastName);
        }

        [Test]
        public void Next_WithAction_And_Number_Allows_Configuration_Of_Objects()
        {
            var session = AutoPocoContainer.CreateDefaultSession();
            var users = session.Collection<SimpleUser>(10, all =>
            {
                all.Random(5)
                    .Impose(x => x.FirstName, "Rob")
                    .Next(5)
                    .Impose(x => x.FirstName, "Scott");

                all.Random(5)
                    .Impose(x => x.LastName, "Red")
                    .Next(5)
                    .Impose(x => x.LastName, "Blue");
            }).ToList();
            
            Assert.AreEqual(5, users.Count(x => x.FirstName == "Rob"));
            Assert.AreEqual(5, users.Count(x => x.FirstName == "Scott"));
            Assert.AreEqual(5, users.Count(x => x.LastName == "Red"));
            Assert.AreEqual(5, users.Count(x => x.LastName == "Blue"));
        }

        [Test]
        public void Next_WithNumber_Returns_Collection_Of_Valid_Objects()
        {
            var session = AutoPocoContainer.CreateDefaultSession();
            var users = session.Collection<SimpleUser>(10).ToList();

            users.ForEach(x =>
            {
                Assert.NotNull(x, "User was not created");
                Assert.NotNull(x.FirstName, "User did not get first name");
            });
        }




    }
}
