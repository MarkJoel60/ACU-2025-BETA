// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FixedAssetCostEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class FixedAssetCostEnq : PXGraph<
#nullable disable
FixedAssetCostEnq>
{
  public PXCancel<FixedAssetCostEnq.FixedAssetFilter> Cancel;
  public PXFilter<FixedAssetCostEnq.FixedAssetFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<FixedAssetCostEnq.Amounts> Amts;
  public PXAction<FixedAssetCostEnq.FixedAssetFilter> viewDetails;

  public FixedAssetCostEnq()
  {
    ((PXSelectBase) this.Amts).Cache.AllowInsert = false;
    ((PXSelectBase) this.Amts).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Amts).Cache.AllowDelete = false;
  }

  protected virtual bool IsAccrualAccount<TAccountID, TSubID>(FATran tran, FixedAsset asset)
    where TAccountID : IBqlField
    where TSubID : IBqlField
  {
    int? nullable1 = (int?) ((PXCache) GraphHelper.Caches<FATran>((PXGraph) this)).GetValue<TAccountID>((object) tran);
    int? faAccrualAcctId = asset.FAAccrualAcctID;
    if (!(nullable1.GetValueOrDefault() == faAccrualAcctId.GetValueOrDefault() & nullable1.HasValue == faAccrualAcctId.HasValue))
      return false;
    int? nullable2 = (int?) ((PXCache) GraphHelper.Caches<FATran>((PXGraph) this)).GetValue<TSubID>((object) tran);
    int? faAccrualSubId = asset.FAAccrualSubID;
    return nullable2.GetValueOrDefault() == faAccrualSubId.GetValueOrDefault() & nullable2.HasValue == faAccrualSubId.HasValue;
  }

  public virtual IEnumerable amts(PXAdapter adapter)
  {
    FixedAssetCostEnq fixedAssetCostEnq = this;
    FixedAssetCostEnq.FixedAssetFilter current = ((PXSelectBase<FixedAssetCostEnq.FixedAssetFilter>) fixedAssetCostEnq.Filter).Current;
    if (current != null && current.AssetID.HasValue && current.PeriodID != null)
    {
      FixedAsset asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAssetCostEnq.FixedAssetFilter.assetID>>>>.Config>.Select((PXGraph) fixedAssetCostEnq, Array.Empty<object>()));
      PXSelectBase<FATran> pxSelectBase = (PXSelectBase<FATran>) new PXSelectJoin<FATran, LeftJoin<PX.Objects.GL.Account, On<FATran.debitAccountID, Equal<PX.Objects.GL.Account.accountID>>, LeftJoin<Sub, On<FATran.debitSubID, Equal<Sub.subID>>, LeftJoin<FixedAssetCostEnq.CreditAccount, On<FATran.creditAccountID, Equal<FixedAssetCostEnq.CreditAccount.accountID>>, LeftJoin<FixedAssetCostEnq.CreditSub, On<FATran.creditSubID, Equal<FixedAssetCostEnq.CreditSub.subID>>, LeftJoin<PX.Objects.GL.Branch, On<FATran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FABook, On<FATran.bookID, Equal<FABook.bookID>>, LeftJoin<FABookPeriod, On<FATran.bookID, Equal<FABookPeriod.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FATran.finPeriodID, Equal<FABookPeriod.finPeriodID>>>>>>>>>>>, Where<FATran.assetID, Equal<Current<FixedAssetCostEnq.FixedAssetFilter.assetID>>, And<FATran.finPeriodID, LessEqual<Current<FixedAssetCostEnq.FixedAssetFilter.periodID>>, And<FATran.released, Equal<True>, And<Where<FATran.tranType, NotEqual<FATran.tranType.calculatedPlus>, And<FATran.tranType, NotEqual<FATran.tranType.calculatedMinus>, And<FATran.tranType, NotEqual<FATran.tranType.reconcilliationPlus>, And<FATran.tranType, NotEqual<FATran.tranType.reconcilliationMinus>>>>>>>>>>((PXGraph) fixedAssetCostEnq);
      if (current.BookID.HasValue)
        pxSelectBase.WhereAnd<Where<FATran.bookID, Equal<Current<FixedAssetCostEnq.FixedAssetFilter.bookID>>>>();
      Dictionary<string, FixedAssetCostEnq.Amounts> dictionary = new Dictionary<string, FixedAssetCostEnq.Amounts>();
      foreach (PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        PX.Objects.GL.Account account = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        Sub sub = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        FixedAssetCostEnq.CreditAccount creditAccount = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        FixedAssetCostEnq.CreditSub creditSub = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        FATran tran = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        FABookPeriod faBookPeriod = PXResult<FATran, PX.Objects.GL.Account, Sub, FixedAssetCostEnq.CreditAccount, FixedAssetCostEnq.CreditSub, PX.Objects.GL.Branch, FABook, FABookPeriod>.op_Implicit(pxResult);
        Decimal? nullable1;
        Decimal? nullable2;
        if (!fixedAssetCostEnq.IsAccrualAccount<FATran.debitAccountID, FATran.debitSubID>(tran, asset))
        {
          FixedAssetCostEnq.Amounts amounts1 = (FixedAssetCostEnq.Amounts) null;
          string key = $"{account.AccountCD}{sub.SubCD}";
          if (!dictionary.TryGetValue(key, out amounts1))
            amounts1 = new FixedAssetCostEnq.Amounts()
            {
              BookID = tran.BookID,
              AccountID = tran.DebitAccountID,
              SubID = tran.DebitSubID,
              AcctDescr = account.Description,
              SubDescr = sub.Description,
              BranchID = tran.BranchID,
              ItdAmt = new Decimal?(0M),
              YtdAmt = new Decimal?(0M),
              PtdAmt = new Decimal?(0M)
            };
          FixedAssetCostEnq.Amounts amounts2 = amounts1;
          nullable1 = amounts2.ItdAmt;
          nullable2 = tran.TranAmt;
          amounts2.ItdAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          if (current.PeriodID.Substring(0, 4) == faBookPeriod.FinYear)
          {
            FixedAssetCostEnq.Amounts amounts3 = amounts1;
            nullable2 = amounts3.YtdAmt;
            nullable1 = tran.TranAmt;
            amounts3.YtdAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          }
          if (current.PeriodID == tran.FinPeriodID)
          {
            FixedAssetCostEnq.Amounts amounts4 = amounts1;
            nullable1 = amounts4.PtdAmt;
            nullable2 = tran.TranAmt;
            amounts4.PtdAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          }
          dictionary[key] = amounts1;
        }
        if (!fixedAssetCostEnq.IsAccrualAccount<FATran.creditAccountID, FATran.creditSubID>(tran, asset))
        {
          FixedAssetCostEnq.Amounts amounts5 = (FixedAssetCostEnq.Amounts) null;
          string key = $"{creditAccount.AccountCD}{creditSub.SubCD}";
          if (!dictionary.TryGetValue(key, out amounts5))
            amounts5 = new FixedAssetCostEnq.Amounts()
            {
              BookID = tran.BookID,
              AccountID = tran.CreditAccountID,
              SubID = tran.CreditSubID,
              AcctDescr = creditAccount.Description,
              SubDescr = creditSub.Description,
              BranchID = tran.BranchID,
              ItdAmt = new Decimal?(0M),
              YtdAmt = new Decimal?(0M),
              PtdAmt = new Decimal?(0M)
            };
          FixedAssetCostEnq.Amounts amounts6 = amounts5;
          nullable2 = amounts6.ItdAmt;
          nullable1 = tran.TranAmt;
          amounts6.ItdAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          if (current.PeriodID.Substring(0, 4) == faBookPeriod.FinYear)
          {
            FixedAssetCostEnq.Amounts amounts7 = amounts5;
            nullable1 = amounts7.YtdAmt;
            nullable2 = tran.TranAmt;
            amounts7.YtdAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          if (current.PeriodID == tran.FinPeriodID)
          {
            FixedAssetCostEnq.Amounts amounts8 = amounts5;
            nullable2 = amounts8.PtdAmt;
            nullable1 = tran.TranAmt;
            amounts8.PtdAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          }
          dictionary[key] = amounts5;
        }
      }
      foreach (FixedAssetCostEnq.Amounts amounts in dictionary.Values)
      {
        Decimal? nullable = amounts.ItdAmt;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          nullable = amounts.YtdAmt;
          Decimal num2 = 0M;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            nullable = amounts.PtdAmt;
            Decimal num3 = 0M;
            if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
              continue;
          }
        }
        yield return (object) amounts;
      }
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    FixedAssetCostEnq.Amounts current1 = ((PXSelectBase<FixedAssetCostEnq.Amounts>) this.Amts).Current;
    FixedAssetCostEnq.FixedAssetFilter current2 = ((PXSelectBase<FixedAssetCostEnq.FixedAssetFilter>) this.Filter).Current;
    if (current1 != null && current2 != null)
    {
      FACostDetailsInq instance = PXGraph.CreateInstance<FACostDetailsInq>();
      ((PXSelectBase<AccountFilter>) instance.Filter).Current = new AccountFilter()
      {
        AssetID = current2.AssetID,
        EndPeriodID = current2.PeriodID,
        AccountID = current1.AccountID,
        SubID = current1.SubID
      };
      ((PXSelectBase<AccountFilter>) instance.Filter).Insert(((PXSelectBase<AccountFilter>) instance.Filter).Current);
      ((PXSelectBase<AccountFilter>) instance.Filter).Current.StartPeriodID = (string) null;
      ((PXSelectBase<AccountFilter>) instance.Filter).Current.BookID = current2.BookID;
      ((PXSelectBase) instance.Filter).Cache.IsDirty = false;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewDetails));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
    return adapter.Get();
  }

  [Serializable]
  public class FixedAssetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AssetID;
    protected string _PeriodID;
    protected int? _BookID;

    [PXDBInt]
    [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
    [PXUIField(DisplayName = "Asset ID")]
    [PXDefault]
    public virtual int? AssetID
    {
      get => this._AssetID;
      set => this._AssetID = value;
    }

    [PXDefault]
    [FABookPeriodExistingInGLSelector(null, null, null, false, typeof (FixedAssetCostEnq.FixedAssetFilter.assetID), typeof (AccessInfo.businessDate), null, null, null, null)]
    [PXUIField]
    public virtual string PeriodID
    {
      get => this._PeriodID;
      set => this._PeriodID = value;
    }

    [PXDBInt]
    [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
    [PXDefault(typeof (Search<FABook.bookID, Where<FABook.updateGL, Equal<True>>>))]
    [PXUIField(DisplayName = "Book")]
    public virtual int? BookID
    {
      get => this._BookID;
      set => this._BookID = value;
    }

    public abstract class assetID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FixedAssetCostEnq.FixedAssetFilter.assetID>
    {
    }

    public abstract class periodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FixedAssetCostEnq.FixedAssetFilter.periodID>
    {
    }

    public abstract class bookID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FixedAssetCostEnq.FixedAssetFilter.bookID>
    {
    }
  }

  [Serializable]
  public class Amounts : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BookID;
    protected int? _AccountID;
    protected string _AcctDescr;
    protected int? _SubID;
    protected string _SubDescr;
    protected int? _BranchID;
    protected Decimal? _ItdAmt;
    protected Decimal? _YtdAmt;
    protected Decimal? _PtdAmt;

    [PXDBInt]
    [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
    [PXUIField(DisplayName = "Book")]
    public virtual int? BookID
    {
      get => this._BookID;
      set => this._BookID = value;
    }

    [Account(IsKey = true, DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Account Description")]
    public virtual string AcctDescr
    {
      get => this._AcctDescr;
      set => this._AcctDescr = value;
    }

    [SubAccount(IsKey = true, DisplayName = "Subaccount")]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Subaccount Description", FieldClass = "SUBACCOUNT")]
    public virtual string SubDescr
    {
      get => this._SubDescr;
      set => this._SubDescr = value;
    }

    [Branch(typeof (Search<FixedAsset.branchID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>), null, true, true, true, Required = false)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Inception to Date")]
    public virtual Decimal? ItdAmt
    {
      get => this._ItdAmt;
      set => this._ItdAmt = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Year to Date")]
    public virtual Decimal? YtdAmt
    {
      get => this._YtdAmt;
      set => this._YtdAmt = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period to Date")]
    public virtual Decimal? PtdAmt
    {
      get => this._PtdAmt;
      set => this._PtdAmt = value;
    }

    public abstract class bookID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAssetCostEnq.Amounts.bookID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.accountID>
    {
    }

    public abstract class acctDescr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.acctDescr>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAssetCostEnq.Amounts.subID>
    {
    }

    public abstract class subDescr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.subDescr>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAssetCostEnq.Amounts.branchID>
    {
    }

    public abstract class itdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.itdAmt>
    {
    }

    public abstract class ytdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.ytdAmt>
    {
    }

    public abstract class ptdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FixedAssetCostEnq.Amounts.ptdAmt>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CreditAccount : PX.Objects.GL.Account
  {
    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FixedAssetCostEnq.CreditAccount.accountID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CreditSub : Sub
  {
    public new abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAssetCostEnq.CreditSub.subID>
    {
    }
  }
}
