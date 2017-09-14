using Moov2.Orchard.DateFromTo.Models;
using Moov2.Orchard.DateFromTo.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.ViewModels;
using Orchard.Localization;
using Orchard.Localization.Services;
using System;

namespace Moov2.Orchard.DateFromTo.Drivers
{
    public class DateFromToPartDriver : ContentPartDriver<DateFromToPart>
    {
        #region Constants
        private const string TemplateName = "Parts.DateFromTo.DateFromToPart";
        #endregion

        #region Dependencies
        private readonly IDateLocalizationServices _dateLocalizationServices;

        public Localizer T { get; set; }
        #endregion

        #region PrivateProperties
        #endregion

        #region Constructor
        public DateFromToPartDriver(IDateLocalizationServices dateLocalizationServices)
        {
            _dateLocalizationServices = dateLocalizationServices;
        }
        #endregion

        #region Overrides
        protected override string Prefix => "DateFromTo";
        #region Display
        protected override DriverResult Display(DateFromToPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DateFromTo", () => shapeHelper.Parts_DateFromTo(FromDateTime: part.FromDateTimeUtc?.ToLocalTime(), ToDateTime: part.ToDateTimeUtc?.ToLocalTime()));
        }
        #endregion
        #region Editor
        protected override DriverResult Editor(DateFromToPart part, dynamic shapeHelper)
        {
            var model = BuildViewModelFromPart(part);

            return ContentShape("Parts_DateFromTo_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: model, Prefix: Prefix));
        }

        protected override DriverResult Editor(DateFromToPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var model = BuildViewModelFromPart(part);

            if (updater.TryUpdateModel(model, Prefix, null, null))
            {
                part.FromDateTimeUtc = !string.IsNullOrWhiteSpace(model.FromDateTimeEditor.Date) &&
                    !string.IsNullOrWhiteSpace(model.FromDateTimeEditor.Time) ?
                    _dateLocalizationServices.ConvertFromLocalizedString(model.FromDateTimeEditor.Date, model.FromDateTimeEditor.Time) :
                    null;
                part.ToDateTimeUtc = !string.IsNullOrWhiteSpace(model.ToDateTimeEditor.Date) &&
                    !string.IsNullOrWhiteSpace(model.ToDateTimeEditor.Time) ?
                    _dateLocalizationServices.ConvertFromLocalizedString(model.ToDateTimeEditor.Date, model.ToDateTimeEditor.Time) :
                    null;
            }

            return Editor(part, shapeHelper);
        }
        #endregion
        #region Importing / Exporting
        protected override void Importing(DateFromToPart part, ImportContentContext context)
        {
            if (context.Data.Element(part.PartDefinition.Name) == null)
                return;
            context.ImportAttribute(part.PartDefinition.Name, "FromDateTimeUtc", x => part.FromDateTimeUtc = ParseNullableDate(x));
            context.ImportAttribute(part.PartDefinition.Name, "ToDateTimeUtc", x => part.ToDateTimeUtc = ParseNullableDate(x));
        }
        #endregion
        protected override void Exporting(DateFromToPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("FromDateTimeUtc", part.FromDateTimeUtc);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ToDateTimeUtc", part.ToDateTimeUtc);
        }
        #endregion

        #region Helpers
        private EditDateFromToViewModel BuildViewModelFromPart(DateFromToPart part)
        {
            return new EditDateFromToViewModel()
            {
                FromDateTimeEditor = new DateTimeEditor()
                {
                    ShowDate = true,
                    ShowTime = true,
                    Date = part.FromDateTimeUtc.HasValue ? _dateLocalizationServices.ConvertToLocalizedDateString(part.FromDateTimeUtc.Value) : "",
                    Time = part.FromDateTimeUtc.HasValue ? _dateLocalizationServices.ConvertToLocalizedTimeString(part.FromDateTimeUtc.Value) : "",
                },
                ToDateTimeEditor = new DateTimeEditor()
                {
                    ShowDate = true,
                    ShowTime = true,
                    Date = part.ToDateTimeUtc.HasValue ? _dateLocalizationServices.ConvertToLocalizedDateString(part.ToDateTimeUtc.Value) : "",
                    Time = part.ToDateTimeUtc.HasValue ? _dateLocalizationServices.ConvertToLocalizedTimeString(part.ToDateTimeUtc.Value) : "",
                }
            };
        }

        private DateTime? ParseNullableDate(string date)
        {
            DateTime parsed;
            return DateTime.TryParse(date, out parsed) ? (DateTime?)parsed : null;
        }
        #endregion
    }
}