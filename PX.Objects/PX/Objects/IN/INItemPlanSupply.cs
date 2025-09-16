// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemPlanSupply
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// An alias for Supply Inventory Plan (<see cref="T:PX.Objects.IN.INItemPlan" />) which can be used for building complex BQL queries.
/// </summary>
[PXBreakInheritance]
[PXHidden]
public class INItemPlanSupply : INItemPlan
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  INItemPlanSupply>.By<INItemPlanSupply.planID>
  {
    public static INItemPlanSupply Find(PXGraph graph, long? planID, PKFindOptions options = 0)
    {
      return (INItemPlanSupply) PrimaryKeyOf<INItemPlanSupply>.By<INItemPlanSupply.planID>.FindBy(graph, (object) planID, options);
    }
  }

  public new abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlanSupply.planID>
  {
  }
}
