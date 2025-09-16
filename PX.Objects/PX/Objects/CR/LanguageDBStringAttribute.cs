// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LanguageDBStringAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Globalization;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public sealed class LanguageDBStringAttribute : PXDBStringAttribute
{
  private const string _FIELD_POSTFIX = "_DisplayName";
  private string _displayNameFieldName;

  [Obsolete]
  public LanguageDBStringAttribute()
  {
  }

  [Obsolete]
  public LanguageDBStringAttribute(int length)
    : base(length)
  {
  }

  public string DisplayName { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._displayNameFieldName = ((PXEventSubscriberAttribute) this)._FieldName + "_DisplayName";
    sender.Fields.Add(this._displayNameFieldName);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._displayNameFieldName, new PXFieldSelecting((object) this, __methodptr(_FieldName_DisplayName_FieldSelecting)));
  }

  private void _FieldName_DisplayName_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs args)
  {
    string name = sender.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) as string;
    string str = string.Empty;
    if (!string.IsNullOrEmpty(name))
    {
      try
      {
        str = CultureInfo.GetCultureInfo(name).DisplayName;
      }
      catch (ArgumentException ex)
      {
      }
    }
    args.ReturnState = (object) PXFieldState.CreateInstance((object) str, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this._displayNameFieldName, (string) null, this.DisplayName, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(true), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
  }
}
