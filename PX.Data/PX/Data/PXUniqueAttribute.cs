// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUniqueAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Applies application-side unique constraint on a field.
/// </summary>
/// <remarks>
/// This attribute is obsolete and will be removed in future Acumatica versions. Please use the <see cref="T:PX.Data.PXCheckUnique" /> attribute instead.
/// </remarks>
[Obsolete("The PX.Data.PXUniqueAttribute is obsolete and will be removed in future Acumatica versions. Please use the PX.Data.PXCheckUnique attribute instead")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class PXUniqueAttribute : PXEventSubscriberAttribute, IPXRowPersistingSubscriber
{
  private PXView _allRecordsView;
  private System.Type _sourceType;
  /// <summary>
  /// Determines if need to skip nulls. False to treat null as unique value.
  /// True by default.
  /// </summary>
  private bool _allowNulls = true;
  private string _errorMessage = "The value of this field must be unique among all records.";

  public PXUniqueAttribute()
  {
  }

  public PXUniqueAttribute(System.Type sourceType)
  {
    this._sourceType = typeof (IBqlTable).IsAssignableFrom(sourceType) ? sourceType : throw new PXArgumentException(nameof (sourceType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
    {
      (object) sourceType
    });
  }

  public bool AllowNulls
  {
    get => this._allowNulls;
    set => this._allowNulls = value;
  }

  public string ErrorMessage
  {
    get => this._errorMessage;
    set => this._errorMessage = value;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (this._sourceType != (System.Type) null)
      return;
    string name = char.ToLower(this._FieldName[0]).ToString() + this._FieldName.Substring(1);
    System.Type nestedType = this.BqlTable.GetNestedType(name);
    if (nestedType == (System.Type) null)
      throw new PXException("There is no nested class in the DAC for this field.");
    BqlCommand select = typeof (IBqlField).IsAssignableFrom(nestedType) ? BqlCommand.CreateInstance(BqlCommand.Compose(typeof (Select<,>), this.BqlTable, typeof (Where<,>), nestedType, typeof (Equal<>), typeof (Required<>), nestedType)) : throw new PXException($"The type {name} doesn't implement the IBqlField interface.");
    this._allRecordsView = new PXView(sender.Graph, false, select);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || (e.Operation & PXDBOperation.Insert) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Update) != PXDBOperation.Update)
      return;
    object obj = sender.GetValue(e.Row, this._FieldName);
    if (obj == null && this.AllowNulls)
      return;
    IEnumerable<object> objects;
    if (this._sourceType == (System.Type) null)
    {
      int startRow = 0;
      int maximumRows = 2;
      int totalRows = 0;
      objects = (IEnumerable<object>) this._allRecordsView.Select((object[]) null, new object[1]
      {
        obj
      }, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, maximumRows, ref totalRows);
    }
    else
      objects = (IEnumerable<object>) PXParentAttribute.SelectSiblings(sender, e.Row, this._sourceType);
    bool flag = false;
    foreach (object data in objects)
    {
      object objB = sender.GetValue(data, this._FieldName);
      if (object.Equals(obj, objB) && e.Row != data)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    sender.RaiseExceptionHandling(this._FieldName, e.Row, obj, (Exception) new PXSetPropertyException(this.ErrorMessage, new object[1]
    {
      (object) $"[{this._FieldName}]"
    }));
  }
}
