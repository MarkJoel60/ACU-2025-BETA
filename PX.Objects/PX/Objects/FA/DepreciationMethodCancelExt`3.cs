// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethodCancelExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class DepreciationMethodCancelExt<TGraph, TPrimary, TWhere> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TWhere : class, IBqlWhere, new()
{
  public PXSelect<TPrimary> View;

  public virtual string Message { get; }

  public static bool IsActive() => true;

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    DepreciationMethodCancelExt<TGraph, TPrimary, TWhere> depreciationMethodCancelExt = this;
    foreach (TPrimary primary in ((PXAction) new PXCancel<TPrimary>((PXGraph) depreciationMethodCancelExt.Base, nameof (Cancel))).Press(a))
    {
      if (primary is FADepreciationMethod depreciationMethod && !string.IsNullOrEmpty(depreciationMethod.MethodCD) && ((PXSelectBase) depreciationMethodCancelExt.View).Cache.GetStatus((object) primary) == 2 && ((IQueryable<PXResult<TPrimary>>) PXSelectBase<TPrimary, PXSelect<TPrimary, TWhere>.Config>.Select((PXGraph) depreciationMethodCancelExt.Base, Array.Empty<object>())).Any<PXResult<TPrimary>>())
        ((PXSelectBase) depreciationMethodCancelExt.View).Cache.RaiseExceptionHandling<FADepreciationMethod.methodCD>((object) primary, (object) depreciationMethod.MethodCD, (Exception) new PXSetPropertyException(depreciationMethodCancelExt.Message));
      yield return (object) primary;
    }
  }
}
