// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRSACryptDataProviderPasswordParameterAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Encryption is used for value of Password parameter for Data Providers.
/// isEncryptedField is used to store current state of field - encrypted or not.
/// </summary>
/// <param name="nameField">BQL field</param>
/// <param name="isEncryptedField">BQL field</param>
public class PXRSACryptDataProviderPasswordParameterAttribute : PXRSACryptStringAttribute
{
  protected System.Type _nameField;
  protected System.Type _isEncryptedField;

  public PXRSACryptDataProviderPasswordParameterAttribute(System.Type nameField, System.Type isEncryptedField)
  {
    this.checkParams(nameField, isEncryptedField);
    this._nameField = nameField;
    this._isEncryptedField = isEncryptedField;
  }

  public PXRSACryptDataProviderPasswordParameterAttribute(
    int length,
    System.Type nameField,
    System.Type isEncryptedField)
    : base(length)
  {
    this.checkParams(nameField, isEncryptedField);
    this._nameField = nameField;
    this._isEncryptedField = isEncryptedField;
  }

  private void checkParams(System.Type nameField, System.Type isEncryptedField)
  {
    if (nameField == (System.Type) null)
      throw new PXArgumentException(nameof (nameField), "The argument cannot be null.");
    if (isEncryptedField == (System.Type) null)
      throw new PXArgumentException(nameof (isEncryptedField), "The argument cannot be null.");
    if (!typeof (IBqlField).IsAssignableFrom(nameField))
      throw new PXArgumentException(nameof (nameField), "The type {0} must inherit the PX.Data.IBqlField interface.");
    if (!typeof (IBqlField).IsAssignableFrom(isEncryptedField))
      throw new PXArgumentException(nameof (isEncryptedField), "The type {0} must inherit the PX.Data.IBqlField interface.");
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      bool? nullable = new bool?((sender.GetValue(e.Row, this._isEncryptedField.Name) as bool?).GetValueOrDefault());
      bool flag = false;
      this.isViewDeprypted = nullable.GetValueOrDefault() == flag & nullable.HasValue;
    }
    base.FieldSelecting(sender, e);
  }

  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      bool? nullable = new bool?((sender.GetValue(e.Row, this._isEncryptedField.Name) as bool?).GetValueOrDefault());
      bool flag = true;
      this.isEncryptionRequired = nullable.GetValueOrDefault() == flag & nullable.HasValue;
    }
    base.RowSelecting(sender, e);
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
    {
      sender.GetValue(e.Row, this._nameField.Name);
      this.isEncryptionRequired = (sender.GetValue(e.Row, this._isEncryptedField.Name) as bool?).GetValueOrDefault();
    }
    base.CommandPreparing(sender, e);
    if (!this.isEncryptionRequired || e.Value != null)
      return;
    e.DataValue = (object) string.Empty;
  }

  public override bool EncryptOnCertificateReplacement(PXCache cache, object row)
  {
    bool? nullable = cache.GetValue(row, this._isEncryptedField.Name) as bool?;
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }
}
