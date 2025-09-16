// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLineType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public class FSLineType
{
  public static 
  #nullable disable
  FSLineType.CustomListAttribute GetDropDownList(
    bool includeIN,
    bool includeTemplate,
    bool includePickup,
    bool includeCI,
    bool includeAll)
  {
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    tupleList.Add(new Tuple<string, string>("SERVI", "Service"));
    tupleList.Add(new Tuple<string, string>("NSTKI", "Non-Stock Item"));
    if (includeIN)
      tupleList.Add(new Tuple<string, string>("SLPRO", "Inventory Item"));
    if (includeCI)
    {
      tupleList.Add(new Tuple<string, string>("CM_LN", "Comment"));
      tupleList.Add(new Tuple<string, string>("IT_LN", "Instruction"));
    }
    if (includePickup)
      tupleList.Add(new Tuple<string, string>("PU_DL", "Pickup/Delivery Item"));
    if (includeTemplate)
      tupleList.Add(new Tuple<string, string>("TEMPL", "Service Template"));
    if (includeAll)
      tupleList.Add(new Tuple<string, string>("<ALL>", "All"));
    return new FSLineType.CustomListAttribute(tupleList.ToArray());
  }

  public static void SetLineTypeList<LineTypeField>(
    PXCache cache,
    object row,
    bool includeIN,
    bool includeTemplate,
    bool includePickup,
    bool includeCI,
    bool includeAll)
    where LineTypeField : class, IBqlField
  {
    FSLineType.CustomListAttribute dropDownList = FSLineType.GetDropDownList(includeIN, includeTemplate, includePickup, includeCI, includeAll);
    PXStringListAttribute.SetList<LineTypeField>(cache, row, dropDownList.AllowedValues, dropDownList.AllowedLabels);
  }

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] allowedValues, string[] allowedLabels)
      : base(allowedValues, allowedLabels)
    {
    }

    public CustomListAttribute(Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class ListAttribute : FSLineType.CustomListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("SERVI", "Service"),
        PXStringListAttribute.Pair("NSTKI", "Non-Stock Item"),
        PXStringListAttribute.Pair("SLPRO", "Inventory Item"),
        PXStringListAttribute.Pair("CM_LN", "Comment"),
        PXStringListAttribute.Pair("IT_LN", "Instruction"),
        PXStringListAttribute.Pair("TEMPL", "Service Template"),
        PXStringListAttribute.Pair("PU_DL", "Pickup/Delivery Item"),
        PXStringListAttribute.Pair("<ALL>", "All")
      })
    {
    }
  }

  public class Service : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLineType.Service>
  {
    public Service()
      : base("SERVI")
    {
    }
  }

  public class NonStockItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLineType.NonStockItem>
  {
    public NonStockItem()
      : base("NSTKI")
    {
    }
  }

  public class Inventory_Item : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLineType.Inventory_Item>
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
  FSLineType.Comment>
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
  FSLineType.Instruction>
  {
    public Instruction()
      : base("IT_LN")
    {
    }
  }

  public class ServiceTemplate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLineType.ServiceTemplate>
  {
    public ServiceTemplate()
      : base("TEMPL")
    {
    }
  }

  public class All : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLineType.All>
  {
    public All()
      : base("<ALL>")
    {
    }
  }
}
