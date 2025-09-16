// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARAdjustCorrectionExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class ARAdjustCorrectionExtension<TGraph, TCancellationField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
{
  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AR.ARRegister.isUnderCorrection, NotEqual<True>>), "The application cannot be created because another cancellation invoice or correction invoice already exists for the invoice {0}.", new Type[] {typeof (ARAdjust.ARInvoice.refNbr)})]
  public virtual void _(PX.Data.Events.CacheAttached<ARAdjust.adjdRefNbr> e)
  {
  }

  protected virtual PX.Objects.AR.ARInvoice GetParentInvoice(PXCache cache, ARAdjust aRAdjust)
  {
    return PXParentAttribute.SelectParent<PX.Objects.AR.ARInvoice>(cache, (object) aRAdjust);
  }

  public virtual void _(PX.Data.Events.FieldVerifying<ARAdjust.adjdRefNbr> e)
  {
    if (!this.GetCancellationFieldValue().GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust.adjdRefNbr>>) e).Cancel = true;
  }

  public virtual void _(PX.Data.Events.RowPersisting<ARAdjust> e)
  {
    if ((e.Operation & 3) != 2 || this.GetCancellationFieldValue().GetValueOrDefault())
      return;
    PX.Objects.AR.ARInvoice parentInvoice = this.GetParentInvoice(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARAdjust>>) e).Cache, e.Row);
    if (parentInvoice == null || !parentInvoice.IsUnderCorrection.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARAdjust>>) e).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) e.Row, (object) e.Row.AdjdRefNbr, (Exception) new PXSetPropertyException("The application cannot be created because another cancellation invoice or correction invoice already exists for the invoice {0}.", new object[1]
    {
      (object) parentInvoice.RefNbr
    }));
  }

  public virtual bool? GetCancellationFieldValue()
  {
    Type c = typeof (TCancellationField);
    if (!typeof (IBqlField).IsAssignableFrom(c))
      return new bool?(false);
    PXCache cach = this.Base.Caches[c.DeclaringType];
    return (bool?) cach.GetValue(cach.Current, c.Name);
  }
}
