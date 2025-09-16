// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXRSACryptStringWithConditionalAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Works very much like PXRSACryptStringAttribute. Encryption is used conditionally, depending on value of EncryptionRequiredField.
/// isEncryptedField is used to store current state of field - encrypted or not.
/// </summary>
/// <param name="EncryptionRequiredField">BQL field</param>
/// <param name="isEncryptedField">BQL field</param>
public class PXRSACryptStringWithConditionalAttribute : 
  PXRSACryptStringAttribute,
  IPXRowPersistingSubscriber
{
  protected Type _EncryptionRequiredField;
  protected Type _isEncryptedField;

  public PXRSACryptStringWithConditionalAttribute(
    Type encryptionRequiredField,
    Type isEncryptedField)
  {
    this.checkParams(encryptionRequiredField, isEncryptedField);
    this._EncryptionRequiredField = encryptionRequiredField;
    this._isEncryptedField = isEncryptedField;
  }

  public PXRSACryptStringWithConditionalAttribute(
    int length,
    Type encryptionRequiredField,
    Type isEncryptedField)
    : base(length)
  {
    this.checkParams(encryptionRequiredField, isEncryptedField);
    this._EncryptionRequiredField = encryptionRequiredField;
    this._isEncryptedField = isEncryptedField;
  }

  private void checkParams(Type encryptionRequiredField, Type isEncryptedField)
  {
    if (encryptionRequiredField == (Type) null)
      throw new PXArgumentException("EncryptionRequiredField", "The argument cannot be null.");
    if (isEncryptedField == (Type) null)
      throw new PXArgumentException(nameof (isEncryptedField), "The argument cannot be null.");
    if (!typeof (IBqlField).IsAssignableFrom(encryptionRequiredField))
      throw new PXArgumentException("EncryptionRequiredField", "Parameter should cointain a BqlField");
    if (!typeof (IBqlField).IsAssignableFrom(isEncryptedField))
      throw new PXArgumentException(nameof (isEncryptedField), "Parameter should cointain a BqlField");
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row != null && !((PXDBCryptStringAttribute) this).isViewDeprypted)
    {
      bool? nullable = new bool?((sender.GetValue(e.Row, this._isEncryptedField.Name) as bool?).GetValueOrDefault());
      bool flag = false;
      ((PXDBCryptStringAttribute) this).isViewDeprypted = nullable.GetValueOrDefault() == flag & nullable.HasValue;
    }
    ((PXDBCryptStringAttribute) this).FieldSelecting(sender, e);
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      ((PXDBCryptStringAttribute) this).isEncryptionRequired = new bool?((sender.GetValue(e.Row, this._isEncryptedField.Name) as bool?).GetValueOrDefault()).GetValueOrDefault();
    ((PXDBCryptStringAttribute) this).RowSelecting(sender, e);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    bool? nullable = sender.GetValue(e.Row, this._EncryptionRequiredField.Name) as bool?;
    sender.SetValue(e.Row, this._isEncryptedField.Name, (object) nullable.GetValueOrDefault());
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) == 2 || (e.Operation & 3) == 1)
      ((PXDBCryptStringAttribute) this).isEncryptionRequired = (sender.GetValue(e.Row, this._EncryptionRequiredField.Name) as bool?).GetValueOrDefault();
    ((PXDBCryptStringAttribute) this).CommandPreparing(sender, e);
  }

  public virtual bool EncryptOnCertificateReplacement(PXCache cache, object row)
  {
    return (cache.GetValue(row, this._isEncryptedField.Name) as bool?).GetValueOrDefault();
  }
}
