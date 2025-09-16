// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMValidationFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXPrimaryGraph(typeof (ProjectBalanceValidation))]
[PXCacheName("Recalculate Project Balances")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMValidationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _RecalculateUnbilledSummary;

  /// <summary>
  /// A check box that indicates (if selected) that balances will be recalculated even if extra check boxes are not selected after you click Process or Process All on the form toolbar.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Project Balances", IsReadOnly = true)]
  public virtual bool? RecalculateProjectBalances { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Unbilled Summary")]
  public virtual bool? RecalculateUnbilledSummary
  {
    get => this._RecalculateUnbilledSummary;
    set => this._RecalculateUnbilledSummary = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Draft Invoice Amount and Quantity")]
  public virtual bool? RecalculateDraftInvoicesAmount { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Rebuild Commitments")]
  public virtual bool? RebuildCommitments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Change Orders", FieldClass = "CHANGEORDER")]
  public virtual bool? RecalculateChangeOrders { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if the value is <see langword="true" />) that inclusive taxes will be recalculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Inclusive Taxes")]
  public virtual bool? RecalculateInclusiveTaxes { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if the value is <see langword="true" />) that project budget history will be recalculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Project Budget History")]
  public virtual bool? RecalculateProjectBudgetHistory { get; set; }

  public abstract class recalculateProjectBalances : 
    BqlType<IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateProjectBalances>
  {
  }

  public abstract class recalculateUnbilledSummary : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateUnbilledSummary>
  {
  }

  public abstract class recalculateDraftInvoicesAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateDraftInvoicesAmount>
  {
  }

  public abstract class rebuildCommitments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.rebuildCommitments>
  {
  }

  public abstract class recalculateChangeOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateChangeOrders>
  {
  }

  public abstract class recalculateInclusiveTaxes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateInclusiveTaxes>
  {
  }

  public abstract class recalculateProjectBudgetHistory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMValidationFilter.recalculateProjectBudgetHistory>
  {
  }
}
