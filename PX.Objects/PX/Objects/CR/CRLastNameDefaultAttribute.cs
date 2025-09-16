// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLastNameDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

internal sealed class CRLastNameDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object obj = sender.GetValue(e.Row, typeof (Contact.contactType).Name);
    string str = sender.GetValue(e.Row, this._FieldOrdinal) as string;
    if (obj == null || !obj.Equals((object) "PN") || !string.IsNullOrWhiteSpace(str))
      return;
    if (sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{this._FieldName}]"
    }))))
      throw new PXRowPersistingException(this._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) this._FieldName
      });
  }
}
