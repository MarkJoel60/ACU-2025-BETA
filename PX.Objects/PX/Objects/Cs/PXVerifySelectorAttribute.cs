// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXVerifySelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class PXVerifySelectorAttribute : PXSelectorAttribute
{
  protected bool _VerifyField = true;

  public bool VerifyField
  {
    get => this._VerifyField;
    set => this._VerifyField = value;
  }

  public PXVerifySelectorAttribute(Type searchtype)
    : base(searchtype)
  {
  }

  public PXVerifySelectorAttribute(Type searchtype, params Type[] fieldList)
    : base(searchtype, fieldList)
  {
  }

  public static void SetVerifyField<Field>(PXCache cache, object data, bool isVerifyField) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXVerifySelectorAttribute)
        ((PXVerifySelectorAttribute) attribute).VerifyField = isVerifyField;
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this._VerifyField)
      return;
    base.FieldVerifying(sender, e);
  }
}
