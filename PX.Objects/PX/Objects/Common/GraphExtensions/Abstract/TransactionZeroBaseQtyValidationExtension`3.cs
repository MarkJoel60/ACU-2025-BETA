// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.TransactionZeroBaseQtyValidationExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class TransactionZeroBaseQtyValidationExtension<TGraph, TDocument, TDocumentHoldField> : 
  PXGraphExtension<
  #nullable disable
  TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TDocumentHoldField : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TDocumentHoldField>
{
  [PXHidden]
  public PXSelectExtension<TransactionLineQty> Transactions;

  protected abstract TransactionLineQtyMapping GetTranLineMapping();

  public virtual bool PreventSaveOnHold => false;

  [PXOverride]
  public virtual void Persist(Action basePersist)
  {
    if (!((bool?) ((PXCache) GraphHelper.Caches<TDocument>((PXGraph) this.Base)).GetValue<TDocumentHoldField>(((PXCache) GraphHelper.Caches<TDocument>((PXGraph) this.Base)).Current)).GetValueOrDefault() || this.PreventSaveOnHold)
    {
      foreach (PXResult<TransactionLineQty> pxResult in ((PXSelectBase<TransactionLineQty>) this.Transactions).Select(Array.Empty<object>()))
      {
        TransactionLineQty transactionLineQty = PXResult<TransactionLineQty>.op_Implicit(pxResult);
        Decimal? qty = transactionLineQty.Qty;
        Decimal num1 = 0M;
        if (!(qty.GetValueOrDefault() == num1 & qty.HasValue))
        {
          Decimal? baseQty = transactionLineQty.BaseQty;
          Decimal num2 = 0M;
          if (baseQty.GetValueOrDefault() == num2 & baseQty.HasValue)
          {
            InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, transactionLineQty.InventoryID);
            string name = typeof (TransactionLineQty.qty).Name;
            GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) transactionLineQty);
            if (((PXSelectBase) this.Transactions).Cache.RaiseExceptionHandling(name, (object) transactionLineQty, (object) transactionLineQty.Qty, (Exception) new PXSetPropertyException((IBqlTable) inventoryItem, "The quantity in the {0} base UOM is 0. Change the quantity in the line to save the document.", (PXErrorLevel) 4, new object[1]
            {
              (object) inventoryItem.BaseUnit
            })))
              throw new PXRowPersistingException(name, (object) transactionLineQty.Qty, "The quantity in the {0} base UOM is 0. Change the quantity in the line to save the document.", new object[1]
              {
                (object) inventoryItem.BaseUnit
              });
          }
        }
      }
    }
    basePersist();
  }
}
