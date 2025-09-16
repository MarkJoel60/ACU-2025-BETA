// Decompiled with JetBrains decompiler
// Type: PX.SM.SMReportSubstituteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SMReportSubstituteAttribute : PXEventSubscriberAttribute
{
  private readonly Type _valueField;
  private readonly Type _textField;

  public SMReportSubstituteAttribute(Type valueField, Type textField)
  {
    SMReportSubstituteAttribute.CheckBqlField(valueField, nameof (valueField));
    SMReportSubstituteAttribute.CheckBqlField(textField, nameof (textField));
    this._valueField = valueField;
    this._textField = textField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SMReportSubstituteAttribute.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new SMReportSubstituteAttribute.\u003C\u003Ec__DisplayClass3_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.\u003C\u003E4__this = this;
    base.CacheAttached(sender);
    Type itemType = sender.GetItemType();
    BqlCommand instance = BqlCommand.CreateInstance(new Type[7]
    {
      typeof (Select<,>),
      itemType,
      typeof (Where<,>),
      this._valueField,
      typeof (Equal<>),
      typeof (Required<>),
      this._valueField
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.view = new PXView(sender.Graph, true, instance);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(itemType, this._FieldName, new PXFieldSelecting((object) cDisplayClass30, __methodptr(\u003CCacheAttached\u003Eb__0)));
  }

  private static void CheckBqlField(Type field, string argumentName)
  {
    if (field == (Type) null)
      throw new ArgumentNullException(argumentName);
    if (!typeof (IBqlField).IsAssignableFrom(field))
      throw new ArgumentException(PXLocalizer.LocalizeFormat("The type {0} must inherit the PX.Data.IBqlField interface.", new object[1]
      {
        (object) field.FullName
      }), argumentName);
  }
}
