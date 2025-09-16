// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LineType_ALL
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LineType_ALL : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LineType_ALL().ID_LIST_ALL, new ID.LineType_ALL().TX_LIST_ALL)
    {
    }
  }

  public class Inventory_Item : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_ALL.Inventory_Item>
  {
    public Inventory_Item()
      : base("SLPRO")
    {
    }
  }

  public class Service : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LineType_ALL.Service>
  {
    public Service()
      : base("SERVI")
    {
    }
  }

  public class Service_Template : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_ALL.Service_Template>
  {
    public Service_Template()
      : base("TEMPL")
    {
    }
  }

  public class NonStockItem : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_ALL.NonStockItem>
  {
    public NonStockItem()
      : base("NSTKI")
    {
    }
  }

  public class Comment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LineType_ALL.Comment>
  {
    public Comment()
      : base("CM_LN")
    {
    }
  }

  public class Instruction : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LineType_ALL.Instruction>
  {
    public Instruction()
      : base("IT_LN")
    {
    }
  }
}
