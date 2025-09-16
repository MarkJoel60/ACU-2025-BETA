// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.RestrictOrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.Attributes;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

[PXDBInt]
[PXUIField]
public class RestrictOrganizationAttribute : 
  BaseOrganizationTreeAttribute,
  IPXRowPersistingSubscriber
{
  public RestrictOrganizationAttribute()
    : base(nullable: false)
  {
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2 && e.Operation != 1 || !this.Required)
      return;
    int? nullable = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(this.FieldName, (object) null, "The Restrict Visibility To box is required.");
    }
  }
}
