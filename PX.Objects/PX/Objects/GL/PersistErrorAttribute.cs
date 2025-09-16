// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PersistErrorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Maps the errors from one field to another. Whenever an error is raised on the Source field it is transfered to
/// the target field and gets displayed on the targer field.
/// To use this attribute decorate the Source field with this attribute and pass Taget field an argument in the constructor of this attrubute.
/// </summary>
/// <example>
/// [PersistError(typeof(GLTrialBalanceImportDetails.importAccountCDError))]
/// </example>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class PersistErrorAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  private readonly string _errorFieldName;

  public PersistErrorAttribute(Type errorField) => this._errorFieldName = errorField.Name;

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    string error = sender.GetValue(e.Row, this._errorFieldName) as string;
    if (string.IsNullOrEmpty(error))
      return;
    this.SetError(sender, e.Row, error);
    if (e.ReturnState == null)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this._FieldName, (string) null, (string) null, error, (PXErrorLevel) 4, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }

  public static void SetError(PXCache sender, object data, string fieldName, string error)
  {
    PersistErrorAttribute attribute = PersistErrorAttribute.GetAttribute(sender, fieldName);
    if (attribute == null)
      return;
    sender.SetValue(data, attribute._errorFieldName, (object) error);
    attribute.SetError(sender, data, error);
  }

  public static void SetError<TField>(PXCache sender, object data, string error) where TField : IBqlField
  {
    PersistErrorAttribute.SetError(sender, data, typeof (TField).Name, error);
  }

  public static void ClearError(PXCache sender, object data, string fieldName)
  {
    PersistErrorAttribute attribute = PersistErrorAttribute.GetAttribute(sender, fieldName);
    if (attribute == null)
      return;
    sender.SetValue(data, attribute._errorFieldName, (object) null);
  }

  public static void ClearError<TField>(PXCache sender, object data) where TField : IBqlField
  {
    PersistErrorAttribute.ClearError(sender, data, typeof (TField).Name);
  }

  private void SetError(PXCache sender, object data, string error)
  {
    object obj = sender.GetValue(data, this._FieldOrdinal);
    PXUIFieldAttribute.SetError(sender, data, this._FieldName, error, obj == null ? (string) null : obj.ToString());
  }

  private static PersistErrorAttribute GetAttribute(PXCache sender, string fieldName)
  {
    foreach (PXEventSubscriberAttribute attribute1 in sender.GetAttributes(fieldName))
    {
      if (attribute1 is PersistErrorAttribute attribute2)
        return attribute2;
    }
    return (PersistErrorAttribute) null;
  }
}
