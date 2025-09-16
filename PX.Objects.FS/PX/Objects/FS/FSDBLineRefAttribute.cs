// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDBLineRefAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSDBLineRefAttribute : PXEventSubscriberAttribute, IPXRowInsertingSubscriber
{
  private Type _lineNbr;

  public FSDBLineRefAttribute(Type lineNbr) => this._lineNbr = lineNbr;

  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    object obj1 = sender.GetValue(e.Row, this._lineNbr.Name);
    object obj2 = sender.GetValue(e.Row, this._FieldName);
    int totalWidth = -1;
    if (obj2 != null)
      return;
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(this._FieldName))
    {
      if (attribute is PXDBStringAttribute)
      {
        totalWidth = ((PXDBStringAttribute) attribute).Length;
        break;
      }
    }
    if (totalWidth <= 0)
      return;
    int? nullable = (int?) obj1;
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    PXCache pxCache = sender;
    object row = e.Row;
    string fieldName = this._FieldName;
    nullable = (int?) obj1;
    string str = nullable.Value.ToString().PadLeft(totalWidth, '0');
    pxCache.SetValue(row, fieldName, (object) str);
  }
}
