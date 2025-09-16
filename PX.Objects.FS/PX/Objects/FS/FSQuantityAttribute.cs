// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSQuantityAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSQuantityAttribute(Type keyField, Type resultField) : PXQuantityAttribute(keyField, resultField)
{
  protected override void CalcBaseQty(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (FSDBQuantityAttribute.IsAnItemLine(sender, e.Row))
    {
      base.CalcBaseQty(sender, e);
    }
    else
    {
      if (!(this._ResultField != (Type) null))
        return;
      sender.SetValue(e.Row, this._ResultField.Name, (object) 0M);
    }
  }
}
