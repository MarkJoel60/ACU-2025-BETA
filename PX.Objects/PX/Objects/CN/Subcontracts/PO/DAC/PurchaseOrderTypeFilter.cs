// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.DAC.PurchaseOrderTypeFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.DAC;

[PXHidden]
public class PurchaseOrderTypeFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public string Type1 { get; set; }

  [PXString]
  public string Type2 { get; set; }

  [PXString]
  public string Type3 { get; set; }

  [PXString]
  public string Type4 { get; set; }

  [PXString]
  public string Type5 { get; set; }

  [PXString]
  [PXUIField]
  public virtual string Graph { get; set; }

  public abstract class graph : IBqlField, IBqlOperand
  {
  }

  public abstract class type1 : IBqlField, IBqlOperand
  {
  }

  public abstract class type2 : IBqlField, IBqlOperand
  {
  }

  public abstract class type3 : IBqlField, IBqlOperand
  {
  }

  public abstract class type4 : IBqlField, IBqlOperand
  {
  }

  public abstract class type5 : IBqlField, IBqlOperand
  {
  }
}
