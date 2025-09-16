// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSMEmailMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CRSMEmailMaint : PXGraph<CRSMEmailMaint>
{
  public PXSelectReadonly<SMEmail> Email;
  public PXCancelClose<SMEmail> CancelClose;
  public PXDelete<SMEmail> Delete;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(Events.CacheAttached<SMEmail.subject> e)
  {
  }

  protected virtual void SMEmail_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    SMEmail row = (SMEmail) e.Row;
    ((PXAction) this.Delete).SetEnabled(row != null && row.MPStatus != "PD");
    cache.RaiseExceptionHandling("exception", (object) row, (object) null, string.IsNullOrEmpty(row.Exception) ? (Exception) null : (Exception) new PXSetPropertyException(row.Exception, (PXErrorLevel) 5));
  }
}
