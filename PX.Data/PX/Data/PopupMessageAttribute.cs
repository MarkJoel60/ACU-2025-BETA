// Decompiled with JetBrains decompiler
// Type: PX.Data.PopupMessageAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Linq;

#nullable disable
namespace PX.Data;

public class PopupMessageAttribute : PXEventSubscriberAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    foreach (PXSelectorAttribute selectorAttribute in sender.GetAttributes(this._FieldName).OfType<PXSelectorAttribute>())
      selectorAttribute.ShowPopupMessage = true;
  }
}
