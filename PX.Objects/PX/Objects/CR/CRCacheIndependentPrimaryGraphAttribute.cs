// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCacheIndependentPrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CRCacheIndependentPrimaryGraphAttribute : PXPrimaryGraphBaseAttribute
{
  private readonly CRCacheIndependentPrimaryGraphListAttribute _att;

  public CRCacheIndependentPrimaryGraphAttribute(System.Type graphType, System.Type condition)
  {
    this._att = new CRCacheIndependentPrimaryGraphListAttribute()
    {
      {
        graphType,
        condition
      }
    };
  }

  public virtual System.Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    System.Type preferedType)
  {
    return ((PXPrimaryGraphBaseAttribute) this._att).GetGraphType(cache, ref row, checkRights, preferedType);
  }

  public virtual IEnumerable<System.Type> GetAllGraphTypes()
  {
    return ((PXPrimaryGraphBaseAttribute) this._att).GetAllGraphTypes();
  }
}
