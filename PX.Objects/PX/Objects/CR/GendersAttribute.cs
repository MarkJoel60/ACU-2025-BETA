// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.GendersAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class GendersAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Male = "M";
  public const string Female = "F";
  private readonly System.Type _titleField;

  public GendersAttribute(System.Type titleField)
    : this()
  {
    this._titleField = !(titleField == (System.Type) null) ? titleField : throw new ArgumentNullException(nameof (titleField));
  }

  public GendersAttribute()
    : base(new string[2]{ "M", "F" }, new string[2]
    {
      nameof (Male),
      nameof (Female)
    })
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this._titleField != (System.Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.RowInserted.AddHandler(((PXEventSubscriberAttribute) this)._BqlTable, new PXRowInserted((object) this, __methodptr(RowInsertedHandler)));
    // ISSUE: method pointer
    sender.Graph.RowUpdated.AddHandler(((PXEventSubscriberAttribute) this)._BqlTable, new PXRowUpdated((object) this, __methodptr(RowUpdatedHandler)));
  }

  private void RowInsertedHandler(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null || !(sender.GetValue(e.Row, this._titleField.Name) is string str))
      return;
    object obj = (object) null;
    switch (str)
    {
      case "Mr.":
        obj = (object) "M";
        break;
      case "Ms.":
      case "Miss":
      case "Mrs.":
        obj = (object) "F";
        break;
    }
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj);
  }

  private void RowUpdatedHandler(PXCache sender, PXRowUpdatedEventArgs e)
  {
    object obj1 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    object obj2 = sender.GetValue(e.OldRow, ((PXEventSubscriberAttribute) this)._FieldName);
    string str1 = sender.GetValue(e.Row, this._titleField.Name) as string;
    string str2 = sender.GetValue(e.OldRow, this._titleField.Name) as string;
    object obj3 = obj2;
    if (obj1 != obj3 || str1 == null || !(str1 != str2))
      return;
    object obj4 = (object) null;
    switch (str1)
    {
      case "Mr.":
        obj4 = (object) "M";
        break;
      case "Ms.":
      case "Miss":
      case "Mrs.":
        obj4 = (object) "F";
        break;
    }
    if (obj4 == null)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj4);
  }

  public class male : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GendersAttribute.male>
  {
    public male()
      : base("M")
    {
    }
  }

  public class female : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GendersAttribute.female>
  {
    public female()
      : base("F")
    {
    }
  }
}
