// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LineType_Schedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LineType_Schedule : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LineType_Schedule().ID_LIST_ALL, new ID.LineType_Schedule().TX_LIST_ALL)
    {
    }
  }

  public class Service : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_LineType_Schedule.Service>
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
    ListField_LineType_Schedule.NonStockItem>
  {
    public NonStockItem()
      : base("NSTKI")
    {
    }
  }

  public class Service_Template : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Schedule.Service_Template>
  {
    public Service_Template()
      : base("TEMPL")
    {
    }
  }

  public class Inventory_Item : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Schedule.Inventory_Item>
  {
    public Inventory_Item()
      : base("SLPRO")
    {
    }
  }

  public class Comment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LineType_Schedule.Comment>
  {
    public Comment()
      : base("CM_LN")
    {
    }
  }

  public class Instruction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Schedule.Instruction>
  {
    public Instruction()
      : base("IT_LN")
    {
    }
  }
}
