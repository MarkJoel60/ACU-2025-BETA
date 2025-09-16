// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderOrchestrationLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// The orchestration detail line that is displayed in the Orchestrate Order dialog box.
/// </summary>
[PXVirtual]
[PXCacheName("Order Orchestration Line")]
public class SOOrderOrchestrationLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXInt(IsKey = true)]
  public virtual int? RecordID { get; set; }

  /// <summary>
  /// The number of the order detail line (which is of the <see cref="T:PX.Objects.SO.SOLine" /> type) that is used in the orchestration.
  /// </summary>
  [PXInt(IsKey = true)]
  public virtual int? OrderLineNbr { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.IN.INSite.SiteID">ID of the new warehouse</see> that was selected for the <see cref="T:PX.Objects.SO.SOLine" /> record during the orchestration.
  /// </summary>
  [PXInt]
  public virtual int? AltSiteID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.IN.INSite.SiteCD">CD of the new warehouse</see> that was selected for the <see cref="T:PX.Objects.SO.SOLine" /> record during the orchestration.
  /// </summary>
  [PXString]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<BqlField<SOOrderOrchestrationLine.altSiteID, IBqlInt>.FromCurrent>>, PX.Objects.IN.INSite>.SearchFor<PX.Objects.IN.INSite.siteCD>))]
  [PXUIField(DisplayName = "Warehouse", Enabled = false)]
  public virtual 
  #nullable disable
  string AltSiteCD { get; set; }

  /// <summary>
  /// The description of the <see cref="T:PX.Objects.IN.INSite">warehouse</see> that was selected during the orchestration.
  /// </summary>
  [PXString]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<BqlField<SOOrderOrchestrationLine.altSiteID, IBqlInt>.FromCurrent>>, PX.Objects.IN.INSite>.SearchFor<PX.Objects.IN.INSite.descr>))]
  [PXUIField(DisplayName = "Warehouse Description", Enabled = false)]
  public virtual string AltSiteDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrchestrationPlanLine.Priority" />
  [PXInt(MinValue = 1)]
  public virtual int? SitePriority { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.IN.InventoryItem.InventoryID">stock item</see> in the <see cref="T:PX.Objects.SO.SOLine" /> record that is used in the orchestration.
  /// </summary>
  [PXInt]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TranDesc" />
  [PXString]
  public virtual string InventoryDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.BaseUnit" />
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOOrderOrchestrationLine.inventoryID>>>>))]
  [INUnboundUnit(DisplayName = "Base UOM", Enabled = false, Visible = false)]
  public virtual string BaseUOM { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> in the <see cref="T:PX.Objects.SO.SOLine" /> record that is used in the orchestration.
  /// </summary>
  [INUnboundUnit(Enabled = false)]
  public virtual string SalesUOM { get; set; }

  /// <summary>
  /// The original quantity in the <see cref="T:PX.Objects.SO.SOLine" /> record that is used in the orchestration.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderQty { get; set; }

  /// <summary>
  /// The quantity (which is specified in the <see cref="T:PX.Objects.SO.SOLine" /> record) that is distributed by <see cref="T:PX.Objects.IN.INSite">warehouses</see> during the orchestration.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty", Enabled = false)]
  public virtual Decimal? LineQty { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrchestrationPlan">orchestration plan</see> selected for the <see cref="T:PX.Objects.SO.SOLine" /> record during the orchestration.
  /// </summary>
  [PXString(15)]
  public virtual string PlanID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the created <see cref="T:PX.Objects.SO.SOLineSplit" /> record will be allocated.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allocated", Enabled = false)]
  public virtual bool? IsAllocated { get; set; }

  /// <exclude />
  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsSplitLine { get; set; }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderOrchestrationLine.recordID>
  {
  }

  public abstract class orderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationLine.orderLineNbr>
  {
  }

  public abstract class altSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderOrchestrationLine.altSiteID>
  {
  }

  public abstract class altSiteCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationLine.altSiteCD>
  {
  }

  public abstract class altSiteDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationLine.altSiteDescr>
  {
  }

  public abstract class sitePriority : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationLine.sitePriority>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationLine.inventoryID>
  {
  }

  public abstract class inventoryDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationLine.inventoryDescr>
  {
  }

  public abstract class baseUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderOrchestrationLine.baseUOM>
  {
  }

  public abstract class salesUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationLine.salesUOM>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderOrchestrationLine.orderQty>
  {
  }

  public abstract class lineQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderOrchestrationLine.lineQty>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderOrchestrationLine.planID>
  {
  }

  public abstract class isAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderOrchestrationLine.isAllocated>
  {
  }

  public abstract class isSplitLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderOrchestrationLine.isSplitLine>
  {
  }
}
