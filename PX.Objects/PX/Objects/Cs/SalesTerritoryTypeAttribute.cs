// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SalesTerritoryTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SalesTerritoryTypeAttribute : PXStringListAttribute, IPXFieldVerifyingSubscriber
{
  public const 
  #nullable disable
  string ByCountry = "C";
  public const string ByState = "S";
  public const string Other = "O";

  public SalesTerritoryTypeAttribute()
    : base(new string[3]{ "C", "S", "O" }, new string[3]
    {
      "By Country",
      "By State",
      nameof (Other)
    })
  {
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!"O".Equals(e.NewValue))
      return;
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this).FieldName, e.Row, (object) null, (Exception) new PXSetPropertyException("A sales territory with the Other type cannot be inserted automatically for a record. You can manually specify the territory in the needed records.", (PXErrorLevel) 2));
  }

  public class byCountry : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SalesTerritoryTypeAttribute.byCountry>
  {
    public byCountry()
      : base("C")
    {
    }
  }

  public class byState : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SalesTerritoryTypeAttribute.byState>
  {
    public byState()
      : base("S")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SalesTerritoryTypeAttribute.other>
  {
    public other()
      : base("O")
    {
    }
  }
}
