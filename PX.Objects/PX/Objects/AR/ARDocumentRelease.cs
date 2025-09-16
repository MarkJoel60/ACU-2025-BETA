// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocumentRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARDocumentRelease : PXGraph<
#nullable disable
ARDocumentRelease>
{
  public PXCancel<BalancedARDocument> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (BalancedARDocument.refNbr))]
  public PXProcessingJoin<BalancedARDocument, LeftJoin<ARDocumentRelease.ARInvoice, On<ARDocumentRelease.ARInvoice.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARInvoice.refNbr, Equal<BalancedARDocument.refNbr>>>, LeftJoin<ARDocumentRelease.ARPayment, On<ARDocumentRelease.ARPayment.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARPayment.refNbr, Equal<BalancedARDocument.refNbr>>>, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<BalancedARDocument.customerID>>>>>, Where<Match<Customer, Current<AccessInfo.userName>>>> ARDocumentList;
  public PXSetup<ARSetup> arsetup;
  public static string[] TransClassesWithoutZeroPost = new string[3]
  {
    "D",
    "R",
    "B"
  };

  public ARDocumentRelease()
  {
    ARSetup current = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<BalancedARDocument>) this.ARDocumentList).SetProcessDelegate(ARDocumentRelease.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (ARDocumentRelease.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXProcessingBase<BalancedARDocument>.ProcessListDelegate((object) ARDocumentRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__5_0))));
    ((PXProcessing<BalancedARDocument>) this.ARDocumentList).SetProcessCaption("Release");
    ((PXProcessing<BalancedARDocument>) this.ARDocumentList).SetProcessAllCaption("Release All");
  }

  public static void ReleaseDoc(List<ARRegister> list, bool isMassProcess)
  {
    ARDocumentRelease.ReleaseDoc(list, isMassProcess, (List<PX.Objects.GL.Batch>) null, (ARDocumentRelease.ARMassProcessDelegate) null);
  }

  public static void ReleaseDoc(
    List<ARRegister> list,
    bool isMassProcess,
    List<PX.Objects.GL.Batch> externalPostList)
  {
    ARDocumentRelease.ReleaseDoc(list, isMassProcess, externalPostList, (ARDocumentRelease.ARMassProcessDelegate) null);
  }

  public static void ReleaseDoc(
    List<ARRegister> list,
    bool isMassProcess,
    List<PX.Objects.GL.Batch> externalPostList,
    ARDocumentRelease.ARMassProcessDelegate onsuccess)
  {
    ARDocumentRelease.ReleaseDoc(list, isMassProcess, externalPostList, onsuccess, (ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate) null);
  }

  /// <summary>
  /// Static function for release of AR documents and posting of the released batch.
  /// Released batches will be posted if the corresponded flag in ARSetup is set to true.
  /// SkipPost parameter is used to override this flag.
  /// This function can not be called from inside of the covering DB transaction scope, unless skipPost is set to true.
  /// </summary>
  /// <param name="list">List of the documents to be released</param>
  /// <param name="isMassProcess">Flag specifing if the function is called from mass process - affects error handling</param>
  /// <param name="externalPostList"> List of batches that should not be posted inside the release procedure</param>
  /// <param name="onsuccess"> Delegate to be called if release process completed successfully</param>
  /// <param name="onreleasecomplete"> Delegate to be called inside the transaction scope of AR document release process</param>
  public static void ReleaseDoc(
    List<ARRegister> list,
    bool isMassProcess,
    List<PX.Objects.GL.Batch> externalPostList,
    ARDocumentRelease.ARMassProcessDelegate onsuccess,
    ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate onreleasecomplete)
  {
    bool flag1 = false;
    ARReleaseProcess instance1 = PXGraph.CreateInstance<ARReleaseProcess>();
    JournalEntry journalEntry = ARDocumentRelease.CreateJournalEntry();
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    List<PX.Objects.GL.Batch> batchList = new List<PX.Objects.GL.Batch>();
    bool flag2 = externalPostList != null;
    string str = "";
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index] != null)
      {
        ARRegister copy = PXCache<ARRegister>.CreateCopy(instance1.GetFullDocumentFromDB(list[index]));
        try
        {
          bool isAborted = false;
          ((PXGraph) instance1).Clear();
          instance1.VerifyInterBranchTransactions(copy);
          ARRegister ardoc1 = instance1.OnBeforeRelease(copy);
          try
          {
            ARRegister arRegister = ardoc1;
            bool? nullable1;
            if (ardoc1.Status == "N" || ardoc1.Status == "U")
            {
              bool? nullable2 = ardoc1.OpenDoc;
              if (nullable2.GetValueOrDefault())
              {
                nullable2 = ardoc1.Released;
                if (nullable2.GetValueOrDefault())
                {
                  nullable2 = new bool?();
                  nullable1 = nullable2;
                  goto label_9;
                }
              }
            }
            nullable1 = new bool?(false);
label_9:
            arRegister.ReleasedToVerify = nullable1;
            List<ARRegister> arRegisterList = instance1.ReleaseDocProc(journalEntry, ardoc1, batchList, onreleasecomplete);
            object obj1;
            if ((obj1 = ((PXSelectBase) instance1.ARDocument).Cache.Locate((object) ardoc1)) != null)
            {
              PXCache<ARRegister>.RestoreCopy(ardoc1, (ARRegister) obj1);
              ardoc1.Selected = new bool?(true);
            }
            int key;
            if ((key = journalEntry.created.IndexOf(((PXSelectBase<PX.Objects.GL.Batch>) journalEntry.BatchModule).Current)) >= 0 && !dictionary.ContainsKey(key))
              dictionary.Add(key, index);
            if (arRegisterList != null)
            {
              foreach (ARRegister ardoc2 in arRegisterList)
              {
                bool flag3 = ardoc2.DocType == "CRM" && ardoc2.DocType == ardoc1.DocType && ardoc2.RefNbr == ardoc1.RefNbr;
                int num;
                if (ardoc2.Status == "N" || ardoc2.Status == "U")
                {
                  bool? nullable3 = ardoc2.OpenDoc;
                  if (nullable3.GetValueOrDefault())
                  {
                    nullable3 = ardoc2.Released;
                    num = nullable3.GetValueOrDefault() ? 1 : 0;
                    goto label_20;
                  }
                }
                num = 0;
label_20:
                bool flag4 = num != 0;
                ardoc2.ReleasedToVerify = flag3 | flag4 ? new bool?() : new bool?(false);
                ((PXGraph) instance1).Clear();
                instance1.ReleaseDocProc(journalEntry, ardoc2, batchList, (ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate) null);
                object obj2;
                if ((obj2 = ((PXSelectBase) instance1.ARDocument).Cache.Locate((object) ardoc1)) != null)
                {
                  PXCache<ARRegister>.RestoreCopy(ardoc1, (ARRegister) obj2);
                  ardoc1.Selected = new bool?(true);
                }
              }
            }
          }
          catch
          {
            ((PXGraph) journalEntry).Clear();
            journalEntry.CleanupCreated((ICollection<int>) dictionary.Keys);
            isAborted = true;
            throw;
          }
          finally
          {
            if (onsuccess != null)
              onsuccess(ardoc1, isAborted);
          }
          if (isMassProcess)
          {
            if (string.IsNullOrEmpty(ardoc1.WarningMessage))
              PXProcessing<ARRegister>.SetInfo(index, "The record has been processed successfully.");
            else
              PXProcessing<ARRegister>.SetWarning(index, ardoc1.WarningMessage);
          }
        }
        catch (Exception ex)
        {
          flag1 = true;
          str = ex.Message.ToString();
          if (isMassProcess)
            PXProcessing<ARRegister>.SetError(index, ex);
          else
            PXTrace.WriteError(ex);
        }
      }
    }
    if (flag2)
    {
      if (instance1.AutoPost)
        externalPostList.AddRange((IEnumerable<PX.Objects.GL.Batch>) journalEntry.created);
    }
    else
    {
      for (int index = 0; index < journalEntry.created.Count; ++index)
      {
        PX.Objects.GL.Batch b = journalEntry.created[index];
        try
        {
          if (instance1.AutoPost)
          {
            ((PXGraph) instance2).Clear();
            instance2.PostBatchProc(b);
          }
        }
        catch (Exception ex)
        {
          if (!isMassProcess)
            throw new PXMassProcessException(dictionary[index], ex);
          flag1 = true;
          PXProcessing<ARRegister>.SetError(dictionary[index], ex);
        }
      }
    }
    if (flag1 && !isMassProcess && !string.IsNullOrEmpty(str))
      throw new PXException(str);
    if (flag1)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
    List<ProcessInfo<PX.Objects.GL.Batch>> infoList = new List<ProcessInfo<PX.Objects.GL.Batch>>();
    ProcessInfo<PX.Objects.GL.Batch> processInfo = new ProcessInfo<PX.Objects.GL.Batch>(0);
    processInfo.Batches.AddRange((IEnumerable<PX.Objects.GL.Batch>) batchList);
    infoList.Add(processInfo);
    RegisterRelease.Post(infoList, isMassProcess);
  }

  public static JournalEntry CreateJournalEntry()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARDocumentRelease.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new ARDocumentRelease.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.je = PXGraph.CreateInstance<JournalEntry>();
    // ISSUE: reference to a compiler-generated field
    ARDocumentRelease.SetContextForExtention(cDisplayClass120.je);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.je.PrepareForDocumentRelease();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) cDisplayClass120.je).RowInserting.AddHandler<PX.Objects.GL.GLTran>(new PXRowInserting((object) cDisplayClass120, __methodptr(\u003CCreateJournalEntry\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return cDisplayClass120.je;
  }

  private static void SetContextForExtention(JournalEntry je)
  {
    ((PXGraph) je).GetExtension<JournalEntry.JournalEntryContextExt>().GraphContext = GraphContextExtention<JournalEntry>.Context.Release;
  }

  protected virtual IEnumerable ardocumentlist()
  {
    ARDocumentRelease arDocumentRelease = this;
    PXSelectJoinGroupBy<BalancedARDocument, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<BalancedARDocument.customerID>>, LeftJoin<ARAdjust, On<ARAdjust.adjgDocType, Equal<BalancedARDocument.docType>, And<ARAdjust.adjgRefNbr, Equal<BalancedARDocument.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.hold, Equal<False>>>>>, LeftJoin<ARDocumentRelease.ARInvoice, On<ARDocumentRelease.ARInvoice.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARInvoice.refNbr, Equal<BalancedARDocument.refNbr>>>, LeftJoin<ARDocumentRelease.ARPayment, On<ARDocumentRelease.ARPayment.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARPayment.refNbr, Equal<BalancedARDocument.refNbr>>>, LeftJoin<PX.Objects.CA.PaymentMethod, On<ARDocumentRelease.ARPayment.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>>>>, Where2<Match<Customer, Current<AccessInfo.userName>>, And2<Where<BalancedARDocument.status, Equal<ARDocStatus.balanced>, Or<BalancedARDocument.status, Equal<ARDocStatus.open>, Or<BalancedARDocument.status, Equal<ARDocStatus.unapplied>, Or<BalancedARDocument.status, Equal<ARDocStatus.cCHold>>>>>, And2<Where<ARDocumentRelease.ARInvoice.refNbr, IsNotNull, Or<ARDocumentRelease.ARPayment.refNbr, IsNotNull>>, And2<Where<BalancedARDocument.released, Equal<False>, And<BalancedARDocument.origModule, In3<BatchModule.moduleAR, BatchModule.moduleEP>, Or<BalancedARDocument.released, Equal<True>, And<BalancedARDocument.openDoc, Equal<True>, And<ARAdjust.adjdRefNbr, IsNotNull, And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>>, And<BalancedARDocument.isMigratedRecord, Equal<Current<ARSetup.migrationMode>>, And2<Not2<Where<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>>, And<PX.Objects.CA.PaymentMethod.paymentMethodID, IsNotNull, And<Current<ARSetup.integratedCCProcessing>, Equal<True>, And<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>, And<BalancedARDocument.status, Equal<ARDocStatus.cCHold>>>>>>, And<Where<BalancedARDocument.released, Equal<True>, Or<ARDocumentRelease.ARInvoice.refNbr, IsNull, Or<Where2<Where<Current<ARSetup.printBeforeRelease>, NotEqual<True>, Or<ARRegister.printed, Equal<True>, Or<ARRegister.dontPrint, Equal<True>>>>, And<Where<Current<ARSetup.emailBeforeRelease>, NotEqual<True>, Or<ARRegister.emailed, Equal<True>, Or<ARRegister.dontEmail, Equal<True>>>>>>>>>>>>>>>>, Aggregate<GroupBy<BalancedARDocument.docType, GroupBy<BalancedARDocument.refNbr, GroupBy<BalancedARDocument.released, GroupBy<BalancedARDocument.openDoc, GroupBy<BalancedARDocument.hold, GroupBy<BalancedARDocument.scheduled, GroupBy<BalancedARDocument.voided, GroupBy<BalancedARDocument.createdByID, GroupBy<BalancedARDocument.lastModifiedByID, GroupBy<BalancedARDocument.isTaxValid, GroupBy<BalancedARDocument.isTaxSaved, GroupBy<BalancedARDocument.isTaxPosted, GroupBy<ARRegister.dontPrint, GroupBy<ARRegister.printed, GroupBy<ARRegister.dontEmail, GroupBy<ARRegister.emailed>>>>>>>>>>>>>>>>>, OrderBy<Asc<BalancedARDocument.docType, Asc<BalancedARDocument.refNbr>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<BalancedARDocument, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<BalancedARDocument.customerID>>, LeftJoin<ARAdjust, On<ARAdjust.adjgDocType, Equal<BalancedARDocument.docType>, And<ARAdjust.adjgRefNbr, Equal<BalancedARDocument.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.hold, Equal<False>>>>>, LeftJoin<ARDocumentRelease.ARInvoice, On<ARDocumentRelease.ARInvoice.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARInvoice.refNbr, Equal<BalancedARDocument.refNbr>>>, LeftJoin<ARDocumentRelease.ARPayment, On<ARDocumentRelease.ARPayment.docType, Equal<BalancedARDocument.docType>, And<ARDocumentRelease.ARPayment.refNbr, Equal<BalancedARDocument.refNbr>>>, LeftJoin<PX.Objects.CA.PaymentMethod, On<ARDocumentRelease.ARPayment.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>>>>, Where2<Match<Customer, Current<AccessInfo.userName>>, And2<Where<BalancedARDocument.status, Equal<ARDocStatus.balanced>, Or<BalancedARDocument.status, Equal<ARDocStatus.open>, Or<BalancedARDocument.status, Equal<ARDocStatus.unapplied>, Or<BalancedARDocument.status, Equal<ARDocStatus.cCHold>>>>>, And2<Where<ARDocumentRelease.ARInvoice.refNbr, IsNotNull, Or<ARDocumentRelease.ARPayment.refNbr, IsNotNull>>, And2<Where<BalancedARDocument.released, Equal<False>, And<BalancedARDocument.origModule, In3<BatchModule.moduleAR, BatchModule.moduleEP>, Or<BalancedARDocument.released, Equal<True>, And<BalancedARDocument.openDoc, Equal<True>, And<ARAdjust.adjdRefNbr, IsNotNull, And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>>, And<BalancedARDocument.isMigratedRecord, Equal<Current<ARSetup.migrationMode>>, And2<Not2<Where<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>>, And<PX.Objects.CA.PaymentMethod.paymentMethodID, IsNotNull, And<Current<ARSetup.integratedCCProcessing>, Equal<True>, And<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>, And<BalancedARDocument.status, Equal<ARDocStatus.cCHold>>>>>>, And<Where<BalancedARDocument.released, Equal<True>, Or<ARDocumentRelease.ARInvoice.refNbr, IsNull, Or<Where2<Where<Current<ARSetup.printBeforeRelease>, NotEqual<True>, Or<ARRegister.printed, Equal<True>, Or<ARRegister.dontPrint, Equal<True>>>>, And<Where<Current<ARSetup.emailBeforeRelease>, NotEqual<True>, Or<ARRegister.emailed, Equal<True>, Or<ARRegister.dontEmail, Equal<True>>>>>>>>>>>>>>>>, Aggregate<GroupBy<BalancedARDocument.docType, GroupBy<BalancedARDocument.refNbr, GroupBy<BalancedARDocument.released, GroupBy<BalancedARDocument.openDoc, GroupBy<BalancedARDocument.hold, GroupBy<BalancedARDocument.scheduled, GroupBy<BalancedARDocument.voided, GroupBy<BalancedARDocument.createdByID, GroupBy<BalancedARDocument.lastModifiedByID, GroupBy<BalancedARDocument.isTaxValid, GroupBy<BalancedARDocument.isTaxSaved, GroupBy<BalancedARDocument.isTaxPosted, GroupBy<ARRegister.dontPrint, GroupBy<ARRegister.printed, GroupBy<ARRegister.dontEmail, GroupBy<ARRegister.emailed>>>>>>>>>>>>>>>>>, OrderBy<Asc<BalancedARDocument.docType, Asc<BalancedARDocument.refNbr>>>>((PXGraph) arDocumentRelease);
    int startRow = PXView.StartRow;
    int num = 0;
    List<PXView.PXSearchColumn> externalSearchColumns = ((PXSelectBase) arDocumentRelease.ARDocumentList).View.GetContextualExternalSearchColumns();
    foreach (PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment> pxResult in ((PXSelectBase) selectJoinGroupBy).View.Select((object[]) null, (object[]) null, externalSearchColumns.GetSearches(), externalSearchColumns.GetSortColumns(), externalSearchColumns.GetDescendings(), ((PXSelectBase) arDocumentRelease.ARDocumentList).View.GetExternalFilters(), ref startRow, PXView.MaximumRows, ref num))
    {
      BalancedARDocument balancedArDocument = PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult);
      BalancedARDocument row = ((PXSelectBase<BalancedARDocument>) arDocumentRelease.ARDocumentList).Locate(balancedArDocument) ?? balancedArDocument;
      ARDocumentRelease.ARInvoice arInvoice = PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult);
      PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult);
      if (arInvoice != null && !string.IsNullOrEmpty(arInvoice.RefNbr))
      {
        bool? nullable = row.Released;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue && row.Status == "P")
        {
          nullable = ((PXSelectBase<ARSetup>) arDocumentRelease.arsetup).Current.PrintBeforeRelease;
          if (!nullable.GetValueOrDefault())
            row.Status = "B";
        }
        nullable = row.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && row.Status == "E")
        {
          nullable = ((PXSelectBase<ARSetup>) arDocumentRelease.arsetup).Current.EmailBeforeRelease;
          if (!nullable.GetValueOrDefault())
            row.Status = "B";
        }
      }
      ARAdjust arAdjust = PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult);
      if (arAdjust.AdjdRefNbr != null)
      {
        row.DocDate = arAdjust.AdjgDocDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<ARRegister.finPeriodID>(((PXSelectBase) arDocumentRelease.ARDocumentList).Cache, (object) row, arAdjust.AdjgTranPeriodID);
      }
      yield return (object) new PXResult<BalancedARDocument, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment, Customer, ARAdjust>(row, PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult), PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult), PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult), PXResult<BalancedARDocument, Customer, ARAdjust, ARDocumentRelease.ARInvoice, ARDocumentRelease.ARPayment>.op_Implicit(pxResult));
    }
    PXView.StartRow = 0;
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXRemoveBaseAttribute(typeof (PXCurrencyAttribute))]
  protected virtual void BalancedARDocument_CuryOrigDocAmtWithRetainageTotal_CacheAttached(
    PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Amount")]
  protected virtual void BalancedARDocument_CuryOrigDocAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BalancedARDocument.branchID> e)
  {
  }

  public class SingleCurrency : SingleCurrencyGraph<ARDocumentRelease, ARRegister>
  {
    public static bool IsActive() => true;
  }

  public delegate void ARMassProcessDelegate(ARRegister ardoc, bool isAborted);

  public delegate void ARMassProcessReleaseTransactionScopeDelegate(ARRegister ardoc);

  [PXHidden]
  [Serializable]
  public class ARInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string RefNbr { get; set; }

    [PXDBString(40, IsKey = true, IsUnicode = true)]
    public virtual string InvoiceNbr { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARInvoice.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARInvoice.refNbr>
    {
    }

    public abstract class invoiceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARInvoice.invoiceNbr>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ARPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true)]
    public virtual string RefNbr { get; set; }

    [PXDBString(40, IsUnicode = true)]
    public virtual string ExtRefNbr { get; set; }

    [PXDBInt]
    public virtual int? PMInstanceID { get; set; }

    [PXDBString(10, IsUnicode = true)]
    public virtual string PaymentMethodID { get; set; }

    [PXDBBool]
    public virtual bool? IsCCCaptured { get; set; }

    [PXDBBool]
    public virtual bool? IsCCRefunded { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.refNbr>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.extRefNbr>
    {
    }

    public abstract class pMInstanceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.pMInstanceID>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.paymentMethodID>
    {
    }

    public abstract class isCCCaptured : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.isCCCaptured>
    {
    }

    public abstract class isCCRefunded : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentRelease.ARPayment.isCCRefunded>
    {
    }
  }
}
