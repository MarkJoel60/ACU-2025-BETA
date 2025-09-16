// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INStockItemXRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXBreakInheritance]
public class INStockItemXRef : INItemXRef
{
  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INStockItemXRef.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INStockItemXRef.subItemID>
  {
  }

  public new abstract class alternateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INStockItemXRef.alternateType>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INStockItemXRef.bAccountID>
  {
  }

  public new abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INStockItemXRef.alternateID>
  {
  }
}
