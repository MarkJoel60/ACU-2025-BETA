// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPendingInvoicesEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
[Serializable]
public class APPendingInvoicesEnq : PXGraph<
#nullable disable
APPendingInvoicesEnq>
{
  public PXFilter<APPendingInvoicesEnq.PendingInvoiceFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<APPendingInvoicesEnq.PendingPaymentSummary> Documents;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXCancel<APPendingInvoicesEnq.PendingInvoiceFilter> Cancel;
  public PXAction<APPendingInvoicesEnq.PendingInvoiceFilter> processPayment;

  public virtual IEnumerable filter()
  {
    if (this.Filter.Cache != null)
    {
      APPendingInvoicesEnq.PendingInvoiceFilter current = this.Filter.Cache.Current as APPendingInvoicesEnq.PendingInvoiceFilter;
      current.Balance = new Decimal?(0M);
      current.CuryBalance = new Decimal?(0M);
      current.CuryID = (string) null;
      string str = (string) null;
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = true;
      foreach (PXResult<APPendingInvoicesEnq.PendingPaymentSummary> pxResult in this.Documents.Select())
      {
        APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary = (APPendingInvoicesEnq.PendingPaymentSummary) pxResult;
        APPendingInvoicesEnq.PendingInvoiceFilter pendingInvoiceFilter1 = current;
        Decimal? nullable1 = pendingInvoiceFilter1.Balance;
        Decimal? nullable2 = pendingPaymentSummary.DocBal;
        pendingInvoiceFilter1.Balance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        if (flag3)
        {
          current.CuryID = pendingPaymentSummary.CuryID;
          str = pendingPaymentSummary.AccountBaseCuryID;
        }
        else
        {
          if ((!string.IsNullOrEmpty(current.CuryID) || !string.IsNullOrEmpty(pendingPaymentSummary.CuryID)) && !(current.CuryID == pendingPaymentSummary.CuryID))
            flag1 = false;
          if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>() && str != pendingPaymentSummary.AccountBaseCuryID)
            flag2 = false;
        }
        if (flag1)
        {
          APPendingInvoicesEnq.PendingInvoiceFilter pendingInvoiceFilter2 = current;
          nullable2 = pendingInvoiceFilter2.CuryBalance;
          nullable1 = pendingPaymentSummary.CuryDocBal;
          pendingInvoiceFilter2.CuryBalance = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        }
        flag3 = false;
      }
      bool flag4 = !string.IsNullOrEmpty(current.CuryID);
      current.CuryID = !flag2 || flag1 & flag4 ? current.CuryID : str;
      PXUIFieldAttribute.SetVisible<APPendingInvoicesEnq.PendingInvoiceFilter.curyID>(this.Filter.Cache, (object) current, flag4 && (flag1 || !flag1 & flag2));
      PXUIFieldAttribute.SetVisible<APPendingInvoicesEnq.PendingInvoiceFilter.curyBalance>(this.Filter.Cache, (object) current, flag1 & flag4);
      PXUIFieldAttribute.SetVisible<APPendingInvoicesEnq.PendingInvoiceFilter.balance>(this.Filter.Cache, (object) current, flag2 && !(flag1 & flag4));
    }
    yield return this.Filter.Cache.Current;
    this.Filter.Cache.IsDirty = false;
  }

  public virtual IEnumerable documents()
  {
    APPendingInvoicesEnq.PendingInvoiceFilter current = this.Filter.Current;
    Dictionary<APPendingInvoicesEnq.CashAcctKey, APPendingInvoicesEnq.PendingPaymentSummary> dictionary = new Dictionary<APPendingInvoicesEnq.CashAcctKey, APPendingInvoicesEnq.PendingPaymentSummary>();
    System.DateTime? payDate;
    if (current == null)
    {
      payDate = current.PayDate;
      if (!payDate.HasValue)
        return (IEnumerable) dictionary.Values;
    }
    PXSelectBase<APInvoice> pxSelectBase = (PXSelectBase<APInvoice>) new PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<APInvoice.payAccountID>>, LeftJoin<APAdjust, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>, And<APAdjust.released, Equal<BitOff>>>>, CrossJoin<PX.Objects.AP.APSetup>>>>, Where<Where2<Where<APInvoice.paySel, Equal<BitOn>, Or<PX.Objects.AP.APSetup.requireApprovePayments, Equal<False>>>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APInvoice.openDoc, Equal<BitOn>>>>>, OrderBy<Asc<APInvoice.docType, Asc<APInvoice.refNbr>>>>((PXGraph) this);
    payDate = current.PayDate;
    if (payDate.HasValue)
      pxSelectBase.WhereAnd<Where<APInvoice.payDate, LessEqual<Current<APPendingInvoicesEnq.PendingInvoiceFilter.payDate>>>>();
    int? payAccountId = current.PayAccountID;
    if (payAccountId.HasValue)
      pxSelectBase.WhereAnd<Where<APInvoice.payAccountID, Equal<Current<APPendingInvoicesEnq.PendingInvoiceFilter.payAccountID>>>>();
    if (current.PayTypeID != null)
      pxSelectBase.WhereAnd<Where<APInvoice.payTypeID, Equal<Current<APPendingInvoicesEnq.PendingInvoiceFilter.payTypeID>>>>();
    APPendingInvoicesEnq.APInvoiceKey apInvoiceKey = (APPendingInvoicesEnq.APInvoiceKey) null;
    foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CA.CashAccount, APAdjust> pxResult in pxSelectBase.Select())
    {
      APInvoice apInvoice = (APInvoice) pxResult;
      PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) pxResult;
      APAdjust apAdjust1 = (APAdjust) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
      this.GetExtension<APPendingInvoicesEnq.MultiCurrency>().currencyinfobykey.StoreCached(new PXCommandKey(new object[1]
      {
        (object) currencyInfo1.CuryInfoID
      }, (object[]) null, (string[]) null, (bool[]) null, new int?(0), new int?(1), (PXFilterRow[]) null, false), new List<object>()
      {
        (object) currencyInfo1
      });
      if (apAdjust1.AdjdDocType == null)
      {
        APPendingInvoicesEnq.APInvoiceKey other = new APPendingInvoicesEnq.APInvoiceKey(apInvoice.DocType, apInvoice.RefNbr);
        if (apInvoiceKey == null || apInvoiceKey.CompareTo((Pair<string, string>) other) != 0)
        {
          apInvoiceKey = other;
          APPendingInvoicesEnq.CashAcctKey key;
          ref APPendingInvoicesEnq.CashAcctKey local = ref key;
          payAccountId = apInvoice.PayAccountID;
          int aAcctID = payAccountId.Value;
          string payTypeId = apInvoice.PayTypeID;
          local = new APPendingInvoicesEnq.CashAcctKey(aAcctID, payTypeId);
          APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary;
          if (!dictionary.TryGetValue(key, out pendingPaymentSummary))
          {
            pendingPaymentSummary = new APPendingInvoicesEnq.PendingPaymentSummary()
            {
              PayAccountID = apInvoice.PayAccountID,
              AccountBaseCuryID = cashAccount.BaseCuryID,
              PayTypeID = apInvoice.PayTypeID,
              CuryID = cashAccount.CuryID,
              BranchID = apInvoice.BranchID
            };
            dictionary[key] = pendingPaymentSummary;
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.GetExtension<APPendingInvoicesEnq.MultiCurrency>().currencyinfobykey.Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
            {
              CuryID = pendingPaymentSummary.CuryID,
              CuryRateTypeID = cashAccount.CuryRateTypeID,
              CuryEffDate = current.PayDate,
              BaseCuryID = currencyInfo1.BaseCuryID
            });
            pendingPaymentSummary.CuryInfoID = currencyInfo2.CuryInfoID;
          }
          APAdjust apAdjust2 = new APAdjust();
          apAdjust2.VendorID = apInvoice.VendorID;
          apAdjust2.AdjdDocType = apInvoice.DocType;
          apAdjust2.AdjdRefNbr = apInvoice.RefNbr;
          apAdjust2.AdjgDocType = "CHK";
          PXCache cach = this.Caches[typeof (APPayment)];
          APPayment data = new APPayment();
          data.DocType = "CHK";
          apAdjust2.AdjgRefNbr = AutoNumberAttribute.GetNewNumberSymbol<APPayment.refNbr>(cach, (object) data);
          APAdjust apAdjust3 = apAdjust2;
          try
          {
            this.CalcBalances(this.GetExtension<APPendingInvoicesEnq.MultiCurrency>(), pendingPaymentSummary.CuryInfoID, current.PayDate, (IInvoice) apInvoice, (IAdjustment) apAdjust3);
          }
          catch (PXRateIsNotDefinedForThisDateException ex)
          {
            this.Documents.Cache.RaiseExceptionHandling<APPendingInvoicesEnq.PendingPaymentSummary.curyID>((object) pendingPaymentSummary, (object) pendingPaymentSummary.CuryID, (Exception) new PXSetPropertyException(ex.Message, PXErrorLevel.RowError));
          }
          APPendingInvoicesEnq.Aggregate(pendingPaymentSummary, new PXResult<APAdjust, APInvoice>(apAdjust3, apInvoice), current.PayDate);
        }
      }
    }
    return (IEnumerable) dictionary.Values;
  }

  private void CalcBalances(
    APPendingInvoicesEnq.MultiCurrency multiCurrencyExtension,
    long? PaymentCuryInfoID,
    System.DateTime? PayDate,
    IInvoice voucher,
    IAdjustment adj)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = multiCurrencyExtension.GetCurrencyInfo(voucher.CuryInfoID);
    using (new ReadOnlyScope(new PXCache[1]
    {
      multiCurrencyExtension.currencyinfobykey.Cache
    }))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = multiCurrencyExtension.CloneCurrencyInfo(currencyInfo1, PayDate);
      new PaymentBalanceCalculator((IPXCurrencyHelper) multiCurrencyExtension).CalcBalances(PaymentCuryInfoID, currencyInfo2.CuryInfoID, voucher, adj);
      multiCurrencyExtension.currencyinfobykey.Cache.Delete((object) currencyInfo2);
    }
  }

  public override bool IsDirty => false;

  public APPendingInvoicesEnq()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Documents.Cache.AllowDelete = false;
    this.Documents.Cache.AllowInsert = false;
    this.Documents.Cache.AllowUpdate = false;
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false, Required = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPendingInvoicesEnq.PendingPaymentSummary.branchID> e)
  {
  }

  [PXUIField(DisplayName = "Process Payment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXProcessButton]
  public virtual IEnumerable ProcessPayment(PXAdapter adapter)
  {
    if (this.Documents.Current != null && this.Filter.Current != null)
    {
      APPendingInvoicesEnq.PendingPaymentSummary current1 = this.Documents.Current;
      APPendingInvoicesEnq.PendingInvoiceFilter current2 = this.Filter.Current;
      APPayBills instance = PXGraph.CreateInstance<APPayBills>();
      PayBillsFilter current3 = instance.Filter.Current;
      current3.PayAccountID = current1.PayAccountID;
      current3.PayTypeID = current1.PayTypeID;
      current3.PayDate = current2.PayDate;
      instance.Filter.Update(current3);
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (ProcessPayment));
    }
    return (IEnumerable) this.Filter.Select();
  }

  public static void Aggregate(
    APPendingInvoicesEnq.PendingPaymentSummary aRes,
    PXResult<APAdjust, APInvoice> aSrc,
    System.DateTime? aPayDate)
  {
    APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary1 = aRes;
    Decimal? nullable1 = pendingPaymentSummary1.DocBal;
    Decimal? docBal = ((APAdjust) aSrc).DocBal;
    pendingPaymentSummary1.DocBal = nullable1.HasValue & docBal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + docBal.GetValueOrDefault()) : new Decimal?();
    APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary2 = aRes;
    Decimal? nullable2 = pendingPaymentSummary2.CuryDocBal;
    nullable1 = ((APAdjust) aSrc).CuryDocBal;
    pendingPaymentSummary2.CuryDocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary3 = aRes;
    int? nullable3 = pendingPaymentSummary3.DocCount;
    pendingPaymentSummary3.DocCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
    aRes.PayDate = aPayDate;
    System.DateTime? nullable4 = ((APInvoice) aSrc).DueDate;
    System.DateTime? nullable5 = aPayDate;
    if ((nullable4.HasValue & nullable5.HasValue ? (nullable4.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary4 = aRes;
      nullable3 = pendingPaymentSummary4.OverdueDocCount;
      pendingPaymentSummary4.OverdueDocCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary5 = aRes;
      nullable1 = pendingPaymentSummary5.OverdueDocBal;
      nullable2 = ((APAdjust) aSrc).DocBal;
      pendingPaymentSummary5.OverdueDocBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary6 = aRes;
      nullable2 = pendingPaymentSummary6.OverdueCuryDocBal;
      nullable1 = ((APAdjust) aSrc).CuryDocBal;
      pendingPaymentSummary6.OverdueCuryDocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    System.DateTime? nullable6 = ((APInvoice) aSrc).DiscDate;
    nullable4 = aPayDate;
    if ((nullable6.HasValue & nullable4.HasValue ? (nullable6.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary7 = aRes;
      nullable3 = pendingPaymentSummary7.LostDiscCount;
      pendingPaymentSummary7.LostDiscCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary8 = aRes;
      nullable1 = pendingPaymentSummary8.LostDiscBal;
      nullable2 = ((APAdjust) aSrc).DiscBal;
      pendingPaymentSummary8.LostDiscBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary9 = aRes;
      nullable2 = pendingPaymentSummary9.LostCuryDiscBal;
      nullable1 = ((APAdjust) aSrc).CuryDiscBal;
      pendingPaymentSummary9.LostCuryDiscBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary10 = aRes;
      nullable3 = pendingPaymentSummary10.ValidDiscCount;
      pendingPaymentSummary10.ValidDiscCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary11 = aRes;
      nullable1 = pendingPaymentSummary11.ValidDiscBal;
      nullable2 = ((APAdjust) aSrc).DiscBal;
      pendingPaymentSummary11.ValidDiscBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary12 = aRes;
      nullable2 = pendingPaymentSummary12.ValidCuryDiscBal;
      nullable1 = ((APAdjust) aSrc).CuryDiscBal;
      pendingPaymentSummary12.ValidCuryDiscBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary13 = aRes;
    nullable1 = aRes.LostDiscBal;
    nullable2 = aRes.ValidDiscBal;
    Decimal? nullable7 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    pendingPaymentSummary13.DiscBal = nullable7;
    APPendingInvoicesEnq.PendingPaymentSummary pendingPaymentSummary14 = aRes;
    nullable2 = aRes.LostCuryDiscBal;
    nullable1 = aRes.ValidCuryDiscBal;
    Decimal? nullable8 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    pendingPaymentSummary14.CuryDiscBal = nullable8;
    nullable4 = aRes.MaxPayDate;
    if (nullable4.HasValue)
    {
      nullable4 = ((APInvoice) aSrc).PayDate;
      nullable6 = aRes.MaxPayDate;
      if ((nullable4.HasValue & nullable6.HasValue ? (nullable4.GetValueOrDefault() > nullable6.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        goto label_8;
    }
    aRes.MaxPayDate = ((APInvoice) aSrc).PayDate;
label_8:
    nullable6 = aRes.MinPayDate;
    if (nullable6.HasValue)
    {
      nullable6 = ((APInvoice) aSrc).PayDate;
      nullable4 = aRes.MinPayDate;
      if ((nullable6.HasValue & nullable4.HasValue ? (nullable6.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    aRes.MinPayDate = ((APInvoice) aSrc).PayDate;
  }

  [Serializable]
  public class PendingInvoiceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected System.DateTime? _PayDate;
    protected int? _PayAccountID;
    protected string _PayTypeID;
    protected Decimal? _Balance;
    protected Decimal? _CuryBalance;
    protected string _CuryID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Pay Date", Visibility = PXUIVisibility.Visible)]
    public virtual System.DateTime? PayDate
    {
      get => this._PayDate;
      set => this._PayDate = value;
    }

    [CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible)]
    [PXDefault]
    public virtual int? PayAccountID
    {
      get => this._PayAccountID;
      set => this._PayAccountID = value;
    }

    [PXDefault]
    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>>>))]
    public virtual string PayTypeID
    {
      get => this._PayTypeID;
      set => this._PayTypeID = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXUIField(DisplayName = "Total Due", Enabled = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCury(typeof (APPendingInvoicesEnq.PendingInvoiceFilter.curyID))]
    [PXUIField(DisplayName = "Total Due", Enabled = false)]
    public virtual Decimal? CuryBalance
    {
      get => this._CuryBalance;
      set => this._CuryBalance = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency", Enabled = false)]
    [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.payDate>
    {
    }

    public abstract class payAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.payAccountID>
    {
    }

    public abstract class payTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.payTypeID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.balance>
    {
    }

    public abstract class curyBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.curyBalance>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingInvoiceFilter.curyID>
    {
    }
  }

  [Serializable]
  public class PendingPaymentSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _PayAccountID;
    protected string _PayTypeID;
    protected string _CuryID;
    protected long? _CuryInfoID;
    protected Decimal? _CuryDocBal;
    protected Decimal? _DocBal;
    protected Decimal? _CuryDiscBal;
    protected Decimal? _DiscBal;
    protected System.DateTime? _MinPayDate;
    protected System.DateTime? _PayDate;
    protected System.DateTime? _MaxPayDate;
    protected int? _DocCount;
    protected int? _OverdueDocCount;
    protected Decimal? _OverdueDocBal;
    protected Decimal? _OverdueCuryDocBal;
    protected int? _ValidDiscCount;
    protected Decimal? _ValidDiscBal;
    protected Decimal? _ValidCuryDiscBal;
    protected int? _LostDiscCount;
    protected Decimal? _LostDiscBal;
    protected Decimal? _LostCuryDiscBal;
    protected int? _BranchID;

    public PendingPaymentSummary() => this.ClearValues();

    [CashAccount(DisplayName = "Cash Account", DescriptionField = typeof (PX.Objects.CA.CashAccount.descr), Visibility = PXUIVisibility.SelectorVisible, IsKey = true)]
    public virtual int? PayAccountID
    {
      get => this._PayAccountID;
      set => this._PayAccountID = value;
    }

    [PXDBString(5, IsUnicode = true)]
    public virtual string AccountBaseCuryID { get; set; }

    [PXDBString(10, IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    public virtual string PayTypeID
    {
      get => this._PayTypeID;
      set => this._PayTypeID = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBLong]
    [PX.Objects.CM.Extensions.CurrencyInfo]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PX.Objects.CM.Extensions.PXCurrency(typeof (APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID), typeof (APPendingInvoicesEnq.PendingPaymentSummary.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Amount", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? CuryDocBal
    {
      get => this._CuryDocBal;
      set => this._CuryDocBal = value;
    }

    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount", Visible = false)]
    public virtual Decimal? DocBal
    {
      get => this._DocBal;
      set => this._DocBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID), typeof (APPendingInvoicesEnq.PendingPaymentSummary.discBal), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Amount", Visible = false)]
    public virtual Decimal? CuryDiscBal
    {
      get => this._CuryDiscBal;
      set => this._CuryDiscBal = value;
    }

    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Amount", Visible = false)]
    public virtual Decimal? DiscBal
    {
      get => this._DiscBal;
      set => this._DiscBal = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Min. Pay Date", Visibility = PXUIVisibility.Visible)]
    public virtual System.DateTime? MinPayDate
    {
      get => this._MinPayDate;
      set => this._MinPayDate = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Pay Date")]
    public virtual System.DateTime? PayDate
    {
      get => this._PayDate;
      set => this._PayDate = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Max. Pay Date")]
    public virtual System.DateTime? MaxPayDate
    {
      get => this._MaxPayDate;
      set => this._MaxPayDate = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Documents", Visible = true)]
    public virtual int? DocCount
    {
      get => this._DocCount;
      set => this._DocCount = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Overdue Documents", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual int? OverdueDocCount
    {
      get => this._OverdueDocCount;
      set => this._OverdueDocCount = value;
    }

    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overdue Documents Amount", Visible = false)]
    public virtual Decimal? OverdueDocBal
    {
      get => this._OverdueDocBal;
      set => this._OverdueDocBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID), typeof (APPendingInvoicesEnq.PendingPaymentSummary.overdueDocBal), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overdue Documents Amount", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? OverdueCuryDocBal
    {
      get => this._OverdueCuryDocBal;
      set => this._OverdueCuryDocBal = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Valid Discount Documents", Visible = true)]
    public virtual int? ValidDiscCount
    {
      get => this._ValidDiscCount;
      set => this._ValidDiscCount = value;
    }

    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Discount Valid", Visible = false)]
    public virtual Decimal? ValidDiscBal
    {
      get => this._ValidDiscBal;
      set => this._ValidDiscBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID), typeof (APPendingInvoicesEnq.PendingPaymentSummary.validDiscBal), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Valid Discount Amount", Visible = true)]
    public virtual Decimal? ValidCuryDiscBal
    {
      get => this._ValidCuryDiscBal;
      set => this._ValidCuryDiscBal = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Lost Discount Documents", Visible = false)]
    public virtual int? LostDiscCount
    {
      get => this._LostDiscCount;
      set => this._LostDiscCount = value;
    }

    [PX.Objects.CM.Extensions.PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Lost Discounts", Visible = false)]
    public virtual Decimal? LostDiscBal
    {
      get => this._LostDiscBal;
      set => this._LostDiscBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID), typeof (APPendingInvoicesEnq.PendingPaymentSummary.lostDiscBal), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Lost Discount Amount", Visible = true)]
    public virtual Decimal? LostCuryDiscBal
    {
      get => this._LostCuryDiscBal;
      set => this._LostCuryDiscBal = value;
    }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    protected virtual void ClearValues()
    {
      this._DocCount = new int?(0);
      this._CuryDocBal = new Decimal?(0M);
      this._DocBal = new Decimal?(0M);
      this._DiscBal = new Decimal?(0M);
      this._CuryDiscBal = new Decimal?(0M);
      this._LostDiscCount = new int?(0);
      this._LostDiscBal = new Decimal?(0M);
      this._LostCuryDiscBal = new Decimal?(0M);
      this._OverdueDocCount = new int?(0);
      this._OverdueDocBal = new Decimal?(0M);
      this._OverdueCuryDocBal = new Decimal?(0M);
      this._ValidDiscCount = new int?(0);
      this._ValidDiscBal = new Decimal?(0M);
      this._ValidCuryDiscBal = new Decimal?(0M);
    }

    public abstract class payAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.payAccountID>
    {
    }

    public abstract class accountBaseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.accountBaseCuryID>
    {
    }

    public abstract class payTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.payTypeID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.curyInfoID>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.docBal>
    {
    }

    public abstract class curyDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.curyDiscBal>
    {
    }

    public abstract class discBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.discBal>
    {
    }

    public abstract class minPayDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.minPayDate>
    {
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.payDate>
    {
    }

    public abstract class maxPayDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.maxPayDate>
    {
    }

    public abstract class docCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.docCount>
    {
    }

    public abstract class overdueDocCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.overdueDocCount>
    {
    }

    public abstract class overdueDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.overdueDocBal>
    {
    }

    public abstract class overdueCuryDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.overdueCuryDocBal>
    {
    }

    public abstract class validDiscCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.validDiscCount>
    {
    }

    public abstract class validDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.validDiscBal>
    {
    }

    public abstract class validCuryDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.validCuryDiscBal>
    {
    }

    public abstract class lostDiscCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.lostDiscCount>
    {
    }

    public abstract class lostDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.lostDiscBal>
    {
    }

    public abstract class lostCuryDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.lostCuryDiscBal>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APPendingInvoicesEnq.PendingPaymentSummary.branchID>
    {
    }
  }

  public struct CashAcctKey(int aAcctID, string aPayTypeID) : 
    IComparable<APPendingInvoicesEnq.CashAcctKey>
  {
    public int AccountID = aAcctID;
    public string PaymentMethodID = aPayTypeID;

    int IComparable<APPendingInvoicesEnq.CashAcctKey>.CompareTo(
      APPendingInvoicesEnq.CashAcctKey other)
    {
      return this.AccountID == other.AccountID ? this.PaymentMethodID.CompareTo(other.PaymentMethodID) : System.Math.Sign(this.AccountID - other.AccountID);
    }
  }

  public class APInvoiceKey(string aFirst, string aSecond) : Pair<string, string>(aFirst, aSecond)
  {
  }

  public class MultiCurrency : 
    APMultiCurrencyGraph<APPendingInvoicesEnq, APPendingInvoicesEnq.PendingInvoiceFilter>
  {
    protected override string DocumentStatus => "N";

    protected override CurySource CurrentSourceSelect() => (CurySource) null;

    protected override MultiCurrencyGraph<APPendingInvoicesEnq, APPendingInvoicesEnq.PendingInvoiceFilter>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APPendingInvoicesEnq, APPendingInvoicesEnq.PendingInvoiceFilter>.DocumentMapping(typeof (APPendingInvoicesEnq.PendingInvoiceFilter))
      {
        DocumentDate = typeof (APPendingInvoicesEnq.PendingInvoiceFilter.payDate),
        BAccountID = typeof (APPendingInvoicesEnq.PendingInvoiceFilter.payAccountID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Documents
      };
    }

    protected override void _(PX.Data.Events.FieldSelecting<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
    {
    }
  }
}
