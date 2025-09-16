// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCategoryBuffer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INItemCategoryBuffer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;

  [PXInt(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INItemCategoryBuffer.inventoryID>
  {
  }
}
