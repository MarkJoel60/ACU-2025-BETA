// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARDunningLetterMaint : PXGraph<ARDunningLetterMaint>
{
  public PXSelect<ARDunningLetter> Document;
  public PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDunningLetter.bAccountID>>>> CurrentCustomer;
  public PXSelectJoin<ARDunningLetterDetail, LeftJoin<ARDunningLetter, On<ARDunningLetter.dunningLetterID, Equal<ARDunningLetterDetail.dunningLetterID>>>, Where<ARDunningLetterDetail.dunningLetterID, Equal<Current<ARDunningLetter.dunningLetterID>>>> Details;
  public PXSave<ARDunningLetter> Save;
  public PXCancel<ARDunningLetter> Cancel;
  public PXDelete<ARDunningLetter> Delete;
  public PXAction<ARDunningLetter> ViewDocument;
  public PXAction<ARDunningLetter> Release;
  public PXAction<ARDunningLetter> VoidLetter;
  public PXAction<ARDunningLetter> PrintLetter;
  public PXAction<ARDunningLetter> Revoke;

  public ARDunningLetterMaint()
  {
    ((PXSelectBase) this.Details).AllowUpdate = false;
    ((PXSelectBase) this.Details).AllowInsert = false;
    ((PXSelectBase) this.CurrentCustomer).AllowUpdate = false;
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) this.Details).Select(Array.Empty<object>()))
    {
      ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
      ARDunningLetterProcess.ARInvoiceWithDL arInvoiceWithDl = PXResultset<ARDunningLetterProcess.ARInvoiceWithDL>.op_Implicit(PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL, PXSelect<ARDunningLetterProcess.ARInvoiceWithDL, Where<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docType, Equal<Required<ARInvoice.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) dunningLetterDetail.RefNbr,
        (object) dunningLetterDetail.DocType
      }));
      if (arInvoiceWithDl != null)
      {
        int? dunningLetterLevel1 = arInvoiceWithDl.DunningLetterLevel;
        int? dunningLetterLevel2 = dunningLetterDetail.DunningLetterLevel;
        if (dunningLetterLevel1.GetValueOrDefault() > dunningLetterLevel2.GetValueOrDefault() & dunningLetterLevel1.HasValue & dunningLetterLevel2.HasValue)
        {
          ((PXAction) this.VoidLetter).SetEnabled(false);
          break;
        }
      }
    }
  }

  [PXCustomizeBaseAttribute(typeof (CustomerRawAttribute), "DisplayName", "Customer")]
  protected virtual void Customer_AcctCD_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARDunningLetter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARDunningLetter row = (ARDunningLetter) e.Row;
    bool valueOrDefault1 = row.Released.GetValueOrDefault();
    bool valueOrDefault2 = row.Voided.GetValueOrDefault();
    sender.AllowDelete = !valueOrDefault1;
    ((PXSelectBase) this.Details).AllowDelete = !valueOrDefault1;
    ((PXAction) this.VoidLetter).SetEnabled(valueOrDefault1 && !valueOrDefault2);
    ((PXAction) this.PrintLetter).SetEnabled(valueOrDefault1 && !valueOrDefault2);
    ((PXAction) this.Revoke).SetEnabled(!valueOrDefault1);
    ((PXAction) this.Release).SetEnabled(!valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<ARDunningLetter.dunningFee>(sender, e.Row, !valueOrDefault1);
  }

  protected virtual void ARDunningLetterDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    int val2 = 0;
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) this.Details).Select(Array.Empty<object>()))
      val2 = Math.Max(PXResult<ARDunningLetterDetail>.op_Implicit(pxResult).DunningLetterLevel.GetValueOrDefault(), val2);
    ((PXSelectBase<ARDunningLetter>) this.Document).Current.DunningLetterLevel = new int?(val2);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetterDetail>) this.Details).Current != null)
    {
      string docType = ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.DocType;
      if (docType == "PMT" || docType == "REF" || docType == "PPM")
      {
        ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.refNbr, Equal<Required<ARDunningLetterDetail.refNbr>>, And<ARPayment.docType, Equal<Required<ARDunningLetterDetail.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.RefNbr,
          (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.DocType
        }));
        if (arPayment != null)
        {
          ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
          ((PXSelectBase<ARPayment>) instance.Document).Current = arPayment;
          PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
        }
      }
      else
      {
        ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.refNbr, Equal<Required<ARDunningLetterDetail.refNbr>>, And<ARInvoice.docType, Equal<Required<ARDunningLetterDetail.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.RefNbr,
          (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.DocType
        }));
        if (arInvoice != null)
        {
          ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
          ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice;
          PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetter>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Voided;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          ((PXAction) this.Save).Press();
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003Crelease\u003Eb__13_0)));
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable voidLetter(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetter>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          ((PXAction) this.Save).Press();
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CvoidLetter\u003Eb__15_0)));
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable printLetter(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetter>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          ((PXSelectBase<ARDunningLetter>) this.Document).Current.Printed = new bool?(true);
          ((PXAction) this.Save).Press();
          int? dunningLetterId = ((PXSelectBase<ARDunningLetter>) this.Document).Current.DunningLetterID;
          throw PXReportRequiredException.CombineReport((PXReportRequiredException) null, ARDunningLetterPrint.GetCustomerReportID((PXGraph) this, "AR661000", ((PXSelectBase<ARDunningLetter>) this.Document).Current.BAccountID, ((PXSelectBase<ARDunningLetter>) this.Document).Current.BranchID), new Dictionary<string, string>()
          {
            ["ARDunningLetter.DunningLetterID"] = dunningLetterId.ToString()
          }, (CurrentLocalization) null);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable revoke(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetterDetail>) this.Details).Current != null && ((PXSelectBase<ARDunningLetter>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<ARDunningLetter>) this.Document).Current.Voided;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.refNbr, Equal<Required<ARDunningLetterDetail.refNbr>>, And<ARInvoice.docType, Equal<Required<ARDunningLetterDetail.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.RefNbr,
            (object) ((PXSelectBase<ARDunningLetterDetail>) this.Details).Current.DocType
          }));
          if (arInvoice != null)
          {
            ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
            ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice;
            arInvoice.Revoked = new bool?(true);
            ((PXSelectBase<ARInvoice>) instance.Document).Update(arInvoice);
            ((PXAction) instance.Save).Press();
            ((PXSelectBase<ARDunningLetterDetail>) this.Details).Delete(((PXSelectBase<ARDunningLetterDetail>) this.Details).Current);
            ((PXAction) this.Save).Press();
          }
        }
      }
    }
    return adapter.Get();
  }

  private static void VoidProcess(ARDunningLetter doc)
  {
    ARDunningLetterMaint instance1 = PXGraph.CreateInstance<ARDunningLetterMaint>();
    ((PXSelectBase<ARDunningLetter>) instance1.Document).Current = doc;
    ((PXSelectBase) instance1.Details).AllowUpdate = true;
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) instance1.Details).Select(Array.Empty<object>()))
    {
      ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
      ARDunningLetterProcess.ARInvoiceWithDL arInvoiceWithDl = PXResultset<ARDunningLetterProcess.ARInvoiceWithDL>.op_Implicit(PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL, PXSelect<ARDunningLetterProcess.ARInvoiceWithDL, Where<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docType, Equal<Required<ARInvoice.docType>>>>>.Config>.Select((PXGraph) instance1, new object[2]
      {
        (object) dunningLetterDetail.RefNbr,
        (object) dunningLetterDetail.DocType
      }));
      if (arInvoiceWithDl != null)
      {
        int? dunningLetterLevel1 = arInvoiceWithDl.DunningLetterLevel;
        int? dunningLetterLevel2 = dunningLetterDetail.DunningLetterLevel;
        if (dunningLetterLevel1.GetValueOrDefault() > dunningLetterLevel2.GetValueOrDefault() & dunningLetterLevel1.HasValue & dunningLetterLevel2.HasValue)
          throw new PXException("The Dunning Letter cannot be voided. A Dunning Letter of a higher level exists for one or more documents. You should void the letters of a higher levels first.");
      }
    }
    ARInvoiceEntry instance2 = PXGraph.CreateInstance<ARInvoiceEntry>();
    ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARInvoice.docType, Equal<Required<ARInvoice.docType>>>>>.Config>.Select((PXGraph) instance2, new object[2]
    {
      (object) doc.FeeRefNbr,
      (object) doc.FeeDocType
    }));
    if (arInvoice != null)
    {
      bool? nullable = arInvoice.Voided;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        ((PXSelectBase<ARInvoice>) instance2.Document).Current = arInvoice;
        nullable = arInvoice.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          ((PXAction) instance2.Delete).Press();
          doc.FeeRefNbr = (string) null;
          doc.FeeDocType = (string) null;
        }
        else
        {
          nullable = arInvoice.Released;
          if (nullable.GetValueOrDefault())
          {
            nullable = arInvoice.OpenDoc;
            if (nullable.GetValueOrDefault())
            {
              Decimal? curyOrigDocAmt = arInvoice.CuryOrigDocAmt;
              Decimal? curyDocBal = arInvoice.CuryDocBal;
              if (!(curyOrigDocAmt.GetValueOrDefault() == curyDocBal.GetValueOrDefault() & curyOrigDocAmt.HasValue == curyDocBal.HasValue))
                throw new PXException("The invoice for the Dunning Letter Fee has already been paid. To void the invoice you should void the payment first.");
              ((PXAction) instance2.reverseInvoiceAndApplyToMemo).Press();
              ((PXSelectBase<ARInvoice>) instance2.Document).Current.Hold = new bool?(false);
              ((PXSelectBase<ARInvoice>) instance2.Document).Update(((PXSelectBase<ARInvoice>) instance2.Document).Current);
              using (new PXTimeStampScope((byte[]) null))
              {
                ((PXAction) instance2.Save).Press();
                ARDocumentRelease.ReleaseDoc(new List<ARRegister>()
                {
                  (ARRegister) ((PXSelectBase<ARInvoice>) instance2.Document).Current
                }, false);
                goto label_23;
              }
            }
          }
          throw new PXException("The invoice for the Dunning Letter Fee has already been paid. To void the invoice you should void the payment first.");
        }
      }
    }
label_23:
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) instance1.Details).Select(Array.Empty<object>()))
    {
      ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
      dunningLetterDetail.Voided = new bool?(true);
      ((PXSelectBase<ARDunningLetterDetail>) instance1.Details).Update(dunningLetterDetail);
    }
    doc.Voided = new bool?(true);
    ((PXSelectBase<ARDunningLetter>) instance1.Document).Update(doc);
    ((PXAction) instance1.Save).Press();
  }

  public static void ReleaseProcess(ARDunningLetter doc)
  {
    ARDunningLetterMaint.ReleaseProcess(PXGraph.CreateInstance<ARDunningLetterMaint>(), doc);
  }

  public static void ReleaseProcess(ARDunningLetterMaint graph, ARDunningLetter doc)
  {
    if (doc == null || doc.Released.GetValueOrDefault() || doc.Voided.GetValueOrDefault())
      return;
    ((PXSelectBase<ARDunningLetter>) graph.Document).Current = doc;
    doc.DunningLetterLevel = new int?(0);
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) graph.Details).Select(Array.Empty<object>()))
    {
      ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
      ARDunningLetter arDunningLetter = doc;
      int? dunningLetterLevel = doc.DunningLetterLevel;
      int valueOrDefault1 = dunningLetterLevel.GetValueOrDefault();
      dunningLetterLevel = dunningLetterDetail.DunningLetterLevel;
      int valueOrDefault2 = dunningLetterLevel.GetValueOrDefault();
      int? nullable = new int?(Math.Max(valueOrDefault1, valueOrDefault2));
      arDunningLetter.DunningLetterLevel = nullable;
    }
    int? dunningLetterLevel1 = doc.DunningLetterLevel;
    int num1 = 0;
    if (dunningLetterLevel1.GetValueOrDefault() == num1 & dunningLetterLevel1.HasValue)
      throw new PXException("The Dunning Letter does not list any overdue documents, therefore it cannot be released.");
    if (doc.DunningFee.HasValue)
    {
      Decimal? dunningFee = doc.DunningFee;
      Decimal num2 = 0M;
      if (!(dunningFee.GetValueOrDefault() == num2 & dunningFee.HasValue))
      {
        ARInvoice arInvoice = ARDunningLetterMaint.InsertFeeInvoice(doc, doc.DunningFee.GetValueOrDefault());
        doc.FeeRefNbr = arInvoice.RefNbr;
        doc.FeeDocType = arInvoice.DocType;
      }
    }
    foreach (PXResult<ARDunningLetterDetail> pxResult in ((PXSelectBase<ARDunningLetterDetail>) graph.Details).Select(Array.Empty<object>()))
    {
      ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
      dunningLetterDetail.Released = new bool?(true);
      ((PXSelectBase<ARDunningLetterDetail>) graph.Details).Update(dunningLetterDetail);
    }
    doc.Released = new bool?(true);
    ((PXSelectBase<ARDunningLetter>) graph.Document).Update(doc);
    ((PXAction) graph.Save).Press();
  }

  private static ARInvoice InsertFeeInvoice(ARDunningLetter doc, Decimal dunningFee)
  {
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(doc.BranchID);
    string finPeriodID = instance.FinPeriodRepository.GetPeriodIDFromDate(doc.DunningLetterDate, parentOrganizationId);
    instance.FinPeriodUtils.ValidateFinPeriod<ARDunningLetter>((IEnumerable<ARDunningLetter>) doc.SingleToArray<ARDunningLetter>(), (Func<ARDunningLetter, string>) (m => finPeriodID), (Func<ARDunningLetter, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<ARDunningLetter.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) doc.BAccountID
    }));
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<ARSetup.dunningFeeNumberingID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) arSetup.DunningFeeNumberingID
    }));
    int? nullable = arSetup.DunningFeeInventoryID;
    if (!nullable.HasValue)
      throw new PXException("The invoice for Dunning Letter Fee cannot be generated as a Dunning Fee Item is not specified in Accounts Receivable Preferences.");
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<ARSetup.dunningFeeInventoryID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) arSetup.DunningFeeInventoryID
    }));
    nullable = inventoryItem != null ? inventoryItem.SalesAcctID : throw new PXException("The invoice for Dunning Letter Fee cannot be generated as a Dunning Fee Item is not specified in Accounts Receivable Preferences.");
    if (!nullable.HasValue)
      throw new PXException("The invoice for the dunning fee cannot be generated. The non-stock item specified in the Dunning Fee Item box on Accounts Receivable Preferences (AR101000) has no sales account. Select another item or specify a sales account for this item on Non-Stock Items (IN202000).");
    ARInvoice arInvoice1 = new ARInvoice();
    arInvoice1.DocType = "INV";
    ARInvoice arInvoice2 = arInvoice1;
    if (numbering != null)
      AutoNumberAttribute.SetNumberingId<ARInvoice.refNbr>(((PXSelectBase) instance.Document).Cache, arInvoice2.DocType, numbering.NumberingID);
    ARInvoice copy1 = (ARInvoice) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<ARInvoice>) instance.Document).Insert(arInvoice2));
    copy1.Released = new bool?(false);
    copy1.Voided = new bool?(false);
    copy1.BranchID = doc.BranchID;
    copy1.DocDate = doc.DunningLetterDate;
    copy1.CustomerID = customer.BAccountID;
    using (new PXLocaleScope(customer.LocaleName))
      copy1.DocDesc = PXLocalizer.Localize("Dunning Letter Fee");
    copy1.CustomerLocationID = customer.DefLocationID;
    ARInvoice arInvoice3 = copy1;
    bool? allowOverrideCury = customer.AllowOverrideCury;
    bool flag = false;
    string str = !(allowOverrideCury.GetValueOrDefault() == flag & allowOverrideCury.HasValue) || customer.CuryID == null ? PXAccess.GetBranch(doc.BranchID)?.BaseCuryID : customer.CuryID;
    arInvoice3.CuryID = str;
    if (arSetup.DunningFeeTermID != null)
      copy1.TermsID = arSetup.DunningFeeTermID;
    ((PXSelectBase<ARInvoice>) instance.Document).Update(copy1);
    PXDBDecimalAttribute.EnsurePrecision(((PXSelectBase) instance.Document).Cache);
    ARTran arTran = new ARTran()
    {
      BranchID = doc.BranchID,
      Qty = new Decimal?((Decimal) 1),
      CuryUnitPrice = new Decimal?(((PXGraph) instance).GetExtension<ARInvoiceEntry.MultiCurrency>().GetDefaultCurrencyInfo().CuryConvCury(dunningFee)),
      IsStockItem = new bool?(false),
      InventoryID = arSetup.DunningFeeInventoryID,
      AccountID = inventoryItem.SalesAcctID,
      SubID = inventoryItem.SalesSubID
    };
    ((PXSelectBase<ARTran>) instance.Transactions).Insert(arTran);
    ARInvoice copy2 = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) instance.Document).Current);
    copy2.OrigDocAmt = copy2.DocBal;
    copy2.CuryOrigDocAmt = copy2.CuryDocBal;
    copy2.Hold = new bool?(false);
    ((PXSelectBase<ARInvoice>) instance.Document).Update(copy2);
    ((PXAction) instance.Save).Press();
    if (arSetup.AutoReleaseDunningFee.GetValueOrDefault())
      ((PXAction) instance.release).Press();
    return ((PXSelectBase<ARInvoice>) instance.Document).Current;
  }
}
