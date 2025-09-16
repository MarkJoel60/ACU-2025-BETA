// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Linked_Entity_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Linked_Entity_Type
{
  public const 
  #nullable disable
  string ExpenseReceipt = "ER";
  public const string SalesOrder = "SO";
  public const string APBill = "AP";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("ER", "Expense Receipt"),
        PXStringListAttribute.Pair("SO", "Sales Order"),
        PXStringListAttribute.Pair("AP", "AP Bill")
      })
    {
    }
  }

  public class expenseReceipt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Linked_Entity_Type.expenseReceipt>
  {
    public expenseReceipt()
      : base("ER")
    {
    }
  }

  public class salesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Linked_Entity_Type.salesOrder>
  {
    public salesOrder()
      : base("SO")
    {
    }
  }

  public class apBill : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Linked_Entity_Type.apBill>
  {
    public apBill()
      : base("AP")
    {
    }
  }
}
