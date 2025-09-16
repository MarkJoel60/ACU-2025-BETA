// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXRestrictorWithEraseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Attributes;
using System;

#nullable disable
namespace PX.Objects.CR;

public class PXRestrictorWithEraseAttribute : RestrictorWithParametersAttribute
{
  public PXRestrictorWithEraseAttribute(System.Type where, string message, params System.Type[] pars)
    : base(where, message, pars)
  {
    this.ShowWarning = true;
  }

  protected virtual PXException TryVerify(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    bool IsErrorValueRequired)
  {
    PXException ex = base.TryVerify(sender, e, IsErrorValueRequired);
    PXCacheEx.AdjustUI(sender, e.Row).For(((PXEventSubscriberAttribute) this).FieldName, (Action<PXUIFieldAttribute>) (attribute =>
    {
      if (attribute.ErrorLevel != null)
        return;
      attribute.ExceptionHandling(sender, new PXExceptionHandlingEventArgs(e.Row, (object) null, ex != null ? (Exception) new PXSetPropertyException(ex.MessageNoPrefix, (PXErrorLevel) 4) : (Exception) null));
    }));
    return ex;
  }
}
