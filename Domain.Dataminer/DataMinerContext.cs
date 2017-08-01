using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Domain.Base;
using Domain.Dataminer.Configuration;
using Domain.Dataminer.Entities;

namespace Domain.Dataminer
{
    public class DataMinerContext : DataContextBase, IDataMinerContext
    {
        static DataMinerContext()
        {
            Database.SetInitializer<DataMinerContext>(null);
        }

        internal DataMinerContext()
            : base("Name=CryptoStore")
        {
        }

        internal DataMinerContext(string connectionString)
            : base(connectionString)
        {
        }

        internal DataMinerContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        internal DataMinerContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        internal DataMinerContext(DbConnection existingConnection,
            DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        public DbSet<ApiAsset> ApiAssets { get; set; }
        public DbSet<ApiExchange> ApiExchange { get; set; }
        public DbSet<Api> Api { get; set; }
        public DbSet<ApiMarket> ApiMarkets { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeRangeInfo> TradeRangeInfos { get; set; }

        public static IDataMinerContext Create()
        {
            return new DataMinerContext();
        }

        public static IDataMinerContext Create(string connectionString)
        {
            return new DataMinerContext(connectionString);
        }

        public static IDataMinerContext Create(string connectionString,
            DbCompiledModel model)
        {
            return new DataMinerContext(connectionString, model);
        }

        public static IDataMinerContext Create(DbConnection existingConnection,
            bool contextOwnsConnection)
        {
            return new DataMinerContext(existingConnection, contextOwnsConnection);
        }

        public static IDataMinerContext Create(DbConnection existingConnection,
            DbCompiledModel model, bool contextOwnsConnection)
        {
            return new DataMinerContext(existingConnection, model, contextOwnsConnection);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ApiAssetConfiguration());
            modelBuilder.Configurations.Add(new ApiConfiguration());
            modelBuilder.Configurations.Add(new ApiMarketConfiguration());
            modelBuilder.Configurations.Add(new ApiExchangeConfiguration());
            modelBuilder.Configurations.Add(new ExchangeConfiguration());
            modelBuilder.Configurations.Add(new MarketConfiguration());
            modelBuilder.Configurations.Add(new AssetConfiguration());
            modelBuilder.Configurations.Add(new TradeConfiguration());
            modelBuilder.Configurations.Add(new TradeRangeInfoConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder,
            string schema)
        {
            modelBuilder.Configurations.Add(new ApiAssetConfiguration(schema));
            modelBuilder.Configurations.Add(new ApiConfiguration(schema));
            modelBuilder.Configurations.Add(new ApiMarketConfiguration(schema));
            modelBuilder.Configurations.Add(new ApiExchangeConfiguration(schema));
            modelBuilder.Configurations.Add(new ExchangeConfiguration(schema));
            modelBuilder.Configurations.Add(new MarketConfiguration(schema));
            modelBuilder.Configurations.Add(new AssetConfiguration(schema));
            modelBuilder.Configurations.Add(new TradeConfiguration(schema));
            modelBuilder.Configurations.Add(new TradeRangeInfoConfiguration(schema));
            return modelBuilder;
        }
    }

    public interface IDataMinerContext : IDisposable, IRepositoryCreator
    {
        DbSet<ApiAsset> ApiAssets { get; set; }
        DbSet<Api> Api { get; set; }
        DbSet<ApiExchange> ApiExchange { get; set; }
        DbSet<ApiMarket> ApiMarkets { get; set; }
        DbSet<Exchange> Exchanges { get; set; }
        DbSet<Market> Markets { get; set; }
        DbSet<Asset> Assets { get; set; }
        DbSet<Trade> Trades { get; set; }
        DbSet<TradeRangeInfo> TradeRangeInfos { get; set; }
    }
}