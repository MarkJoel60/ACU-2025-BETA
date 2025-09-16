// Decompiled with JetBrains decompiler
// Type: PX.SM.TypeInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Concurrent;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

internal static class TypeInfoProvider
{
  private static readonly ConcurrentDictionary<string, System.Type> KnownTypes = new ConcurrentDictionary<string, System.Type>();

  static TypeInfoProvider()
  {
    PXCodeDirectoryCompiler.NotifyOnChange((System.Action) (() => TypeInfoProvider.KnownTypes.Clear()));
  }

  internal static System.Type GetType(string typeName)
  {
    return string.IsNullOrEmpty(typeName) ? (System.Type) null : TypeInfoProvider.KnownTypes.GetOrAdd(typeName, (Func<string, System.Type>) (x =>
    {
      System.Type type = PXBuildManager.GetType(x, false);
      return (object) type != null ? type : System.Type.GetType(x);
    }));
  }
}
