// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INItemTypes
{
  public const 
  #nullable disable
  string NonStockItem = "N";
  public const string LaborItem = "L";
  public const string ServiceItem = "S";
  public const string ChargeItem = "C";
  public const string ExpenseItem = "E";
  public const string FinishedGood = "F";
  public const string Component = "M";
  public const string SubAssembly = "A";

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
      : base(AllowedValues, AllowedLabels)
    {
    }

    public CustomListAttribute(params Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class StockListAttribute : INItemTypes.CustomListAttribute
  {
    public StockListAttribute()
      : base(PXStringListAttribute.Pair("F", "Finished Good"), PXStringListAttribute.Pair("M", "Component Part"), PXStringListAttribute.Pair("A", "Subassembly"))
    {
    }
  }

  public class NonStockListAttribute : INItemTypes.CustomListAttribute
  {
    public NonStockListAttribute()
      : base(PXStringListAttribute.Pair("N", "Non-Stock Item"), PXStringListAttribute.Pair("L", "Labor"), PXStringListAttribute.Pair("S", "Service"), PXStringListAttribute.Pair("C", "Charge"), PXStringListAttribute.Pair("E", "Expense"))
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("F", "Finished Good"),
        PXStringListAttribute.Pair("M", "Component Part"),
        PXStringListAttribute.Pair("A", "Subassembly"),
        PXStringListAttribute.Pair("N", "Non-Stock Item"),
        PXStringListAttribute.Pair("L", "Labor"),
        PXStringListAttribute.Pair("S", "Service"),
        PXStringListAttribute.Pair("C", "Charge"),
        PXStringListAttribute.Pair("E", "Expense")
      })
    {
    }
  }

  public class nonStockItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.nonStockItem>
  {
    public nonStockItem()
      : base("N")
    {
    }
  }

  public class laborItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.laborItem>
  {
    public laborItem()
      : base("L")
    {
    }
  }

  public class serviceItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.serviceItem>
  {
    public serviceItem()
      : base("S")
    {
    }
  }

  public class chargeItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.chargeItem>
  {
    public chargeItem()
      : base("C")
    {
    }
  }

  public class expenseItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.expenseItem>
  {
    public expenseItem()
      : base("E")
    {
    }
  }

  public class finishedGood : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.finishedGood>
  {
    public finishedGood()
      : base("F")
    {
    }
  }

  public class component : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.component>
  {
    public component()
      : base("M")
    {
    }
  }

  public class subAssembly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemTypes.subAssembly>
  {
    public subAssembly()
      : base("A")
    {
    }
  }
}
