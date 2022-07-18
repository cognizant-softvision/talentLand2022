using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TalentLand.Trivia.UnitTesting.Mocks
{
    public abstract class FixtureBase : IDisposable
    {
        public DbContextTest DbContext { get; private set; }

        protected FixtureBase()
        {
            Dispose();
            DbContext = new DbContextTest();
            GetData();
        }

        private void GetData()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();

            AddDataEvent();

            DbContext.SaveChanges();
        }

        protected abstract void AddDataEvent();

        public IEnumerable<T> DeserializeJson<T>(string jsonObject) =>
            JsonConvert.DeserializeObject<IEnumerable<T>>(jsonObject);

        protected virtual void Dispose(bool disposing)
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
