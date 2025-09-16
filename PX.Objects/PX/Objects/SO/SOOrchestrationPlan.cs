// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrchestrationPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>An order orchestration plan.</summary>
[PXCacheName("Orchestration Plan")]
[PXPrimaryGraph(typeof (SOOrchestrationPlanMaint))]
[Serializable]
public class SOOrchestrationPlan : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The ID of the order orchestration plan.</summary>
  [PXDefault]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>, SOOrchestrationPlan>.SearchFor<SOOrchestrationPlan.planID>), new Type[] {typeof (SOOrchestrationPlan.planID), typeof (SOOrchestrationPlan.planDescription)})]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Plan ID")]
  public virtual 
  #nullable disable
  string PlanID { get; set; }

  /// <summary>
  /// A description of the <see cref="T:PX.Objects.SO.SOOrchestrationPlan" /> record.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string PlanDescription { get; set; }

  /// <summary>
  /// The order orchestration fulfillment strategy for the orchestration plan.
  /// </summary>
  [PXDefault("DP")]
  [OrchestrationStrategies.ShortList]
  [PXDBString(2)]
  [PXUIField(DisplayName = "Fulfillment Strategy")]
  public string Strategy { get; set; }

  /// <summary>
  /// The shipping zone (which is identified by <see cref="P:PX.Objects.CS.ShippingZone.ZoneID" />) that helps to find
  /// a correct <see cref="T:PX.Objects.SO.SOOrchestrationPlan" /> record for the <see cref="T:PX.Objects.SO.SOOrder" /> record.
  /// </summary>
  [PXDefault]
  [PXForeignReference(typeof (SOOrchestrationPlan.FK.ShippingZone))]
  [PXFormula(typeof (Default<SOOrchestrationPlan.strategy>))]
  [PXUIVisible(typeof (Where<SOOrchestrationPlan.strategy, Equal<OrchestrationStrategies.destinationPriority>>))]
  [PXUIRequired(typeof (Where<SOOrchestrationPlan.strategy, Equal<OrchestrationStrategies.destinationPriority>>))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.CS.ShippingZone, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CS.ShippingZone>.SearchFor<PX.Objects.CS.ShippingZone.zoneID>), DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField]
  public virtual string ShippingZoneID { get; set; }

  /// <summary>
  /// The warehouse (which is identified by <see cref="P:PX.Objects.IN.INSite.SiteID" />) that helps to find a
  /// correct <see cref="T:PX.Objects.SO.SOOrchestrationPlan" /> record for the <see cref="T:PX.Objects.SO.SOLine" /> record.
  /// </summary>
  [PXDefault]
  [PXForeignReference(typeof (SOOrchestrationPlan.FK.INSite))]
  [PXFormula(typeof (Default<SOOrchestrationPlan.strategy>))]
  [PXUIVisible(typeof (Where<SOOrchestrationPlan.strategy, Equal<OrchestrationStrategies.warehousePriority>>))]
  [PXUIRequired(typeof (Where<SOOrchestrationPlan.strategy, Equal<OrchestrationStrategies.warehousePriority>>))]
  [Site(DisplayName = "Source Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), BqlField = typeof (SOOrchestrationPlan.sourceSiteID))]
  public virtual int? SourceSiteID { get; set; }

  /// <summary>
  /// When this value is <see langword="true" />, a new line will be added to the top of the grid
  /// on the Orchestration Plans (SO304600) form. The Warehouse field in this line will display the
  /// current source warehouse (which is defined by the <see cref="T:PX.Objects.SO.SOOrchestrationPlan.sourceSiteID" /> field).
  /// </summary>
  [PXDefault(false)]
  [PXFormula(typeof (Default<SOOrchestrationPlan.strategy>))]
  [PXUIVisible(typeof (Where<SOOrchestrationPlan.strategy, Equal<OrchestrationStrategies.warehousePriority>>))]
  [PXDBBool]
  [PXUIField(DisplayName = "Include Source Warehouse")]
  public bool? IncludeSourceWarehouse { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the orchestration plan has active status.
  /// </summary>
  [PXDefault(true)]
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  /// <summary>
  /// The line counter for <see cref="T:PX.Objects.SO.SOOrchestrationPlanLine" /> detail lines.
  /// </summary>
  [PXDefault(0)]
  [PXDBInt]
  public virtual int? LineCntr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<SOOrchestrationPlan>.By<SOOrchestrationPlan.planID>
  {
    public static SOOrchestrationPlan Find(PXGraph graph, string planID, PKFindOptions options = 0)
    {
      return (SOOrchestrationPlan) PrimaryKeyOf<SOOrchestrationPlan>.By<SOOrchestrationPlan.planID>.FindBy(graph, (object) planID, options);
    }
  }

  public static class FK
  {
    public class INSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrchestrationPlan>.By<SOOrchestrationPlan.sourceSiteID>
    {
    }

    public class ShippingZone : 
      PrimaryKeyOf<PX.Objects.CS.ShippingZone>.By<PX.Objects.CS.ShippingZone.zoneID>.ForeignKeyOf<SOOrchestrationPlan>.By<SOOrchestrationPlan.shippingZoneID>
    {
    }
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrchestrationPlan.planID>
  {
  }

  public abstract class planDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlan.planDescription>
  {
  }

  public abstract class strategy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrchestrationPlan.strategy>
  {
  }

  public abstract class shippingZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlan.shippingZoneID>
  {
  }

  public abstract class sourceSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrchestrationPlan.sourceSiteID>
  {
  }

  public abstract class includeSourceWarehouse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrchestrationPlan.includeSourceWarehouse>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrchestrationPlan.isActive>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrchestrationPlan.lineCntr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrchestrationPlan.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlan.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrchestrationPlan.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrchestrationPlan.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlan.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrchestrationPlan.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrchestrationPlan.Tstamp>
  {
  }
}
