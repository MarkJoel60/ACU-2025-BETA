// Decompiled with JetBrains decompiler
// Type: PX.Data.Validate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class Validate : IBqlTrigger
{
  /// <exclude />
  public virtual void Verify(PXCache cache, string fieldName, object item)
  {
    Validate.VerifyField(cache, fieldName, item);
  }

  public static void VerifyField<Field>(PXCache cache, object item) where Field : IBqlField
  {
    Validate.VerifyField(cache, typeof (Field).Name, item);
  }

  public static void VerifyField(PXCache cache, string fieldName, object item)
  {
    object newValue = cache.GetValue(item, fieldName);
    if (newValue == null)
      return;
    try
    {
      cache.RaiseFieldVerifying(fieldName, item, ref newValue);
    }
    catch (Exception ex)
    {
      cache.SetValue(item, fieldName, (object) null);
      cache.RaiseExceptionHandling(fieldName, item, newValue, ex);
    }
  }
}
