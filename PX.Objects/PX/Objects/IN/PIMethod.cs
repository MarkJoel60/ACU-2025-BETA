// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public static class PIMethod
{
  public const 
  #nullable disable
  string FullPhysicalInventory = "F";
  public const string ByInventoryItemSelected = "I";
  public const string ByMovementClass = "M";
  public const string ByABCClass = "A";
  public const string ByCycle = "Y";
  public const string ByItemClassID = "C";

  public static bool IsByFrequencyAllowed(string generationMethod)
  {
    return EnumerableExtensions.IsIn<string>(generationMethod, "M", "A", "Y");
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("F", "Full Physical Inventory"),
        PXStringListAttribute.Pair("I", "By Inventory"),
        PXStringListAttribute.Pair("M", "By Movement Class"),
        PXStringListAttribute.Pair("A", "By ABC Code"),
        PXStringListAttribute.Pair("Y", "By Cycle"),
        PXStringListAttribute.Pair("C", "By Item Class")
      })
    {
    }
  }

  public class fullPhysicalInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PIMethod.fullPhysicalInventory>
  {
    public fullPhysicalInventory()
      : base("F")
    {
    }
  }
}
