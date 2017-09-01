using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Data.Migration.Schema;
using System;

namespace Moov2.Orchard.DateFromTo
{
    public class Migrations : DataMigrationImpl
    {
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
    }
}