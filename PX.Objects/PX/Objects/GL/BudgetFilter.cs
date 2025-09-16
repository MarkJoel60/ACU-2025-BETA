// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BudgetFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using PX.Objects.GL.Descriptor;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class BudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public short? Precision;

  [Branch(null, null, true, true, true, Required = true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger", Required = true)]
  [PXSelector(typeof (Search5<Ledger.ledgerID, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<Ledger.balanceType, Equal<LedgerBalanceType.budget>, And<Branch.branchID, Equal<Current2<BudgetFilter.branchID>>>>, Aggregate<GroupBy<Ledger.ledgerID>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr))]
  [PXDefault(typeof (Search5<Ledger.ledgerID, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<Ledger.balanceType, Equal<LedgerBalanceType.budget>, And<Branch.branchID, Equal<Current2<BudgetFilter.branchID>>>>, Aggregate<GroupBy<Ledger.ledgerID>>>))]
  public virtual int? LedgerID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Base Currency ID")]
  public virtual 
  #nullable disable
  string BaseCuryID { get; set; }

  [PXUIField(DisplayName = "Financial Year", Required = true)]
  [PXDBString(4)]
  [GenericFinYearSelector(typeof (Search3<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>), null, typeof (BudgetFilter.branchID), null, null, false, false, null, false, null)]
  public virtual string FinYear { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Tree View")]
  public virtual bool? ShowTree { get; set; }

  [Branch(null, null, true, true, true, DisplayName = "Compare to Branch", Required = false)]
  public virtual int? CompareToBranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Compare to Ledger")]
  [PXSelector(typeof (Search5<Ledger.ledgerID, LeftJoin<OrganizationLedgerLink, On<Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, LeftJoin<Branch, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<Branch.branchID, Equal<Current2<BudgetFilter.compareToBranchID>>, Or<Current2<BudgetFilter.compareToBranchID>, IsNull>>, Aggregate<GroupBy<Ledger.ledgerID>>>), SubstituteKey = typeof (Ledger.ledgerCD), DescriptionField = typeof (Ledger.descr))]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<BudgetFilter.baseCuryID>, Equal<Ledger.baseCuryID>>>>>.Or<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>), "The ledger currency is different from the budget currency.", new Type[] {})]
  public virtual int? CompareToLedgerId { get; set; }

  [PXUIField(DisplayName = "Compare to Year")]
  [PXDBString(4)]
  [GenericFinYearSelector(typeof (Search3<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>), null, typeof (BudgetFilter.compareToBranchID), null, null, false, false, null, true, null)]
  public virtual string CompareToFinYear { get; set; }

  [SubAccountRaw(DisplayName = "Subaccount Filter")]
  public virtual string SubIDFilter { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this.SubIDFilter, "SUBACCOUNT");
  }

  [PXUIField(DisplayName = "Tree Node Filter")]
  [PXDBString(30, IsUnicode = true)]
  public virtual string TreeNodeFilter { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetFilter.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BudgetFilter.ledgerID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetFilter.baseCuryID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetFilter.finYear>
  {
  }

  public abstract class showTree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BudgetFilter.showTree>
  {
  }

  public abstract class compareToBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BudgetFilter.compareToBranchID>
  {
  }

  public abstract class compareToLedgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BudgetFilter.compareToLedgerID>
  {
  }

  public abstract class compareToFinYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetFilter.compareToFinYear>
  {
  }

  public abstract class subIDFilter : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetFilter.subIDFilter>
  {
  }

  public abstract class subCDWildcard : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetFilter.subCDWildcard>
  {
  }

  public abstract class treeNodeFilter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BudgetFilter.treeNodeFilter>
  {
  }
}
