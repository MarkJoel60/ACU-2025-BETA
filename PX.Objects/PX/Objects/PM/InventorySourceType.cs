// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.InventorySourceType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PM;

public class InventorySourceType
{
  public const 
  #nullable disable
  string FreeStock = "F";
  public const string ProjectStock = "P";
  public const string SpecialStock = "S";

  public class freeStock : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventorySourceType.freeStock>
  {
    public freeStock()
      : base("F")
    {
    }
  }

  public class projectStock : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventorySourceType.projectStock>
  {
    public projectStock()
      : base("P")
    {
    }
  }

  public class specialStock : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventorySourceType.specialStock>
  {
    public specialStock()
      : base("S")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public bool AllowSpecialOrders { get; set; }

    public ListAttribute(bool allowSpecialOrders = false)
    {
      this.AllowSpecialOrders = allowSpecialOrders;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this.SetValues(sender, (object) null);
    }

    public virtual int SetValues(PXCache cache, object row)
    {
      List<(string, string)> valueTupleList = new List<(string, string)>()
      {
        ("F", "Free Stock")
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
        valueTupleList.Add(("P", "Project Stock"));
      if (PXAccess.FeatureInstalled<FeaturesSet.specialOrders>() && this.AllowSpecialOrders)
        valueTupleList.Add(("S", "Special Stock"));
      PXStringListAttribute.SetList(cache, row, ((PXEventSubscriberAttribute) this).FieldName, valueTupleList.ToArray());
      return valueTupleList.Count;
    }
  }
}
