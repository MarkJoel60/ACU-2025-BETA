// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class ProjectType
{
  public const string Contract = "C";
  public const string Project = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("C", "Customer Contract"),
        PXStringListAttribute.Pair("P", "Project")
      })
    {
    }
  }
}
