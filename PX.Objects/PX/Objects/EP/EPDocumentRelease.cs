// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPDocumentRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.MigrationMode;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.EP;

[TableDashboardType]
public class EPDocumentRelease : PXGraph<EPDocumentRelease>
{
  public PXCancel<EPExpenseClaim> Cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<EPExpenseClaim, Where<EPExpenseClaim.released, Equal<False>, And<EPExpenseClaim.approved, Equal<True>>>> EPDocumentList;
  public APSetupNoMigrationMode APSetup;

  public EPDocumentRelease()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<EPExpenseClaim>) this.EPDocumentList).SetProcessDelegate(new PXProcessingBase<EPExpenseClaim>.ProcessItemDelegate((object) null, __methodptr(ReleaseDoc)));
    ((PXProcessing<EPExpenseClaim>) this.EPDocumentList).SetProcessCaption("Release");
    ((PXProcessing<EPExpenseClaim>) this.EPDocumentList).SetProcessAllCaption("Release All");
    ((PXProcessingBase<EPExpenseClaim>) this.EPDocumentList).SetSelected<EPExpenseClaim.selected>();
  }

  public static void ReleaseDoc(EPExpenseClaim claim)
  {
    PXGraph.CreateInstance<EPReleaseProcess>().ReleaseDocProc(claim);
  }
}
