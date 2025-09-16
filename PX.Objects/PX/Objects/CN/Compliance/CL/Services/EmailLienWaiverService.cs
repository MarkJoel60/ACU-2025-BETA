// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.EmailLienWaiverService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Common.Services.DataProviders;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Models;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal class EmailLienWaiverService : 
  PrintEmailLienWaiverBaseService,
  IEmailLienWaiverService,
  IPrintEmailLienWaiverBaseService
{
  public EmailLienWaiverService(PXGraph graph)
    : base(graph)
  {
    this.RecipientEmailDataProvider = graph.GetService<IRecipientEmailDataProvider>();
  }

  private IRecipientEmailDataProvider RecipientEmailDataProvider { get; }

  protected override async Task ProcessLienWaiver(
    NotificationSourceModel notificationSourceModel,
    ComplianceDocument complianceDocument,
    CancellationToken cancellationToken)
  {
    await base.ProcessLienWaiver(notificationSourceModel, complianceDocument, cancellationToken);
    EnumerableExtensions.ForEach<NotificationRecipient>(this.GetNotificationRecipients(notificationSourceModel.NotificationSource, notificationSourceModel.VendorId), (Action<NotificationRecipient>) (nr => this.SendEmail(nr, notificationSourceModel, complianceDocument)));
    PXProcessing.SetProcessed();
  }

  private void SendEmail(
    NotificationRecipient notificationRecipient,
    NotificationSourceModel notificationSourceModel,
    ComplianceDocument complianceDocument)
  {
    string recipientEmail = this.RecipientEmailDataProvider.GetRecipientEmail(notificationRecipient, notificationSourceModel.VendorId);
    if (recipientEmail == null)
      return;
    LienWaiverReportGenerationModel appropriateFormat = this.GetReportInAppropriateFormat(notificationRecipient.Format, notificationSourceModel, complianceDocument);
    TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create((object) complianceDocument, notificationSourceModel.NotificationSource.NotificationID);
    notificationGenerator.MailAccountId = notificationSourceModel.NotificationSource.EMailAccountID;
    notificationGenerator.RefNoteID = complianceDocument.NoteID;
    notificationGenerator.To = recipientEmail;
    notificationGenerator.AddAttachmentLink(appropriateFormat.ReportFileInfo.UID.GetValueOrDefault());
    notificationGenerator.Send();
    this.UpdateLienWaiverProcessedStatus(complianceDocument);
  }

  private LienWaiverReportGenerationModel GetReportInAppropriateFormat(
    string format,
    NotificationSourceModel notificationSourceModel,
    ComplianceDocument complianceDocument)
  {
    return !(format == "E") ? this.LienWaiverReportGenerationModel : this.LienWaiverReportCreator.CreateReport(notificationSourceModel.NotificationSource.ReportID, complianceDocument, notificationSourceModel.IsJointCheck, "Excel", false);
  }

  private IEnumerable<NotificationRecipient> GetNotificationRecipients(
    NotificationSource notificationSource,
    int? vendorId)
  {
    return (IEnumerable<NotificationRecipient>) this.GetNotificationRecipientsQuery(notificationSource, vendorId).FirstTableItems.GroupBy<NotificationRecipient, int?>((Func<NotificationRecipient, int?>) (nr => nr.ContactID)).Select<IGrouping<int?, NotificationRecipient>, NotificationRecipient>((Func<IGrouping<int?, NotificationRecipient>, NotificationRecipient>) (nr => EmailLienWaiverService.GetAppropriateNotificationRecipient((IReadOnlyCollection<NotificationRecipient>) nr.ToList<NotificationRecipient>()))).Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (nr => nr.Active.GetValueOrDefault())).ToList<NotificationRecipient>();
  }

  private static NotificationRecipient GetAppropriateNotificationRecipient(
    IReadOnlyCollection<NotificationRecipient> notificationRecipients)
  {
    return !notificationRecipients.HasAtLeastTwoItems<NotificationRecipient>() ? notificationRecipients.Single<NotificationRecipient>() : EnumerableExtensions.FirstOrAny<NotificationRecipient>((IEnumerable<NotificationRecipient>) notificationRecipients, (Func<NotificationRecipient, bool>) (nr => nr.ClassID == null), (NotificationRecipient) null);
  }

  private PXResultset<NotificationRecipient> GetNotificationRecipientsQuery(
    NotificationSource notificationSource,
    int? vendorId)
  {
    string classId = VendorDataProvider.GetVendor((PXGraph) this.PrintEmailLienWaiversProcess, vendorId).ClassID;
    return PXSelectBase<NotificationRecipient, PXViewOf<NotificationRecipient>.BasedOn<SelectFromBase<NotificationRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationRecipient.setupID, Equal<P.AsGuid>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationRecipient.sourceID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationRecipient.classID, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) this.PrintEmailLienWaiversProcess, new object[3]
    {
      (object) notificationSource.SetupID,
      (object) notificationSource.SourceID,
      (object) classId
    });
  }
}
