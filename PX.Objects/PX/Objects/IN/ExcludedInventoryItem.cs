// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ExcludedInventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class ExcludedInventoryItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [StockItem(IsKey = true, DescriptionField = typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExcludedInventoryItem.inventoryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExcludedInventoryItem.descr>
  {
  }
}
