// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBatchDocRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CA.MultiCurrency;
using PX.Objects.Common.Exceptions;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class GLBatchDocRelease : PXGraph<
#nullable disable
GLBatchDocRelease>
{
  private const int RefNbrLength = 15;
  protected bool _suppressARCreditVerification = true;
  protected bool _suppressARPrintVerification = true;
  public PXSelect<GLDocBatch, Where<GLDocBatch.module, Equal<Required<GLDocBatch.module>>, And<GLDocBatch.batchNbr, Equal<Required<GLDocBatch.batchNbr>>>>> batch;
  public PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Current<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>>>> details;
  public PXSelect<GLTaxTran, Where<GLTaxTran.module, Equal<Required<GLTaxTran.module>>, And<GLTaxTran.batchNbr, Equal<Required<GLTaxTran.batchNbr>>, And<GLTaxTran.lineNbr, Equal<Required<GLTaxTran.lineNbr>>>>>> taxTotals;
  public PXSelect<GLBatchDocRelease.GLTranDocU, Where<GLBatchDocRelease.GLTranDocU.module, Equal<Current<GLDocBatch.module>>, And<GLBatchDocRelease.GLTranDocU.batchNbr, Equal<Current<GLDocBatch.batchNbr>>>>> detailUpdate;

  public void ReleaseBatchProc(GLDocBatch aBatch, bool useLongOperationErrorHandling)
  {
    ((PXGraph) this).Clear();
    GLDocBatch glDocBatch = PXResultset<GLDocBatch>.op_Implicit(((PXSelectBase<GLDocBatch>) this.batch).Select(new object[2]
    {
      (object) aBatch.Module,
      (object) aBatch.BatchNbr
    }));
    ((PXSelectBase<GLDocBatch>) this.batch).Current = glDocBatch;
    this.CreateBatchDetailsProc(aBatch, useLongOperationErrorHandling);
    if (this.HasUnreleasedDetails(aBatch))
      return;
    GLDocBatch copy = (GLDocBatch) ((PXSelectBase) this.batch).Cache.CreateCopy((object) glDocBatch);
    copy.Released = new bool?(true);
    ((SelectedEntityEvent<GLDocBatch>) PXEntityEventBase<GLDocBatch>.Container<GLDocBatch.Events>.Select((Expression<Func<GLDocBatch.Events, PXEntityEvent<GLDocBatch.Events>>>) (ev => ev.ReleaseVoucher))).FireOn((PXGraph) this, copy);
    ((PXSelectBase<GLDocBatch>) this.batch).Update(copy);
    ((PXGraph) this).Actions.PressSave();
  }

  private void CreateDocProc(KeyValuePair<int, List<GLTranDoc>> aDocInfo, List<Batch> toPost)
  {
    GLTranDoc doc = (GLTranDoc) null;
    foreach (GLTranDoc glTranDoc in aDocInfo.Value)
    {
      if (glTranDoc.LineNbr.Value == aDocInfo.Key)
        doc = glTranDoc;
    }
    if (!string.IsNullOrEmpty(doc.RefNbr))
    {
      bool? docCreated = doc.DocCreated;
      bool flag = false;
      if (!(docCreated.GetValueOrDefault() == flag & docCreated.HasValue))
        return;
    }
    if (doc.TranModule == "AP" && this.CreateAP(doc, aDocInfo.Value, toPost) == null)
      throw new PXException("Row {0} has invalid on unsupported type {1} {2}", new object[3]
      {
        (object) doc.LineNbr,
        (object) doc.Module,
        (object) doc.TranType
      });
    if (doc.TranModule == "AR" && this.CreateAR(doc, aDocInfo.Value, toPost) == null)
      throw new PXException("Row {0} has invalid on unsupported type {1} {2}", new object[3]
      {
        (object) doc.LineNbr,
        (object) doc.Module,
        (object) doc.TranType
      });
    if (doc.TranModule == "CA" && this.CreateCA(doc, aDocInfo.Value, toPost) == null)
      throw new PXException("Row {0} has invalid on unsupported type {1} {2}", new object[3]
      {
        (object) doc.LineNbr,
        (object) doc.Module,
        (object) doc.TranType
      });
    if (doc.TranModule == "GL" && this.CreateGL(doc, aDocInfo.Value, toPost) == null)
      throw new PXException("Row {0} has invalid on unsupported type {1} {2}", new object[3]
      {
        (object) doc.LineNbr,
        (object) doc.Module,
        (object) doc.TranType
      });
    foreach (GLTranDoc aSrc in aDocInfo.Value)
    {
      GLBatchDocRelease.GLTranDocU aDest = new GLBatchDocRelease.GLTranDocU();
      this.Copy(aDest, aSrc);
      aDest.RefNbr = doc.RefNbr;
      aDest.DocCreated = doc.DocCreated;
      aDest.Released = doc.Released;
      ((PXSelectBase<GLBatchDocRelease.GLTranDocU>) this.detailUpdate).Update(aDest);
    }
  }

  private void CreateBatchDetailsProc(GLDocBatch aBatch, bool useLongOperationErrorHandling)
  {
    Dictionary<int, List<GLTranDoc>> dictionary1 = new Dictionary<int, List<GLTranDoc>>();
    Dictionary<int, List<GLTranDoc>> dictionary2 = new Dictionary<int, List<GLTranDoc>>();
    Dictionary<long, CAMessage> dictionary3 = new Dictionary<long, CAMessage>();
    foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.details).Select(Array.Empty<object>()))
    {
      GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
      int? nullable;
      int num;
      if (!glTranDoc.IsChildTran)
      {
        nullable = glTranDoc.LineNbr;
        num = nullable.Value;
      }
      else
      {
        nullable = glTranDoc.ParentLineNbr;
        num = nullable.Value;
      }
      int key = num;
      Dictionary<int, List<GLTranDoc>> dictionary4 = (JournalWithSubEntry.IsMixedType(glTranDoc) ? 0 : (JournalWithSubEntry.IsAPInvoice(glTranDoc) ? 1 : (JournalWithSubEntry.IsARInvoice(glTranDoc) ? 1 : 0))) != 0 ? dictionary1 : dictionary2;
      if (!dictionary4.ContainsKey(key))
        dictionary4.Add(key, new List<GLTranDoc>(1));
      GLTranDoc copy = (GLTranDoc) ((PXSelectBase) this.details).Cache.CreateCopy((object) glTranDoc);
      dictionary4[key].Add(copy);
    }
    List<int> intList = new List<int>();
    List<Batch> toPost = new List<Batch>(dictionary1.Count + dictionary2.Count);
    Dictionary<int, List<GLTranDoc>>[] source = new Dictionary<int, List<GLTranDoc>>[2]
    {
      dictionary1,
      dictionary2
    };
    foreach (Dictionary<int, List<GLTranDoc>> dictionary5 in source)
    {
      foreach (KeyValuePair<int, List<GLTranDoc>> aDocInfo in dictionary5)
      {
        try
        {
          using (new PXTimeStampScope(((PXGraph) this).TimeStamp))
          {
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
              this.CreateDocProc(aDocInfo, toPost);
              ((PXGraph) this).Actions.PressSave();
              transactionScope.Complete();
            }
          }
          dictionary3.Add((long) aDocInfo.Key, new CAMessage((long) aDocInfo.Key, (PXErrorLevel) 1, "The record has been processed successfully."));
        }
        catch (Exception ex)
        {
          intList.Add(aDocInfo.Key);
          string aMessage = ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message;
          dictionary3.Add((long) aDocInfo.Key, new CAMessage((long) aDocInfo.Key, (PXErrorLevel) 5, aMessage));
        }
      }
    }
    if (useLongOperationErrorHandling && dictionary3.Count > 0)
      PXLongOperation.SetCustomInfoPersistent((object) dictionary3);
    List<Batch> batchList = new List<Batch>();
    if (toPost.Count > 0)
    {
      PostGraph instance = PXGraph.CreateInstance<PostGraph>();
      foreach (Batch b in toPost)
      {
        try
        {
          ((PXGraph) instance).Clear();
          instance.PostBatchProc(b);
        }
        catch (Exception ex)
        {
          batchList.Add(b);
        }
      }
    }
    if (intList.Count > 0)
      throw new PXException("{0} of {1} documents were not created", new object[2]
      {
        (object) intList.Count,
        (object) ((IEnumerable<Dictionary<int, List<GLTranDoc>>>) source).Sum<Dictionary<int, List<GLTranDoc>>>((Func<Dictionary<int, List<GLTranDoc>>, int>) (i => i.Count))
      });
    if (batchList.Count > 0)
      throw new PXException("Documents were successfully created, but {0} of {1} were not posted", new object[2]
      {
        (object) batchList.Count,
        (object) toPost.Count
      });
  }

  private bool HasUnreleasedDetails(GLDocBatch aBatch)
  {
    foreach (PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch> pxResult in PXSelectBase<GLTranDoc, PXSelectReadonly2<GLTranDoc, LeftJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<GLTranDoc.tranType>, And<PX.Objects.AP.APRegister.refNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleAP>>>>, LeftJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<GLTranDoc.tranType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleAR>>>>, LeftJoin<CAAdj, On<CAAdj.adjTranType, Equal<GLTranDoc.tranType>, And<CAAdj.adjRefNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleCA>>>>, LeftJoin<Batch, On<Batch.batchNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleGL>>>>>>>, Where<GLTranDoc.module, Equal<Required<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aBatch.Module,
      (object) aBatch.BatchNbr
    }))
    {
      PX.Objects.AP.APRegister apRegister = PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch>.op_Implicit(pxResult);
      PX.Objects.AR.ARRegister arRegister = PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch>.op_Implicit(pxResult);
      CAAdj caAdj = PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch>.op_Implicit(pxResult);
      GLTranDoc glTranDoc = PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch>.op_Implicit(pxResult);
      Batch batch = PXResult<GLTranDoc, PX.Objects.AP.APRegister, PX.Objects.AR.ARRegister, CAAdj, Batch>.op_Implicit(pxResult);
      if (glTranDoc.TranModule == "AR")
      {
        if (arRegister != null && !string.IsNullOrEmpty(arRegister.RefNbr))
        {
          bool? released = arRegister.Released;
          bool flag = false;
          if (!(released.GetValueOrDefault() == flag & released.HasValue))
            goto label_6;
        }
        return true;
      }
label_6:
      if (glTranDoc.TranModule == "AP")
      {
        if (apRegister != null && !string.IsNullOrEmpty(apRegister.RefNbr))
        {
          bool? released = apRegister.Released;
          bool flag = false;
          if (!(released.GetValueOrDefault() == flag & released.HasValue))
            goto label_10;
        }
        return true;
      }
label_10:
      if (glTranDoc.TranModule == "CA")
      {
        if (apRegister != null && !string.IsNullOrEmpty(caAdj.RefNbr))
        {
          bool? released = caAdj.Released;
          bool flag = false;
          if (!(released.GetValueOrDefault() == flag & released.HasValue))
            goto label_14;
        }
        return true;
      }
label_14:
      if (glTranDoc.TranModule == "GL")
      {
        if (batch != null && !string.IsNullOrEmpty(batch.BatchNbr))
        {
          bool? released = batch.Released;
          bool flag = false;
          if (!(released.GetValueOrDefault() == flag & released.HasValue))
            goto label_18;
        }
        return true;
      }
label_18:
      if (glTranDoc == null || string.IsNullOrEmpty(apRegister.RefNbr) && string.IsNullOrEmpty(arRegister.RefNbr) && string.IsNullOrEmpty(caAdj.RefNbr) && string.IsNullOrEmpty(batch.BatchNbr))
      {
        if (!string.IsNullOrEmpty(glTranDoc.RefNbr))
          throw new PXException("Unsupported type of the document detected on line {0} - {1} {2} {3}", new object[4]
          {
            (object) glTranDoc.LineNbr,
            (object) glTranDoc.TranModule,
            (object) glTranDoc.TranType,
            (object) glTranDoc.RefNbr
          });
        throw new PXException("Document is not created for the line {0} {1}", new object[2]
        {
          (object) glTranDoc.TranLineNbr,
          (object) glTranDoc.TranCode
        });
      }
    }
    return false;
  }

  protected virtual PXGraph CreateAP(GLTranDoc doc, List<GLTranDoc> aList, List<Batch> toPost)
  {
    PXGraph ap = (PXGraph) null;
    bool flag = aList.Count > 1;
    PX.Objects.AP.APRegister apRegister1 = (PX.Objects.AP.APRegister) null;
    PX.Objects.AP.Standalone.APInvoice apInvoice1 = (PX.Objects.AP.Standalone.APInvoice) null;
    PX.Objects.AP.Standalone.APPayment apPayment1 = (PX.Objects.AP.Standalone.APPayment) null;
    Batch batch = (Batch) null;
    List<PX.Objects.AP.APRegister> list = new List<PX.Objects.AP.APRegister>(1);
    if (!string.IsNullOrEmpty(doc.RefNbr))
    {
      PXResultset<PX.Objects.AP.APRegister> pxResultset = PXSelectBase<PX.Objects.AP.APRegister, PXSelectJoin<PX.Objects.AP.APRegister, LeftJoin<PX.Objects.AP.Standalone.APInvoice, On<PX.Objects.AP.Standalone.APInvoice.refNbr, Equal<PX.Objects.AP.APRegister.refNbr>, And<PX.Objects.AP.Standalone.APInvoice.docType, Equal<PX.Objects.AP.APRegister.docType>>>, LeftJoin<PX.Objects.AP.Standalone.APPayment, On<PX.Objects.AP.Standalone.APPayment.refNbr, Equal<PX.Objects.AP.APRegister.refNbr>, And<PX.Objects.AP.Standalone.APPayment.docType, Equal<PX.Objects.AP.APRegister.docType>>>, LeftJoin<Batch, On<Batch.origModule, Equal<BatchModule.moduleAP>, And<Batch.batchNbr, Equal<PX.Objects.AP.APRegister.batchNbr>>>>>>, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) doc.TranType,
        (object) doc.RefNbr
      });
      if (pxResultset.Count > 0)
      {
        apRegister1 = PXResult<PX.Objects.AP.APRegister>.op_Implicit(pxResultset[0]);
        apInvoice1 = PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>.op_Implicit((PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>) pxResultset[0]);
        apPayment1 = PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>.op_Implicit((PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>) pxResultset[0]);
        batch = PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>.op_Implicit((PXResult<PX.Objects.AP.APRegister, PX.Objects.AP.Standalone.APInvoice, PX.Objects.AP.Standalone.APPayment, Batch>) pxResultset[0]);
      }
    }
    if (JournalWithSubEntry.IsAPInvoice(doc) && JournalWithSubEntry.IsAPPayment(doc))
    {
      APQuickCheckEntry instance = PXGraph.CreateInstance<APQuickCheckEntry>();
      ((PXSelectBase<APSetup>) instance.apsetup).Current.HoldEntry = new bool?(false);
      APQuickCheck apQuickCheck1 = (APQuickCheck) null;
      APQuickCheck aDest1;
      if (apRegister1 != null)
      {
        if (apInvoice1 != null && !string.IsNullOrEmpty(apInvoice1.RefNbr) || apPayment1 != null && !string.IsNullOrEmpty(apPayment1.RefNbr) || batch != null && !string.IsNullOrEmpty(batch.BatchNbr))
          throw new PXException("Document with these Reference Number and Document type already exists");
        this.Copy(apRegister1, doc);
        GLBatchDocRelease.ClearEmployeeIDField(apRegister1, (PXGraph) instance);
        aDest1 = (APQuickCheck) ((PXSelectBase) instance.Document).Cache.Extend<PX.Objects.AP.APRegister>(apRegister1);
      }
      else
      {
        apQuickCheck1 = new APQuickCheck();
        apQuickCheck1.DocType = doc.TranType;
        ((PXSelectBase<APQuickCheck>) instance.Document).Current = ((PXSelectBase<APQuickCheck>) instance.Document).Insert(apQuickCheck1);
        aDest1 = (APQuickCheck) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<APQuickCheck>) instance.Document).Current);
      }
      PXUIFieldAttribute.SetError(((PXSelectBase) instance.Document).Cache, (object) apQuickCheck1, (string) null, (string) null);
      this.Copy(aDest1, doc);
      aDest1.Hold = new bool?(true);
      ((PXSelectBase<APQuickCheck>) instance.Document).Update(aDest1).TaxCalcMode = "T";
      ((PXSelectBase<APQuickCheck>) instance.Document).Update(aDest1);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      foreach (GLTranDoc a in aList)
      {
        if (!flag || doc != a)
        {
          APTran apTran1 = new APTran();
          APTran apTran2 = ((PXSelectBase<APTran>) instance.Transactions).Insert(apTran1);
          APTran copy = (APTran) ((PXSelectBase) instance.Transactions).Cache.CreateCopy((object) apTran2);
          this.Copy(copy, a);
          APTran aDest2 = ((PXSelectBase<APTran>) instance.Transactions).Update(copy);
          this.Copy(aDest2, a);
          APTran tran = ((PXSelectBase<APTran>) instance.Transactions).Update(aDest2);
          GLBatchDocRelease.SetAccountAndSub(a, tran);
          ((PXSelectBase<APTran>) instance.Transactions).Update(tran);
        }
      }
      aDest1.CuryOrigDocAmt = doc.CuryDocTotal;
      aDest1.CuryOrigDiscAmt = doc.CuryDiscAmt;
      if (doc.DocType == "QCK")
      {
        APQuickCheck apQuickCheck2 = aDest1;
        Decimal? curyOrigDocAmt = apQuickCheck2.CuryOrigDocAmt;
        Decimal? curyOrigDiscAmt = aDest1.CuryOrigDiscAmt;
        apQuickCheck2.CuryOrigDocAmt = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      }
      aDest1.Hold = new bool?(false);
      APQuickCheck apQuickCheck3 = ((PXSelectBase<APQuickCheck>) instance.Document).Update(aDest1);
      TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      this.MergeAPTaxes((PXSelectBase<APTaxTran>) instance.Taxes, doc);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, taxCalc);
      ((PXAction) instance.Save).Press();
      doc.RefNbr = apQuickCheck3.RefNbr;
      doc.DocCreated = new bool?(true);
      ap = (PXGraph) instance;
      list.Add((PX.Objects.AP.APRegister) apQuickCheck3);
    }
    if (ap == null && JournalWithSubEntry.IsAPInvoice(doc))
    {
      APInvoiceEntry instance1 = PXGraph.CreateInstance<APInvoiceEntry>();
      ((PXSelectBase<APSetup>) instance1.APSetup).Current.HoldEntry = new bool?(false);
      PX.Objects.AP.APInvoice apInvoice2 = (PX.Objects.AP.APInvoice) null;
      PX.Objects.AP.APInvoice apInvoice3;
      if (apRegister1 != null)
      {
        if (apInvoice1 != null && !string.IsNullOrEmpty(apInvoice1.RefNbr) || apPayment1 != null && !string.IsNullOrEmpty(apPayment1.RefNbr) || batch != null && !string.IsNullOrEmpty(batch.BatchNbr))
          throw new PXException("Document with these Reference Number and Document type already exists");
        this.Copy(apRegister1, doc);
        GLBatchDocRelease.ClearEmployeeIDField(apRegister1, (PXGraph) instance1);
        apInvoice3 = (PX.Objects.AP.APInvoice) ((PXSelectBase) instance1.Document).Cache.Extend<PX.Objects.AP.APRegister>(apRegister1);
      }
      else
      {
        PX.Objects.AP.APInvoice apInvoice4 = new PX.Objects.AP.APInvoice();
        apInvoice4.DocType = doc.TranType;
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Current = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Insert(apInvoice4);
        apInvoice3 = (PX.Objects.AP.APInvoice) ((PXSelectBase) instance1.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Current);
      }
      this.Copy(apInvoice3, doc);
      if (apInvoice3.DocType == "ACR")
        this.SetCreditAdjustmentRequiredInfo(apInvoice3, doc);
      apInvoice3.Hold = new bool?(true);
      ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Update(apInvoice3).TaxCalcMode = "T";
      apInvoice2 = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Update(apInvoice3);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance1.currencyinfo, doc.CuryInfoID);
      foreach (GLTranDoc a in aList)
      {
        if (!flag || doc != a)
        {
          APTran apTran3 = new APTran();
          APTran apTran4 = ((PXSelectBase<APTran>) instance1.Transactions).Insert(apTran3);
          APTran copy = (APTran) ((PXSelectBase) instance1.Transactions).Cache.CreateCopy((object) apTran4);
          this.Copy(copy, a);
          APTran aDest = ((PXSelectBase<APTran>) instance1.Transactions).Update(copy);
          this.Copy(aDest, a);
          APTran tran = ((PXSelectBase<APTran>) instance1.Transactions).Update(aDest);
          GLBatchDocRelease.SetAccountAndSub(a, tran);
          ((PXSelectBase<APTran>) instance1.Transactions).Update(tran);
        }
      }
      apInvoice3.CuryOrigDocAmt = doc.CuryDocTotal;
      apInvoice3.CuryOrigDiscAmt = doc.CuryDiscAmt;
      if (doc.DocType == "QCK")
      {
        PX.Objects.AP.APInvoice apInvoice5 = apInvoice3;
        Decimal? curyOrigDocAmt = apInvoice5.CuryOrigDocAmt;
        Decimal? curyOrigDiscAmt = apInvoice3.CuryOrigDiscAmt;
        apInvoice5.CuryOrigDocAmt = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      }
      apInvoice3.Hold = new bool?(false);
      PX.Objects.AP.APInvoice apInvoice6 = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Update(apInvoice3);
      TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance1.Transactions).Cache, (object) null);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance1.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      this.MergeAPTaxes((PXSelectBase<APTaxTran>) instance1.Taxes, doc);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(((PXSelectBase) instance1.Transactions).Cache, (object) null, taxCalc);
      JournalWithSubEntry instance2 = PXGraph.CreateInstance<JournalWithSubEntry>();
      foreach (PXResult<PX.Objects.AP.APAdjust, PX.Objects.AP.APRegister> pxResult in PXSelectBase<PX.Objects.AP.APAdjust, PXViewOf<PX.Objects.AP.APAdjust>.BasedOn<SelectFromBase<PX.Objects.AP.APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.APRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.docType, Equal<PX.Objects.AP.APAdjust.adjgDocType>>>>>.And<BqlOperand<PX.Objects.AP.APRegister.refNbr, IBqlString>.IsEqual<PX.Objects.AP.APAdjust.adjgRefNbr>>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.AP.APAdjust.adjdDocType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.Config>.Select((PXGraph) instance2, new object[2]
      {
        (object) apInvoice3.RefNbr,
        (object) apInvoice3.DocType
      }))
      {
        PX.Objects.AP.APAdjust apAdjust = PXResult<PX.Objects.AP.APAdjust, PX.Objects.AP.APRegister>.op_Implicit(pxResult);
        PX.Objects.AP.APRegister apRegister2 = PXResult<PX.Objects.AP.APAdjust, PX.Objects.AP.APRegister>.op_Implicit(pxResult);
        PX.Objects.AP.APInvoice apInvoice7 = apInvoice3;
        if (apRegister2.DocType == "ADR")
        {
          apAdjust.InvoiceID = apInvoice7.NoteID;
          apAdjust.PaymentID = new Guid?();
          apAdjust.MemoID = apRegister2.NoteID;
        }
        else if (apInvoice7.DocType == "ADR")
        {
          apAdjust.InvoiceID = new Guid?();
          apAdjust.PaymentID = apRegister2.NoteID;
          apAdjust.MemoID = apInvoice3.NoteID;
        }
        else
        {
          apAdjust.InvoiceID = apInvoice7.NoteID;
          apAdjust.PaymentID = apRegister2.NoteID;
          apAdjust.MemoID = new Guid?();
        }
        GraphHelper.MarkUpdated(((PXGraph) instance2).Caches["APAdjust"], (object) apAdjust);
      }
      PXGraph.RowPersistingEvents rowPersisting1 = ((PXGraph) instance2).RowPersisting;
      JournalWithSubEntry journalWithSubEntry1 = instance2;
      // ISSUE: virtual method pointer
      PXRowPersisting pxRowPersisting1 = new PXRowPersisting((object) journalWithSubEntry1, __vmethodptr(journalWithSubEntry1, APAdjust_RowPersisting));
      rowPersisting1.RemoveHandler<PX.Objects.AP.APAdjust>(pxRowPersisting1);
      ((PXAction) instance2.Save).Press();
      PXGraph.RowPersistingEvents rowPersisting2 = ((PXGraph) instance2).RowPersisting;
      JournalWithSubEntry journalWithSubEntry2 = instance2;
      // ISSUE: virtual method pointer
      PXRowPersisting pxRowPersisting2 = new PXRowPersisting((object) journalWithSubEntry2, __vmethodptr(journalWithSubEntry2, APAdjust_RowPersisting));
      rowPersisting2.AddHandler<PX.Objects.AP.APAdjust>(pxRowPersisting2);
      ((PXAction) instance1.Save).Press();
      doc.RefNbr = apInvoice6.RefNbr;
      doc.DocCreated = new bool?(true);
      ap = (PXGraph) instance1;
      list.Add((PX.Objects.AP.APRegister) apInvoice6);
    }
    if (ap == null && JournalWithSubEntry.IsAPPayment(doc))
    {
      Decimal? curyUnappliedBal = doc.CuryUnappliedBal;
      Decimal num = 0M;
      if (!(curyUnappliedBal.GetValueOrDefault() == num & curyUnappliedBal.HasValue) && doc.TranType != "PPM")
        throw new PXException("You have to apply full amount of this document before it may be released");
      APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
      PX.Objects.AP.APPayment row = (PX.Objects.AP.APPayment) null;
      PX.Objects.AP.APPayment copy;
      if (apRegister1 != null)
      {
        if (apInvoice1 != null && !string.IsNullOrEmpty(apInvoice1.RefNbr) || apPayment1 != null && !string.IsNullOrEmpty(apPayment1.RefNbr) || batch != null && !string.IsNullOrEmpty(batch.BatchNbr))
          throw new PXException("Document with these Reference Number and Document type already exists");
        this.Copy(apRegister1, doc);
        GLBatchDocRelease.ClearEmployeeIDField(apRegister1, (PXGraph) instance);
        copy = (PX.Objects.AP.APPayment) ((PXSelectBase) instance.Document).Cache.CreateCopy(((PXSelectBase) instance.Document).Cache.Extend<PX.Objects.AP.APRegister>(apRegister1));
      }
      else
      {
        row = new PX.Objects.AP.APPayment();
        row.DocType = doc.TranType;
        ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current = ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Insert(row);
        copy = (PX.Objects.AP.APPayment) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current);
      }
      PXUIFieldAttribute.SetError(((PXSelectBase) instance.Document).Cache, (object) row, (string) null, (string) null);
      this.Copy(copy, ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current, doc);
      ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current.Hold = new bool?(true);
      using (new APPaymentEntry.SuppressCuryAdjustingScope())
        row = ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Update(copy);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      GLBatchDocRelease.SetFinPeriodID(doc, row);
      instance.RecalcApplAmounts(((PXSelectBase) instance.Document).Cache, row, false);
      PX.Objects.AP.APPayment apPayment2 = ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Update(row);
      apPayment2.IsReleaseCheckProcess = new bool?(true);
      ((PXAction) instance.Save).Press();
      doc.RefNbr = apPayment2.RefNbr;
      doc.DocCreated = new bool?(true);
      ap = (PXGraph) instance;
      list.Add((PX.Objects.AP.APRegister) apPayment2);
    }
    if (list.Count > 0)
    {
      try
      {
        APDocumentRelease.ReleaseDoc(list, false, false, toPost);
        doc.Released = new bool?(true);
      }
      catch (PX.Objects.AP.PXMassProcessException ex)
      {
        if (((Exception) ex).InnerException is ReleaseException innerException && innerException.FailedWith == FailedWith.DocumentNotApproved)
          throw new ReleaseException("The document cannot be created on this form because an approval map is active on the Accounts Payable Preferences (AP101000) form.", Array.Empty<object>());
        throw;
      }
    }
    return ap;
  }

  /// <summary>Workaround for AC-162943</summary>
  /// <param name="apRegister"></param>
  /// <param name="qcGraph"></param>
  private static void ClearEmployeeIDField(PX.Objects.AP.APRegister apRegister, PXGraph qcGraph)
  {
    if (!apRegister.EmployeeID.HasValue)
      return;
    PXResultset<CREmployee> pxResultset = PXSelectBase<CREmployee, PXSelectJoin<CREmployee, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<CREmployee.defContactID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<PX.Objects.AP.APRegister.employeeID>>>>.Config>.Select(qcGraph, new object[1]
    {
      (object) apRegister.EmployeeID
    });
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.AP.APRegister.vendorID>>>>.Config>.Select(qcGraph, new object[1]
    {
      (object) apRegister.VendorID
    }));
    bool flag = true;
    foreach (PXResult<CREmployee, EPCompanyTreeMember> pxResult in pxResultset)
    {
      int? workGroupId = (int?) PXResult<CREmployee, EPCompanyTreeMember>.op_Implicit(pxResult)?.WorkGroupID;
      int? workgroupId = (int?) baccount?.WorkgroupID;
      if (workGroupId.GetValueOrDefault() == workgroupId.GetValueOrDefault() & workGroupId.HasValue == workgroupId.HasValue)
        flag = false;
    }
    if (!flag)
      return;
    apRegister.EmployeeID = new int?();
  }

  private static void SetAccountAndSub(GLTranDoc iRow, APTran tran)
  {
    bool flag = APInvoiceType.DrCr(iRow.TranType) == "D";
    tran.AccountID = flag ? iRow.DebitAccountID : iRow.CreditAccountID;
    tran.SubID = flag ? iRow.DebitSubID : iRow.CreditSubID;
  }

  private PX.Objects.AR.ARRegister ObtainCorrespondingARRegisterEntry(GLTranDoc doc)
  {
    if (string.IsNullOrEmpty(doc.RefNbr))
      return (PX.Objects.AR.ARRegister) null;
    PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch> pxResult = ((IQueryable<PXResult<PX.Objects.AR.ARRegister>>) PXSelectBase<PX.Objects.AR.ARRegister, PXSelectJoin<PX.Objects.AR.ARRegister, LeftJoin<PX.Objects.AR.Standalone.ARInvoice, On<PX.Objects.AR.Standalone.ARInvoice.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>, And<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<PX.Objects.AR.ARRegister.docType>>>, LeftJoin<PX.Objects.AR.Standalone.ARPayment, On<PX.Objects.AR.Standalone.ARPayment.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>, And<PX.Objects.AR.Standalone.ARPayment.docType, Equal<PX.Objects.AR.ARRegister.docType>>>, LeftJoin<Batch, On<Batch.origModule, Equal<BatchModule.moduleAR>, And<Batch.batchNbr, Equal<PX.Objects.AR.ARRegister.batchNbr>>>>>>, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) doc.TranType,
      (object) doc.RefNbr
    })).FirstOrDefault<PXResult<PX.Objects.AR.ARRegister>>() as PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch>;
    PX.Objects.AR.ARRegister correspondingArRegisterEntry = PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch>.op_Implicit(pxResult);
    if (correspondingArRegisterEntry == null)
      return correspondingArRegisterEntry;
    GLBatchDocRelease.AssertNotAlreadyExists(PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARRegister, PX.Objects.AR.Standalone.ARInvoice, PX.Objects.AR.Standalone.ARPayment, Batch>.op_Implicit(pxResult));
    return correspondingArRegisterEntry;
  }

  private static void AssertNotAlreadyExists(PX.Objects.AR.Standalone.ARInvoice arInvoice, PX.Objects.AR.Standalone.ARPayment arPayment, Batch batch)
  {
    if (!string.IsNullOrEmpty(arInvoice?.RefNbr) || !string.IsNullOrEmpty(arPayment?.RefNbr) || !string.IsNullOrEmpty(batch?.BatchNbr))
      throw new PXException("Document with these Reference Number and Document type already exists");
  }

  protected virtual PXGraph CreateAR(GLTranDoc doc, List<GLTranDoc> aList, List<Batch> toPost)
  {
    PXGraph ar = (PXGraph) null;
    bool flag = aList.Count > 1;
    PX.Objects.AR.ARRegister correspondingArRegisterEntry = this.ObtainCorrespondingARRegisterEntry(doc);
    List<PX.Objects.AR.ARRegister> list1 = new List<PX.Objects.AR.ARRegister>(1);
    if (JournalWithSubEntry.IsARInvoice(doc) && JournalWithSubEntry.IsARPayment(doc))
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ((PXSelectBase<ARSetup>) instance.arsetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase<ARSetup>) instance.arsetup).Current.CreditCheckError = new bool?(false);
      PX.Objects.AR.PaymentRefAttribute.SetAllowAskUpdateLastRefNbr<ARCashSale.extRefNbr>(((PXSelectBase) instance.Document).Cache, false);
      ARCashSale arCashSale1 = (ARCashSale) null;
      ARCashSale aDest;
      if (correspondingArRegisterEntry != null)
      {
        this.Copy(correspondingArRegisterEntry, doc);
        aDest = (ARCashSale) ((PXSelectBase) instance.Document).Cache.Extend<PX.Objects.AR.ARRegister>(correspondingArRegisterEntry);
      }
      else
      {
        ARCashSale arCashSale2 = new ARCashSale();
        arCashSale2.DocType = doc.TranType;
        arCashSale1 = arCashSale2;
        ((PXSelectBase<ARCashSale>) instance.Document).Current = ((PXSelectBase<ARCashSale>) instance.Document).Insert(arCashSale1);
        aDest = (ARCashSale) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<ARCashSale>) instance.Document).Current);
      }
      PXUIFieldAttribute.SetError(((PXSelectBase) instance.Document).Cache, (object) arCashSale1, (string) null, (string) null);
      this.Copy(aDest, doc);
      aDest.Hold = new bool?(true);
      if (this._suppressARPrintVerification && ((PXSelectBase<ARSetup>) instance.arsetup).Current.PrintBeforeRelease.GetValueOrDefault())
        aDest.DontPrint = new bool?(true);
      if (this._suppressARPrintVerification && ((PXSelectBase<ARSetup>) instance.arsetup).Current.EmailBeforeRelease.GetValueOrDefault())
        aDest.DontEmail = new bool?(true);
      ((PXSelectBase<ARCashSale>) instance.Document).Update(aDest);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      foreach (GLTranDoc a in aList)
      {
        if (!flag || doc != a)
        {
          ARTran arTran1 = new ARTran();
          ARTran arTran2 = ((PXSelectBase<ARTran>) instance.Transactions).Insert(arTran1);
          ARTran copy = (ARTran) ((PXSelectBase) instance.Transactions).Cache.CreateCopy((object) arTran2);
          this.Copy(copy, a);
          ((PXSelectBase<ARTran>) instance.Transactions).Update(copy);
        }
      }
      aDest.CuryOrigDocAmt = doc.CuryDocTotal;
      aDest.CuryOrigDiscAmt = doc.CuryDiscAmt;
      if (doc.DocType == "CSL")
      {
        ARCashSale arCashSale3 = aDest;
        Decimal? curyOrigDocAmt = arCashSale3.CuryOrigDocAmt;
        Decimal? curyOrigDiscAmt = aDest.CuryOrigDiscAmt;
        arCashSale3.CuryOrigDocAmt = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      }
      aDest.Hold = new bool?(false);
      ARCashSale arCashSale4 = ((PXSelectBase<ARCashSale>) instance.Document).Update(aDest);
      TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null);
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      this.MergeARTaxes((PXSelectBase<ARTaxTran>) instance.Taxes, doc);
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, taxCalc);
      ((PXAction) instance.Save).Press();
      doc.RefNbr = arCashSale4.RefNbr;
      doc.DocCreated = new bool?(true);
      list1.Add((PX.Objects.AR.ARRegister) arCashSale4);
      ar = (PXGraph) instance;
    }
    if (ar == null && JournalWithSubEntry.IsARInvoice(doc))
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARSetup>) instance.ARSetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase<ARSetup>) instance.ARSetup).Current.CreditCheckError = new bool?(false);
      PX.Objects.AR.ARInvoice arInvoice1 = (PX.Objects.AR.ARInvoice) null;
      PX.Objects.AR.ARInvoice aDest;
      if (correspondingArRegisterEntry != null)
      {
        this.Copy(correspondingArRegisterEntry, doc);
        aDest = (PX.Objects.AR.ARInvoice) ((PXSelectBase) instance.Document).Cache.Extend<PX.Objects.AR.ARRegister>(correspondingArRegisterEntry);
      }
      else
      {
        PX.Objects.AR.ARInvoice arInvoice2 = new PX.Objects.AR.ARInvoice();
        arInvoice2.DocType = doc.TranType;
        PX.Objects.AR.ARInvoice arInvoice3 = arInvoice2;
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Insert(arInvoice3);
        aDest = (PX.Objects.AR.ARInvoice) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current);
      }
      this.Copy(aDest, doc);
      if (this._suppressARPrintVerification && ((PXSelectBase<ARSetup>) instance.ARSetup).Current.PrintBeforeRelease.GetValueOrDefault())
        aDest.DontPrint = new bool?(true);
      if (this._suppressARPrintVerification && ((PXSelectBase<ARSetup>) instance.ARSetup).Current.EmailBeforeRelease.GetValueOrDefault())
        aDest.DontEmail = new bool?(true);
      if (JournalWithSubEntry.IsARCreditMemo(doc) && ((PXSelectBase<ARSetup>) instance.ARSetup).Current.TermsInCreditMemos.GetValueOrDefault())
      {
        aDest.TermsID = doc.TermsID;
        aDest.DiscDate = doc.DiscDate;
        aDest.DueDate = doc.DueDate;
      }
      arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Update(aDest);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      foreach (GLTranDoc a in aList)
      {
        if (!flag || doc != a)
        {
          ARTran arTran3 = new ARTran();
          ARTran arTran4 = ((PXSelectBase<ARTran>) instance.Transactions).Insert(arTran3);
          ARTran copy = (ARTran) ((PXSelectBase) instance.Transactions).Cache.CreateCopy((object) arTran4);
          this.Copy(copy, a);
          ((PXSelectBase<ARTran>) instance.Transactions).Update(copy);
        }
      }
      aDest.CuryOrigDocAmt = doc.CuryDocTotal;
      if (!JournalWithSubEntry.IsARCreditMemo(doc) || JournalWithSubEntry.IsARCreditMemo(doc) && ((PXSelectBase<ARSetup>) instance.ARSetup).Current.TermsInCreditMemos.GetValueOrDefault())
        aDest.CuryOrigDiscAmt = doc.CuryDiscAmt;
      aDest.Hold = new bool?(false);
      PX.Objects.AR.ARInvoice arInvoice4 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Update(aDest);
      TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null);
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      this.MergeARTaxes((PXSelectBase<ARTaxTran>) instance.Taxes, doc);
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) instance.Transactions).Cache, (object) null, taxCalc);
      ((PXAction) instance.Save).Press();
      doc.RefNbr = arInvoice4.RefNbr;
      doc.DocCreated = new bool?(true);
      list1.Add((PX.Objects.AR.ARRegister) arInvoice4);
      ar = (PXGraph) instance;
    }
    if (ar == null && JournalWithSubEntry.IsARPayment(doc))
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARSetup>) instance.arsetup).Current.HoldEntry = new bool?(false);
      PX.Objects.AR.ARPayment arPayment1 = (PX.Objects.AR.ARPayment) null;
      PX.Objects.AR.ARPayment copy;
      if (correspondingArRegisterEntry != null)
      {
        this.Copy(correspondingArRegisterEntry, doc);
        copy = (PX.Objects.AR.ARPayment) ((PXSelectBase) instance.Document).Cache.CreateCopy(((PXSelectBase) instance.Document).Cache.Extend<PX.Objects.AR.ARRegister>(correspondingArRegisterEntry));
      }
      else
      {
        PX.Objects.AR.ARPayment arPayment2 = new PX.Objects.AR.ARPayment();
        arPayment2.DocType = doc.TranType;
        arPayment1 = arPayment2;
        ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment1);
        copy = (PX.Objects.AR.ARPayment) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current);
      }
      PXUIFieldAttribute.SetError(((PXSelectBase) instance.Document).Cache, (object) arPayment1, (string) null, (string) null);
      this.Copy(copy, ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current, doc);
      ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current.Hold = new bool?(true);
      PX.Objects.AR.ARPayment row = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(copy);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      GLBatchDocRelease.SetFinPeriodID(doc, row);
      instance.RecalcApplAmounts(((PXSelectBase) instance.Document).Cache, row);
      PX.Objects.AR.ARPayment arPayment3 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(row);
      List<PX.Objects.AR.ARInvoice> list2 = ((PXSelectBase) instance.ARInvoice_DocType_RefNbr).Cache.Updated.Cast<PX.Objects.AR.ARInvoice>().ToList<PX.Objects.AR.ARInvoice>();
      ((PXAction) instance.Save).Press();
      foreach (PX.Objects.AR.ARInvoice arInvoice in list2)
        PXTimeStampScope.DuplicatePersisted((PXCache) GraphHelper.Caches<PX.Objects.AR.ARRegister>((PXGraph) this), (object) arInvoice, typeof (PX.Objects.AR.ARInvoice));
      doc.RefNbr = arPayment3.RefNbr;
      doc.DocCreated = new bool?(true);
      list1.Add((PX.Objects.AR.ARRegister) arPayment3);
      ar = (PXGraph) instance;
    }
    if (list1.Count > 0)
    {
      try
      {
        ARDocumentRelease.ReleaseDoc(list1, false, toPost, (ARDocumentRelease.ARMassProcessDelegate) null);
        doc.Released = new bool?(true);
      }
      catch (PX.Objects.AR.PXMassProcessException ex)
      {
        if (((Exception) ex).InnerException is ReleaseException innerException && innerException.FailedWith == FailedWith.DocumentNotApproved)
          throw new ReleaseException("The document cannot be created on this form because an approval map is active on the Accounts Receivable Preferences (AR101000) form.", Array.Empty<object>());
        throw;
      }
    }
    return ar;
  }

  protected virtual PXGraph CreateCA(GLTranDoc doc, List<GLTranDoc> aList, List<Batch> toPost)
  {
    PXGraph ca = (PXGraph) null;
    bool flag = aList.Count > 1;
    List<CAAdj> caAdjList = new List<CAAdj>(1);
    if (doc.TranModule == "CA" && doc.TranType == "CAE")
    {
      CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
      CATranEntryMultiCurrency extension = ((PXGraph) instance).GetExtension<CATranEntryMultiCurrency>();
      ((PXSelectBase<CASetup>) instance.casetup).Current.HoldEntry = new bool?(true);
      CAAdj row;
      if (!string.IsNullOrEmpty(doc.RefNbr))
      {
        PXResultset<CAAdj> pxResultset = PXSelectBase<CAAdj, PXSelectJoin<CAAdj, LeftJoin<CASplit, On<CASplit.adjRefNbr, Equal<CAAdj.adjRefNbr>, And<CASplit.adjTranType, Equal<CAAdj.adjTranType>>>>, Where<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>>.Config>.SelectWindowed((PXGraph) instance, 0, 1, new object[2]
        {
          (object) doc.TranType,
          (object) doc.RefNbr
        });
        if (pxResultset.Count > 0)
        {
          row = PXResult<CAAdj>.op_Implicit(pxResultset[0]);
          CASplit caSplit = PXResult<CAAdj, CASplit>.op_Implicit((PXResult<CAAdj, CASplit>) pxResultset[0]);
          if (caSplit != null && !string.IsNullOrEmpty(caSplit.AdjRefNbr))
            throw new PXException("Document with these Reference Number and Document type already exists");
          ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = row;
        }
      }
      else
      {
        row = new CAAdj();
        row.AdjTranType = "CAE";
        ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Insert(row);
      }
      CAAdj copy1 = (CAAdj) ((PXSelectBase) instance.CAAdjRecords).Cache.CreateCopy((object) ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current);
      this.Copy(copy1, ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current, doc);
      copy1.Draft = new bool?(false);
      using (new SuppressThowFinPeriodExceptionForVoucherCAEntryScope())
        row = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(copy1);
      row.TaxCalcMode = "T";
      GLBatchDocRelease.SetFinPeriodID(doc, row);
      CAAdj caAdj1 = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(row);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo, doc.CuryInfoID);
      foreach (GLTranDoc a in aList)
      {
        if (!flag || doc != a)
        {
          CASplit caSplit = ((PXSelectBase<CASplit>) instance.CASplitRecords).Insert(new CASplit()
          {
            AdjTranType = caAdj1.AdjTranType
          });
          CASplit copy2 = (CASplit) ((PXSelectBase) instance.CASplitRecords).Cache.CreateCopy((object) caSplit);
          this.Copy(copy2, a);
          CASplit tran = ((PXSelectBase<CASplit>) instance.CASplitRecords).Update(copy2);
          this.OptionallyUpdateProjectFields(instance, a, tran, copy2);
        }
      }
      TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) instance.CASplitRecords).Cache, (object) null);
      TaxBaseAttribute.SetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) instance.CASplitRecords).Cache, (object) null, TaxCalc.ManualCalc);
      this.MergeCATaxes((PXSelectBase<CATaxTran>) instance.Taxes, doc);
      TaxBaseAttribute.SetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) instance.CASplitRecords).Cache, (object) null, taxCalc);
      caAdj1.CuryTaxAmt = caAdj1.CuryTaxTotal;
      caAdj1.Hold = new bool?(false);
      foreach (CAAdj caAdj2 in ((PXAction) instance.releaseFromHold).Press(new PXAdapter((PXSelectBase) instance.CurrentDocument, new object[1]
      {
        (object) caAdj1
      })))
        ;
      ((PXAction) instance.Save).Press();
      doc.RefNbr = caAdj1.RefNbr;
      doc.DocCreated = new bool?(true);
      ca = (PXGraph) instance;
      caAdjList.Add(caAdj1);
    }
    if (caAdjList.Count > 0)
    {
      try
      {
        int num1 = 0;
        foreach (CAAdj _doc in caAdjList)
        {
          int num2 = num1;
          List<Batch> externalPostList = toPost;
          SelectedEntityEvent<CAAdj.Events> releaseEvent = PXEntityEventBase<CAAdj>.Container<CAAdj.Events>.Select((Expression<Func<CAAdj.Events, PXEntityEvent<CAAdj.Events>>>) (ev => ev.ReleaseDocument));
          CATrxRelease.ReleaseDoc<CAAdj>(_doc, num2, externalPostList, (SelectedEntityEvent<CAAdj>) releaseEvent);
          ++num1;
        }
        doc.Released = new bool?(true);
      }
      catch (Exception ex)
      {
        if (ex is ReleaseException releaseException && releaseException.FailedWith == FailedWith.DocumentNotApproved)
          throw new ReleaseException("The document cannot be created on this form because an approval map is active on the Cash Management Preferences (CA101000) form.", Array.Empty<object>());
        throw;
      }
    }
    return ca;
  }

  private void OptionallyUpdateProjectFields(
    CATranEntry caGraph,
    GLTranDoc iRow,
    CASplit tran,
    CASplit tranCopy)
  {
    int? projectId1 = tran.ProjectID;
    int? projectId2 = tranCopy.ProjectID;
    if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
      return;
    int? projectId3 = tranCopy.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (!(projectId3.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId3.HasValue == nullable.HasValue))
      return;
    this.CopyProjectFields(tran, iRow);
    ((PXSelectBase<CASplit>) caGraph.CASplitRecords).Update(tranCopy);
  }

  protected virtual PXGraph CreateGL(GLTranDoc doc, List<GLTranDoc> aList, List<Batch> toPost)
  {
    PXGraph gl = (PXGraph) null;
    int count = aList.Count;
    List<Batch> list = new List<Batch>(1);
    if (doc.TranModule == "GL")
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<GLSetup>) instance.glsetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase<GLSetup>) instance.glsetup).Current.RequireControlTotal = new bool?(false);
      if (!string.IsNullOrEmpty(doc.RefNbr))
      {
        PXResultset<Batch> pxResultset = PXSelectBase<Batch, PXSelectJoin<Batch, LeftJoin<GLTran, On<GLTran.batchNbr, Equal<Batch.batchNbr>, And<GLTran.module, Equal<Batch.module>>>>, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.SelectWindowed((PXGraph) instance, 0, 1, new object[2]
        {
          (object) doc.TranModule,
          (object) doc.RefNbr
        });
        if (pxResultset.Count > 0)
        {
          Batch batch = PXResultset<Batch>.op_Implicit(pxResultset);
          GLTran glTran = PXResult<Batch, GLTran>.op_Implicit((PXResult<Batch, GLTran>) PXResultset<Batch>.op_Implicit(pxResultset));
          if (glTran != null && !string.IsNullOrEmpty(glTran.BatchNbr))
            throw new PXException("Document with these Reference Number and Document type already exists");
          ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
        }
      }
      else
        ((PXSelectBase<Batch>) instance.BatchModule).Current = ((PXSelectBase<Batch>) instance.BatchModule).Insert(new Batch()
        {
          Module = doc.TranModule
        });
      Batch copy = (Batch) ((PXSelectBase) instance.BatchModule).Cache.CreateCopy((object) ((PXSelectBase<Batch>) instance.BatchModule).Current);
      this.Copy(copy, doc);
      copy.Draft = new bool?(false);
      copy.Hold = new bool?(false);
      Batch batch1 = ((PXSelectBase<Batch>) instance.BatchModule).Update(copy);
      this.CloneCuryInfo((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance.currencyinfo, doc.CuryInfoID);
      bool[] flagArray = new bool[2]{ true, false };
      foreach (GLTranDoc a in aList)
      {
        int num = a.IsBalanced ? 2 : 1;
        for (int index = 0; index < num; ++index)
        {
          GLTran aDest = new GLTran();
          aDest.Module = batch1.Module;
          this.Copy(aDest, a, flagArray[index]);
          aDest.NoteID = new Guid?();
          ((PXSelectBase<GLTran>) instance.GLTranModuleBatNbr).Insert(aDest);
        }
      }
      batch1.Hold = new bool?(false);
      ((PXAction) instance.Save).Press();
      doc.RefNbr = batch1.BatchNbr;
      doc.DocCreated = new bool?(true);
      gl = (PXGraph) instance;
      list.Add(batch1);
    }
    if (list.Count > 0)
    {
      JournalEntry.ReleaseBatch((IList<Batch>) list, (IList<Batch>) toPost);
      doc.Released = new bool?(true);
    }
    return gl;
  }

  protected virtual void Copy(APQuickCheck aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.CuryTaxAmt = aSrc.CuryTaxAmt;
    aDest.InvoiceNbr = aSrc.ExtRefNbr;
    aDest.InvoiceDate = aSrc.TranDate;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.AdjDate = aSrc.TranDate;
    aDest.AdjFinPeriodID = aSrc.FinPeriodID;
    aDest.AdjTranPeriodID = aSrc.TranPeriodID;
    bool flag = APPaymentType.DrCr(aSrc.TranType) == "C";
    aDest.CashAccountID = flag ? aSrc.CreditCashAccountID : aSrc.DebitCashAccountID;
    aDest.VendorID = aSrc.BAccountID;
    aDest.ExtRefNbr = aSrc.ExtRefNbr;
    aDest.VendorLocationID = aSrc.LocationID;
    aDest.PaymentMethodID = aSrc.PaymentMethodID;
    aDest.TaxZoneID = aSrc.TaxZoneID;
    aDest.TermsID = aSrc.TermsID;
    aDest.APAccountID = aDest.APAccountID ?? (flag ? aSrc.CreditAccountID : aSrc.DebitAccountID);
    aDest.APSubID = aDest.APSubID ?? (flag ? aSrc.CreditSubID : aSrc.DebitSubID);
    aDest.BranchID = aSrc.BranchID;
  }

  protected virtual void Copy(PX.Objects.AP.APInvoice aDest, GLTranDoc aSrc)
  {
    GLBatchDocRelease.DocCopyHelper.Copy(aDest, aSrc);
  }

  protected virtual void Copy(PX.Objects.AP.APPayment aDest, PX.Objects.AP.APPayment aOrig, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.ExtRefNbr = aSrc.ExtRefNbr;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.AdjDate = aSrc.TranDate;
    aDest.AdjFinPeriodID = aSrc.FinPeriodID;
    aDest.AdjTranPeriodID = aSrc.TranPeriodID;
    aDest.CuryOrigDocAmt = aSrc.CuryTranAmt;
    aDest.VendorID = aSrc.BAccountID;
    aDest.VendorLocationID = aSrc.LocationID;
    aDest.PaymentMethodID = aSrc.PaymentMethodID;
    bool flag = APPaymentType.DrCr(aSrc.TranType) == "C";
    aDest.CashAccountID = flag ? aSrc.CreditCashAccountID : aSrc.DebitCashAccountID;
    aDest.APAccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
    aDest.APSubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
    aDest.BranchID = aSrc.BranchID;
    aDest.Hold = new bool?(false);
    aOrig.NoteID = new Guid?();
    aOrig.DocDesc = (string) null;
    aOrig.DocDate = new DateTime?();
    aOrig.CuryID = (string) null;
    aOrig.ExtRefNbr = (string) null;
    aOrig.FinPeriodID = (string) null;
    aOrig.TranPeriodID = (string) null;
    aOrig.AdjDate = new DateTime?();
    aOrig.AdjFinPeriodID = (string) null;
    aOrig.AdjTranPeriodID = (string) null;
    aOrig.CuryOrigDocAmt = new Decimal?();
    aOrig.CashAccountID = new int?();
    aOrig.VendorID = new int?();
    aOrig.VendorLocationID = new int?();
    aOrig.PaymentMethodID = (string) null;
    aOrig.APAccountID = new int?();
    aOrig.APSubID = new int?();
    aOrig.BranchID = new int?();
    aOrig.Hold = new bool?();
  }

  protected virtual void Copy(PX.Objects.AP.APRegister aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.CuryOrigDocAmt = aSrc.CuryTranAmt;
    aDest.VendorID = aSrc.BAccountID;
    aDest.VendorLocationID = aSrc.LocationID;
    aDest.APAccountID = new int?();
    aDest.APSubID = new int?();
    aDest.BranchID = aSrc.BranchID;
  }

  protected virtual void Copy(APTran aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    bool flag = APInvoiceType.DrCr(aSrc.TranType) == "D";
    aDest.AccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
    aDest.SubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
    aDest.CuryTranAmt = aSrc.CuryTranAmt;
    aDest.Qty = new Decimal?((Decimal) 1);
    aDest.CuryUnitCost = aSrc.CuryTranAmt;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.TaxCategoryID = aSrc.TaxCategoryID;
    aDest.BranchID = aSrc.BranchID;
    aDest.ProjectID = aSrc.ProjectID;
    aDest.TaskID = aSrc.TaskID;
    aDest.CostCodeID = aSrc.CostCodeID;
    aDest.ManualPrice = new bool?(true);
  }

  protected virtual void Copy(ARCashSale aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.InvoiceNbr = aSrc.ExtRefNbr;
    aDest.InvoiceDate = aSrc.TranDate;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.AdjDate = aSrc.TranDate;
    aDest.AdjFinPeriodID = aSrc.FinPeriodID;
    aDest.AdjTranPeriodID = aSrc.TranPeriodID;
    bool flag = ARPaymentType.DrCr(aSrc.TranType) == "D";
    aDest.CashAccountID = flag ? aSrc.DebitCashAccountID : aSrc.CreditCashAccountID;
    aDest.CustomerID = aSrc.BAccountID;
    aDest.ExtRefNbr = aSrc.ExtRefNbr;
    aDest.CustomerLocationID = aSrc.LocationID;
    aDest.PaymentMethodID = aSrc.PaymentMethodID;
    aDest.PMInstanceID = aSrc.PMInstanceID;
    aDest.TaxZoneID = aSrc.TaxZoneID;
    aDest.TermsID = aSrc.TermsID;
    aDest.BranchID = aSrc.BranchID;
    aDest.ProjectID = aSrc.ProjectID;
  }

  protected virtual void Copy(PX.Objects.AR.ARInvoice aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.InvoiceDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.InvoiceNbr = aSrc.ExtRefNbr;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.CustomerID = aSrc.BAccountID;
    aDest.CustomerLocationID = aSrc.LocationID;
    aDest.TaxZoneID = aSrc.TaxZoneID;
    if (!JournalWithSubEntry.IsARCreditMemo(aSrc))
    {
      aDest.TermsID = aSrc.TermsID;
      aDest.DueDate = aSrc.DueDate;
      aDest.DiscDate = aSrc.DiscDate;
    }
    bool flag = ARInvoiceType.DrCr(aSrc.TranType) == "C";
    aDest.ARAccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
    aDest.ARSubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
    aDest.BranchID = aSrc.BranchID;
    aDest.ProjectID = aSrc.ProjectID;
    aDest.PaymentMethodID = aSrc.PaymentMethodID;
    aDest.PMInstanceID = aSrc.PMInstanceID;
    aDest.Hold = new bool?(true);
  }

  protected virtual void Copy(PX.Objects.AR.ARPayment aDest, PX.Objects.AR.ARPayment aOrig, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.ExtRefNbr = aSrc.ExtRefNbr;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.AdjDate = aSrc.TranDate;
    aDest.AdjFinPeriodID = aSrc.TranPeriodID;
    aDest.AdjTranPeriodID = aSrc.TranPeriodID;
    aDest.CuryOrigDocAmt = aSrc.CuryTranAmt;
    aDest.CustomerID = aSrc.BAccountID;
    aDest.CustomerLocationID = aSrc.LocationID;
    aDest.PaymentMethodID = aSrc.PaymentMethodID;
    aDest.PMInstanceID = aSrc.PMInstanceID;
    bool flag = ARPaymentType.DrCr(aSrc.TranType) == "D";
    aDest.CashAccountID = flag ? aSrc.DebitCashAccountID : aSrc.CreditCashAccountID;
    aDest.ARAccountID = flag ? aSrc.CreditAccountID : aSrc.DebitAccountID;
    aDest.ARSubID = flag ? aSrc.CreditSubID : aSrc.DebitSubID;
    aDest.BranchID = aSrc.BranchID;
    aDest.ProjectID = aSrc.ProjectID;
    aDest.TaskID = aSrc.TaskID;
    aDest.Hold = new bool?(false);
    aOrig.NoteID = new Guid?();
    aOrig.DocDesc = (string) null;
    aOrig.DocDate = new DateTime?();
    aOrig.CuryID = (string) null;
    aOrig.ExtRefNbr = (string) null;
    aOrig.FinPeriodID = (string) null;
    aOrig.TranPeriodID = (string) null;
    aOrig.AdjDate = new DateTime?();
    aOrig.AdjFinPeriodID = (string) null;
    aOrig.AdjTranPeriodID = (string) null;
    aOrig.CashAccountID = new int?();
    aOrig.CustomerID = new int?();
    aOrig.CustomerLocationID = new int?();
    aOrig.PaymentMethodID = (string) null;
    aOrig.PMInstanceID = new int?();
    aOrig.ARAccountID = new int?();
    aOrig.ARSubID = new int?();
    aOrig.BranchID = new int?();
    aOrig.Hold = new bool?();
  }

  protected virtual void Copy(ARTran aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    bool flag = aSrc.TranType == "INV" || aSrc.TranType == "CSL" || aSrc.TranType == "DRM";
    aDest.AccountID = flag ? aSrc.CreditAccountID : aSrc.DebitAccountID;
    aDest.SubID = flag ? aSrc.CreditSubID : aSrc.DebitSubID;
    aDest.CuryExtPrice = aSrc.CuryTranAmt;
    aDest.Qty = new Decimal?((Decimal) 1);
    aDest.CuryUnitPrice = aSrc.CuryTranAmt;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.TaxCategoryID = aSrc.TaxCategoryID;
    aDest.BranchID = aSrc.BranchID;
    aDest.TaskID = aSrc.TaskID;
    aDest.CostCodeID = aSrc.CostCodeID;
    aDest.ManualPrice = new bool?(true);
  }

  protected virtual void Copy(PX.Objects.AR.ARRegister aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.DocDesc = aSrc.TranDesc;
    aDest.DocDate = aSrc.TranDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.CuryOrigDocAmt = aSrc.CuryTranAmt;
    aDest.CustomerID = aSrc.BAccountID;
    aDest.CustomerLocationID = aSrc.LocationID;
    aDest.ARAccountID = new int?();
    aDest.ARSubID = new int?();
    aDest.BranchID = aSrc.BranchID;
  }

  protected virtual void Copy(CAAdj aDest, CAAdj aOrig, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.CashAccountID = aSrc.CashAccountID;
    aDest.CuryID = aSrc.CuryID;
    aDest.DrCr = aSrc.CADrCr;
    aDest.ExtRefNbr = aSrc.ExtRefNbr;
    aDest.Released = new bool?(false);
    aDest.Cleared = new bool?(false);
    aDest.TranDate = aSrc.TranDate;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.EntryTypeID = aSrc.EntryTypeID;
    aDest.CuryControlAmt = aSrc.CuryDocTotal;
    aDest.TaxZoneID = aSrc.TaxZoneID;
    aDest.BranchID = aSrc.BranchID;
    aOrig.NoteID = new Guid?();
    aOrig.CashAccountID = new int?();
    aOrig.CuryID = (string) null;
    aOrig.DrCr = (string) null;
    aOrig.ExtRefNbr = (string) null;
    aOrig.TranDate = new DateTime?();
    aOrig.TranDesc = (string) null;
    aOrig.FinPeriodID = (string) null;
    aOrig.TranPeriodID = (string) null;
    aOrig.EntryTypeID = (string) null;
    aOrig.CuryControlAmt = new Decimal?();
    aOrig.TaxZoneID = (string) null;
    aOrig.BranchID = new int?();
  }

  protected virtual void Copy(CASplit aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    bool flag = aSrc.CADrCr == "C";
    aDest.AccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
    aDest.SubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
    aDest.CuryTranAmt = aSrc.CuryTranAmt;
    aDest.Qty = new Decimal?(1M);
    aDest.CuryUnitPrice = aSrc.CuryTranAmt;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.TaxCategoryID = aSrc.TaxCategoryID;
    aDest.BranchID = aSrc.BranchID;
    this.CopyProjectFields(aDest, aSrc);
  }

  protected virtual void CopyProjectFields(CASplit aDest, GLTranDoc aSrc)
  {
    aDest.ProjectID = aSrc.ProjectID;
    aDest.TaskID = aSrc.TaskID;
  }

  protected virtual void Copy(Batch aDest, GLTranDoc aSrc)
  {
    aDest.NoteID = aSrc.NoteID;
    aDest.CuryID = aSrc.CuryID;
    aDest.Released = new bool?(false);
    aDest.DateEntered = aSrc.TranDate;
    aDest.Description = aSrc.TranDesc;
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.BranchID = aSrc.BranchID;
  }

  protected virtual void Copy(GLTran aDest, GLTranDoc aSrc, bool debit)
  {
    aDest.NoteID = aSrc.NoteID;
    int? debitAccountId;
    int num1;
    if (aSrc.CreditAccountID.HasValue)
    {
      debitAccountId = aSrc.DebitAccountID;
      num1 = debitAccountId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    int num2;
    if (num1 != 0)
    {
      num2 = debit ? 1 : 0;
    }
    else
    {
      debitAccountId = aSrc.DebitAccountID;
      num2 = debitAccountId.HasValue ? 1 : 0;
    }
    bool flag = num2 != 0;
    aDest.AccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
    aDest.SubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
    aDest.CuryDebitAmt = flag ? aSrc.CuryTranAmt : new Decimal?(0M);
    aDest.DebitAmt = flag ? aSrc.TranAmt : new Decimal?(0M);
    aDest.CuryCreditAmt = !flag ? aSrc.CuryTranAmt : new Decimal?(0M);
    aDest.CreditAmt = !flag ? aSrc.TranAmt : new Decimal?(0M);
    aDest.FinPeriodID = aSrc.FinPeriodID;
    aDest.TranPeriodID = aSrc.TranPeriodID;
    aDest.TranDate = aSrc.TranDate;
    aDest.BranchID = aSrc.BranchID;
    aDest.TranDesc = aSrc.TranDesc;
    aDest.ProjectID = aSrc.ProjectID;
    aDest.TaskID = aSrc.TaskID;
    aDest.CostCodeID = aSrc.CostCodeID;
    string extRefNbr = aSrc.ExtRefNbr;
    int length = extRefNbr != null ? extRefNbr.Length : 0;
    aDest.RefNbr = aSrc.ExtRefNbr?.Substring(0, length > 15 ? 15 : length);
  }

  protected virtual void Copy(PXCache cache, TaxDetail aDest, GLTaxTran aSrc)
  {
    cache.SetValue((object) aDest, typeof (TaxAttribute.curyTaxableAmt).Name, (object) aSrc.CuryTaxableAmt);
    cache.SetValue((object) aDest, typeof (TaxAttribute.curyTaxAmt).Name, (object) aSrc.CuryTaxAmt);
    aDest.TaxRate = aSrc.TaxRate;
  }

  protected virtual void Copy(GLBatchDocRelease.GLTranDocU aDest, GLTranDoc aSrc)
  {
    aDest.BranchID = aSrc.BranchID;
    aDest.Module = aSrc.Module;
    aDest.BatchNbr = aSrc.BatchNbr;
    aDest.LineNbr = aSrc.LineNbr;
    aDest.RefNbr = aSrc.RefNbr;
  }

  protected void CloneCuryInfo(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> aCuryInfoView, long? aCuryInfoID)
  {
    if (!aCuryInfoID.HasValue)
      return;
    PX.Objects.CM.CurrencyInfo info = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aCuryInfoID
    }));
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in aCuryInfoView.Select(Array.Empty<object>()))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo ex = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info);
      ex.CuryInfoID = currencyInfo.CuryInfoID;
      ex.tstamp = currencyInfo.tstamp;
      ((PXSelectBase) aCuryInfoView).Cache.Update((object) ex);
    }
  }

  protected void CloneCuryInfo(PXSelectBase<PX.Objects.CM.CurrencyInfo> aCuryInfoView, long? aCuryInfoID)
  {
    if (!aCuryInfoID.HasValue)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aCuryInfoID
    }));
    foreach (PXResult<PX.Objects.CM.CurrencyInfo> pxResult in aCuryInfoView.Select(Array.Empty<object>()))
    {
      PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResult<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo2);
      ((PXSelectBase) aCuryInfoView).Cache.RestoreCopy((object) copy, (object) currencyInfo1);
      copy.CuryInfoID = currencyInfo2.CuryInfoID;
      copy.tstamp = currencyInfo2.tstamp;
      ((PXSelectBase) aCuryInfoView).Cache.Update((object) copy);
    }
  }

  protected void SetCreditAdjustmentRequiredInfo(PX.Objects.AP.APInvoice doc, GLTranDoc voucherDetail)
  {
    doc.SuppliedByVendorID = voucherDetail.BAccountID;
    doc.SuppliedByVendorLocationID = voucherDetail.LocationID;
    doc.PayLocationID = voucherDetail.LocationID;
    doc.SeparateCheck = new bool?(false);
  }

  protected void MergeAPTaxes(PXSelectBase<APTaxTran> aDestTaxView, GLTranDoc doc)
  {
    this.MergeTaxes<APTaxTran, APTaxTran.taxID>(aDestTaxView, doc);
  }

  protected void MergeARTaxes(PXSelectBase<ARTaxTran> aDestTaxView, GLTranDoc doc)
  {
    this.MergeTaxes<ARTaxTran, ARTaxTran.taxID>(aDestTaxView, doc);
  }

  protected void MergeCATaxes(PXSelectBase<CATaxTran> aDestTaxView, GLTranDoc doc)
  {
    this.MergeTaxes<CATaxTran, CATaxTran.taxID>(aDestTaxView, doc);
  }

  protected void MergeTaxes<T, TTaxIDField>(PXSelectBase<T> aDestTaxView, GLTranDoc doc)
    where T : TaxDetail, IBqlTable, new()
    where TTaxIDField : IBqlField
  {
    foreach (PXResult<GLTaxTran> pxResult in ((PXSelectBase<GLTaxTran>) this.taxTotals).Select(new object[3]
    {
      (object) doc.Module,
      (object) doc.BatchNbr,
      (object) doc.LineNbr
    }))
    {
      GLTaxTran aSrc = PXResult<GLTaxTran>.op_Implicit(pxResult);
      T obj1 = PXResultset<T>.op_Implicit(aDestTaxView.Search<TTaxIDField>((object) aSrc.TaxID, Array.Empty<object>()));
      if ((object) obj1 != null && obj1.TaxID == aSrc.TaxID)
      {
        T copy = (T) ((PXSelectBase) aDestTaxView).Cache.CreateCopy((object) obj1);
        this.Copy(((PXSelectBase) aDestTaxView).Cache, (TaxDetail) copy, aSrc);
        T obj2 = (T) ((PXSelectBase) aDestTaxView).Cache.Update((object) copy);
      }
    }
    foreach (PXResult<T> pxResult in aDestTaxView.Select(Array.Empty<object>()))
    {
      T obj = PXResult<T>.op_Implicit(pxResult);
      GLTaxTran glTaxTran = PXResultset<GLTaxTran>.op_Implicit(((PXSelectBase<GLTaxTran>) this.taxTotals).Search<GLTaxTran.taxID>((object) obj.TaxID, new object[3]
      {
        (object) doc.Module,
        (object) doc.BatchNbr,
        (object) doc.LineNbr
      }));
      if (glTaxTran == null || string.IsNullOrEmpty(glTaxTran.TaxID))
        ((PXSelectBase) aDestTaxView).Cache.Delete((object) obj);
    }
  }

  private static void SetFinPeriodID(GLTranDoc doc, PX.Objects.AP.APPayment row)
  {
    row.FinPeriodID = doc.FinPeriodID;
    row.AdjFinPeriodID = doc.FinPeriodID;
  }

  private static void SetFinPeriodID(GLTranDoc doc, PX.Objects.AR.ARPayment row)
  {
    row.FinPeriodID = doc.FinPeriodID;
    row.AdjFinPeriodID = doc.FinPeriodID;
  }

  private static void SetFinPeriodID(GLTranDoc doc, CAAdj row) => row.FinPeriodID = doc.FinPeriodID;

  [PXProjection(typeof (Select<GLTranDoc>), Persistent = true)]
  [Serializable]
  public class GLTranDocU : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BranchID;
    protected string _Module;
    protected string _BatchNbr;
    protected int? _LineNbr;
    protected string _RefNbr;
    protected bool? _DocCreated;
    protected bool? _Released;

    [PXDBInt(BqlField = typeof (GLTranDoc.branchID), IsKey = true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (GLTranDoc.module))]
    [PXUIField]
    public virtual string Module
    {
      get => this._Module;
      set => this._Module = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (GLTranDoc.batchNbr))]
    [PXUIField]
    public virtual string BatchNbr
    {
      get => this._BatchNbr;
      set => this._BatchNbr = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (GLTranDoc.lineNbr))]
    [PXDefault]
    [PXUIField]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (GLTranDoc.refNbr))]
    [PXUIField]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXDBBool(BqlField = typeof (GLTranDoc.docCreated))]
    [PXDefault(false)]
    public virtual bool? DocCreated
    {
      get => this._DocCreated;
      set => this._DocCreated = value;
    }

    [PXDBBool(BqlField = typeof (GLTranDoc.released))]
    [PXDefault(false)]
    public virtual bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.branchID>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.module>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.batchNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLBatchDocRelease.GLTranDocU.lineNbr>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.refNbr>
    {
    }

    public abstract class docCreated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.docCreated>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GLBatchDocRelease.GLTranDocU.released>
    {
    }
  }

  public static class DocCopyHelper
  {
    public static void Copy(PX.Objects.AP.APInvoice aDest, GLTranDoc aSrc)
    {
      aDest.NoteID = aSrc.NoteID;
      aDest.DocDesc = aSrc.TranDesc;
      aDest.DocDate = aSrc.TranDate;
      aDest.InvoiceDate = aSrc.TranDate;
      aDest.CuryID = aSrc.CuryID;
      aDest.CuryTaxAmt = aSrc.CuryTaxAmt;
      aDest.InvoiceNbr = aSrc.ExtRefNbr;
      aDest.FinPeriodID = aSrc.FinPeriodID;
      aDest.TranPeriodID = aSrc.TranPeriodID;
      aDest.VendorID = aSrc.BAccountID;
      aDest.VendorLocationID = aSrc.LocationID;
      aDest.TaxZoneID = aSrc.TaxZoneID;
      aDest.TermsID = aSrc.TermsID;
      aDest.DueDate = aSrc.DueDate;
      aDest.DiscDate = aSrc.DiscDate;
      bool flag = APInvoiceType.DrCr(aSrc.TranType) == "D";
      aDest.APAccountID = flag ? aSrc.CreditAccountID : aSrc.DebitAccountID;
      aDest.APSubID = flag ? aSrc.CreditSubID : aSrc.DebitSubID;
      aDest.BranchID = aSrc.BranchID;
      aDest.PayTypeID = aSrc.PaymentMethodID;
    }
  }
}
