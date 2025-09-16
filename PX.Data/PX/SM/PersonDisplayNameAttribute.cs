// Decompiled with JetBrains decompiler
// Type: PX.SM.PersonDisplayNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public class PersonDisplayNameAttribute : 
  PXDBStringAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowInsertedSubscriber
{
  private readonly System.Type _lastNameBqlField;
  private readonly System.Type _firstNameBqlField;
  private readonly System.Type _midNameBqlField;
  private readonly System.Type _titleBqlField;
  private int _lastNameFieldOrdinal;
  private int _firstNameFieldOrdinal;
  private int _midNameFieldOrdinal;
  private int _titleFieldOrdinal;

  public PersonDisplayNameAttribute(
    System.Type lastNameBqlField,
    System.Type firstNameBqlField,
    System.Type midNameBqlField,
    System.Type titleBqlField)
    : base((int) byte.MaxValue)
  {
    this.IsUnicode = true;
    this._lastNameBqlField = !(lastNameBqlField == (System.Type) null) ? lastNameBqlField : throw new ArgumentNullException(nameof (lastNameBqlField));
    this._firstNameBqlField = firstNameBqlField;
    this._midNameBqlField = midNameBqlField;
    this._titleBqlField = titleBqlField;
  }

  public PersonDisplayNameAttribute(System.Type lastNameBqlField, System.Type firstNameBqlField)
    : this(lastNameBqlField, firstNameBqlField, (System.Type) null, (System.Type) null)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._lastNameFieldOrdinal = sender.GetFieldOrdinal(sender.GetField(this._lastNameBqlField));
    if (this._firstNameBqlField != (System.Type) null)
      this._firstNameFieldOrdinal = sender.GetFieldOrdinal(sender.GetField(this._firstNameBqlField));
    if (this._midNameBqlField != (System.Type) null)
      this._midNameFieldOrdinal = sender.GetFieldOrdinal(sender.GetField(this._midNameBqlField));
    if (!(this._titleBqlField != (System.Type) null))
      return;
    this._titleFieldOrdinal = sender.GetFieldOrdinal(sender.GetField(this._titleBqlField));
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.Handler(sender, e.Row);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.Handler(sender, e.Row);
  }

  private void Handler(PXCache sender, object data)
  {
    string lastName = sender.GetValue(data, this._lastNameFieldOrdinal) as string;
    string firstName = this._firstNameBqlField == (System.Type) null ? (string) null : sender.GetValue(data, this._firstNameFieldOrdinal) as string;
    string midName = this._midNameBqlField == (System.Type) null ? (string) null : sender.GetValue(data, this._midNameFieldOrdinal) as string;
    string localizedValue = this._titleBqlField == (System.Type) null ? (string) null : sender.GetValue(data, this._titleFieldOrdinal) as string;
    if (!string.IsNullOrEmpty(localizedValue))
      localizedValue = this.GetLocalizedValue(sender, localizedValue);
    string str = this.FormatDisplayName(lastName, firstName, midName, localizedValue) ?? string.Empty;
    sender.SetValue(data, this._FieldOrdinal, (object) str);
  }

  private string FormatDisplayName(
    string lastName,
    string firstName,
    string midName,
    string title)
  {
    switch (this.Definitions.PersonNamesFormat)
    {
      case "WESTERN":
        return PersonDisplayNameAttribute.Western(lastName, firstName, midName, title);
      case "EASTERN":
        return PersonDisplayNameAttribute.Eastern(lastName, firstName, midName, title);
      case "EASTERN_WITH_TITLE":
        return PersonDisplayNameAttribute.EasternWithTitle(lastName, firstName, midName, title);
      default:
        return PersonDisplayNameAttribute.Legacy(lastName, firstName, midName, title);
    }
  }

  private static bool entered(params string[] fields)
  {
    foreach (string field in fields)
    {
      if (string.IsNullOrEmpty(field))
        return false;
    }
    return true;
  }

  private static string Legacy(string lastName, string firstName, string midName, string title)
  {
    if (PersonDisplayNameAttribute.entered(lastName))
    {
      if (PersonDisplayNameAttribute.entered(firstName, midName, title))
        return $"{lastName} {firstName} {midName}, {title}";
      if (PersonDisplayNameAttribute.entered(firstName, midName))
        return $"{lastName}, {firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, title))
        return $"{lastName} {firstName}, {title}";
      if (PersonDisplayNameAttribute.entered(midName, title))
        return $"{lastName} {midName}, {title}";
      if (PersonDisplayNameAttribute.entered(firstName))
        return $"{lastName}, {firstName}";
      if (PersonDisplayNameAttribute.entered(midName))
        return $"{lastName}, {midName}";
      if (PersonDisplayNameAttribute.entered(title))
        return $"{lastName}, {title}";
    }
    else
    {
      if (PersonDisplayNameAttribute.entered(firstName, midName, title))
        return $"{firstName} {midName}, {title}";
      if (PersonDisplayNameAttribute.entered(firstName, midName))
        return $"{firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, title))
        return $"{firstName}, {title}";
      if (PersonDisplayNameAttribute.entered(midName, title))
        return $"{midName}, {title}";
      if (PersonDisplayNameAttribute.entered(firstName))
        return firstName ?? "";
      if (PersonDisplayNameAttribute.entered(midName))
        return midName ?? "";
      if (PersonDisplayNameAttribute.entered(title))
        return title ?? "";
    }
    return lastName;
  }

  private static string Western(string lastName, string firstName, string midName, string title)
  {
    if (PersonDisplayNameAttribute.entered(firstName, lastName))
      return $"{firstName} {lastName}";
    return PersonDisplayNameAttribute.entered(firstName) ? firstName ?? "" : lastName;
  }

  private static string Eastern(string lastName, string firstName, string midName, string title)
  {
    if (PersonDisplayNameAttribute.entered(firstName, lastName))
      return $"{lastName}, {firstName}";
    return PersonDisplayNameAttribute.entered(firstName) ? firstName ?? "" : lastName;
  }

  private static string EasternWithTitle(
    string lastName,
    string firstName,
    string midName,
    string title)
  {
    if (PersonDisplayNameAttribute.entered(lastName))
    {
      if (PersonDisplayNameAttribute.entered(firstName, midName, title))
        return $"{title} {lastName}, {firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, midName))
        return $"{lastName}, {firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, title))
        return $"{title} {lastName}, {firstName}";
      if (PersonDisplayNameAttribute.entered(midName, title))
        return $"{title} {lastName}, {midName}";
      if (PersonDisplayNameAttribute.entered(firstName))
        return $"{lastName}, {firstName}";
      if (PersonDisplayNameAttribute.entered(midName))
        return $"{lastName}, {midName}";
      if (PersonDisplayNameAttribute.entered(title))
        return $"{title} {lastName}";
    }
    else
    {
      if (PersonDisplayNameAttribute.entered(firstName, midName, title))
        return $"{title} {firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, midName))
        return $"{firstName} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName, title))
        return $"{title} {firstName}";
      if (PersonDisplayNameAttribute.entered(midName, title))
        return $"{title} {midName}";
      if (PersonDisplayNameAttribute.entered(firstName))
        return firstName ?? "";
      if (PersonDisplayNameAttribute.entered(midName))
        return midName ?? "";
      if (PersonDisplayNameAttribute.entered(title))
        return title ?? "";
    }
    return lastName;
  }

  private string GetLocalizedValue(PXCache cache, string message)
  {
    string message1 = $"{PXUIFieldAttribute.GetNeutralDisplayName(cache, this._titleBqlField.Name)} -> {message}";
    string str = PXLocalizer.Localize(message1, this._BqlTable.FullName);
    return !string.IsNullOrEmpty(str) && str != message1 ? str : message;
  }

  private PersonDisplayNameAttribute.Definition Definitions
  {
    get
    {
      PersonDisplayNameAttribute.Definition definitions = PXContext.GetSlot<PersonDisplayNameAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<PersonDisplayNameAttribute.Definition>(PXDatabase.GetSlot<PersonDisplayNameAttribute.Definition>(typeof (PersonDisplayNameAttribute.Definition).FullName, typeof (PreferencesGeneral)));
      return definitions;
    }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private string _personNamesFormat;

    public string PersonNamesFormat => this._personNamesFormat;

    public void Prefetch()
    {
      this._personNamesFormat = (string) null;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PreferencesGeneral>(new PXDataField("PersonNameFormat")))
      {
        if (pxDataRecord == null)
          return;
        this._personNamesFormat = pxDataRecord.GetString(0);
      }
    }
  }
}
