// Decompiled with JetBrains decompiler
// Type: PX.Data.ConvertToBool`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

public class ConvertToBool<Source> : ConvertTo<Source, bool>, IImplement<IBqlCastableTo<IBqlBool>> where Source : IBqlOperand
{
  protected override object ConvertValue(object value)
  {
    if (value is bool)
      return value;
    int result;
    value = !(value is string) || !int.TryParse(value.ToInvariantString(), out result) ? (!(value is IConvertible) ? (object) bool.Parse(value.ToInvariantString()) : (object) ((IConvertible) value).ToBoolean((IFormatProvider) CultureInfo.InvariantCulture)) : (object) Convert.ToBoolean(result);
    return value;
  }
}
