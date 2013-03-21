using System;
using System.Collections.Generic;
using System.Globalization;
using CropStats.Models;
using Ploeh.AutoFixture;
using Raven.Client.Document;
using Xunit;
using Xunit.Extensions;

namespace CropStats.ETL
{
    public class SetupRaven : IDisposable
    {
        public SetupRaven()
        {
            DocumentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            };
            DocumentStore.Initialize();
        }

        protected DocumentStore DocumentStore { get; set; }
        
        public void Dispose()
        {

        }

        [Theory, Auto]
        public void CreateSomeUsers(Fixture fixture, PasswordBuilder passwordBuilder)
        {

            foreach (var customerNumber in fixture.CreateMany<int>(1000))
            {
                using (var documentSession = DocumentStore.OpenSession())
                {
                    var entity = new Farmer {CustomerNumber = customerNumber};
                    entity.Password = passwordBuilder.CreatePassword(customerNumber.ToString(CultureInfo.InvariantCulture));
                    documentSession.Store(entity);
                    documentSession.SaveChanges();
                }
            }

        } 
    }
}