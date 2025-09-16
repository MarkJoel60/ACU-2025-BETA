// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AccBalanceByAssetInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class AccBalanceByAssetInq : PXGraph<
#nullable disable
AccBalanceByAssetInq>
{
  public PXCancel<AccBalanceByAssetInq.AccBalanceByAssetFilter> Cancel;
  public PXFilter<AccBalanceByAssetInq.AccBalanceByAssetFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<AccBalanceByAssetInq.Amounts> Amts;
  public PXAction<AccBalanceByAssetInq.AccBalanceByAssetFilter> viewDetails;

  public AccBalanceByAssetInq()
  {
    ((PXSelectBase) this.Amts).Cache.AllowInsert = false;
    ((PXSelectBase) this.Amts).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Amts).Cache.AllowDelete = false;
  }

  protected virtual IEnumerable filter()
  {
    AccBalanceByAssetInq balanceByAssetInq = this;
    PXCache cache = ((PXGraph) balanceByAssetInq).Caches[typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter)];
    AccBalanceByAssetInq.AccBalanceByAssetFilter current = (AccBalanceByAssetInq.AccBalanceByAssetFilter) cache.Current;
    if (current != null)
    {
      current.Balance = new Decimal?(0M);
      foreach (PXResult<AccBalanceByAssetInq.Amounts> pxResult in ((PXSelectBase<AccBalanceByAssetInq.Amounts>) balanceByAssetInq.Amts).Select(Array.Empty<object>()))
      {
        AccBalanceByAssetInq.Amounts amounts = PXResult<AccBalanceByAssetInq.Amounts>.op_Implicit(pxResult);
        AccBalanceByAssetInq.AccBalanceByAssetFilter balanceByAssetFilter = current;
        Decimal? balance = balanceByAssetFilter.Balance;
        Decimal? itdAmt = amounts.ItdAmt;
        balanceByAssetFilter.Balance = balance.HasValue & itdAmt.HasValue ? new Decimal?(balance.GetValueOrDefault() + itdAmt.GetValueOrDefault()) : new Decimal?();
      }
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  public virtual BqlCommand GetSelectCommand(
    AccBalanceByAssetInq.AccBalanceByAssetFilter filter)
  {
    BqlCommand selectCommand = ((PXSelectBase) new PXSelectJoin<FATran, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FATran.assetID>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<AccBalanceByAssetInq.FALocationHistoryCurrent, On<AccBalanceByAssetInq.FALocationHistoryCurrent.assetID, Equal<FixedAsset.assetID>>, InnerJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FixedAsset.assetID>, And<FALocationHistory.periodID, Equal<AccBalanceByAssetInq.FALocationHistoryCurrent.lastPeriodID>, And<FALocationHistory.revisionID, Equal<AccBalanceByAssetInq.FALocationHistoryCurrent.lastRevisionID>>>>>>>>>, Where<FATran.released, Equal<True>, And<FATran.finPeriodID, LessEqual<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.periodID>>, And<FATran.bookID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.bookID>>, And2<Where<FALocationHistory.fAAccountID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID>>, And<FALocationHistory.fASubID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.subID>>, Or<FALocationHistory.accumulatedDepreciationAccountID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID>>, And<FALocationHistory.accumulatedDepreciationSubID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.subID>>>>>>, And<Where<FATran.debitAccountID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID>>, And<FATran.debitSubID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.subID>>, Or<FATran.creditAccountID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID>>, And<FATran.creditSubID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.subID>>>>>>>>>>>>((PXGraph) this)).View.BqlSelect;
    if (filter.OrganizationID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Current2<AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>();
    if (filter.BranchID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccBalanceByAssetInq.AccBalanceByAssetFilter.branchID>>>>();
    return selectCommand;
  }

  public virtual IEnumerable amts(PXAdapter adapter)
  {
    AccBalanceByAssetInq balanceByAssetInq = this;
    AccBalanceByAssetInq.AccBalanceByAssetFilter current = ((PXSelectBase<AccBalanceByAssetInq.AccBalanceByAssetFilter>) balanceByAssetInq.Filter).Current;
    if (current != null)
    {
      BqlCommand selectCommand = balanceByAssetInq.GetSelectCommand(current);
      Dictionary<int?, AccBalanceByAssetInq.Amounts> dictionary = new Dictionary<int?, AccBalanceByAssetInq.Amounts>();
      AccBalanceByAssetInq graph = balanceByAssetInq;
      foreach (PXResult<FATran, FixedAsset, FADetails, PX.Objects.GL.Branch, AccBalanceByAssetInq.FALocationHistoryCurrent, FALocationHistory> pxResult in selectCommand.CreateView((PXGraph) graph, mergeCache: true).SelectMulti(Array.Empty<object>()))
      {
        FATran faTran = PXResult<FATran, FixedAsset, FADetails, PX.Objects.GL.Branch, AccBalanceByAssetInq.FALocationHistoryCurrent, FALocationHistory>.op_Implicit(pxResult);
        FixedAsset fixedAsset = PXResult<FATran, FixedAsset, FADetails, PX.Objects.GL.Branch, AccBalanceByAssetInq.FALocationHistoryCurrent, FALocationHistory>.op_Implicit(pxResult);
        FADetails faDetails = PXResult<FATran, FixedAsset, FADetails, PX.Objects.GL.Branch, AccBalanceByAssetInq.FALocationHistoryCurrent, FALocationHistory>.op_Implicit(pxResult);
        FALocationHistory faLocationHistory = PXResult<FATran, FixedAsset, FADetails, PX.Objects.GL.Branch, AccBalanceByAssetInq.FALocationHistoryCurrent, FALocationHistory>.op_Implicit(pxResult);
        AccBalanceByAssetInq.Amounts amounts1 = (AccBalanceByAssetInq.Amounts) null;
        if (!dictionary.TryGetValue(fixedAsset.AssetID, out amounts1))
          amounts1 = new AccBalanceByAssetInq.Amounts()
          {
            AssetID = fixedAsset.AssetID,
            Description = fixedAsset.Description,
            Status = faDetails.Status,
            ClassID = fixedAsset.ClassID,
            DepreciateFromDate = faDetails.DepreciateFromDate,
            BranchID = faLocationHistory.BranchID,
            Department = faLocationHistory.Department,
            ItdAmt = new Decimal?(0M),
            YtdAmt = new Decimal?(0M),
            PtdAmt = new Decimal?(0M)
          };
        Decimal? nullable1 = faTran.TranAmt;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        int? nullable2 = faTran.DebitAccountID;
        int? nullable3 = faTran.CreditAccountID;
        Decimal num1;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          nullable3 = faTran.DebitSubID;
          nullable2 = faTran.CreditSubID;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
          {
            num1 = 0M;
            goto label_12;
          }
        }
        nullable2 = faTran.DebitAccountID;
        nullable3 = current.AccountID;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          nullable3 = faTran.DebitSubID;
          nullable2 = current.SubID;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
          {
            num1 = valueOrDefault;
            goto label_12;
          }
        }
        num1 = -valueOrDefault;
label_12:
        Decimal num2 = num1;
        AccBalanceByAssetInq.Amounts amounts2 = amounts1;
        nullable1 = amounts2.ItdAmt;
        Decimal num3 = num2;
        amounts2.ItdAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num3) : new Decimal?();
        AccBalanceByAssetInq.Amounts amounts3 = amounts1;
        nullable1 = amounts3.YtdAmt;
        Decimal num4 = FinPeriodUtils.FinPeriodEqual(current.PeriodID, faTran.FinPeriodID, FinPeriodUtils.FinPeriodComparison.Year) ? num2 : 0M;
        amounts3.YtdAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num4) : new Decimal?();
        AccBalanceByAssetInq.Amounts amounts4 = amounts1;
        nullable1 = amounts4.PtdAmt;
        Decimal num5 = current.PeriodID == faTran.FinPeriodID ? num2 : 0M;
        amounts4.PtdAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num5) : new Decimal?();
        dictionary[fixedAsset.AssetID] = amounts1;
      }
      foreach (AccBalanceByAssetInq.Amounts amounts in dictionary.Values)
      {
        Decimal? nullable = amounts.ItdAmt;
        Decimal num6 = 0M;
        if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
        {
          nullable = amounts.YtdAmt;
          Decimal num7 = 0M;
          if (nullable.GetValueOrDefault() == num7 & nullable.HasValue)
          {
            nullable = amounts.PtdAmt;
            Decimal num8 = 0M;
            if (nullable.GetValueOrDefault() == num8 & nullable.HasValue)
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
    AccBalanceByAssetInq.Amounts current1 = ((PXSelectBase<AccBalanceByAssetInq.Amounts>) this.Amts).Current;
    AccBalanceByAssetInq.AccBalanceByAssetFilter current2 = ((PXSelectBase<AccBalanceByAssetInq.AccBalanceByAssetFilter>) this.Filter).Current;
    if (current1 != null && current2 != null)
    {
      FACostDetailsInq instance = PXGraph.CreateInstance<FACostDetailsInq>();
      PXFilter<AccountFilter> filter = instance.Filter;
      AccountFilter accountFilter1 = new AccountFilter();
      accountFilter1.AssetID = current1.AssetID;
      accountFilter1.EndPeriodID = current2.PeriodID;
      accountFilter1.AccountID = current2.AccountID;
      accountFilter1.SubID = current2.SubID;
      AccountFilter accountFilter2 = accountFilter1;
      ((PXSelectBase<AccountFilter>) filter).Current = accountFilter1;
      AccountFilter accountFilter3 = ((PXSelectBase<AccountFilter>) instance.Filter).Insert(accountFilter2);
      accountFilter3.StartPeriodID = (string) null;
      accountFilter3.BookID = current2.BookID;
      ((PXSelectBase) instance.Filter).Cache.IsDirty = false;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewDetails));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
    return adapter.Get();
  }

  [Serializable]
  public class AccBalanceByAssetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BookID;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _PeriodID;
    protected Decimal? _Balance;

    [Organization(true)]
    [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleCalendarsSupport>>))]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID), typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [PXDBInt]
    [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
    [PXDefault(typeof (Search<FABook.bookID, Where<FABook.updateGL, Equal<True>>>))]
    [PXUIField(DisplayName = "Book", Enabled = false)]
    public virtual int? BookID
    {
      get => this._BookID;
      set => this._BookID = value;
    }

    [PXDefault]
    [Account(DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDefault]
    [SubAccount(typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID), DisplayName = "Subaccount")]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDefault]
    [FABookPeriodExistingInGLSelector(null, null, null, false, null, typeof (AccessInfo.businessDate), typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.branchID), null, typeof (AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID), null)]
    [PXUIField]
    public virtual string PeriodID
    {
      get => this._PeriodID;
      set => this._PeriodID = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBBaseCury(null, null)]
    [PXUIField(DisplayName = "Balance by Assets", Enabled = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    public abstract class organizationID : IBqlField, IBqlOperand
    {
    }

    public abstract class branchID : IBqlField, IBqlOperand
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class bookID : IBqlField, IBqlOperand
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccBalanceByAssetInq.AccBalanceByAssetFilter.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccBalanceByAssetInq.AccBalanceByAssetFilter.subID>
    {
    }

    public abstract class periodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccBalanceByAssetInq.AccBalanceByAssetFilter.periodID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      AccBalanceByAssetInq.AccBalanceByAssetFilter.balance>
    {
    }
  }

  [Serializable]
  public class Amounts : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AssetID;
    protected string _Description;
    protected string _Status;
    protected int? _ClassID;
    protected DateTime? _DepreciateFromDate;
    protected int? _BranchID;
    protected string _Department;
    protected Decimal? _ItdAmt;
    protected Decimal? _YtdAmt;
    protected Decimal? _PtdAmt;

    [PXDBInt(IsKey = true)]
    [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
    [PXUIField(DisplayName = "Asset ID")]
    [PXDefault]
    public virtual int? AssetID
    {
      get => this._AssetID;
      set => this._AssetID = value;
    }

    [PXString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField]
    [PXFieldDescription]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Status", Enabled = false)]
    [FixedAssetStatus.List]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXInt]
    [PXSelector(typeof (Search<FAClass.assetID, Where<FAClass.recordType, Equal<FARecordType.classType>>>), new Type[] {typeof (FAClass.assetCD), typeof (FAClass.assetTypeID), typeof (FAClass.description), typeof (FAClass.usefulLife)}, SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description), CacheGlobal = true)]
    [PXUIField]
    public int? ClassID
    {
      get => this._ClassID;
      set => this._ClassID = value;
    }

    [PXDate]
    [PXUIField(DisplayName = "Placed-in-Service Date")]
    public virtual DateTime? DepreciateFromDate
    {
      get => this._DepreciateFromDate;
      set => this._DepreciateFromDate = value;
    }

    [Branch(typeof (Search<FixedAsset.branchID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>), null, true, true, true, Required = false)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Department")]
    public virtual string Department
    {
      get => this._Department;
      set => this._Department = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXBaseCury]
    [PXUIField(DisplayName = "Inception to Date", Enabled = false)]
    public virtual Decimal? ItdAmt
    {
      get => this._ItdAmt;
      set => this._ItdAmt = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXBaseCury]
    [PXUIField(DisplayName = "Year to Date", Enabled = false)]
    public virtual Decimal? YtdAmt
    {
      get => this._YtdAmt;
      set => this._YtdAmt = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXBaseCury]
    [PXUIField(DisplayName = "Period to Date", Enabled = false)]
    public virtual Decimal? PtdAmt
    {
      get => this._PtdAmt;
      set => this._PtdAmt = value;
    }

    public abstract class assetID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AccBalanceByAssetInq.Amounts.assetID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.description>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.status>
    {
    }

    public abstract class classID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AccBalanceByAssetInq.Amounts.classID>
    {
    }

    public abstract class depreciateFromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.depreciateFromDate>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.branchID>
    {
    }

    public abstract class department : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.department>
    {
    }

    public abstract class itdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.itdAmt>
    {
    }

    public abstract class ytdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.ytdAmt>
    {
    }

    public abstract class ptdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      AccBalanceByAssetInq.Amounts.ptdAmt>
    {
    }
  }

  [PXProjection(typeof (Select5<FixedAsset, InnerJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FixedAsset.assetID>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>>, Where<FALocationHistory.periodID, LessEqual<CurrentValue<AccBalanceByAssetInq.AccBalanceByAssetFilter.periodID>>>, Aggregate<GroupBy<FALocationHistory.assetID, Max<FALocationHistory.periodID, Max<FALocationHistory.revisionID>>>>>))]
  [PXHidden]
  [Serializable]
  public class FALocationHistoryCurrent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AssetID;
    protected string _LastPeriodID;
    protected int? _LastRevisionID;

    [PXDBInt(IsKey = true, BqlField = typeof (FixedAsset.assetID))]
    [PXDefault]
    public virtual int? AssetID
    {
      get => this._AssetID;
      set => this._AssetID = value;
    }

    [FABookPeriodID(null, null, true, typeof (AccBalanceByAssetInq.FALocationHistoryCurrent.assetID), null, null, null, null, null, BqlField = typeof (FALocationHistory.periodID))]
    [PXDefault]
    public virtual string LastPeriodID
    {
      get => this._LastPeriodID;
      set => this._LastPeriodID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (FALocationHistory.revisionID))]
    [PXDefault(0)]
    public virtual int? LastRevisionID
    {
      get => this._LastRevisionID;
      set => this._LastRevisionID = value;
    }

    public abstract class assetID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccBalanceByAssetInq.FALocationHistoryCurrent.assetID>
    {
    }

    public abstract class lastPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AccBalanceByAssetInq.FALocationHistoryCurrent.lastPeriodID>
    {
    }

    public abstract class lastRevisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AccBalanceByAssetInq.FALocationHistoryCurrent.lastRevisionID>
    {
    }
  }
}
