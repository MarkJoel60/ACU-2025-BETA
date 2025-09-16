// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrchestrationPlanLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// A detail line of an orchestration plan (<see cref="T:PX.Objects.SO.SOOrchestrationPlan" />).
/// </summary>
[PXCacheName("Orchestration Plan Line")]
public class SOOrchestrationPlanLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The ID of the parent order orchestration plan.</summary>
  [PXDBDefault(typeof (SOOrchestrationPlan.planID))]
  [PXParent(typeof (SOOrchestrationPlanLine.FK.SOOrchestrationPlan))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Plan ID")]
  public virtual 
  #nullable disable
  string PlanID { get; set; }

  /// <summary>
  /// The sequence number of the <see cref="T:PX.Objects.SO.SOOrchestrationPlanLine" /> detail line.
  /// </summary>
  [PXLineNbr(typeof (SOOrchestrationPlan.lineCntr))]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The alternative warehouse (which is identified by <see cref="P:PX.Objects.IN.INSite.SiteID" />) that
  /// can be assigned to a <see cref="T:PX.Objects.SO.SOLine" /> record during the orchestration.
  /// </summary>
  [PXDefault]
  [PXForeignReference(typeof (SOOrchestrationPlanLine.FK.INSite))]
  [PXCheckUnique(new Type[] {typeof (SOOrchestrationPlanLine.targetSiteID)}, Where = typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlanLine.targetSiteID, Equal<BqlField<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<SOOrchestrationPlanLine.planID, IBqlString>.IsEqual<BqlField<SOOrchestrationPlanLine.planID, IBqlString>.FromCurrent>>>), ErrorMessage = "The warehouse has already been added to the plan.", ClearOnDuplicate = true)]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrchestrationPlan.strategy>, NotEqual<OrchestrationStrategies.warehousePriority>>>>, Or<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsNull>>, Or<BqlOperand<Current<SOOrchestrationPlan.sourceSiteID>, IBqlInt>.IsNull>>>.Or<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsNotEqual<BqlField<SOOrchestrationPlan.sourceSiteID, IBqlInt>.FromCurrent>>>))]
  [Site(DisplayName = "Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), Required = true)]
  public virtual int? TargetSiteID { get; set; }

  /// <summary>
  ///  The priority of the warehouse when the system checks available quantity for shipping.
  /// </summary>
  [PXDefault]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrchestrationPlan.strategy>, NotEqual<OrchestrationStrategies.warehousePriority>>>>, Or<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsNull>>, Or<BqlOperand<Current<SOOrchestrationPlan.sourceSiteID>, IBqlInt>.IsNull>>>.Or<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsNotEqual<BqlField<SOOrchestrationPlan.sourceSiteID, IBqlInt>.FromCurrent>>>))]
  [PXDBInt]
  [PXUIField(DisplayName = "Priority", Required = true)]
  public virtual int? Priority { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the safety stock
  /// should be maintained during the order orchestration.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Maintain Safety Stock")]
  public bool? MaintainSaftyStock { get; set; }

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

  public class PK : 
    PrimaryKeyOf<SOOrchestrationPlanLine>.By<SOOrchestrationPlanLine.planID, SOOrchestrationPlanLine.lineNbr>
  {
    public static SOOrchestrationPlanLine Find(
      PXGraph graph,
      string planID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOOrchestrationPlanLine) PrimaryKeyOf<SOOrchestrationPlanLine>.By<SOOrchestrationPlanLine.planID, SOOrchestrationPlanLine.lineNbr>.FindBy(graph, (object) planID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class SOOrchestrationPlan : 
      PrimaryKeyOf<SOOrchestrationPlan>.By<SOOrchestrationPlan.planID>.ForeignKeyOf<SOOrchestrationPlanLine>.By<SOOrchestrationPlanLine.planID>
    {
    }

    public class INSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrchestrationPlanLine>.By<SOOrchestrationPlanLine.targetSiteID>
    {
    }
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrchestrationPlanLine.planID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrchestrationPlanLine.lineNbr>
  {
  }

  public abstract class targetSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrchestrationPlanLine.targetSiteID>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrchestrationPlanLine.priority>
  {
  }

  public abstract class maintainSaftyStock : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrchestrationPlanLine.maintainSaftyStock>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrchestrationPlanLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlanLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrchestrationPlanLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrchestrationPlanLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrchestrationPlanLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrchestrationPlanLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrchestrationPlanLine.Tstamp>
  {
  }
}
