// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderOrchestrationSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// The orchestration settings that are displayed in the Orchestrate Order dialog box.
/// </summary>
[PXCacheName("Order Orchestration Settings")]
public class SOOrderOrchestrationSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="T:PX.Objects.SO.SOOrderType.orchestrationStrategy" />
  [PXString]
  [PXUIField(DisplayName = "Fulfillment Strategy", Enabled = false)]
  [OrchestrationStrategies.List]
  public 
  #nullable disable
  string OrchestrationStrategy { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderType.LimitWarehouse" />
  [PXBool]
  [PXUIField(DisplayName = "Limit Number Of Fulfillment Warehouses", Enabled = false)]
  public bool? LimitWarehouse { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderType.NumberOfWarehouses" />
  [PXInt]
  [PXUIField(DisplayName = "Number of Fulfillment Warehouses", Enabled = false)]
  [PXUIVisible(typeof (Where<SOOrderOrchestrationSettings.limitWarehouse, Equal<True>>))]
  public int? NumberOfWarehouses { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CS.ShippingZone.ZoneID">shipping zone</see> that is used to found the <see cref="T:PX.Objects.SO.SOOrchestrationPlan" /> record.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Shipping Zone", Enabled = false)]
  [PXUIVisible(typeof (Where<SOOrderOrchestrationSettings.orchestrationStrategy, Equal<OrchestrationStrategies.destinationPriority>>))]
  public virtual string ShippingZoneID { get; set; }

  public abstract class orchestrationStrategy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSettings.orchestrationStrategy>
  {
  }

  public abstract class limitWarehouse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderOrchestrationSettings.limitWarehouse>
  {
  }

  public abstract class numberOfWarehouses : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderOrchestrationSettings.numberOfWarehouses>
  {
  }

  public abstract class shippingZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderOrchestrationSettings.shippingZoneID>
  {
  }
}
