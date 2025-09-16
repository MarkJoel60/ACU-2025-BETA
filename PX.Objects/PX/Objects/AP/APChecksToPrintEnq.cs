// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APChecksToPrintEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
[Serializable]
public class APChecksToPrintEnq : PXGraph<
#nullable disable
APChecksToPrintEnq>
{
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXCancel<APChecksToPrintEnq.DocFilter> Cancel;
  public PXAction<APChecksToPrintEnq.DocFilter> processPayment;
  public PXAction<APChecksToPrintEnq.DocFilter> viewCashAccount;
  public PXFilter<APChecksToPrintEnq.DocFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<APChecksToPrintEnq.CheckSummary> Documents;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;

  public APChecksToPrintEnq()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Documents.Cache.AllowDelete = false;
    this.Documents.Cache.AllowInsert = false;
    this.Documents.Cache.AllowUpdate = false;
  }

  public override bool IsDirty => false;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false, Required = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APChecksToPrintEnq.CheckSummary.branchID> e)
  {
  }

  [PXUIField(DisplayName = "Print Checks", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXProcessButton]
  public virtual IEnumerable ProcessPayment(PXAdapter adapter)
  {
    if (this.Documents.Current != null)
    {
      APChecksToPrintEnq.CheckSummary current1 = this.Documents.Current;
      APPrintChecks instance = PXGraph.CreateInstance<APPrintChecks>();
      PrintChecksFilter current2 = instance.Filter.Current;
      current2.PayAccountID = current1.PayAccountID;
      current2.PayTypeID = current1.PayTypeID;
      current2.CuryID = current1.CuryID;
      instance.Filter.Update(current2);
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (ProcessPayment));
    }
    return adapter.Get();
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewCashAccount(PXAdapter adapter)
  {
    if (this.Documents.Current != null)
    {
      CashAccountMaint instance = PXGraph.CreateInstance<CashAccountMaint>();
      instance.CashAccount.Current = (PX.Objects.CA.CashAccount) instance.CashAccount.Search<PX.Objects.CA.CashAccount.cashAccountID>((object) this.Documents.Current.PayAccountID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "CashAccount");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void DocFilter_PayTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.Filter.Cache.SetDefaultExt<APChecksToPrintEnq.DocFilter.payAccountID>(e.Row);
  }

  public virtual IEnumerable filter()
  {
    if (this.Filter.Cache.Current is APChecksToPrintEnq.DocFilter current)
    {
      current.Balance = new Decimal?(0M);
      current.CuryBalance = new Decimal?(0M);
      current.CuryID = (string) null;
      string str = (string) null;
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = true;
      foreach (PXResult<APChecksToPrintEnq.CheckSummary> pxResult in this.Documents.Select())
      {
        APChecksToPrintEnq.CheckSummary checkSummary = (APChecksToPrintEnq.CheckSummary) pxResult;
        APChecksToPrintEnq.DocFilter docFilter1 = current;
        Decimal? nullable1 = docFilter1.Balance;
        Decimal? nullable2 = checkSummary.DocBal;
        docFilter1.Balance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        if (flag3)
        {
          current.CuryID = checkSummary.CuryID;
          str = checkSummary.AccountBaseCuryID;
        }
        else
        {
          if ((!string.IsNullOrEmpty(current.CuryID) || !string.IsNullOrEmpty(checkSummary.CuryID)) && !(current.CuryID == checkSummary.CuryID))
            flag1 = false;
          if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>() && str != checkSummary.AccountBaseCuryID)
            flag2 = false;
        }
        if (flag1)
        {
          APChecksToPrintEnq.DocFilter docFilter2 = current;
          nullable2 = docFilter2.CuryBalance;
          nullable1 = checkSummary.CuryDocBal;
          docFilter2.CuryBalance = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        }
        flag3 = false;
      }
      bool flag4 = !string.IsNullOrEmpty(current.CuryID);
      current.CuryID = !flag2 || flag1 & flag4 ? current.CuryID : str;
      PXUIFieldAttribute.SetVisible<APChecksToPrintEnq.DocFilter.curyID>(this.Filter.Cache, (object) current, flag4 && (flag1 || !flag1 & flag2));
      PXUIFieldAttribute.SetVisible<APChecksToPrintEnq.DocFilter.curyBalance>(this.Filter.Cache, (object) current, flag1 & flag4);
      PXUIFieldAttribute.SetVisible<APChecksToPrintEnq.DocFilter.balance>(this.Filter.Cache, (object) current, flag2 && !(flag1 & flag4));
    }
    yield return this.Filter.Cache.Current;
    this.Filter.Cache.IsDirty = false;
  }

  public virtual IEnumerable documents()
  {
    APChecksToPrintEnq.DocFilter current = this.Filter.Current;
    Dictionary<APChecksToPrintEnq.CashAcctKey, APChecksToPrintEnq.CheckSummary> dictionary = new Dictionary<APChecksToPrintEnq.CashAcctKey, APChecksToPrintEnq.CheckSummary>();
    System.DateTime? payDate;
    int num;
    if (current == null)
    {
      num = 1;
    }
    else
    {
      payDate = current.PayDate;
      num = !payDate.HasValue ? 1 : 0;
    }
    if (num != 0)
      return (IEnumerable) dictionary.Values;
    PXSelectBase<APChecksToPrintEnq.APPaymentExt> pxSelectBase = (PXSelectBase<APChecksToPrintEnq.APPaymentExt>) new PXSelectJoin<APChecksToPrintEnq.APPaymentExt, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<APPayment.cashAccountID>>, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<APPayment.paymentMethodID>>, LeftJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<APPayment.refNbr>>>>>>>>, Where<APChecksToPrintEnq.APPaymentExt.released, Equal<False>, And<APPayment.docType, NotEqual<APDocType.prepayment>, And<APPayment.docType, NotEqual<APDocType.refund>, And<APChecksToPrintEnq.APPaymentExt.openDoc, Equal<True>, And2<Match<Vendor, Current<AccessInfo.userName>>, PX.Data.And<Where2<Where<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment, Equal<True>, And<CABatchDetail.batchNbr, PX.Data.IsNull>>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment, Equal<False>, And<PX.Objects.CA.PaymentMethod.aPPrintChecks, Equal<True>, And<APPayment.printed, Equal<False>>>>>>>>>>>>, OrderBy<Asc<APChecksToPrintEnq.APPaymentExt.docType, Asc<APChecksToPrintEnq.APPaymentExt.refNbr>>>>((PXGraph) this);
    payDate = current.PayDate;
    if (payDate.HasValue)
      pxSelectBase.WhereAnd<Where<APChecksToPrintEnq.APPaymentExt.docDate, LessEqual<Current<APChecksToPrintEnq.DocFilter.payDate>>>>();
    int? nullable = current.PayAccountID;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<APPayment.cashAccountID, Equal<Current<APChecksToPrintEnq.DocFilter.payAccountID>>>>();
    if (current.PayTypeID != null)
      pxSelectBase.WhereAnd<Where<APPayment.paymentMethodID, Equal<Current<APChecksToPrintEnq.DocFilter.payTypeID>>>>();
    APChecksToPrintEnq.APPaymentKey apPaymentKey = (APChecksToPrintEnq.APPaymentKey) null;
    foreach (PXResult<APChecksToPrintEnq.APPaymentExt, PX.Objects.CA.CashAccount, Vendor, PX.Objects.CA.PaymentMethod, CABatchDetail> pxResult in pxSelectBase.Select())
    {
      APChecksToPrintEnq.APPaymentExt aSrc = (APChecksToPrintEnq.APPaymentExt) pxResult;
      PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) pxResult;
      APChecksToPrintEnq.APPaymentKey other = new APChecksToPrintEnq.APPaymentKey(aSrc.DocType, aSrc.RefNbr);
      if (apPaymentKey == null || apPaymentKey.CompareTo((Pair<string, string>) other) != 0)
      {
        apPaymentKey = other;
        APChecksToPrintEnq.CashAcctKey key;
        ref APChecksToPrintEnq.CashAcctKey local = ref key;
        nullable = aSrc.CashAccountID;
        int aAccountID = nullable.Value;
        string paymentMethodId = aSrc.PaymentMethodID;
        local = new APChecksToPrintEnq.CashAcctKey(aAccountID, paymentMethodId);
        APChecksToPrintEnq.CheckSummary aRes;
        if (!dictionary.ContainsKey(key))
        {
          aRes = new APChecksToPrintEnq.CheckSummary();
          aRes.PayAccountID = aSrc.CashAccountID;
          aRes.AccountBaseCuryID = cashAccount.BaseCuryID;
          aRes.PayTypeID = aSrc.PaymentMethodID;
          aRes.CuryID = cashAccount.CuryID;
          aRes.CuryInfoID = aSrc.CuryInfoID;
          aRes.BranchID = aSrc.BranchID;
          dictionary[key] = aRes;
        }
        else
          aRes = dictionary[key];
        APChecksToPrintEnq.Aggregate(aRes, aSrc, current.PayDate);
      }
    }
    return (IEnumerable) dictionary.Values;
  }

  protected static void Aggregate(
    APChecksToPrintEnq.CheckSummary aRes,
    APChecksToPrintEnq.APPaymentExt aSrc,
    System.DateTime? aPayDate)
  {
    APChecksToPrintEnq.CheckSummary checkSummary1 = aRes;
    Decimal? nullable1 = checkSummary1.DocBal;
    Decimal? origDocAmt = aSrc.OrigDocAmt;
    checkSummary1.DocBal = nullable1.HasValue & origDocAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + origDocAmt.GetValueOrDefault()) : new Decimal?();
    APChecksToPrintEnq.CheckSummary checkSummary2 = aRes;
    Decimal? nullable2 = checkSummary2.CuryDocBal;
    nullable1 = aSrc.CuryOrigDocAmt;
    checkSummary2.CuryDocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    APChecksToPrintEnq.CheckSummary checkSummary3 = aRes;
    int? nullable3 = checkSummary3.DocCount;
    checkSummary3.DocCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
    aRes.PayDate = aPayDate;
    System.DateTime? nullable4 = aSrc.DocDate;
    System.DateTime? nullable5 = aPayDate;
    if ((nullable4.HasValue & nullable5.HasValue ? (nullable4.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      APChecksToPrintEnq.CheckSummary checkSummary4 = aRes;
      nullable3 = checkSummary4.OverdueDocCount;
      checkSummary4.OverdueDocCount = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
      APChecksToPrintEnq.CheckSummary checkSummary5 = aRes;
      nullable1 = checkSummary5.OverdueDocBal;
      nullable2 = aSrc.OrigDocAmt;
      checkSummary5.OverdueDocBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      APChecksToPrintEnq.CheckSummary checkSummary6 = aRes;
      nullable2 = checkSummary6.OverdueCuryDocBal;
      nullable1 = aSrc.CuryOrigDocAmt;
      checkSummary6.OverdueCuryDocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    System.DateTime? nullable6 = aRes.MaxPayDate;
    if (nullable6.HasValue)
    {
      nullable6 = aSrc.DocDate;
      nullable4 = aRes.MaxPayDate;
      if ((nullable6.HasValue & nullable4.HasValue ? (nullable6.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        goto label_5;
    }
    aRes.MaxPayDate = aSrc.DocDate;
label_5:
    nullable4 = aRes.MinPayDate;
    if (nullable4.HasValue)
    {
      nullable4 = aSrc.DocDate;
      nullable6 = aRes.MinPayDate;
      if ((nullable4.HasValue & nullable6.HasValue ? (nullable4.GetValueOrDefault() < nullable6.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    aRes.MinPayDate = aSrc.DocDate;
  }

  [Serializable]
  public class DocFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _PayTypeID;
    protected int? _PayAccountID;
    protected System.DateTime? _PayDate;
    protected Decimal? _Balance;
    protected Decimal? _CuryBalance;
    protected string _CuryID;

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
    [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>>), "Payment Method '{0}' is not configured to use in Accounts Payable.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
    [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>), "Payment Method '{0}' is inactive.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
    [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.aPPrintChecks, Equal<True>, Or<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment, Equal<True>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
    public virtual string PayTypeID
    {
      get => this._PayTypeID;
      set => this._PayTypeID = value;
    }

    [CashAccount(null, typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APChecksToPrintEnq.DocFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
    public virtual int? PayAccountID
    {
      get => this._PayAccountID;
      set => this._PayAccountID = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Pay Date", Visibility = PXUIVisibility.Visible)]
    public virtual System.DateTime? PayDate
    {
      get => this._PayDate;
      set => this._PayDate = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBBaseCury(null, null)]
    [PXUIField(DisplayName = "Total Due", Enabled = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCury(typeof (APChecksToPrintEnq.DocFilter.curyID))]
    [PXUIField(DisplayName = "Total Due", Enabled = false)]
    public virtual Decimal? CuryBalance
    {
      get => this._CuryBalance;
      set => this._CuryBalance = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency", Enabled = false)]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    public abstract class payTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.payTypeID>
    {
    }

    public abstract class payAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.payAccountID>
    {
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.payDate>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.balance>
    {
    }

    public abstract class curyBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.curyBalance>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.DocFilter.curyID>
    {
    }
  }

  [PXSubstitute(GraphType = typeof (APChecksToPrintEnq))]
  [PXHidden]
  [Serializable]
  public class APPaymentExt : APPayment
  {
    protected System.DateTime? _MinPayDate;
    protected int? _DocCount;

    [PXDBDate(BqlField = typeof (APRegister.docDate))]
    [PXUIField(DisplayName = "Min. Pay Date", Visibility = PXUIVisibility.Visible)]
    public virtual System.DateTime? MinPayDate
    {
      get => this._MinPayDate;
      set => this._MinPayDate = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Documents", Visible = true)]
    public virtual int? DocCount
    {
      get => this._DocCount;
      set => this._DocCount = value;
    }

    public new abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.docDate>
    {
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.refNbr>
    {
    }

    public new abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.openDoc>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.released>
    {
    }

    public new abstract class hold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.hold>
    {
    }

    public abstract class minPayDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.minPayDate>
    {
    }

    public abstract class docCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.APPaymentExt.docCount>
    {
    }
  }

  [Serializable]
  public class CheckSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _PayAccountID;
    protected string _PayTypeID;
    protected string _CuryID;
    protected long? _CuryInfoID;
    protected Decimal? _CuryDocBal;
    protected Decimal? _DocBal;
    protected System.DateTime? _MinPayDate;
    protected System.DateTime? _PayDate;
    protected System.DateTime? _MaxPayDate;
    protected int? _DocCount;
    protected int? _OverdueDocCount;
    protected Decimal? _OverdueDocBal;
    protected Decimal? _OverdueCuryDocBal;
    protected int? _BranchID;

    public CheckSummary() => this.ClearValues();

    [CashAccount(DescriptionField = typeof (PX.Objects.CA.CashAccount.descr), Visibility = PXUIVisibility.SelectorVisible, IsKey = true)]
    public virtual int? PayAccountID
    {
      get => this._PayAccountID;
      set => this._PayAccountID = value;
    }

    [PXDBString(5, IsUnicode = true)]
    public virtual string AccountBaseCuryID { get; set; }

    [PXDBString(10, IsUnicode = true, IsKey = true)]
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
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBLong]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APChecksToPrintEnq.CheckSummary.curyInfoID), typeof (APChecksToPrintEnq.CheckSummary.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Amount", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? CuryDocBal
    {
      get => this._CuryDocBal;
      set => this._CuryDocBal = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount", Visible = false)]
    public virtual Decimal? DocBal
    {
      get => this._DocBal;
      set => this._DocBal = value;
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
    [PXUIField(DisplayName = "Max.Pay Date")]
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
    [PXUIField(DisplayName = "Overdue Docs", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual int? OverdueDocCount
    {
      get => this._OverdueDocCount;
      set => this._OverdueDocCount = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overdue Docs. Amount", Visible = false)]
    public virtual Decimal? OverdueDocBal
    {
      get => this._OverdueDocBal;
      set => this._OverdueDocBal = value;
    }

    [PXCurrency(typeof (APChecksToPrintEnq.CheckSummary.curyInfoID), typeof (APChecksToPrintEnq.CheckSummary.overdueDocBal), BaseCalc = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overdue Docs. Amount", Visible = true, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? OverdueCuryDocBal
    {
      get => this._OverdueCuryDocBal;
      set => this._OverdueCuryDocBal = value;
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

    protected void ClearValues()
    {
      this.DocCount = new int?(0);
      this.CuryDocBal = new Decimal?(0M);
      this.DocBal = new Decimal?(0M);
      this.OverdueDocCount = new int?(0);
      this.OverdueDocBal = new Decimal?(0M);
      this.OverdueCuryDocBal = new Decimal?(0M);
    }

    public abstract class payAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.payAccountID>
    {
    }

    public abstract class accountBaseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.accountBaseCuryID>
    {
    }

    public abstract class payTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.payTypeID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.curyInfoID>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.docBal>
    {
    }

    public abstract class minPayDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.minPayDate>
    {
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.payDate>
    {
    }

    public abstract class maxPayDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.maxPayDate>
    {
    }

    public abstract class docCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.docCount>
    {
    }

    public abstract class overdueDocCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.overdueDocCount>
    {
    }

    public abstract class overdueDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.overdueDocBal>
    {
    }

    public abstract class overdueCuryDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.overdueCuryDocBal>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APChecksToPrintEnq.CheckSummary.branchID>
    {
    }
  }

  protected struct CashAcctKey(int aAccountID, string aPayTypeID) : 
    IComparable<APChecksToPrintEnq.CashAcctKey>
  {
    public int AccountID = aAccountID;
    public string PaymentTypeID = aPayTypeID;

    int IComparable<APChecksToPrintEnq.CashAcctKey>.CompareTo(APChecksToPrintEnq.CashAcctKey other)
    {
      return this.AccountID == other.AccountID ? this.PaymentTypeID.CompareTo(other.PaymentTypeID) : System.Math.Sign(this.AccountID - other.AccountID);
    }
  }

  protected class APPaymentKey(string aFirst, string aSecond) : Pair<string, string>(aFirst, aSecond)
  {
  }
}
