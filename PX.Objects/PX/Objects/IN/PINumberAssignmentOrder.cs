// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PINumberAssignmentOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class PINumberAssignmentOrder
{
  public const 
  #nullable disable
  string EmptySort = "ES";
  public const string ByLocationID = "LI";
  public const string ByInventoryID = "II";
  public const string BySubItem = "SI";
  public const string ByLotSerial = "LS";
  public const string ByInventoryDescription = "ID";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("ES", "-"),
        PXStringListAttribute.Pair("LI", "By Location"),
        PXStringListAttribute.Pair("II", "By Inventory ID"),
        PXStringListAttribute.Pair("SI", "By Subitem"),
        PXStringListAttribute.Pair("LS", "By Lot/Serial Number"),
        PXStringListAttribute.Pair("ID", "By Inventory Description")
      })
    {
    }
  }

  public class emptySort : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PINumberAssignmentOrder.emptySort>
  {
    public emptySort()
      : base("ES")
    {
    }
  }

  public class byLocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PINumberAssignmentOrder.byLocationID>
  {
    public byLocationID()
      : base("LI")
    {
    }
  }

  public class byInventoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PINumberAssignmentOrder.byInventoryID>
  {
    public byInventoryID()
      : base("II")
    {
    }
  }

  public class bySubItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PINumberAssignmentOrder.bySubItem>
  {
    public bySubItem()
      : base("SI")
    {
    }
  }

  public class byLotSerial : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PINumberAssignmentOrder.byLotSerial>
  {
    public byLotSerial()
      : base("LS")
    {
    }
  }

  public class byInventoryDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PINumberAssignmentOrder.byInventoryDescription>
  {
    public byInventoryDescription()
      : base("ID")
    {
    }
  }
}
