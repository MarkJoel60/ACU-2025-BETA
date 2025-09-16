// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardShiftCodeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class TimeCardShiftCodeSelectorAttribute : EPShiftCodeSelectorWithEmployeeIDAttribute
{
  private Type _WeekEndField;

  public TimeCardShiftCodeSelectorAttribute(Type employeeIDField, Type weekEndField)
    : base(employeeIDField, weekEndField)
  {
    this._WeekEndField = weekEndField;
  }

  protected override object[] GetQueryParameters(PXCache cache, object row)
  {
    return new object[1]
    {
      CacheHelper.GetCurrentValue(cache.Graph, this._WeekEndField)
    };
  }
}
