// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMActivityDetailsExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMActivityDetailsExt<TGraph, TPrimaryEntity, TPrimaryEntity_NoteID> : 
  ActivityDetailsExt<TGraph, TPrimaryEntity, TPrimaryEntity_NoteID>
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, INotable, new()
  where TPrimaryEntity_NoteID : IBqlField, IImplement<IBqlCastableTo<IBqlGuid>>
{
  public override object GetBAccountRow(string sourceType, CRPMTimeActivity activity)
  {
    object current = this.Base.Caches[typeof (TPrimaryEntity)].Current;
    if (current != null && sourceType == "Project")
    {
      PMProject baccountRow = PMProject.PK.Find((PXGraph) this.Base, (int?) this.Base.Caches[typeof (TPrimaryEntity)].GetValue(current, "ProjectID"));
      if (baccountRow != null && !baccountRow.NonProject.GetValueOrDefault() && baccountRow.BaseType == "P")
        return (object) baccountRow;
    }
    return base.GetBAccountRow(sourceType, activity);
  }

  public virtual bool IsProjectSourceActive(int? projectID, string notificationCD)
  {
    return ((PXSelectBase<NotificationSource>) new PXSelectJoin<NotificationSource, InnerJoin<NotificationSetup, On<NotificationSource.setupID, Equal<NotificationSetup.setupID>>, InnerJoin<PMProject, On<PMProject.noteID, Equal<NotificationSource.refNoteID>>>>, Where<NotificationSetup.notificationCD, Equal<Required<NotificationSetup.notificationCD>>, And<PMProject.contractID, Equal<Required<PMProject.contractID>>, And<NotificationSource.active, Equal<True>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) notificationCD,
      (object) projectID
    }) != null;
  }

  public virtual string ProjectInvoiceReportActive(int? projectID)
  {
    return ((PXSelectBase<NotificationSource>) new PXSelectJoin<NotificationSource, InnerJoin<NotificationSetup, On<NotificationSource.setupID, Equal<NotificationSetup.setupID>>, InnerJoin<PMProject, On<PMProject.noteID, Equal<NotificationSource.refNoteID>>>>, Where<NotificationSetup.notificationCD, Equal<Required<NotificationSetup.notificationCD>>, And<PMProject.contractID, Equal<Required<PMProject.contractID>>, And<NotificationSource.active, Equal<True>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) "INVOICE",
      (object) projectID
    })?.ReportID;
  }

  public virtual string GetDefaultProjectInvoiceReport() => "PM641000";
}
