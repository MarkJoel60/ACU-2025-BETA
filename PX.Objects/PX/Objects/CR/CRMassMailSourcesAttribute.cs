// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMassMailSourcesAttribute
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
public class CRMassMailSourcesAttribute : PXIntListAttribute
{
  public const int MailList = 0;
  public const int Campaign = 1;
  public const int Lead = 2;

  public CRMassMailSourcesAttribute()
    : base(new int[3]{ 0, 1, 2 }, new string[3]
    {
      "Marketing Lists",
      "Campaigns",
      "Leads/Contacts/Employees"
    })
  {
  }

  public class hold : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  CRMassMailSourcesAttribute.hold>
  {
    public hold()
      : base(1)
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRMassMailSourcesAttribute.pending>
  {
    public pending()
      : base(2)
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRMassMailSourcesAttribute.rejected>
  {
    public rejected()
      : base(0)
    {
    }
  }
}
