// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LineType_Profitability
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LineType_Profitability : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LineType_Profitability().ID_LIST_ALL, new ID.LineType_Profitability().TX_LIST_ALL)
    {
    }
  }

  public class Inventory_Item : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Profitability.Inventory_Item>
  {
    public Inventory_Item()
      : base("SLPRO")
    {
    }
  }

  public class Service : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Profitability.Service>
  {
    public Service()
      : base("SERVI")
    {
    }
  }

  public class NonStockItem : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Profitability.NonStockItem>
  {
    public NonStockItem()
      : base("NSTKI")
    {
    }
  }

  public class LaborItem : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Profitability.LaborItem>
  {
    public LaborItem()
      : base("LABOR")
    {
    }
  }
}
