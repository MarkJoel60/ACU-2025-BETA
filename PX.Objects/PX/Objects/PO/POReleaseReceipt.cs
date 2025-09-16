// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReleaseReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.MigrationMode;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
public class POReleaseReceipt : PXGraph<POReleaseReceipt>
{
  public PXCancel<POReceipt> Cancel;
  public PXAction<POReceipt> ViewDocument;
  [PXFilterable(new System.Type[] {})]
  public PXProcessing<POReceipt> Orders;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Orders).Current != null)
    {
      bool? released = ((PXSelectBase<POReceipt>) this.Orders).Current.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
      {
        POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
        POReceipt poReceipt = PXResultset<POReceipt>.op_Implicit(((PXSelectBase<POReceipt>) instance.Document).Search<POReceipt.receiptNbr>((object) ((PXSelectBase<POReceipt>) this.Orders).Current.ReceiptNbr, new object[1]
        {
          (object) ((PXSelectBase<POReceipt>) this.Orders).Current.ReceiptType
        }));
        if (poReceipt != null)
        {
          ((PXSelectBase<POReceipt>) instance.Document).Current = poReceipt;
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
    }
    return adapter.Get();
  }

  public POReleaseReceipt()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessingBase<POReceipt>) this.Orders).SetSelected<POReceipt.selected>();
    this.Orders.SetProcessCaption("Process");
    this.Orders.SetProcessAllCaption("Process All");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<POReceipt>) this.Orders).SetProcessDelegate(POReleaseReceipt.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (POReleaseReceipt.\u003C\u003Ec.\u003C\u003E9__4_0 = new PXProcessingBase<POReceipt>.ProcessListDelegate((object) POReleaseReceipt.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__4_0))));
  }

  public virtual IEnumerable orders()
  {
    POReleaseReceipt poReleaseReceipt = this;
    foreach (PXResult<POReceipt> pxResult in PXSelectBase<POReceipt, PXSelectJoinGroupBy<POReceipt, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<POReceipt.FK.Vendor>, InnerJoin<POReceiptLine, On<POReceiptLine.FK.Receipt>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.FK.POReceiptLine>>>>, Where2<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, And<POReceipt.hold, Equal<boolFalse>, And<POReceipt.released, Equal<boolFalse>, And<PX.Objects.AP.APTran.refNbr, IsNull>>>>, Aggregate<GroupBy<POReceipt.receiptNbr, GroupBy<POReceipt.receiptType, GroupBy<POReceipt.released, GroupBy<POReceipt.hold, GroupBy<POReceipt.autoCreateInvoice>>>>>>>.Config>.Select((PXGraph) poReleaseReceipt, Array.Empty<object>()))
    {
      POReceipt poReceipt1 = PXResult<POReceipt>.op_Implicit(pxResult);
      POReceipt poReceipt2;
      if ((poReceipt2 = (POReceipt) ((PXSelectBase) poReleaseReceipt.Orders).Cache.Locate((object) poReceipt1)) != null)
        poReceipt1.Selected = poReceipt2.Selected;
      yield return (object) poReceipt1;
    }
    ((PXSelectBase) poReleaseReceipt.Orders).Cache.IsDirty = false;
  }

  public static void ReleaseDoc(List<POReceipt> list, bool aIsMassProcess)
  {
    string printLabelsReportID = (string) null;
    if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
    {
      POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
      POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find((PXGraph) instance, ((PXGraph) instance).Accessinfo.BranchID);
      if (receivePutAwaySetup != null && receivePutAwaySetup.PrintInventoryLabelsAutomatically.GetValueOrDefault() && !string.IsNullOrEmpty(receivePutAwaySetup.InventoryLabelsReportID))
        printLabelsReportID = receivePutAwaySetup.InventoryLabelsReportID;
    }
    POReleaseReceipt.ReleaseDoc(list, printLabelsReportID, aIsMassProcess);
  }

  public static void ReleaseDoc(
    List<POReceipt> list,
    string printLabelsReportID,
    bool aIsMassProcess)
  {
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    DocumentList<PX.Objects.IN.INRegister> documentList1 = new DocumentList<PX.Objects.IN.INRegister>((PXGraph) instance);
    DocumentList<PX.Objects.AP.APInvoice> documentList2 = new DocumentList<PX.Objects.AP.APInvoice>((PXGraph) instance);
    int num = 0;
    bool flag = false;
    foreach (POReceipt aDoc in list)
    {
      try
      {
        switch (aDoc.ReceiptType)
        {
          case "RT":
            instance.ReleaseReceipt(aDoc, documentList1, documentList2, aIsMassProcess);
            if (!string.IsNullOrEmpty(printLabelsReportID) && PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
            {
              string invtRefNbr = ((POReceipt) PrimaryKeyOf<POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<POReceipt.receiptType, POReceipt.receiptNbr>>.Find((PXGraph) instance, (TypeArrayOf<IBqlField>.IFilledWith<POReceipt.receiptType, POReceipt.receiptNbr>) aDoc, (PKFindOptions) 0))?.InvtRefNbr;
              if (invtRefNbr != null)
              {
                Dictionary<string, string> reportParameters = new Dictionary<string, string>()
                {
                  ["RefNbr"] = invtRefNbr
                };
                DeviceHubTools.PrintReportViaDeviceHub<PX.Objects.CR.BAccount>((PXGraph) instance, printLabelsReportID, reportParameters, "None", (PX.Objects.CR.BAccount) null, new CancellationToken()).GetAwaiter().GetResult();
                break;
              }
              break;
            }
            break;
          case "RX":
            instance.ReleaseReceipt(aDoc, documentList1, documentList2, aIsMassProcess);
            break;
          case "RN":
            instance.ReleaseReturn(aDoc, documentList1, documentList2, aIsMassProcess);
            break;
        }
        if (aIsMassProcess)
          PXProcessing<POReceipt>.SetInfo(num, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        if (aIsMassProcess)
        {
          if (ex is PXIntercompanyReceivedNotIssuedException)
          {
            PXProcessing<POReceipt>.SetWarning(num, ex);
          }
          else
          {
            PXProcessing<POReceipt>.SetError(num, ex);
            flag = true;
          }
        }
        else
          throw;
      }
      ++num;
    }
    if (flag)
      throw new PXException("Release of one or more PO Receipts  has failed");
  }
}
