// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class ProjectTaskStatus
{
  public const 
  #nullable disable
  string Planned = "D";
  public const string Active = "A";
  public const string Canceled = "C";
  public const string Completed = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("D", "In Planning"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("F", "Completed")
      })
    {
    }
  }

  public class planned : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskStatus.planned>
  {
    public planned()
      : base("D")
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskStatus.completed>
  {
    public completed()
      : base("F")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskStatus.canceled>
  {
    public canceled()
      : base("C")
    {
    }
  }
}
