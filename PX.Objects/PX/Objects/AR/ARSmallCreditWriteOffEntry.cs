// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSmallCreditWriteOffEntry
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
public class ARSmallCreditWriteOffEntry : PXGraph<ARSmallCreditWriteOffEntry>, IARWriteOffEntry
{
  public PXSelect<ARInvoice> Document;
  public PXSelect<ARAdjust2> Adjustments;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public CMSetupSelect CMSetup;
  public PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>, And<Customer.smallBalanceAllow, Equal<True>>>> customer;
  public PXSelect<ARPayment, Where<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>> ARPayment_CustomerID_DocType_RefNbr;
  public PXSelect<ARBalances> arbalances;
  protected int? _CustomerID;

  public void SaveWriteOff() => ((PXGraph) this).Actions.PressSave();

  [PXDBInt]
  [PXDefault]
  protected virtual void ARAdjust2_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  protected virtual void ARAdjust2_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXRemoveBaseAttribute(typeof (ARPaymentType.AdjgRefNbrAttribute))]
  protected virtual void ARAdjust2_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  protected virtual void ARAdjust2_AdjdDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  protected virtual void ARAdjust2_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  protected virtual void ARAdjust2_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PXDefault]
  protected virtual void ARAdjust2_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void ARAdjust2_AdjgDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("SMC")]
  [ARWriteOffType.List]
  [PXUIField]
  protected virtual void ARInvoice_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Current<ARInvoice.docType>>>>))]
  [AutoNumber(typeof (PX.Objects.AR.ARSetup.writeOffNumberingID), typeof (ARInvoice.docDate))]
  protected virtual void ARInvoice_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [Account]
  [PXDefault]
  protected virtual void ARInvoice_ARAccountID_CacheAttached(PXCache sender)
  {
  }

  [SubAccount(typeof (ARInvoice.aRAccountID), DisplayName = "Credit WO Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  protected virtual void ARInvoice_ARSubID_CacheAttached(PXCache sender)
  {
  }

  [PXString(1, IsFixed = true)]
  [ARWriteOffType.DefaultDrCr(typeof (ARInvoice.docType))]
  protected virtual void ARInvoice_DrCr_CacheAttached(PXCache sender)
  {
  }

  public ARRegister ARDocument => (ARRegister) ((PXSelectBase<ARInvoice>) this.Document).Current;

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
    PXSelect<ARInvoice> document = this.Document;
    ARInvoice arInvoice = new ARInvoice();
    arInvoice.BranchID = ardoc.BranchID;
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) document).Insert(arInvoice));
    copy.CustomerID = ardoc.CustomerID;
    copy.CustomerLocationID = ardoc.CustomerLocationID;
    copy.DocDate = WODate;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy, masterPeriodID);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyID>((object) copy, (object) ardoc.CuryID);
    copy.ARAccountID = accountId;
    copy.Hold = new bool?(false);
    copy.DontPrint = new bool?(true);
    copy.DontEmail = new bool?(true);
    copy.Status = "B";
    copy.AdjCntr = new int?(-1);
    copy.DocDesc = reasonCode.Descr;
    ARInvoice data = ((PXSelectBase<ARInvoice>) this.Document).Update(copy);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.aRSubID>((object) data, (object) WOSubCD);
    SharedRecordAttribute.DefaultRecord<ARInvoice.billAddressID>(((PXSelectBase) this.Document).Cache, (object) data);
    SharedRecordAttribute.DefaultRecord<ARInvoice.billContactID>(((PXSelectBase) this.Document).Cache, (object) data);
    SharedRecordAttribute.DefaultRecord<ARInvoice.shipAddressID>(((PXSelectBase) this.Document).Cache, (object) data);
    SharedRecordAttribute.DefaultRecord<ARInvoice.shipContactID>(((PXSelectBase) this.Document).Cache, (object) data);
    PXSelect<ARAdjust2> adjustments = this.Adjustments;
    ARAdjust2 arAdjust2_1 = new ARAdjust2();
    arAdjust2_1.AdjgDocType = ardoc.DocType;
    arAdjust2_1.AdjgRefNbr = ardoc.RefNbr;
    arAdjust2_1.AdjNbr = data.AdjCntr;
    arAdjust2_1.AdjdCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    arAdjust2_1.AdjdOrigCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    ARAdjust2 arAdjust2_2 = ((PXSelectBase<ARAdjust2>) adjustments).Insert(arAdjust2_1);
    arAdjust2_2.InvoiceID = data.NoteID;
    arAdjust2_2.PaymentID = ardoc.DocType != "CRM" ? ardoc.NoteID : new Guid?();
    arAdjust2_2.MemoID = ardoc.DocType == "CRM" ? ardoc.NoteID : new Guid?();
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    Decimal? nullable1 = current1.CuryDocBal;
    Decimal? nullable2 = arAdjust2_2.CuryAdjdAmt;
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
    ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    nullable1 = current2.DocBal;
    nullable2 = arAdjust2_2.AdjAmt;
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
    ARInvoice current3 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    nullable1 = current3.CuryOrigDocAmt;
    nullable2 = arAdjust2_2.CuryAdjdAmt;
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
    ARInvoice current4 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    nullable1 = current4.OrigDocAmt;
    nullable2 = arAdjust2_2.AdjAmt;
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
    ARReleaseProcess.UpdateARBalances((PXGraph) this, ((PXSelectBase<ARInvoice>) this.Document).Current, arAdjust2_2.AdjAmt);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<ARInvoice>) this.Document).Current);
  }

  protected virtual void ARAdjust2_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      ARAdjust2 row = e.Row as ARAdjust2;
      row.AdjdDocType = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType;
      row.AdjdRefNbr = ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr;
      row.AdjdBranchID = ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID;
      row.AdjdCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
      row.AdjdOrigCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    }
  }

  protected virtual void ARAdjust2_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>>, Where<ARPayment.customerID, Equal<Current<ARInvoice.customerID>>, And<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.AdjgDocType,
      (object) row.AdjgRefNbr
    }))
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this).GetExtension<ARSmallCreditWriteOffEntry.MultiCurrency>().GetDefaultCurrencyInfo();
      PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.RestoreCopy(defaultCurrencyInfo, PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
      defaultCurrencyInfo.CuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
      ARPayment payment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      row.CustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
      row.AdjgDocDate = ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
      row.AdjgFinPeriodID = ((PXSelectBase<ARInvoice>) this.Document).Current.FinPeriodID;
      row.AdjgTranPeriodID = ((PXSelectBase<ARInvoice>) this.Document).Current.TranPeriodID;
      row.AdjgCuryInfoID = payment.CuryInfoID;
      row.AdjgBranchID = payment.BranchID;
      row.AdjdCustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
      row.AdjdARAcct = payment.ARAccountID;
      row.AdjdARSub = payment.ARSubID;
      row.AdjdDocDate = ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
      row.AdjdTranPeriodID = ((PXSelectBase<ARInvoice>) this.Document).Current.TranPeriodID;
      row.AdjdFinPeriodID = ((PXSelectBase<ARInvoice>) this.Document).Current.FinPeriodID;
      row.Released = new bool?(false);
      ARInvoiceBalanceCalculator balanceCalculator = new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARSmallCreditWriteOffEntry.MultiCurrency>(), (PXGraph) this);
      balanceCalculator.CalcBalancesFromInvoiceSide((ARAdjust) row, (IInvoice) ((PXSelectBase<ARInvoice>) this.Document).Current, payment, false, e.ExternalCall);
      row.CuryAdjdAmt = row.CuryDocBal;
      row.CuryAdjdDiscAmt = new Decimal?(0M);
      balanceCalculator.CalcBalancesFromInvoiceSide((ARAdjust) row, (IInvoice) ((PXSelectBase<ARInvoice>) this.Document).Current, payment, true, e.ExternalCall);
    }
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARSmallCreditWriteOffEntry, ARInvoice>
  {
    protected override string DocumentStatus => "B";

    protected override MultiCurrencyGraph<ARSmallCreditWriteOffEntry, ARInvoice>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARSmallCreditWriteOffEntry, ARInvoice>.DocumentMapping(typeof (ARInvoice))
      {
        DocumentDate = typeof (ARInvoice.docDate),
        BAccountID = typeof (ARInvoice.customerID)
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
