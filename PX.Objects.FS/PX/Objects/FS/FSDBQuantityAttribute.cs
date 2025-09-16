// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDBQuantityAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class FSDBQuantityAttribute : PXDBQuantityAttribute
{
  public FSDBQuantityAttribute(Type keyField, Type resultField)
    : base(keyField, resultField)
  {
  }

  public FSDBQuantityAttribute(
    Type keyField,
    Type resultField,
    InventoryUnitType decimalVerifyUnits)
    : base(keyField, resultField, decimalVerifyUnits)
  {
  }

  protected override void CalcBaseQty(PXCache sender, PXDBQuantityAttribute.QtyConversionArgs e)
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

  public static bool IsAnItemLine(PXCache sender, object row)
  {
    string lower = typeof (FSAppointmentDet.lineType).Name.ToLower();
    string str1 = string.Empty;
    foreach (string field in (List<string>) sender.Fields)
    {
      if (field.ToLower() == lower)
      {
        str1 = field;
        break;
      }
    }
    if (str1 != string.Empty)
    {
      object obj = sender.GetValue(row, str1);
      if (obj != null)
      {
        string str2 = (string) obj;
        if (str2 == "IT_LN" || str2 == "CM_LN")
          return false;
      }
    }
    return true;
  }
}
