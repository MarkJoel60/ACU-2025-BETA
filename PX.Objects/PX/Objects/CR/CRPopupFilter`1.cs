// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRPopupFilter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
[Obsolete("Use CRValidationFilter instead.")]
public class CRPopupFilter<Table> : PXFilter<Table> where Table : class, IBqlTable, new()
{
  public CRPopupFilter(PXGraph graph)
    : base(graph)
  {
  }

  public CRPopupFilter(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public virtual bool VerifyRequired(bool suppressError)
  {
    bool flag = true;
    foreach (string field in (List<string>) ((PXSelectBase) this).Cache.Fields)
    {
      object obj = ((PXSelectBase) this).Cache.GetValue(((PXSelectBase) this).Cache.Current, field);
      try
      {
        ((PXSelectBase) this).Cache.RaiseFieldVerifying(field, ((PXSelectBase) this).Cache.Current, ref obj);
      }
      catch (PXException ex)
      {
        if (!suppressError)
          ((PXSelectBase) this).Cache.RaiseExceptionHandling(field, ((PXSelectBase) this).Cache.Current, obj, (Exception) ex);
        flag = false;
      }
    }
    return flag;
  }
}
