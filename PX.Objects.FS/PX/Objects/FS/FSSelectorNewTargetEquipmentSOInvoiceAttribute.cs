// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorNewTargetEquipmentSOInvoiceAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.FS.DAC;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

public class FSSelectorNewTargetEquipmentSOInvoiceAttribute : PXCustomSelectorAttribute
{
  public FSSelectorNewTargetEquipmentSOInvoiceAttribute()
    : base(typeof (PX.Objects.AR.ARTran.lineNbr), new Type[2]
    {
      typeof (PX.Objects.AR.ARTran.sortOrder),
      typeof (PX.Objects.AR.ARTran.inventoryID)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (PX.Objects.AR.ARTran.sortOrder);
  }

  protected virtual 
  #nullable disable
  IEnumerable GetRecords()
  {
    PXResultset<PX.Objects.AR.ARTran> pxResultset = PXSelectBase<PX.Objects.AR.ARTran, PXSelectJoin<PX.Objects.AR.ARTran, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<PX.Objects.AR.ARTran.inventoryID>>>, Where<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.ModelEquipment>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentActionBase.SellingTargetEquipment>, And<Where<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARTran.tranType>>>>>>>>.Config>.Select(this._Graph, Array.Empty<object>());
    if (pxResultset != null && pxResultset.Count > 0)
    {
      foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem> pxResult in pxResultset)
      {
        PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.AR.ARTran, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        yield return (object) new FSSelectorNewTargetEquipmentSOInvoiceAttribute.ARTranExt()
        {
          LineNbr = arTran.LineNbr,
          SortOrder = arTran.SortOrder,
          InventoryID = inventoryItem.InventoryCD
        };
      }
    }
  }

  [PXHidden]
  public class ARTranExt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
      FSSelectorNewTargetEquipmentSOInvoiceAttribute.ARTranExt.lineNbr>
    {
    }

    public abstract class sortOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSSelectorNewTargetEquipmentSOInvoiceAttribute.ARTranExt.sortOrder>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSSelectorNewTargetEquipmentSOInvoiceAttribute.ARTranExt.inventoryID>
    {
    }
  }
}
