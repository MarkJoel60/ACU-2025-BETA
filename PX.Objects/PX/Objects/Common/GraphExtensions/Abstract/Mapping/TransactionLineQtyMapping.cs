// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.TransactionLineQtyMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class TransactionLineQtyMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  /// <exclude />
  public Type Qty = typeof (TransactionLineQty.qty);
  /// <exclude />
  public Type BaseQty = typeof (TransactionLineQty.baseQty);
  /// <exclude />
  public Type InventoryID = typeof (TransactionLineQty.inventoryID);

  /// <exclude />
  public Type Extension => typeof (TransactionLineQty);

  /// <exclude />
  public Type Table => this._table;

  /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Common.GraphExtensions.Abstract.DAC.TransactionLineQty" /> mapped cache extension to the specified table.</summary>
  /// <param name="table">A DAC.</param>
  public TransactionLineQtyMapping(Type table) => this._table = table;
}
