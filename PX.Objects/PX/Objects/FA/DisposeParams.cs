// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DisposeParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("Disposal Info")]
[Serializable]
public class DisposeParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _DisposalDate;
  protected 
  #nullable disable
  string _DisposalPeriodID;
  protected Decimal? _DisposalAmt;
  protected int? _DisposalMethodID;
  protected int? _DisposalAccountID;
  protected int? _DisposalSubID;
  protected string _Reason;

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Disposal Date")]
  public virtual DateTime? DisposalDate
  {
    get => this._DisposalDate;
    set => this._DisposalDate = value;
  }

  [PXUIField(DisplayName = "Disposal Period")]
  [FABookPeriodSelector(typeof (Search2<FABookPeriod.finPeriodID, InnerJoin<FABook, On<FABookPeriod.bookID, Equal<FABook.bookID>>, LeftJoin<FABookBalance, On<FABookBalance.updateGL, Equal<True>, And<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookPeriod.bookID, Equal<FABookBalance.bookID>>>>, LeftJoin<FinPeriod, On<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>, And<FABookPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<FABook.updateGL, Equal<True>>>>>>>, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And2<Where<FinPeriod.finPeriodID, GreaterEqual<FABookBalance.lastDeprPeriod>, Or<FABookBalance.lastDeprPeriod, IsNull>>, And<Where<FinPeriod.finPeriodID, IsNotNull, And<FinPeriod.fAClosed, NotEqual<True>, Or<FABook.updateGL, NotEqual<True>>>>>>>>), null, null, null, false, null, typeof (DisposeParams.disposalDate), typeof (FixedAsset.branchID), null, null, null, ReportParametersFlag.None)]
  public virtual string DisposalPeriodID
  {
    get => this._DisposalPeriodID;
    set => this._DisposalPeriodID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Proceeds Amount")]
  public virtual Decimal? DisposalAmt
  {
    get => this._DisposalAmt;
    set => this._DisposalAmt = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (Search<FADisposalMethod.disposalMethodID>), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXUIField(DisplayName = "Disposal Method", Required = true)]
  public virtual int? DisposalMethodID
  {
    get => this._DisposalMethodID;
    set => this._DisposalMethodID = value;
  }

  [PXDefault(typeof (Coalesce<Search<FixedAsset.disposalAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>, Coalesce<Search<FADisposalMethod.proceedsAcctID, Where<FADisposalMethod.disposalMethodID, Equal<Current<DisposeParams.disposalMethodID>>>>, Search<FASetup.proceedsAcctID>>>))]
  [Account(null, typeof (Search2<PX.Objects.GL.Account.accountID, LeftJoin<CashAccount, On<CashAccount.accountID, Equal<PX.Objects.GL.Account.accountID>>>, Where<Match<Current<AccessInfo.userName>>>>), DisplayName = "Proceeds Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXRestrictor(typeof (Where<CashAccount.cashAccountID, IsNull, Or<CashAccount.branchID, Equal<Current<FixedAsset.branchID>>>>), "The {0} account cannot be selected because it is linked to a cash account with a branch that differs from the asset's branch.", new Type[] {typeof (PX.Objects.GL.Account.accountCD)})]
  public virtual int? DisposalAccountID
  {
    get => this._DisposalAccountID;
    set => this._DisposalAccountID = value;
  }

  [PXDefault(typeof (Coalesce<Search<FixedAsset.disposalSubID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>, Coalesce<Search<FADisposalMethod.proceedsSubID, Where<FADisposalMethod.disposalMethodID, Equal<Current<DisposeParams.disposalMethodID>>>>, Search<FASetup.proceedsSubID>>>))]
  [SubAccount(typeof (DisposeParams.disposalAccountID), typeof (FixedAsset.branchID), false, DisplayName = "Proceeds Sub.", DescriptionField = typeof (Sub.description))]
  public virtual int? DisposalSubID
  {
    get => this._DisposalSubID;
    set => this._DisposalSubID = value;
  }

  [PXDBString]
  [PXDefault("S")]
  [PXUIField(DisplayName = "Before Disposal")]
  [DisposeParams.actionBeforeDisposal.List]
  public virtual string ActionBeforeDisposal { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Reason")]
  public virtual string Reason
  {
    get => this._Reason;
    set => this._Reason = value;
  }

  public abstract class disposalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DisposeParams.disposalDate>
  {
  }

  public abstract class disposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DisposeParams.disposalPeriodID>
  {
  }

  public abstract class disposalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DisposeParams.disposalAmt>
  {
  }

  public abstract class disposalMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DisposeParams.disposalMethodID>
  {
  }

  public abstract class disposalAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DisposeParams.disposalAccountID>
  {
  }

  public abstract class disposalSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DisposeParams.disposalSubID>
  {
  }

  public abstract class actionBeforeDisposal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DisposeParams.actionBeforeDisposal>
  {
    public const string Depreciate = "D";
    public const string Suspend = "S";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "D", "S" }, new string[2]
        {
          "Depreciate",
          "Suspend"
        })
      {
      }
    }

    public class depreciate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      DisposeParams.actionBeforeDisposal.depreciate>
    {
      public depreciate()
        : base("D")
      {
      }
    }

    public class suspend : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      DisposeParams.actionBeforeDisposal.suspend>
    {
      public suspend()
        : base("S")
      {
      }
    }
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DisposeParams.reason>
  {
  }
}
