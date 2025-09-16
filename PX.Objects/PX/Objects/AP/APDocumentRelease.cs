// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APDocumentRelease : PXGraph<APDocumentRelease>
{
  public PXCancel<BalancedAPDocument> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<BalancedAPDocument, LeftJoin<APInvoice, On<APInvoice.docType, Equal<BalancedAPDocument.docType>, And<APInvoice.refNbr, Equal<BalancedAPDocument.refNbr>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<BalancedAPDocument.docType>, And<APPayment.refNbr, Equal<BalancedAPDocument.refNbr>>>, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APRegister.vendorID>>>>>, Where<Match<Vendor, Current<AccessInfo.userName>>>, OrderBy<Asc<BalancedAPDocument.docType, Asc<BalancedAPDocument.refNbr>>>> APDocumentList;
  public static string[] TransClassesWithoutZeroPost = new string[2]
  {
    "D",
    "R"
  };
  public PXAction<BalancedAPDocument> ViewDocument;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BalancedAPDocument.branchID> e)
  {
  }

  public APDocumentRelease()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.APDocumentList.SetProcessDelegate((PXProcessingBase<BalancedAPDocument>.ProcessListDelegate) (list =>
    {
      List<APRegister> list1 = new List<APRegister>(list.Count);
      foreach (BalancedAPDocument balancedApDocument in list)
        list1.Add((APRegister) balancedApDocument);
      APDocumentRelease.ReleaseDoc(list1, true);
    }));
    this.APDocumentList.SetProcessCaption("Release");
    this.APDocumentList.SetProcessAllCaption("Release All");
    PXNoteAttribute.ForcePassThrow<BalancedAPDocument.noteID>(this.APDocumentList.Cache);
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (this.APDocumentList.Current != null)
      PXRedirectHelper.TryRedirect(this.APDocumentList.Cache, (object) this.APDocumentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  public static void ReleaseDoc(List<APRegister> list, bool isMassProcess)
  {
    APDocumentRelease.ReleaseDoc(list, isMassProcess, false);
  }

  public static void ReleaseDoc(
    List<APRegister> list,
    bool isMassProcess,
    List<Batch> externalPostList)
  {
    APDocumentRelease.ReleaseDoc(list, isMassProcess, false, externalPostList);
  }

  public static void ReleaseDoc(List<APRegister> list, bool isMassProcess, bool isPrebooking)
  {
    APDocumentRelease.ReleaseDoc(list, isMassProcess, isPrebooking, (List<Batch>) null);
  }

  /// <summary>
  /// Static function for release of AP documents and posting of the released batch.
  /// Released batches will be posted if the corresponded flag in APSetup is set to true.
  /// SkipPost parameter is used to override this flag.
  /// This function can not be called from inside of the covering DB transaction scope, unless skipPost is set to true.
  /// </summary>
  /// <param name="list">List of the documents to be released</param>
  /// <param name="isMassProcess">Flag specifing if the function is called from mass process - affects error handling</param>
  /// <param name="skipPost"> Prevent Posting of the released batch(es). This parameter must be set to true if this function is called from "covering" DB transaction</param>
  public static void ReleaseDoc(
    List<APRegister> list,
    bool isMassProcess,
    bool isPrebooking,
    List<Batch> externalPostList)
  {
    bool flag1 = false;
    bool flag2 = externalPostList != null;
    APReleaseProcess instance1 = PXGraph.CreateInstance<APReleaseProcess>();
    JournalEntry journalEntry = APDocumentRelease.CreateJournalEntry();
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    bool flag3 = false;
    bool flag4 = false;
    string message = "";
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      APRegister apRegister = list[index1];
      List<PX.Objects.IN.INRegister> inDocs1 = (List<PX.Objects.IN.INRegister>) null;
      if (apRegister != null)
      {
        try
        {
          instance1.Clear();
          instance1.VerifyStockItemLineHasReceipt(apRegister);
          instance1.VerifyInterBranchTransactions(apRegister);
          APRegister doc = instance1.OnBeforeRelease(apRegister);
          doc.ReleasedToVerify = !(doc.Status == "N") && !(doc.Status == "U") || !doc.OpenDoc.GetValueOrDefault() || !doc.Released.GetValueOrDefault() ? new bool?(false) : new bool?();
          List<APRegister> apRegisterList = instance1.ReleaseDocProc(journalEntry, doc, isPrebooking, out inDocs1);
          int key;
          if ((key = journalEntry.created.IndexOf(journalEntry.BatchModule.Current)) >= 0 && !dictionary.ContainsKey(key))
            dictionary.Add(key, index1);
          if (apRegisterList != null)
          {
            for (int index2 = 0; index2 < apRegisterList.Count; ++index2)
            {
              bool flag5 = apRegisterList[index2].DocType == "ADR" && apRegisterList[index2].DocType == doc.DocType && apRegisterList[index2].RefNbr == doc.RefNbr;
              int num;
              if (apRegisterList[index2].Status == "N" || apRegisterList[index2].Status == "U")
              {
                bool? nullable = apRegisterList[index2].OpenDoc;
                if (nullable.GetValueOrDefault())
                {
                  nullable = apRegisterList[index2].Released;
                  num = nullable.GetValueOrDefault() ? 1 : 0;
                  goto label_10;
                }
              }
              num = 0;
label_10:
              bool flag6 = num != 0;
              apRegisterList[index2].ReleasedToVerify = flag5 | flag6 ? new bool?() : new bool?(false);
              doc = apRegisterList[index2];
              instance1.Clear();
              List<PX.Objects.IN.INRegister> inDocs2;
              instance1.ReleaseDocProc(journalEntry, doc, isPrebooking, out inDocs2);
              // ISSUE: explicit non-virtual call
              if (inDocs2 != null && __nonvirtual (inDocs2.Count) > 0)
              {
                if (inDocs1 == null)
                  inDocs1 = new List<PX.Objects.IN.INRegister>((IEnumerable<PX.Objects.IN.INRegister>) inDocs2);
                else
                  inDocs1.Add<PX.Objects.IN.INRegister>((IEnumerable<PX.Objects.IN.INRegister>) inDocs2);
              }
            }
          }
          if (isMassProcess)
          {
            if (string.IsNullOrEmpty(doc.WarningMessage))
              PXProcessing<APRegister>.SetInfo(index1, "The record has been processed successfully.");
            else
              PXProcessing<APRegister>.SetWarning(index1, doc.WarningMessage);
          }
        }
        catch (Exception ex)
        {
          journalEntry.Clear();
          journalEntry.CleanupCreated((ICollection<int>) dictionary.Keys);
          flag1 = true;
          message = ex.Message.ToString();
          if (isMassProcess)
            PXProcessing<APRegister>.SetError(index1, ex);
          else
            PXTrace.WriteError(ex);
        }
        try
        {
          List<PX.Objects.IN.INRegister> list1 = inDocs1 != null ? inDocs1.Where<PX.Objects.IN.INRegister>((Func<PX.Objects.IN.INRegister, bool>) (d => d.IsTaxAdjustmentTran.GetValueOrDefault())).ToList<PX.Objects.IN.INRegister>() : (List<PX.Objects.IN.INRegister>) null;
          if (list1 != null)
          {
            // ISSUE: explicit non-virtual call
            if (__nonvirtual (list1.Count) > 0)
            {
              POSetup posetup = instance1.posetup;
              if ((posetup != null ? (posetup.AutoReleaseIN.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                INDocumentRelease.ReleaseDoc(list1, false);
            }
          }
        }
        catch (Exception ex)
        {
          flag4 = true;
          if (isMassProcess)
            PXProcessing<APRegister>.SetError(index1, ex);
          else
            PXTrace.WriteError(ex);
        }
        try
        {
          List<PX.Objects.IN.INRegister> list2 = inDocs1 != null ? inDocs1.Where<PX.Objects.IN.INRegister>((Func<PX.Objects.IN.INRegister, bool>) (d => !d.IsTaxAdjustmentTran.GetValueOrDefault())).ToList<PX.Objects.IN.INRegister>() : (List<PX.Objects.IN.INRegister>) null;
          if (list2 != null)
          {
            // ISSUE: explicit non-virtual call
            if (__nonvirtual (list2.Count) > 0)
            {
              POSetup posetup = instance1.posetup;
              if ((posetup != null ? (posetup.AutoReleaseIN.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                INDocumentRelease.ReleaseDoc(list2, false);
            }
          }
        }
        catch (Exception ex)
        {
          flag3 = true;
          if (isMassProcess)
            PXProcessing<APRegister>.SetError(index1, ex);
          else
            PXTrace.WriteError(ex);
        }
      }
    }
    if (flag2)
    {
      if (instance1.AutoPost)
        externalPostList.AddRange((IEnumerable<Batch>) journalEntry.created);
    }
    else
    {
      for (int index = 0; index < journalEntry.created.Count; ++index)
      {
        Batch b = journalEntry.created[index];
        try
        {
          if (instance1.AutoPost)
          {
            instance2.Clear();
            instance2.PostBatchProc(b);
          }
        }
        catch (Exception ex)
        {
          if (!isMassProcess)
            throw new PXMassProcessException(dictionary[index], ex);
          flag1 = true;
          PXProcessing<APRegister>.SetError(dictionary[index], ex);
        }
      }
    }
    if (flag1 & isMassProcess | flag3 | flag4)
      PXProcessing<APPayment>.SetCurrentItem((object) null);
    if (flag1 && !isMassProcess && !string.IsNullOrEmpty(message))
      throw new PXException(message);
    if (flag1)
      throw new PXException("One or more documents could not be released.");
    if (flag3)
      throw new PXException("Processing of the purchase price variance adjustment for one or more AP documents has failed.");
    if (flag4)
      throw new PXException("Processing of the tax adjustment for one or more AP documents has failed.");
  }

  public static JournalEntry CreateJournalEntry()
  {
    JournalEntry je = PXGraph.CreateInstance<JournalEntry>();
    je.PrepareForDocumentRelease();
    je.RowInserting.AddHandler<PX.Objects.GL.GLTran>((PXRowInserting) ((sender, e) => je.SetZeroPostIfUndefined((PX.Objects.GL.GLTran) e.Row, (IReadOnlyCollection<string>) APDocumentRelease.TransClassesWithoutZeroPost)));
    return je;
  }

  public static void VoidDoc(List<APRegister> list)
  {
    bool flag = false;
    APReleaseProcess instance1 = PXGraph.CreateInstance<APReleaseProcess>();
    JournalEntry journalEntry = APDocumentRelease.CreateJournalEntry();
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    for (int index = 0; index < list.Count; ++index)
    {
      APRegister doc = list[index];
      if (doc != null)
      {
        try
        {
          instance1.Clear();
          if (doc.Passed.GetValueOrDefault())
            instance1.TimeStamp = doc.tstamp;
          instance1.VoidDocProc(journalEntry, doc);
          PXProcessing<APRegister>.SetInfo(index, "The record has been processed successfully.");
          int key;
          if ((key = journalEntry.created.IndexOf(journalEntry.BatchModule.Current)) >= 0)
          {
            if (!dictionary.ContainsKey(key))
              dictionary.Add(key, index);
          }
        }
        catch (Exception ex)
        {
          throw new PXMassProcessException(index, ex);
        }
      }
    }
    for (int index = 0; index < journalEntry.created.Count; ++index)
    {
      Batch b = journalEntry.created[index];
      try
      {
        if (instance1.AutoPost)
        {
          instance2.Clear();
          instance2.PostBatchProc(b);
        }
      }
      catch (Exception ex)
      {
        throw new PXMassProcessException(dictionary[index], ex);
      }
    }
    if (flag)
      throw new PXException("One or more documents could not be released.");
  }

  protected virtual IEnumerable apdocumentlist()
  {
    PXResultset<BalancedAPDocument, APInvoice, APPayment, Vendor, APAdjust> pxResultset = new PXResultset<BalancedAPDocument, APInvoice, APPayment, Vendor, APAdjust>();
    PXSelectJoinGroupBy<BalancedAPDocument, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APRegister.vendorID>>, LeftJoin<APAdjust, On<APAdjust.adjgDocType, Equal<BalancedAPDocument.docType>, And<APAdjust.adjgRefNbr, Equal<BalancedAPDocument.refNbr>, And<APAdjust.released, NotEqual<True>, And<APAdjust.hold, Equal<boolFalse>>>>>, LeftJoin<APInvoice, On<APInvoice.docType, Equal<BalancedAPDocument.docType>, And<APInvoice.refNbr, Equal<BalancedAPDocument.refNbr>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<BalancedAPDocument.docType>, And<APPayment.refNbr, Equal<BalancedAPDocument.refNbr>>>>>>>, Where2<Match<Vendor, Current<AccessInfo.userName>>, And<APRegister.hold, Equal<boolFalse>, And<APRegister.voided, Equal<boolFalse>, And<APRegister.scheduled, Equal<boolFalse>, And<APRegister.approved, Equal<boolTrue>, And<APRegister.docType, NotEqual<APDocType.check>, And<APRegister.docType, NotEqual<APDocType.quickCheck>, And2<Where<APInvoice.refNbr, PX.Data.IsNotNull, Or<APPayment.refNbr, PX.Data.IsNotNull>>, And2<Where<BalancedAPDocument.released, Equal<boolFalse>, Or<BalancedAPDocument.openDoc, Equal<boolTrue>, And<APAdjust.adjdRefNbr, PX.Data.IsNotNull, And<APAdjust.isInitialApplication, NotEqual<True>>>>>, And<APRegister.isMigratedRecord, Equal<Current<PX.Objects.AP.APSetup.migrationMode>>, PX.Data.And<Where<BalancedAPDocument.docType, NotEqual<APDocType.prepayment>, Or<BalancedAPDocument.printed, Equal<True>, Or<BalancedAPDocument.printCheck, PX.Data.IsNull, Or<BalancedAPDocument.printCheck, NotEqual<True>>>>>>>>>>>>>>>>, Aggregate<GroupBy<BalancedAPDocument.docType, GroupBy<BalancedAPDocument.refNbr, GroupBy<BalancedAPDocument.released, GroupBy<BalancedAPDocument.prebooked, GroupBy<BalancedAPDocument.openDoc, GroupBy<BalancedAPDocument.hold, GroupBy<BalancedAPDocument.scheduled, GroupBy<BalancedAPDocument.voided, GroupBy<BalancedAPDocument.printed, GroupBy<BalancedAPDocument.approved, GroupBy<BalancedAPDocument.noteID, GroupBy<BalancedAPDocument.createdByID, GroupBy<BalancedAPDocument.lastModifiedByID>>>>>>>>>>>>>>, OrderBy<Asc<BalancedAPDocument.docType, Asc<BalancedAPDocument.refNbr>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<BalancedAPDocument, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APRegister.vendorID>>, LeftJoin<APAdjust, On<APAdjust.adjgDocType, Equal<BalancedAPDocument.docType>, And<APAdjust.adjgRefNbr, Equal<BalancedAPDocument.refNbr>, And<APAdjust.released, NotEqual<True>, And<APAdjust.hold, Equal<boolFalse>>>>>, LeftJoin<APInvoice, On<APInvoice.docType, Equal<BalancedAPDocument.docType>, And<APInvoice.refNbr, Equal<BalancedAPDocument.refNbr>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<BalancedAPDocument.docType>, And<APPayment.refNbr, Equal<BalancedAPDocument.refNbr>>>>>>>, Where2<Match<Vendor, Current<AccessInfo.userName>>, And<APRegister.hold, Equal<boolFalse>, And<APRegister.voided, Equal<boolFalse>, And<APRegister.scheduled, Equal<boolFalse>, And<APRegister.approved, Equal<boolTrue>, And<APRegister.docType, NotEqual<APDocType.check>, And<APRegister.docType, NotEqual<APDocType.quickCheck>, And2<Where<APInvoice.refNbr, PX.Data.IsNotNull, Or<APPayment.refNbr, PX.Data.IsNotNull>>, And2<Where<BalancedAPDocument.released, Equal<boolFalse>, Or<BalancedAPDocument.openDoc, Equal<boolTrue>, And<APAdjust.adjdRefNbr, PX.Data.IsNotNull, And<APAdjust.isInitialApplication, NotEqual<True>>>>>, And<APRegister.isMigratedRecord, Equal<Current<PX.Objects.AP.APSetup.migrationMode>>, PX.Data.And<Where<BalancedAPDocument.docType, NotEqual<APDocType.prepayment>, Or<BalancedAPDocument.printed, Equal<True>, Or<BalancedAPDocument.printCheck, PX.Data.IsNull, Or<BalancedAPDocument.printCheck, NotEqual<True>>>>>>>>>>>>>>>>, Aggregate<GroupBy<BalancedAPDocument.docType, GroupBy<BalancedAPDocument.refNbr, GroupBy<BalancedAPDocument.released, GroupBy<BalancedAPDocument.prebooked, GroupBy<BalancedAPDocument.openDoc, GroupBy<BalancedAPDocument.hold, GroupBy<BalancedAPDocument.scheduled, GroupBy<BalancedAPDocument.voided, GroupBy<BalancedAPDocument.printed, GroupBy<BalancedAPDocument.approved, GroupBy<BalancedAPDocument.noteID, GroupBy<BalancedAPDocument.createdByID, GroupBy<BalancedAPDocument.lastModifiedByID>>>>>>>>>>>>>>, OrderBy<Asc<BalancedAPDocument.docType, Asc<BalancedAPDocument.refNbr>>>>((PXGraph) this);
    int startRow = PXView.StartRow;
    int totalRows = 0;
    List<PXView.PXSearchColumn> externalSearchColumns = this.APDocumentList.View.GetContextualExternalSearchColumns();
    foreach (PXResult<BalancedAPDocument, Vendor, APAdjust, APInvoice, APPayment> pxResult in selectJoinGroupBy.View.Select((object[]) null, (object[]) null, externalSearchColumns.GetSearches(), externalSearchColumns.GetSortColumns(), externalSearchColumns.GetDescendings(), this.APDocumentList.View.GetExternalFilters(), ref startRow, PXView.MaximumRows, ref totalRows))
    {
      BalancedAPDocument balancedApDocument1 = (BalancedAPDocument) pxResult;
      BalancedAPDocument balancedApDocument2 = this.APDocumentList.Locate(balancedApDocument1) ?? balancedApDocument1;
      APAdjust apAdjust = (APAdjust) pxResult;
      if (apAdjust.AdjdRefNbr != null)
      {
        balancedApDocument2.DocDate = apAdjust.AdjgDocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<APRegister.finPeriodID>(this.APDocumentList.Cache, (object) balancedApDocument2, apAdjust.AdjgTranPeriodID);
      }
      pxResultset.Add((PXResult<BalancedAPDocument>) new PXResult<BalancedAPDocument, APInvoice, APPayment, Vendor, APAdjust>(balancedApDocument2, (APInvoice) pxResult, (APPayment) pxResult, (Vendor) pxResult, (APAdjust) pxResult));
    }
    PXView.StartRow = 0;
    return (IEnumerable) pxResultset;
  }

  public class SingleCurrency : SingleCurrencyGraph<APDocumentRelease, APRegister>
  {
    public static bool IsActive() => true;
  }
}
