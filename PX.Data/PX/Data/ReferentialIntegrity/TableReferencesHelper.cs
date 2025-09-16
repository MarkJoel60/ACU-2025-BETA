// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.TableReferencesHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

internal static class TableReferencesHelper
{
  private static IEnumerable<System.Type> GetInheritanceChain(System.Type table)
  {
    if (!table.IgnoreInheritanceChain())
      return table.GetInheritanceChain();
    return (IEnumerable<System.Type>) new System.Type[1]{ table };
  }

  public static int GetInheritanceDepth(System.Type table)
  {
    return !table.IgnoreInheritanceChain() ? table.GetInheritanceDepth() : 1;
  }

  public static bool TableSupportsReferences(this System.Type table)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return !TableReferencesHelper.GetInheritanceChain(table).All<System.Type>(TableReferencesHelper.\u003C\u003EO.\u003C0\u003E__IsVirtualTable ?? (TableReferencesHelper.\u003C\u003EO.\u003C0\u003E__IsVirtualTable = new Func<System.Type, bool>(PXDatabase.IsVirtualTable))) || table.IsDefined(typeof (PXProjectionAttribute)) || table.IsDefined(typeof (PXAccumulatorAttribute));
  }

  public static bool IgnoreInheritanceChain(this System.Type table)
  {
    return table.IsDefined(typeof (PXAccumulatorAttribute));
  }
}
