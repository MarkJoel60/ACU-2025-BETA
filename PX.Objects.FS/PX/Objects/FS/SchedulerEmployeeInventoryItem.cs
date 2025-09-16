// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerEmployeeInventoryItem
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (SelectFrom<PX.Objects.IN.InventoryItem>))]
[PXCacheName("Employee Inventory Item")]
[Serializable]
public class SchedulerEmployeeInventoryItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXUIField(DisplayName = "Employee Service Type")]
  [PXRestrictor(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)})]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.itemType, IBqlString>.IsEqual<INItemTypes.serviceItem>>), "An inventory item must be of the Service type.", new Type[] {})]
  [PXSelector(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), new Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), typeof (PX.Objects.IN.InventoryItem.itemClassID)}, SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual 
  #nullable disable
  string InventoryCD { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.IN.InventoryItem.itemStatus))]
  public virtual string ItemStatus { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.IN.InventoryItem.itemType))]
  public virtual string ItemType { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerEmployeeInventoryItem.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerEmployeeInventoryItem.inventoryCD>
  {
  }

  public abstract class itemStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerEmployeeInventoryItem.itemStatus>
  {
  }

  public abstract class itemType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerEmployeeInventoryItem.itemType>
  {
  }
}
