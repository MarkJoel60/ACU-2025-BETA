// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorNewTargetEquipmentSalesOrderAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

public class FSSelectorNewTargetEquipmentSalesOrderAttribute : PXCustomSelectorAttribute
{
  public FSSelectorNewTargetEquipmentSalesOrderAttribute()
    : base(typeof (FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.lineNbr), new Type[2]
    {
      typeof (FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.sortOrder),
      typeof (FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.inventoryID)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.sortOrder);
  }

  protected virtual 
  #nullable disable
  IEnumerable GetRecords()
  {
    PXResultset<PX.Objects.SO.SOLine> pxResultset = PXSelectBase<PX.Objects.SO.SOLine, PXSelectJoin<PX.Objects.SO.SOLine, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<PX.Objects.SO.SOLine.inventoryID>>>, Where<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.ModelEquipment>, And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentActionBase.SellingTargetEquipment>, And<Where<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOLine.orderType>>>>>>>>.Config>.Select(this._Graph, Array.Empty<object>());
    if (pxResultset.Count > 0)
    {
      foreach (PXResult<PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem> pxResult in pxResultset)
      {
        PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        yield return (object) new FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine()
        {
          LineNbr = soLine.LineNbr,
          SortOrder = soLine.SortOrder,
          InventoryID = inventoryItem.InventoryCD
        };
      }
    }
  }

  [PXHidden]
  public class FSSOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField(DisplayName = "Line Nbr.")]
    public int? LineNbr { get; set; }

    [PXInt]
    [PXUIField]
    public int? SortOrder { get; set; }

    [PXString]
    [PXUIField]
    public virtual string InventoryID { get; set; }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.lineNbr>
    {
    }

    public abstract class sortOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.sortOrder>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSSelectorNewTargetEquipmentSalesOrderAttribute.FSSOLine.inventoryID>
    {
    }
  }
}
