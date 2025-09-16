// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BudgetPreloadFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.DAC;
using PX.Objects.GL.Descriptor;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class BudgetPreloadFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected short? _PreloadAction;

  [Branch(typeof (BudgetFilter.branchID), null, true, true, true, Required = true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger", Required = true)]
  [PXSelector(typeof (Search5<Ledger.ledgerID, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<Branch.branchID, Equal<Current2<BudgetPreloadFilter.branchID>>, And<Current2<BudgetPreloadFilter.branchID>, IsNotNull>>, Aggregate<GroupBy<Ledger.ledgerID>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr))]
  [PXDefault(typeof (BudgetFilter.compareToLedgerID))]
  public virtual int? LedgerID { get; set; }

  [PXUIField(DisplayName = "Financial Year", Required = true)]
  [PXDBString(4)]
  [GenericFinYearSelector(typeof (Search3<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>), null, typeof (BudgetPreloadFilter.branchID), null, null, false, false, null, false, null)]
  [PXDefault(typeof (BudgetFilter.compareToFinYear))]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [PXShort]
  [PXDefault(100)]
  [PXUIField(DisplayName = "Multiplier (in %)", Required = true)]
  public virtual short? ChangePercent { get; set; }

  [PXDBInt]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedGroup.accountMaskWildcard>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>, OrderBy<Asc<Account.accountCD>>>), typeof (Account.accountCD), DescriptionField = typeof (Account.description))]
  [PXUIField]
  public virtual int? FromAccount { get; set; }

  [PXDBInt]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedGroup.accountMaskWildcard>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>, OrderBy<Asc<Account.accountCD>>>), typeof (Account.accountCD), DescriptionField = typeof (Account.description))]
  [PXUIField]
  public virtual int? ToAccount { get; set; }

  [AccountRaw(DisplayName = "Account Mask")]
  public virtual string AccountIDFilter { get; set; }

  [SubAccountRaw(DisplayName = "Subaccount Mask")]
  public virtual string SubIDFilter { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this.SubIDFilter, "SUBACCOUNT");
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string AccountCDWildcard { get; set; }

  [PXUIField]
  [PXDefault(2)]
  [PXShort]
  public short? PreloadAction
  {
    get => this._PreloadAction;
    set
    {
      if (!value.HasValue)
        return;
      this._PreloadAction = value;
    }
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [BudgetPreloadFilter.strategy.List]
  public virtual string Strategy { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetPreloadFilter.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetPreloadFilter.ledgerID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetPreloadFilter.finYear>
  {
  }

  public abstract class changePercent : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    BudgetPreloadFilter.changePercent>
  {
  }

  public abstract class fromAccount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetPreloadFilter.fromAccount>
  {
  }

  public abstract class toAccount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetPreloadFilter.toAccount>
  {
  }

  public abstract class accountIDFilter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetPreloadFilter.accountIDFilter>
  {
  }

  public abstract class subIDFilter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetPreloadFilter.subIDFilter>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetPreloadFilter.subCDWildcard>
  {
  }

  public abstract class accountCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetPreloadFilter.accountCDWildcard>
  {
  }

  public abstract class strategy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetPreloadFilter.strategy>
  {
    public const string ReloadAll = "R";
    public const string UpdateExisting = "U";
    public const string UpdateAndLoad = "F";
    public const string LoadNotExisting = "L";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "U", "F", "L" }, new string[3]
        {
          "Update Existing Articles Only",
          "Update Existing Articles and Load Nonexistent Articles",
          "Load Nonexistent Articles Only"
        })
      {
      }
    }

    public class reloadAll : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetPreloadFilter.strategy.reloadAll>
    {
      public reloadAll()
        : base("R")
      {
      }
    }

    public class updateExisting : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetPreloadFilter.strategy.updateExisting>
    {
      public updateExisting()
        : base("U")
      {
      }
    }

    public class updateAndLoad : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetPreloadFilter.strategy.updateAndLoad>
    {
      public updateAndLoad()
        : base("F")
      {
      }
    }

    public class loadNotExisting : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetPreloadFilter.strategy.loadNotExisting>
    {
      public loadNotExisting()
        : base("L")
      {
      }
    }
  }
}
