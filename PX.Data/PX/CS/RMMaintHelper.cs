// Decompiled with JetBrains decompiler
// Type: PX.CS.RMMaintHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.CS;

internal static class RMMaintHelper
{
  internal static void CheckFieldIsNonEmpty<Field>(PXCache sender, object row) where Field : class, IBqlField
  {
    if (sender == null || row == null)
      return;
    object newValue = sender.GetValue<Field>(row);
    if (!string.IsNullOrEmpty(newValue as string))
      return;
    sender.RaiseExceptionHandling<Field>(row, newValue, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<Field>(sender)
    }));
  }

  internal static void CheckFieldIsUnique<DAC, FieldInDAC, Field>(
    PXGraph graph,
    PXCache sender,
    object row)
    where DAC : class, IBqlTable, new()
    where FieldInDAC : class, IBqlField
    where Field : class, IBqlField
  {
    if (sender == null || row == null)
      return;
    object obj = sender.GetValue<Field>(row);
    if ((object) (DAC) PXSelectBase<DAC, PXSelect<DAC>.Config>.Search<FieldInDAC>(graph, obj) == null)
      return;
    sender.RaiseExceptionHandling<Field>(row, obj, (Exception) new PXSetPropertyException("The value of this field must be unique among all records.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<Field>(sender)
    }));
  }

  internal static void ThrowIfUIException<Field>(PXCache cache, object row) where Field : IBqlField
  {
    string errorOnly = PXUIFieldAttribute.GetErrorOnly<Field>(cache, row);
    if (!string.IsNullOrWhiteSpace(errorOnly))
    {
      string name = typeof (Field).Name;
      string str = PXUIFieldAttribute.GetDisplayName(cache, name);
      if (string.IsNullOrEmpty(str))
        str = name;
      throw new PXRowPersistingException(name, (object) null, errorOnly, new object[1]
      {
        (object) str
      });
    }
  }

  internal static void CheckKeyAndDescription<DAC, KeyFieldInDAC, KeyField, DescriptionField>(
    PXGraph graph,
    PXCache sender,
    object row)
    where DAC : class, IBqlTable, new()
    where KeyFieldInDAC : class, IBqlField
    where KeyField : class, IBqlField
    where DescriptionField : class, IBqlField
  {
    RMMaintHelper.CheckFieldIsUnique<DAC, KeyFieldInDAC, KeyField>(graph, sender, row);
    RMMaintHelper.CheckFieldIsNonEmpty<KeyField>(sender, row);
    RMMaintHelper.CheckFieldIsNonEmpty<DescriptionField>(sender, row);
    RMMaintHelper.ThrowIfUIException<KeyField>(sender, row);
    RMMaintHelper.ThrowIfUIException<DescriptionField>(sender, row);
  }
}
