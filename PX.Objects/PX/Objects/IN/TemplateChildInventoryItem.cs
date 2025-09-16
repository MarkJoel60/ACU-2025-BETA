// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.TemplateChildInventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// An alias for Matrix Item (<see cref="T:PX.Objects.IN.InventoryItem" />) which can be used for building complex BQL queries.
/// </summary>
[PXHidden]
public class TemplateChildInventoryItem : InventoryItem
{
  public new abstract class itemClassID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    TemplateChildInventoryItem.itemClassID>
  {
  }

  public new abstract class isTemplate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    TemplateChildInventoryItem.isTemplate>
  {
  }

  public new abstract class templateItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TemplateChildInventoryItem.templateItemID>
  {
  }
}
