// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactDisplayNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.CR;

public class ContactDisplayNameAttribute : 
  PXDBStringAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowInsertedSubscriber
{
  private readonly System.Type _lastNameBqlField;
  private readonly System.Type _firstNameBqlField;
  private readonly System.Type _midNameBqlField;
  private readonly System.Type _titleBqlField;
  private readonly bool _reversed;
  private int _lastNameFieldOrdinal;
  private int _firstNameFieldOrdinal;
  private int _midNameFieldOrdinal;
  private int _titleFieldOrdinal;

  public ContactDisplayNameAttribute(
    System.Type lastNameBqlField,
    System.Type firstNameBqlField,
    System.Type midNameBqlField,
    System.Type titleBqlField,
    bool reversed)
    : base((int) byte.MaxValue)
  {
    if (lastNameBqlField == (System.Type) null)
      throw new ArgumentNullException(nameof (lastNameBqlField));
    if (firstNameBqlField == (System.Type) null)
      throw new ArgumentNullException(nameof (firstNameBqlField));
    if (midNameBqlField == (System.Type) null)
      throw new ArgumentNullException(nameof (midNameBqlField));
    if (titleBqlField == (System.Type) null)
      throw new ArgumentNullException(nameof (titleBqlField));
    this._lastNameBqlField = lastNameBqlField;
    this._firstNameBqlField = firstNameBqlField;
    this._midNameBqlField = midNameBqlField;
    this._titleBqlField = titleBqlField;
    this._reversed = reversed;
    this.IsUnicode = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._lastNameFieldOrdinal = ContactDisplayNameAttribute.GetFieldOrdinal(sender, this._lastNameBqlField);
    this._firstNameFieldOrdinal = ContactDisplayNameAttribute.GetFieldOrdinal(sender, this._firstNameBqlField);
    this._midNameFieldOrdinal = ContactDisplayNameAttribute.GetFieldOrdinal(sender, this._midNameBqlField);
    this._titleFieldOrdinal = ContactDisplayNameAttribute.GetFieldOrdinal(sender, this._titleBqlField);
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.Handler(sender, e.Row);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.Handler(sender, e.Row);
  }

  private string FormatDisplayName(
    PXCache sender,
    string aLastName,
    string aFirstName,
    string aMidName,
    string aTitle,
    bool aReversed)
  {
    if (aLastName == null)
      aLastName = string.Empty;
    if (aFirstName == null)
      aFirstName = string.Empty;
    if (aMidName == null)
      aMidName = string.Empty;
    string str = this.GetLocolizedValue(sender, aTitle) ?? string.Empty;
    return string.IsNullOrEmpty(str) ? ContactDisplayNameAttribute.Concat(aLastName, ",", aFirstName, aMidName) : (aReversed ? ContactDisplayNameAttribute.Concat(aLastName, aFirstName, aMidName, ",", str) : ContactDisplayNameAttribute.Concat(str, aFirstName, aMidName, aLastName));
  }

  private static string Concat(params string[] args)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str1 in args)
    {
      string str2 = str1.Trim();
      switch (str2)
      {
        case "":
          continue;
        case ",":
          if (stringBuilder.Length > 0)
          {
            stringBuilder.Append(str2);
            continue;
          }
          continue;
        default:
          if (stringBuilder.Length > 0)
            stringBuilder.Append(" ");
          stringBuilder.Append(str2);
          continue;
      }
    }
    return stringBuilder.ToString().TrimEnd(',');
  }

  private string GetLocolizedValue(PXCache cache, string message)
  {
    string str1 = $"{PXUIFieldAttribute.GetNeutralDisplayName(cache, this._titleBqlField.Name)} -> {message}";
    string str2 = PXLocalizer.Localize(str1, ((PXEventSubscriberAttribute) this)._BqlTable.FullName);
    return !string.IsNullOrEmpty(str2) && str2 != str1 ? str2 : message;
  }

  private static int GetFieldOrdinal(PXCache sender, System.Type bqlField)
  {
    return sender.GetFieldOrdinal(sender.GetField(bqlField));
  }

  private void Handler(PXCache sender, object data)
  {
    string str = this.FormatDisplayName(sender, sender.GetValue(data, this._lastNameFieldOrdinal) as string, sender.GetValue(data, this._firstNameFieldOrdinal) as string, sender.GetValue(data, this._midNameFieldOrdinal) as string, sender.GetValue(data, this._titleFieldOrdinal) as string, this._reversed);
    sender.SetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) str);
  }
}
