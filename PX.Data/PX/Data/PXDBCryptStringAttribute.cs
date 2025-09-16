// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBCryptStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name.</summary>
public class PXDBCryptStringAttribute : 
  PXDBStringAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowSelectingSubscriber
{
  protected bool isViewDeprypted;
  protected string viewAsString;
  protected System.Type viewAsField;
  protected bool isEncryptionRequired = true;

  /// <summary>Get, set.</summary>
  public bool IsViewDecrypted
  {
    get => this.isViewDeprypted;
    set => this.isViewDeprypted = value;
  }

  /// <summary>Get, set.</summary>
  public string ViewAsString
  {
    get => this.viewAsString;
    set
    {
      this.viewAsField = (System.Type) null;
      this.viewAsString = value;
    }
  }

  /// <summary>Get, set.</summary>
  public System.Type ViewAsField
  {
    get => this.viewAsField;
    set
    {
      this.viewAsField = value;
      this.viewAsString = (string) null;
    }
  }

  /// <summary>Get, set.</summary>
  public bool IsEncryptionRequired
  {
    get => this.isEncryptionRequired;
    set => this.isEncryptionRequired = value;
  }

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBCryptStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum
  /// length.</summary>
  public PXDBCryptStringAttribute(int length)
    : base(length)
  {
  }

  /// <summary>Overrides the visible state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetDecrypted(PXCache cache, object data, string field, bool isDecrypted)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, field))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).IsViewDecrypted = isDecrypted;
    }
  }

  /// <summary>Overrides the visible state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetDecrypted(PXCache cache, string field, bool isDecrypted)
  {
    cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).IsViewDecrypted = isDecrypted;
        break;
      }
    }
  }

  /// <summary>Overrides the visible state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetDecrypted<Field>(PXCache cache, object data, bool isDecrypted) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).IsViewDecrypted = isDecrypted;
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetDecrypted<Field>(PXCache cache, bool isDecrypted) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).IsViewDecrypted = isDecrypted;
        break;
      }
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs(PXCache cache, object data, string field, string source)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, field))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).ViewAsString = source;
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs(PXCache cache, string field, string source)
  {
    cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).ViewAsString = source;
        break;
      }
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs<Field>(PXCache cache, object data, string source) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).ViewAsString = source;
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs<Field>(PXCache cache, string source) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).ViewAsString = source;
        break;
      }
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs(PXCache cache, object data, string field, System.Type sourceField)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, field))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).ViewAsField = sourceField;
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs(PXCache cache, string field, System.Type sourceField)
  {
    cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).ViewAsField = sourceField;
        break;
      }
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs<Field>(PXCache cache, object data, System.Type sourceField) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBCryptStringAttribute)
        ((PXDBCryptStringAttribute) attribute).ViewAsField = sourceField;
    }
  }

  /// <summary>Overrides the view as state for the particular data
  /// item.</summary>
  /// <param name="cache">Cache containing the data item.</param>
  /// <param name="def">Default value.</param>
  public static void SetViewAs<Field>(PXCache cache, System.Type sourceField) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDBCryptStringAttribute)
      {
        ((PXDBCryptStringAttribute) attribute).ViewAsField = sourceField;
        break;
      }
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (sender.BypassAuditFields.Contains(this.FieldName))
      return;
    sender.BypassAuditFields.Add(this.FieldName);
  }

  /// <exclude />
  public new virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.isViewDeprypted || e.Row == null || !((string) e.NewValue == this.ViewString(sender, e.Row)))
      return;
    e.Cancel = true;
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    base.RowSelecting(sender, e);
    if (e.Row == null || sender.GetStatus(e.Row) != PXEntryStatus.Notchanged)
      return;
    string s = (string) sender.GetValue(e.Row, this._FieldOrdinal);
    string str = s;
    if (!string.IsNullOrEmpty(s))
    {
      if (this.isEncryptionRequired)
      {
        try
        {
          str = Encoding.Unicode.GetString(this.Decrypt(Convert.FromBase64String(s)));
        }
        catch (Exception ex1)
        {
          try
          {
            str = Encoding.Unicode.GetString(Convert.FromBase64String(s));
          }
          catch (Exception ex2)
          {
            str = s;
          }
        }
      }
      else
        str = s;
    }
    sender.SetValue(e.Row, this._FieldOrdinal, (object) str?.Replace(sender.Graph.SqlDialect.WildcardFieldSeparator, string.Empty));
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!this.isViewDeprypted && e.Row != null)
      e.ReturnValue = (object) this.ViewString(sender, e.Row);
    base.FieldSelecting(sender, e);
    if (!this.isViewDeprypted && e.Row != null && e.ReturnState is PXStringState returnState)
      returnState.IsPassword = true;
    if (!sender._SelectingForAuditExplore || !(e.ReturnValue is string) || sender._EncryptAuditFields == null)
      return;
    if (!sender._EncryptAuditFields.Contains(this.FieldName))
      return;
    try
    {
      e.ReturnValue = (object) Encoding.Unicode.GetString(this.Decrypt(Convert.FromBase64String((string) e.ReturnValue)));
    }
    catch
    {
    }
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update) && this.isEncryptionRequired)
    {
      string s = (string) sender.GetValue(e.Row, this._FieldOrdinal);
      e.Value = !string.IsNullOrEmpty(s) ? (object) Convert.ToBase64String(this.Encrypt(Encoding.Unicode.GetBytes(s))) : (object) (string) null;
    }
    base.CommandPreparing(sender, e);
  }

  /// <exclude />
  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    string str = (string) sender.GetValue(e.NewRow, this._FieldOrdinal);
    if (this.IsViewDecrypted || !(str == this.ViewString(sender, e.NewRow)))
      return;
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    sender.SetValue(e.NewRow, this._FieldOrdinal, obj);
  }

  private void Encrypt(PXCache sender, object row)
  {
    if (row == null)
      return;
    string s = (string) sender.GetValue(row, this._FieldOrdinal);
    string str = string.Empty;
    if (!string.IsNullOrEmpty(s))
      str = Convert.ToBase64String(this.Encrypt(Encoding.Unicode.GetBytes(s)));
    sender.SetValue(row, this._FieldOrdinal, (object) str);
  }

  protected virtual string DefaultViewAsString
  {
    get => new string('*', this.Length <= 0 || this.Length >= 8 ? 8 : this.Length);
  }

  protected virtual byte[] Encrypt(byte[] source) => source;

  protected virtual byte[] Decrypt(byte[] source) => source;

  private string ViewString(PXCache cache, object data)
  {
    if (this.ViewAsField != (System.Type) null)
      return cache.GetValue(data, this.viewAsField.Name).ToString();
    return this.ViewAsString == null ? this.DefaultViewAsString : this.ViewAsString;
  }
}
