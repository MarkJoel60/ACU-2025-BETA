// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UpdateMCAssignmentSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class UpdateMCAssignmentSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [UpdateMCAssignmentSettings.action.List]
  [PXUnboundDefault("CalculateMC")]
  [PXUIField(DisplayName = "Action")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [Site(Required = true)]
  public virtual int? SiteID { get; set; }

  [AnyPeriodFilterable]
  [PXUIField(DisplayName = "End Period")]
  [PXDefault(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.finPeriodID>>>>))]
  public virtual string EndFinPeriodID { get; set; }

  [PXDBDate]
  public virtual DateTime? StartDateForAvailableQuantity { get; set; }

  /// <summary>
  /// A period before the last period to find transactions that were created during the last period.
  /// </summary>
  [PXString]
  public virtual string PenultimateFinPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<UpdateMCAssignmentSettings.itemClassID>, IsNull>>>>.Or<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<UpdateMCAssignmentSettings.itemClassID, IBqlInt>.FromCurrent>>>>, InventoryItem>.SearchFor<InventoryItem.inventoryID>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateMCAssignmentSettings.action>
  {
    public const string CalculateMC = "CalculateMC";
    public const string CalculateAndUpdateMC = "CalculateAndUpdateMC";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("CalculateMC", "Calculate Movement Class"),
          PXStringListAttribute.Pair("CalculateAndUpdateMC", "Calculate and Update Movement Class")
        })
      {
      }
    }

    public class calculateMC : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      UpdateMCAssignmentSettings.action.calculateMC>
    {
      public calculateMC()
        : base("CalculateMC")
      {
      }
    }

    public class calculateAndUpdateMC : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      UpdateMCAssignmentSettings.action.calculateAndUpdateMC>
    {
      public calculateAndUpdateMC()
        : base("CalculateAndUpdateMC")
      {
      }
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UpdateMCAssignmentSettings.siteID>
  {
  }

  public abstract class endFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateMCAssignmentSettings.endFinPeriodID>
  {
  }

  public abstract class startDateForAvailableQuantity : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UpdateMCAssignmentSettings.startDateForAvailableQuantity>
  {
  }

  public abstract class penultimateFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateMCAssignmentSettings.penultimateFinPeriodID>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateMCAssignmentSettings.itemClassID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateMCAssignmentSettings.inventoryID>
  {
  }
}
