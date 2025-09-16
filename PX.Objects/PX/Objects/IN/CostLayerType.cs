// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CostLayerType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

public class CostLayerType
{
  public const 
  #nullable disable
  string Normal = "N";
  public const string Project = "P";
  public const string Special = "S";
  public const string Production = "R";

  public class normal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostLayerType.normal>
  {
    public normal()
      : base("N")
    {
    }
  }

  public class project : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostLayerType.project>
  {
    public project()
      : base("P")
    {
    }
  }

  public class special : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostLayerType.special>
  {
    public special()
      : base("S")
    {
    }
  }

  public class production : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostLayerType.production>
  {
    public production()
      : base("R")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public bool AllowSpecialOrders { get; set; } = true;

    public bool AllowProjects { get; set; } = true;

    public bool AllowProductionOrders { get; set; } = true;

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this.SetValues(sender, (object) null);
    }

    public virtual int SetValues(PXCache cache, object row)
    {
      List<(string, string)> valueTupleList = new List<(string, string)>()
      {
        ("N", "Normal")
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.specialOrders>() && this.AllowSpecialOrders)
        valueTupleList.Add(("S", "Special"));
      if (PXAccess.FeatureInstalled<FeaturesSet.materialManagement>() && this.AllowProjects)
        valueTupleList.Add(("P", "Project"));
      PXStringListAttribute.SetList(cache, row, ((PXEventSubscriberAttribute) this).FieldName, valueTupleList.ToArray());
      return valueTupleList.Count;
    }
  }
}
