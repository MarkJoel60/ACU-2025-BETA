// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidationFilter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public sealed class CRValidationFilter<TTable> : 
  PXFilter<TTable>,
  ICRValidationFilter,
  ICRPreserveCachedRecordsFilter
  where TTable : class, IBqlTable, new()
{
  public CRValidationFilter(PXGraph graph)
    : base(graph)
  {
  }

  public CRValidationFilter(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public virtual bool VerifyRequired(bool suppressError) => this.TryValidate();

  public bool TryValidate() => this.VerifyRequiredFields() & base.VerifyRequired(false);

  public void Validate()
  {
    if (!this.TryValidate())
    {
      TTable table = PXResult<TTable>.op_Implicit(((IQueryable<PXResult<TTable>>) ((PXSelectBase<TTable>) this).Select(Array.Empty<object>())).First<PXResult<TTable>>());
      throw new PXOuterException(PXUIFieldAttribute.GetErrors(((PXSelectBase) this).Cache, (object) table, Array.Empty<PXErrorLevel>()), ((PXSelectBase) this).Cache.Graph.GetType(), (object) table, "{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
      {
        (object) "Validating",
        (object) table.GetType().Name
      });
    }
  }

  public IDisposable PreserveCachedRecords()
  {
    TTable current = ((PXSelectBase<TTable>) this).Current;
    List<(object, PXEntryStatus)> cached = ((PXSelectBase) this).Cache.Cached.OfType<object>().Select<object, (object, PXEntryStatus)>((Func<object, (object, PXEntryStatus)>) (i => (i, ((PXSelectBase) this).Cache.GetStatus(i)))).ToList<(object, PXEntryStatus)>();
    return Disposable.Create((Action) (() =>
    {
      ((PXSelectBase<TTable>) this).Current = current;
      foreach ((object, PXEntryStatus) valueTuple in cached)
        ((PXSelectBase) this).Cache.SetStatus(valueTuple.Item1, valueTuple.Item2);
    }));
  }

  private bool VerifyRequiredFields()
  {
    bool flag = true;
    foreach (PXResult<TTable> pxResult in ((PXSelectBase<TTable>) this).Select(Array.Empty<object>()))
    {
      TTable table = PXResult<TTable>.op_Implicit(pxResult);
      foreach (string field in (List<string>) ((PXSelectBase) this).Cache.Fields)
      {
        object obj = ((PXSelectBase) this).Cache.GetValue((object) table, field);
        try
        {
          ((PXSelectBase) this).Cache.RaiseFieldVerifying(field, (object) table, ref obj);
        }
        catch (PXException ex)
        {
          ((PXSelectBase) this).Cache.RaiseExceptionHandling(field, (object) table, obj, (Exception) ex);
          flag = false;
        }
        if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly(((PXSelectBase) this).Cache, (object) table, field)))
          flag = false;
      }
    }
    return flag;
  }

  void ICRValidationFilter.Reset() => this.Reset();
}
