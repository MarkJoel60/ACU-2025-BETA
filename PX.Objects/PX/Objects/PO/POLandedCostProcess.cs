// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.MigrationMode;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
[Serializable]
public class POLandedCostProcess : PXGraph<POLandedCostProcess>
{
  public PXCancel<POLandedCostDoc> Cancel;
  public PXAction<POLandedCostDoc> ViewDocument;
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<POLandedCostDoc, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POLandedCostDoc.vendorID>>>, Where<POLandedCostDoc.released, Equal<False>, And<POLandedCostDoc.hold, Equal<False>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>> landedCostDocsList;
  public PXSetup<PX.Objects.PO.POSetup> POSetup;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<POLandedCostDoc>) this.landedCostDocsList).Current != null)
    {
      POLandedCostDocEntry instance = PXGraph.CreateInstance<POLandedCostDocEntry>();
      PXResultset<POLandedCostDoc> pxResultset = ((PXSelectBase<POLandedCostDoc>) instance.Document).Search<POLandedCostDoc.refNbr>((object) ((PXSelectBase<POLandedCostDoc>) this.landedCostDocsList).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<POLandedCostDoc>) this.landedCostDocsList).Current.DocType
      });
      if (pxResultset != null)
      {
        ((PXSelectBase<POLandedCostDoc>) instance.Document).Current = PXResultset<POLandedCostDoc>.op_Implicit(pxResultset);
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public POLandedCostProcess()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    PX.Objects.PO.POSetup current = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current;
    ((PXProcessingBase<POLandedCostDoc>) this.landedCostDocsList).SetSelected<POLandedCostDoc.selected>();
    ((PXProcessing<POLandedCostDoc>) this.landedCostDocsList).SetProcessCaption("Process");
    ((PXProcessing<POLandedCostDoc>) this.landedCostDocsList).SetProcessAllCaption("Process All");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<POLandedCostDoc>) this.landedCostDocsList).SetProcessDelegate(POLandedCostProcess.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (POLandedCostProcess.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXProcessingBase<POLandedCostDoc>.ProcessListDelegate((object) POLandedCostProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__5_0))));
  }

  public static void ReleaseDoc(List<POLandedCostDoc> list, bool aIsMassProcess)
  {
    POLandedCostDocEntry instance = PXGraph.CreateInstance<POLandedCostDocEntry>();
    int num = 0;
    bool flag = false;
    foreach (POLandedCostDoc doc in list)
    {
      try
      {
        instance.ReleaseDoc(doc);
        PXProcessing<POLandedCostDoc>.SetInfo(num, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (aIsMassProcess)
        {
          PXProcessing<POLandedCostDoc>.SetError(num, ex);
          flag = true;
        }
        else
          throw;
      }
      ++num;
    }
    if (flag)
      throw new PXException("At least one of the Document selected has failed to process");
  }
}
