// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Standalone.INUnit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.Standalone;

[PXPrimaryGraph(typeof (UnitOfMeasureMaint))]
[PXHidden]
[PXBreakInheritance]
[Serializable]
public class INUnit : PX.Objects.IN.INUnit
{
  public new class PK : PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.recordID>
  {
    public static INUnit Find(PXGraph graph, long? recordID, PKFindOptions options = 0)
    {
      return (INUnit) PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public new static class UK
  {
    public class ByGlobal : PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.fromUnit, PX.Objects.IN.INUnit.toUnit>
    {
      public static INUnit Find(PXGraph graph, string fromUnit, string toUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.fromUnit, PX.Objects.IN.INUnit.toUnit>.FindBy(graph, (object) (short) 3, (object) fromUnit, (object) toUnit, (PKFindOptions) 0);
      }

      internal static INUnit FindDirty(PXGraph graph, string fromUnit, string toUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<PX.Objects.IN.INUnit.unitType, Equal<INUnitType.global>, And<PX.Objects.IN.INUnit.fromUnit, Equal<Required<PX.Objects.IN.INUnit.fromUnit>>, And<PX.Objects.IN.INUnit.toUnit, Equal<Required<PX.Objects.IN.INUnit.toUnit>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
        {
          (object) fromUnit,
          (object) toUnit
        }));
      }
    }

    public class ByInventory : 
      PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.inventoryID, PX.Objects.IN.INUnit.fromUnit>
    {
      public static INUnit Find(PXGraph graph, int? inventoryID, string fromUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.inventoryID, PX.Objects.IN.INUnit.fromUnit>.FindBy(graph, (object) (short) 1, (object) inventoryID, (object) fromUnit, (PKFindOptions) 0);
      }

      public static INUnit FindDirty(PXGraph graph, int? inventoryID, string fromUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<PX.Objects.IN.INUnit.unitType, Equal<INUnitType.inventoryItem>, And<PX.Objects.IN.INUnit.inventoryID, Equal<Required<PX.Objects.IN.INUnit.inventoryID>>, And<PX.Objects.IN.INUnit.fromUnit, Equal<Required<PX.Objects.IN.INUnit.fromUnit>>>>>>.Config>.Select(graph, new object[2]
        {
          (object) inventoryID,
          (object) fromUnit
        }));
      }
    }

    public class ByItemClass : 
      PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.itemClassID, PX.Objects.IN.INUnit.fromUnit>
    {
      public static INUnit Find(PXGraph graph, int? itemClassID, string fromUnit)
      {
        return (INUnit) PrimaryKeyOf<INUnit>.By<PX.Objects.IN.INUnit.unitType, PX.Objects.IN.INUnit.itemClassID, PX.Objects.IN.INUnit.fromUnit>.FindBy(graph, (object) (short) 2, (object) itemClassID, (object) fromUnit, (PKFindOptions) 0);
      }

      internal static INUnit FindDirty(PXGraph graph, int? itemClassID, string fromUnit)
      {
        return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelect<INUnit, Where<PX.Objects.IN.INUnit.unitType, Equal<INUnitType.itemClass>, And<PX.Objects.IN.INUnit.itemClassID, Equal<Required<PX.Objects.IN.INUnit.itemClassID>>, And<PX.Objects.IN.INUnit.fromUnit, Equal<Required<PX.Objects.IN.INUnit.fromUnit>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
        {
          (object) itemClassID,
          (object) fromUnit
        }));
      }
    }
  }

  public new static class FK
  {
    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INUnit>.By<PX.Objects.IN.INUnit.itemClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INUnit>.By<PX.Objects.IN.INUnit.inventoryID>
    {
    }
  }
}
