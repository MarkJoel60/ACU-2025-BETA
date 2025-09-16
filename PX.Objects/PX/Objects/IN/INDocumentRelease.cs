// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDocumentRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.InventoryRelease;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INDocumentRelease : PXGraph<INDocumentRelease>
{
  public PXCancel<INRegister> Cancel;
  public PXAction<INRegister> viewDocument;
  [PXFilterable(new Type[] {})]
  public PXProcessing<INRegister, Where<INRegister.released, Equal<boolFalse>, And<INRegister.hold, Equal<boolFalse>>>> INDocumentList;
  public PXSetup<INSetup> insetup;

  public INDocumentRelease()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<INRegister>) this.INDocumentList).SetProcessDelegate(INDocumentRelease.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (INDocumentRelease.\u003C\u003Ec.\u003C\u003E9__4_0 = new PXProcessingBase<INRegister>.ProcessListDelegate((object) INDocumentRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__4_0))));
    ((PXProcessing<INRegister>) this.INDocumentList).SetProcessCaption("Release");
    ((PXProcessing<INRegister>) this.INDocumentList).SetProcessAllCaption("Release All");
  }

  public static void ReleaseDoc(
    List<INRegister> list,
    bool isMassProcess,
    bool releaseFromHold = false,
    PXQuickProcess.ActionFlow processFlow = 0)
  {
    bool flag = false;
    INReleaseProcess rg = PXGraph.CreateInstance<INReleaseProcess>();
    JournalEntry journalEntry = rg.CreateJournalEntry();
    Lazy<PostGraph> lazy = new Lazy<PostGraph>((Func<PostGraph>) (() => rg.CreatePostGraph()));
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    List<INRegister> inRegisterList = new List<INRegister>();
    for (int index = 0; index < list.Count; ++index)
    {
      INRegister doc = list[index];
      try
      {
        ((PXGraph) rg).Clear();
        rg.ReleaseDocProcR(journalEntry, doc, releaseFromHold);
        int key;
        if ((key = journalEntry.created.IndexOf(((PXSelectBase<Batch>) journalEntry.BatchModule).Current)) >= 0 && !dictionary.ContainsKey(key))
          dictionary.Add(key, index);
        if (isMassProcess)
          PXProcessing<INRegister>.SetInfo(index, "The record has been processed successfully.");
        inRegisterList.Add(doc);
      }
      catch (Exception ex)
      {
        ((PXGraph) journalEntry).Clear();
        if (isMassProcess)
        {
          PXProcessing<INRegister>.SetError(index, ex);
          flag = true;
        }
        else
        {
          if (list.Count == 1)
            throw new PXOperationCompletedSingleErrorException(ex);
          PXTrace.WriteError(ex);
          flag = true;
        }
      }
    }
    for (int index = 0; index < journalEntry.created.Count; ++index)
    {
      Batch b = journalEntry.created[index];
      try
      {
        if (rg.AutoPost)
        {
          ((PXGraph) lazy.Value).Clear();
          lazy.Value.PostBatchProc(b);
        }
      }
      catch (Exception ex)
      {
        if (isMassProcess)
        {
          flag = true;
          PXProcessing<INRegister>.SetError(dictionary[index], ex);
        }
        else
        {
          if (list.Count == 1)
            throw new PXMassProcessException(dictionary[index], ex);
          PXTrace.WriteError(ex);
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    if (inRegisterList.Count != 1 || processFlow == null)
      return;
    INDocumentRelease.RedirectTo(inRegisterList[0]);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INRegister.branchBaseCuryID> eventArgs)
  {
  }

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  protected virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<INRegister>) this.INDocumentList).Current != null)
      INDocumentRelease.RedirectTo(PXCache<INRegister>.CreateCopy(((PXSelectBase<INRegister>) this.INDocumentList).Current), (PXBaseRedirectException.WindowMode) 3);
    return adapter.Get();
  }

  public static void RedirectTo(INRegister doc, PXBaseRedirectException.WindowMode windowMode = 1)
  {
    switch (doc.DocType)
    {
      case "I":
        INIssueEntry instance1 = PXGraph.CreateInstance<INIssueEntry>();
        ((PXSelectBase<INRegister>) instance1.issue).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance1.issue).Search<INRegister.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, "IN Issue");
        ((PXBaseRedirectException) requiredException1).Mode = windowMode;
        throw requiredException1;
      case "R":
        INReceiptEntry instance2 = PXGraph.CreateInstance<INReceiptEntry>();
        ((PXSelectBase<INRegister>) instance2.receipt).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance2.receipt).Search<INRegister.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, "IN Receipt");
        ((PXBaseRedirectException) requiredException2).Mode = windowMode;
        throw requiredException2;
      case "T":
        INTransferEntry instance3 = PXGraph.CreateInstance<INTransferEntry>();
        ((PXSelectBase<INRegister>) instance3.transfer).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance3.transfer).Search<INRegister.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException((PXGraph) instance3, "IN Transfer");
        ((PXBaseRedirectException) requiredException3).Mode = windowMode;
        throw requiredException3;
      case "A":
        INAdjustmentEntry instance4 = PXGraph.CreateInstance<INAdjustmentEntry>();
        ((PXSelectBase<INRegister>) instance4.adjustment).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) instance4.adjustment).Search<INRegister.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PXRedirectRequiredException requiredException4 = new PXRedirectRequiredException((PXGraph) instance4, "IN Adjustment");
        ((PXBaseRedirectException) requiredException4).Mode = windowMode;
        throw requiredException4;
      case "P":
      case "D":
        KitAssemblyEntry instance5 = PXGraph.CreateInstance<KitAssemblyEntry>();
        ((PXSelectBase<INKitRegister>) instance5.Document).Current = PXResultset<INKitRegister>.op_Implicit(((PXSelectBase<INKitRegister>) instance5.Document).Search<INKitRegister.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PXRedirectRequiredException requiredException5 = new PXRedirectRequiredException((PXGraph) instance5, "IN Kit Assembly");
        ((PXBaseRedirectException) requiredException5).Mode = windowMode;
        throw requiredException5;
      default:
        throw new PXException("The Document Type is unknown.");
    }
  }
}
