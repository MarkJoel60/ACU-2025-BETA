// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPrimaryGraphBaseAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class PXPrimaryGraphBaseAttribute : Attribute
{
  public abstract System.Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    System.Type preferedType);

  public virtual bool UseParent { get; set; }

  /// <summary>Returns all possible graph types</summary>
  public virtual IEnumerable<System.Type> GetAllGraphTypes() => Enumerable.Empty<System.Type>();
}
