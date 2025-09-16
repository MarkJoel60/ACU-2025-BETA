// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UpdateMCAssignmentResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select2<INItemSite, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INItemSite.inventoryID>, And<InventoryItem.stkItem, Equal<boolTrue>>>>, Where<INItemSite.siteID, Equal<CurrentValue<UpdateMCAssignmentSettings.siteID>>>>))]
[PXHidden]
public class UpdateMCAssignmentResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the record is selected for
  /// processing by a user.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [Inventory(IsKey = true, DisplayName = "Inventory ID", BqlField = typeof (INItemSite.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
  public virtual int? ItemClassID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (InventoryItem.descr))]
  [PXUIField(DisplayName = "Description")]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  /// <summary>
  /// Current movement class for an Inventory Item in a Warehouse.
  /// </summary>
  [PXDBString(1, BqlField = typeof (INItemSite.movementClassID))]
  [PXUIField(DisplayName = "Current Movement Class")]
  [PXSelector(typeof (Search<INMovementClass.movementClassID>), DescriptionField = typeof (INMovementClass.descr))]
  public virtual string OldMC { get; set; }

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that movement class will not be changed when movement class assignments are updated.
  /// </summary>
  [PXDBBool(BqlField = typeof (INItemSite.movementClassIsFixed))]
  [PXUIField(DisplayName = "Fixed")]
  public virtual bool? MCFixed { get; set; }

  /// <summary>
  /// Preview of a Movement class that was calculated earlier and most likely would be stored as current value after update.
  /// </summary>
  [PXString(1)]
  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.movementClassIsFixed, Equal<True>>, INItemSite.movementClassID, Case<Where<CurrentValue<UpdateMCAssignmentSettings.endFinPeriodID>, Equal<INItemSite.pendingMovementClassPeriodID>, And<DateDiff<INItemSite.pendingMovementClassUpdateDate, CurrentValue<AccessInfo.businessDate>, DateDiff.day>, Equal<int0>>>, INItemSite.pendingMovementClassID>>, Null>), typeof (string))]
  [PXUIField(DisplayName = "Projected Movement Class")]
  [PXSelector(typeof (Search<INMovementClass.movementClassID>), DescriptionField = typeof (INMovementClass.descr))]
  public virtual string NewMC { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.UpdateMCAssignmentResult.Selected" />
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UpdateMCAssignmentResult.selected>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateMCAssignmentResult.inventoryID>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateMCAssignmentResult.itemClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateMCAssignmentResult.descr>
  {
  }

  public abstract class oldMC : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateMCAssignmentResult.oldMC>
  {
  }

  public abstract class mCFixed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UpdateMCAssignmentResult.mCFixed>
  {
  }

  public abstract class newMC : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateMCAssignmentResult.newMC>
  {
  }
}
