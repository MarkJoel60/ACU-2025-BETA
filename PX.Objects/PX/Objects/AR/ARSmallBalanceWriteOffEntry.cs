// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSmallBalanceWriteOffEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AR;
using PX.Objects.GL;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

[PXHidden]
public class ARSmallBalanceWriteOffEntry : PXGraph<ARSmallBalanceWriteOffEntry>, IARWriteOffEntry
{
  public PXSelect<ARPayment> Document;
  public PXSelect<ARAdjust> Adjustments;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public CMSetupSelect CMSetup;
  public PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>, And<Customer.smallBalanceAllow, Equal<True>>>> customer;
  public PXSelect<ARInvoice, Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>> ARInvoice_CustomerID_DocType_RefNbr;
  public PXSelect<ARBalances> arbalances;
  protected int? _CustomerID;

  public void SaveWriteOff() => ((PXGraph) this).Actions.PressSave();

  [PXDBInt]
  [PXDefault]
  protected virtual void ARAdjust_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  protected virtual void ARAdjust_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  protected virtual void ARAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  protected virtual void ARAdjust_AdjdDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  protected virtual void ARAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  protected virtual void ARAdjust_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PXDefault]
  protected virtual void ARAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void ARAdjust_AdjgDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("SMB")]
  [ARWriteOffType.List]
  [PXUIField]
  protected virtual void ARPayment_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<ARPayment.refNbr, Where<ARPayment.docType, Equal<Current<ARPayment.docType>>>>))]
  [AutoNumber(typeof (PX.Objects.AR.ARSetup.writeOffNumberingID), typeof (ARPayment.docDate))]
  protected virtual void ARPayment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void ARPayment_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void ARPayment_CashSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  protected virtual void ARPayment_CATranID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  [PXDefault]
  protected virtual void ARPayment_ARAccountID_CacheAttached(PXCache sender)
  {
  }

  [SubAccount(typeof (ARRegister.aRAccountID), DisplayName = "Balance WO Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  protected virtual void ARPayment_ARSubID_CacheAttached(PXCache sender)
  {
  }

  [PXString(1, IsFixed = true)]
  [ARWriteOffType.DefaultDrCr(typeof (ARPayment.docType))]
  protected virtual void ARPayment_DrCr_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  protected virtual void ARPayment_DepositAsBatch_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  protected virtual void ARPayment_Deposited_CacheAttached(PXCache sender)
  {
  }

  public ARSmallBalanceWriteOffEntry()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
  }

  public ARRegister ARDocument => (ARRegister) ((PXSelectBase<ARPayment>) this.Document).Current;

  public Customer CUSTOMER
  {
    get
    {
      return PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) this.customer).Select(new object[1]
      {
        (object) this._CustomerID
      }));
    }
  }

  public virtual void PrepareWriteOff(
    ReasonCode reasonCode,
    string WOSubCD,
    DateTime? WODate,
    string masterPeriodID,
    ARRegister ardoc)
  {
    int? accountId = reasonCode.AccountID;
    if (!accountId.HasValue)
      throw new ArgumentNullException("WOAccountID");
    if (WOSubCD == null)
      throw new ArgumentNullException(nameof (WOSubCD));
    ((PXGraph) this).Clear();
    ((PXSelectBase<Customer>) this.customer).Current = (Customer) null;
    this._CustomerID = ardoc.CustomerID;
    PXSelect<ARPayment> document = this.Document;
    ARPayment arPayment1 = new ARPayment();
    arPayment1.BranchID = ardoc.BranchID;
    ARPayment copy = PXCache<ARPayment>.CreateCopy(((PXSelectBase<ARPayment>) document).Insert(arPayment1));
    copy.CustomerID = ardoc.CustomerID;
    copy.CustomerLocationID = ardoc.CustomerLocationID;
    copy.AdjDate = WODate;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy, masterPeriodID);
    copy.DocDate = WODate;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy, masterPeriodID);
    copy.CuryID = ardoc.CuryID;
    copy.ARAccountID = accountId;
    copy.Hold = new bool?(false);
    copy.Status = "B";
    copy.DocDesc = reasonCode.Descr;
    copy.DontPrint = new bool?(true);
    copy.DontEmail = new bool?(true);
    ARPayment arPayment2 = ((PXSelectBase<ARPayment>) this.Document).Update(copy);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARRegister.aRSubID>((object) arPayment2, (object) WOSubCD);
    PXResultset<ARTran> pxResultset1;
    if (!ardoc.PaymentsByLinesAllowed.GetValueOrDefault())
    {
      PXResultset<ARTran> pxResultset2 = new PXResultset<ARTran>();
      pxResultset2.Add((PXResult<ARTran>) null);
      pxResultset1 = pxResultset2;
    }
    else
      pxResultset1 = PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ardoc.DocType,
        (object) ardoc.RefNbr
      });
    foreach (PXResult<ARTran> pxResult in pxResultset1)
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
      ARAdjust arAdjust = ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(new ARAdjust()
      {
        AdjdDocType = ardoc.DocType,
        AdjdRefNbr = ardoc.RefNbr,
        AdjdLineNbr = new int?(((int?) arTran?.LineNbr).GetValueOrDefault()),
        WriteOffReasonCode = reasonCode.ReasonCodeID
      });
      arAdjust.InvoiceID = ardoc.DocType != "CRM" ? ardoc.NoteID : new Guid?();
      arAdjust.PaymentID = arPayment2.NoteID;
      arAdjust.MemoID = ardoc.DocType == "CRM" ? ardoc.NoteID : new Guid?();
      ARPayment current1 = ((PXSelectBase<ARPayment>) this.Document).Current;
      Decimal? nullable1 = current1.CuryDocBal;
      Decimal? nullable2 = arAdjust.CuryAdjgAmt;
      Decimal num1 = nullable2.Value;
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num1);
      current1.CuryDocBal = nullable3;
      ARPayment current2 = ((PXSelectBase<ARPayment>) this.Document).Current;
      nullable1 = current2.DocBal;
      nullable2 = arAdjust.AdjAmt;
      Decimal num2 = nullable2.Value;
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num2);
      current2.DocBal = nullable4;
      ARPayment current3 = ((PXSelectBase<ARPayment>) this.Document).Current;
      nullable1 = current3.CuryOrigDocAmt;
      nullable2 = arAdjust.CuryAdjgAmt;
      Decimal num3 = nullable2.Value;
      Decimal? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() + num3);
      current3.CuryOrigDocAmt = nullable5;
      ARPayment current4 = ((PXSelectBase<ARPayment>) this.Document).Current;
      nullable1 = current4.OrigDocAmt;
      nullable2 = arAdjust.AdjAmt;
      Decimal num4 = nullable2.Value;
      Decimal? nullable6;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new Decimal?(nullable1.GetValueOrDefault() + num4);
      current4.OrigDocAmt = nullable6;
      ARReleaseProcess.UpdateARBalances((PXGraph) this, (ARRegister) ((PXSelectBase<ARPayment>) this.Document).Current, arAdjust.AdjAmt);
    }
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<ARPayment>) this.Document).Current);
  }

  protected virtual void ARAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<ARPayment>) this.Document).Current == null)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      ARAdjust row = e.Row as ARAdjust;
      row.AdjgDocType = ((PXSelectBase<ARPayment>) this.Document).Current.DocType;
      row.AdjgRefNbr = ((PXSelectBase<ARPayment>) this.Document).Current.RefNbr;
      row.AdjgBranchID = ((PXSelectBase<ARPayment>) this.Document).Current.BranchID;
      row.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) this.Document).Current.CuryInfoID;
      row.AdjgFinPeriodID = ((PXSelectBase<ARPayment>) this.Document).Current.FinPeriodID;
      row.AdjgTranPeriodID = ((PXSelectBase<ARPayment>) this.Document).Current.TranPeriodID;
    }
  }

  protected virtual void ARAdjust_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ARAdjust row = e.Row as ARAdjust;
    foreach (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, ARTran> pxResult in PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.lineNbr, Equal<Required<ARAdjust.adjdLineNbr>>>>>>>>, Where<ARInvoice.customerID, Equal<Current<ARPayment.customerID>>, And<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.AdjdLineNbr,
      (object) row.AdjdDocType,
      (object) row.AdjdRefNbr
    }))
    {
      ARTran tran = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, ARTran>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<ARSmallBalanceWriteOffEntry.MultiCurrency>().CloneCurrencyInfo(PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, ARTran>.op_Implicit(pxResult), ((PXSelectBase<ARPayment>) this.Document).Current.DocDate);
      ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, ARTran>.op_Implicit(pxResult);
      row.CustomerID = ((PXSelectBase<ARPayment>) this.Document).Current.CustomerID;
      row.AdjgDocDate = ((PXSelectBase<ARPayment>) this.Document).Current.DocDate;
      row.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) this.Document).Current.CuryInfoID;
      row.AdjdCustomerID = arInvoice.CustomerID;
      row.AdjdCuryInfoID = currencyInfo.CuryInfoID;
      row.AdjdOrigCuryInfoID = arInvoice.CuryInfoID;
      row.AdjdBranchID = arInvoice.BranchID;
      row.AdjdARAcct = arInvoice.ARAccountID;
      row.AdjdARSub = arInvoice.ARSubID;
      row.AdjdDocDate = arInvoice.DocDate;
      row.AdjdTranPeriodID = arInvoice.TranPeriodID;
      row.AdjdFinPeriodID = arInvoice.FinPeriodID;
      row.Released = new bool?(false);
      this.CalcBalances(e.Row, false, e.ExternalCall, tran);
      Decimal? curyDocBal = row.CuryDocBal;
      Decimal? nullable = new Decimal?(0M);
      row.CuryAdjgAmt = curyDocBal;
      row.CuryAdjgDiscAmt = nullable;
      this.CalcBalances(e.Row, true, e.ExternalCall, tran);
    }
  }

  private void CalcBalances(object row, bool isCalcRGOL, bool DiscOnDiscDate, ARTran tran)
  {
    ARAdjust adj = (ARAdjust) row;
    ARInvoice voucher = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this.ARInvoice_CustomerID_DocType_RefNbr).Select(new object[3]
    {
      (object) adj.AdjdCustomerID,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }));
    if (voucher == null)
      return;
    new PaymentBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARSmallBalanceWriteOffEntry.MultiCurrency>()).CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) voucher, (IAdjustment) adj, (IDocumentTran) tran);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) voucher, (IAdjustment) adj);
    new PaymentBalanceAjuster((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARSmallBalanceWriteOffEntry.MultiCurrency>()).AdjustBalance((IAdjustment) adj);
    if (!isCalcRGOL)
      return;
    new PaymentRGOLCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARSmallBalanceWriteOffEntry.MultiCurrency>(), (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) voucher, (IDocumentTran) tran);
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARSmallBalanceWriteOffEntry, ARPayment>
  {
    protected override string DocumentStatus => "B";

    protected override MultiCurrencyGraph<ARSmallBalanceWriteOffEntry, ARPayment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARSmallBalanceWriteOffEntry, ARPayment>.DocumentMapping(typeof (ARPayment))
      {
        DocumentDate = typeof (ARPayment.adjDate),
        BAccountID = typeof (ARPayment.customerID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Document
      };
    }
  }
}
