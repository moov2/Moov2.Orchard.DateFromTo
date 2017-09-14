using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;
using Orchard.Data.Migration.Schema;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Projections.Models;
using System;

namespace Moov2.Orchard.DateFromTo
{
    public class Migrations : DataMigrationImpl
    {
        #region Implementation
        public int Create()
        {
            SchemaBuilder.CreateTable("DateFromToPartRecord",
                table => table
                    .ContentPartVersionRecord()
                    .Column<DateTime>("FromDateTimeUtc", c => c.Nullable())
                    .Column<DateTime>("ToDateTimeUtc", c => c.Nullable())
            );

            ContentDefinitionManager.AlterPartDefinition("DateFromToPart", builder => builder
                .WithDescription("Adds a time from and to to content types.")
                .Attachable());

            return 1;
        }
        #endregion
    }

    [OrchardFeature(Features.DateFromToProjections)]
    public class MigrationsProjections: DataMigrationImpl
    {
        #region Dependencies
        private readonly IRepository<MemberBindingRecord> _memberBindingRepository;

        public Localizer T { get; set; }
        #endregion

        #region Constructor
        public MigrationsProjections(IRepository<MemberBindingRecord> memberBindingRepository)
        {
            _memberBindingRepository = memberBindingRepository;
            T = NullLocalizer.Instance;
        }
        #endregion

        #region Implementation
        public int Create()
        {
            _memberBindingRepository.Create(new MemberBindingRecord
            {
                Type = typeof(DateFromToPartRecord).FullName,
                Member = "FromDateTimeUtc",
                DisplayName = T("From Date Time").Text,
                Description = T("From date and time given to content item.").Text
            });

            _memberBindingRepository.Create(new MemberBindingRecord
            {
                Type = typeof(DateFromToPartRecord).FullName,
                Member = "ToDateTimeUtc",
                DisplayName = T("To Date Time").Text,
                Description = T("To date and time given to content item.").Text
            });

            return 1;
        }
        #endregion
    }
}