// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMassMailStatusesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CRMassMailStatusesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Prepared = "P";
  public const string Send = "S";

  public CRMassMailStatusesAttribute()
    : base(new string[3]{ "H", "P", "S" }, new string[3]
    {
      "On Hold",
      nameof (Prepared),
      "Sent"
    })
  {
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRMassMailStatusesAttribute.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class prepared : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRMassMailStatusesAttribute.prepared>
  {
    public prepared()
      : base("P")
    {
    }
  }

  public class send : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRMassMailStatusesAttribute.send>
  {
    public send()
      : base("S")
    {
    }
  }
}
