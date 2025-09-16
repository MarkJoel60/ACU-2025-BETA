// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAvailabilityScheme
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INAvailabilitySchemeMaint))]
[PXCacheName]
[Serializable]
public class INAvailabilityScheme : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _InclQtyProductionSupplyPrepared;
  protected bool? _InclQtyProductionSupply;
  protected bool? _InclQtyProductionDemandPrepared;
  protected bool? _InclQtyProductionDemand;
  protected bool? _InclQtyProductionAllocated;

  [PXDefault]
  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (INAvailabilityScheme.availabilitySchemeID))]
  public virtual 
  #nullable disable
  string AvailabilitySchemeID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. on Service Orders", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. Allocated for Service Orders", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. on Service Orders Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. on Sales Returns")]
  public virtual bool? InclQtySOReverse { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. on Back Orders")]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deduct Qty. on Sales Prepared")]
  public virtual bool? InclQtySOPrepared { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. on Sales Orders")]
  public virtual bool? InclQtySOBooked { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. Shipped")]
  public virtual bool? InclQtySOShipped { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. Allocated")]
  public virtual bool? InclQtySOShipping { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. in Transit")]
  public virtual bool? InclQtyInTransit { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. on PO Receipts")]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. on Purchase Prepared")]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. on Purchase Orders")]
  public virtual bool? InclQtyPOOrders { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. of Purchase for SO and SO to Purchase")]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. on Issues")]
  public virtual bool? InclQtyINIssues { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. on Receipts")]
  public virtual bool? InclQtyINReceipts { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Deduct Qty. of Kit Assembly Demand")]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. of Kit Assembly Supply")]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>true</c>) that the Product Supply Prepared quantity is added to the total item availability.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. of Production Supply Prepared")]
  public virtual bool? InclQtyProductionSupplyPrepared
  {
    get => this._InclQtyProductionSupplyPrepared;
    set => this._InclQtyProductionSupplyPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>true</c>) that the Product Supply quantity is added to the total item availability.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Qty. of Production Supply")]
  public virtual bool? InclQtyProductionSupply
  {
    get => this._InclQtyProductionSupply;
    set => this._InclQtyProductionSupply = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>true</c>) that the Production Demand Prepared quantity is deducted from the total item availability.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deduct Qty. on Production Demand Prepared")]
  public virtual bool? InclQtyProductionDemandPrepared
  {
    get => this._InclQtyProductionDemandPrepared;
    set => this._InclQtyProductionDemandPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>true</c>) that the Production Demand quantity is deducted from the total item availability.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deduct Qty. on Production Demand")]
  public virtual bool? InclQtyProductionDemand
  {
    get => this._InclQtyProductionDemand;
    set => this._InclQtyProductionDemand = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>true</c>) that the Production Allocated quantity is deducted from the total item availability.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deduct Qty. on Production Allocated")]
  public virtual bool? InclQtyProductionAllocated
  {
    get => this._InclQtyProductionAllocated;
    set => this._InclQtyProductionAllocated = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<INAvailabilityScheme>.By<INAvailabilityScheme.availabilitySchemeID>
  {
    public static INAvailabilityScheme Find(
      PXGraph graph,
      string availabilitySchemeID,
      PKFindOptions options = 0)
    {
      return (INAvailabilityScheme) PrimaryKeyOf<INAvailabilityScheme>.By<INAvailabilityScheme.availabilitySchemeID>.FindBy(graph, (object) availabilitySchemeID, options);
    }
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAvailabilityScheme.availabilitySchemeID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAvailabilityScheme.description>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtySOShipping>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyInTransit>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INAvailabilityScheme.inclQtyProductionAllocated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INAvailabilityScheme.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INAvailabilityScheme.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAvailabilityScheme.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAvailabilityScheme.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INAvailabilityScheme.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAvailabilityScheme.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAvailabilityScheme.lastModifiedDateTime>
  {
  }
}
