// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.UniquenessChecker`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// The class allows to emulate the behavior of unique index via <see cref="E:PX.Data.PXGraph.OnBeforeCommit" /> event.
/// </summary>
/// <typeparam name="TSelect">The query which must return a unique record.</typeparam>
public class UniquenessChecker<TSelect> where TSelect : BqlCommand, new()
{
  private IBqlTable _binding;

  public UniquenessChecker(IBqlTable binding) => this._binding = binding;

  public virtual void OnBeforeCommitImpl(PXGraph graph)
  {
    BqlCommand bqlCommand = (BqlCommand) new TSelect();
    if (new PXView(graph, true, bqlCommand).SelectMultiBound((object[]) new IBqlTable[1]
    {
      this._binding
    }, Array.Empty<object>()).Count > 1)
    {
      PXCache cache = graph.Caches[this._binding.GetType()];
      object[] array = ((IEnumerable<string>) cache.Keys).Select<string, object>((Func<string, object>) (f => cache.GetValue((object) this._binding, f))).ToArray<object>();
      throw new PXLockViolationException(this._binding.GetType(), (PXDBOperation) 1, array);
    }
  }
}
