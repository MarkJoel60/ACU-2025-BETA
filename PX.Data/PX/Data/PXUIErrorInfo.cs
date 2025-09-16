// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIErrorInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXUIErrorInfo
{
  public string FieldName { get; private set; }

  public object CacheItem { get; private set; }

  public string ErrorText { get; private set; }

  public PXErrorLevel? ErrorLevel { get; private set; }

  public string ErrorValue { get; private set; }

  public PXUIErrorInfo(
    string fieldName,
    object cacheItem,
    string errorText,
    PXErrorLevel? errorLevel,
    object errorValue)
  {
    this.FieldName = fieldName;
    this.CacheItem = cacheItem;
    this.ErrorText = errorText;
    this.ErrorLevel = errorLevel;
    this.ErrorValue = errorValue == null ? (string) null : errorValue.ToString();
  }
}
