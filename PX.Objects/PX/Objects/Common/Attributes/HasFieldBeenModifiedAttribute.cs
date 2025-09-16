// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.HasFieldBeenModifiedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>
/// Sets the value of the target boolean field to true once the <see cref="P:PX.Objects.Common.Attributes.HasFieldBeenModifiedAttribute.ObservedField" /> got modified.
/// </summary>
public class HasFieldBeenModifiedAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXRowUpdatedSubscriber
{
  /// <summary>A field the attribute waits an update for.</summary>
  public Type ObservedField { get; set; }

  /// <summary>
  /// A field of whose value the attribute compares to the value of the observed field with to avoid original row selection from the database.
  /// </summary>
  public Type OriginalValueField { get; set; }

  /// <summary>
  /// Indicates whether <c>false</c> or <c>true</c> shall be assigned to the target field once the <see cref="P:PX.Objects.Common.Attributes.HasFieldBeenModifiedAttribute.ObservedField" /> got modified.
  /// </summary>
  public bool InvertResult { get; set; }

  /// <exclude />
  public HasFieldBeenModifiedAttribute(Type observedField) => this.ObservedField = observedField;

  void IPXRowPersistingSubscriber.RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 1 || (this.OriginalValueField == (Type) null ? (!object.Equals(cache.GetValue(e.Row, this.ObservedField.Name), cache.GetValueOriginal(e.Row, this.ObservedField.Name)) ? 1 : 0) : (!object.Equals(cache.GetValue(e.Row, this.ObservedField.Name), cache.GetValue(e.Row, this.OriginalValueField.Name)) ? 1 : 0)) == 0)
      return;
    cache.SetValue(e.Row, this.FieldName, (object) !this.InvertResult);
  }

  void IPXRowUpdatedSubscriber.RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.GetStatus(e.Row) != 2 || object.Equals(cache.GetValue(e.Row, this.ObservedField.Name), cache.GetValue(e.OldRow, this.ObservedField.Name)))
      return;
    cache.SetValue(e.Row, this.FieldName, (object) !this.InvertResult);
  }
}
