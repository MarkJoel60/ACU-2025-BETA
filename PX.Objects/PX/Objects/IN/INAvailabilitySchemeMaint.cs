// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAvailabilitySchemeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INAvailabilitySchemeMaint : PXGraph<INAvailabilitySchemeMaint, INAvailabilityScheme>
{
  public PXSelect<INAvailabilityScheme> Schemes;

  protected virtual void INAvailabilityScheme_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelectReadonly<INItemClass, Where<INItemClass.availabilitySchemeID, Equal<Current<INAvailabilityScheme.availabilitySchemeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("This availability calculation rule cannot be deleted because it is assigned to at least one item class.");
  }
}
