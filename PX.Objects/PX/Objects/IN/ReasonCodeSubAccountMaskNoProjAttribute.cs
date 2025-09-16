// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ReasonCodeSubAccountMaskNoProjAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXUIField]
public sealed class ReasonCodeSubAccountMaskNoProjAttribute : PXEntityAttribute
{
  public const string ReasonCode = "R";
  public const string InventoryItem = "I";
  public const string PostingClass = "P";
  public const string Warehouse = "W";
  private static readonly string[] writeOffValues = new string[6]
  {
    "R",
    "I",
    "W",
    "P",
    "J",
    "T"
  };
  private static readonly string[] writeOffLabels = new string[6]
  {
    "Reason Code",
    "Inventory Item",
    nameof (Warehouse),
    "Posting Class",
    "Project",
    "Project Task"
  };
  private static readonly string[] writeOffValuesWithoutProjects = new string[4]
  {
    "R",
    "I",
    "W",
    "P"
  };
  private static readonly string[] writeOffLabelsWithoutProjects = new string[4]
  {
    "Reason Code",
    "Inventory Item",
    nameof (Warehouse),
    "Posting Class"
  };
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "ReasonCodeINShort";

  public ReasonCodeSubAccountMaskNoProjAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "ReasonCodeINShort", "R", ReasonCodeSubAccountMaskNoProjAttribute.writeOffValuesWithoutProjects, ReasonCodeSubAccountMaskNoProjAttribute.writeOffLabelsWithoutProjects);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, ReasonCodeSubAccountMaskNoProjAttribute.writeOffValues, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) ReasonCodeSubAccountMaskNoProjAttribute.writeOffLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
