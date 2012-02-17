using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenCreatingACustomisedList
    {
        IGenerationSession mSession;
        IList<SimpleUser> mUsers;

        [SetUp]
        public void Setup()
        {
            // As default as it gets
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.AddFromAssemblyContainingType<SimpleUser>();
            })
            .CreateSession();

            SimpleUserRole roleOne = mSession.Single<SimpleUserRole>()
                              .Impose(x => x.Name, "RoleOne").Get();
            SimpleUserRole roleTwo = mSession.Single<SimpleUserRole>()
                              .Impose(x => x.Name, "RoleTwo").Get();
            SimpleUserRole roleThree = mSession.Single<SimpleUserRole>()
                              .Impose(x => x.Name, "RoleThree").Get();

            mUsers = mSession.List<SimpleUser>(100)
                 .First(50)
                      .Impose(x => x.FirstName, "Rob")
                      .Impose(x => x.LastName, "Ashton")
                  .Next(50)
                      .Impose(x => x.FirstName, "Luke")
                      .Impose(x => x.LastName, "Smith")
                  .All()
                  .Random(25)
                      .Impose(x => x.Role,roleOne)
                   .Next(25)
                      .Impose(x => x.Role,roleTwo)
                  .Next(50)
                      .Impose(x => x.Role, roleThree)
                 .All().Get();

        }

        [Test]
        public void CorrectNumberOfRobsExist()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.FirstName == "Rob"));
        }

        [Test]
        public void CorrectNumberOfAshtonsExist()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.LastName == "Ashton"));
        }

        [Test]
        public void CorrectNumberOfLukesExist()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.FirstName == "Luke"));
        }
        
        [Test]
        public void CorrectNumberOfSmithsExist()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.LastName == "Smith"));
        }

        [Test]
        public void AllRobsAreAshtons()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.FirstName == "Rob" &&  x.LastName == "Ashton"));
        }

        [Test]
        public void AllLukesAreSmiths()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.FirstName == "Luke" && x.LastName == "Smith"));
        }

        [Test]
        public void CorrectNumberOfRoleOnesExist()
        {
            Assert.AreEqual(25, mUsers.Count(x => x.Role.Name == "RoleOne"));
        }

        [Test]
        public void CorrectNumberOfRoleTwosExist()
        {
            Assert.AreEqual(25, mUsers.Count(x => x.Role.Name == "RoleTwo"));
        }

        [Test]
        public void CorrectNumberOfRoleThreesExist()
        {
            Assert.AreEqual(50, mUsers.Count(x => x.Role.Name == "RoleThree"));
        }

    }
}
