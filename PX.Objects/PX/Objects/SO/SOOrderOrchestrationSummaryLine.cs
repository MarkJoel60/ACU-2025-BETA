// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderOrchestrationSummaryLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// The orchestration summary line that is displayed in the Orchestrate Order dialog box.
/// </summary>
[PXVirtual]
[PXCacheName("Order Orchestration Summary Line")]
public class SOOrderOrchestrationSummaryLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The sequence number of the <see cref="T:PX.Objects.SO.SOOrderOrchestrationSummaryLine" /> detail line.
  /// </summary>
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.OrderLineNbr" />
  [PXInt]
  [PXUIField(DisplayName = "Order Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? OrderLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.InventoryID" />
  [Inventory(Enabled = false)]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.InventoryDescr" />
  [PXString]
  [PXUIField(DisplayName = "Line Description", Enabled = false)]
  public virtual 
  #nullable disable
  string InventoryDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.BaseUOM" />
  [INUnboundUnit(DisplayName = "Base UOM", Enabled = false, Visible = false)]
  public virtual string BaseUOM { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.SalesUOM" />
  [INUnboundUnit(Enabled = false)]
  public virtual string SalesUOM { get; set; }

  /// <summary>
  /// The original <see cref="P:PX.Objects.IN.INSite.SiteCD">warehouse</see> in the <see cref="T:PX.Objects.SO.SOLine" /> record used in the orchestration.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Warehouse", Enabled = false)]
  public virtual string SiteCD { get; set; }

  /// <summary>
  /// A description of the original <see cref="T:PX.Objects.IN.INSite">warehouse</see> in the <see cref="T:PX.Objects.SO.SOLine" /> record that is used in the orchestration.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Warehouse Description", Enabled = false)]
  public virtual string SiteDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderOrchestrationLine.OrderQty" />
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? OrderQty { get; set; }

  /// <summary>
  /// The quantity (from the <see cref="T:PX.Objects.SO.SOLine" /> record) available for orchestration.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available To Orchestrate", Enabled = false)]
  public virtual Decimal? SplitQty { get; set; }

  /// <summary>
  /// The number of <see cref="P:PX.Objects.IN.INSite.SiteCD">warehouses</see> selected during the orchestration
  /// to fulfill the quantity required for the <see cref="T:PX.Objects.SO.SOLine" /> record.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Nbr. Of Warehouses", Enabled = false)]
  public virtual int? Splits { get; set; }

  /// <summary>
  /// The summary of changes that will be applied to the <see cref="T:PX.Objects.SO.SOLine" /> record after the orchestration.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Orchestration Details", Enabled = false)]
  public virtual string SplitDetails { get; set; }

  public abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.lineNbr>
  {
  }

  public abstract class orderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.orderLineNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.inventoryID>
  {
  }

  public abstract class inventoryDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.inventoryDescr>
  {
  }

  public abstract class baseUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.baseUOM>
  {
  }

  public abstract class salesUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.salesUOM>
  {
  }

  public abstract class siteCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.siteCD>
  {
  }

  public abstract class siteDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.siteDescr>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.orderQty>
  {
  }

  public abstract class splitQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.splitQty>
  {
  }

  public abstract class splits : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderOrchestrationSummaryLine.splits>
  {
  }

  public abstract class splitDetails : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSummaryLine.splitDetails>
  {
  }
}
