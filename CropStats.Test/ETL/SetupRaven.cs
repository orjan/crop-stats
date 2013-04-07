using System;
using System.Globalization;
using System.Linq;
using CropStats.Indexes;
using CropStats.Models;
using Ploeh.AutoFixture;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Listeners;
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
            DocumentStore.RegisterListener(new NoStaleQueriesListener());
            DocumentStore.Initialize();
        }

        protected DocumentStore DocumentStore { get; set; }

        public IDocumentSession Session
        {
            get { return DocumentStore.OpenSession(); }
        }

        public void Dispose()
        {
            if (Session != null)
            {
                Session.Dispose();
            }
        }

        [Theory(Skip = "Setup"), Auto]
        public void CreateSomeUsers(Fixture fixture, PasswordBuilder passwordBuilder)
        {
            foreach (int customerNumber in fixture.CreateMany<int>(1000))
            {
                using (IDocumentSession documentSession = DocumentStore.OpenSession())
                {
                    var entity = new Farmer {CustomerNumber = customerNumber};
                    entity.Password =
                        passwordBuilder.CreatePassword(customerNumber.ToString(CultureInfo.InvariantCulture));
                    documentSession.Store(entity);
                    documentSession.SaveChanges();
                }
            }
        }

        [Theory(Skip = "Setup"), Auto]
        public void CreateSomeUsers2(Fixture fixture, Crop crop)
        {
            using (IDocumentSession documentSession = DocumentStore.OpenSession())
            {
                crop.Year = 2012;
                crop.FarmerId = 133;

                documentSession.Store(crop);
                documentSession.SaveChanges();
            }
        }

        [Fact]
        public void Stats()
        {
            IndexCreation.CreateIndexes(typeof (TotalTilledAreaIndex).Assembly, DocumentStore);

            var ravenQueryable = Session.Query<TotalTilledAreaIndex.Result, TotalTilledAreaIndex>();
            foreach (var result in ravenQueryable)
            {
                Console.WriteLine(result.Hectare);
            }
        }

        private class NoStaleQueriesListener : IDocumentQueryListener
        {
            public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
            {
                queryCustomization.WaitForNonStaleResults();
            }
        }
    }


    
}