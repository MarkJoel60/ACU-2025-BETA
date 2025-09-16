// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIWarningInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXUIWarningInfo
{
  public System.Type CacheItemType { get; }

  public object CacheItem { get; }

  public string FieldName { get; }

  public string WarningText { get; }

  public PXUIWarningInfo(System.Type cacheItemType, object cacheItem, string fieldName, string text)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(cacheItemType, nameof (cacheItemType), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(fieldName, nameof (fieldName), (string) null);
    this.CacheItemType = cacheItemType;
    this.CacheItem = cacheItem;
    this.FieldName = fieldName;
    this.WarningText = text;
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is PXUIWarningInfo pxuiWarningInfo && this.CacheItemType == pxuiWarningInfo.CacheItemType && this.FieldName.Equals(pxuiWarningInfo.FieldName) && this.CacheItem == pxuiWarningInfo.CacheItem && string.Equals(this.WarningText, pxuiWarningInfo.WarningText);
  }

  public override int GetHashCode()
  {
    int num1 = (this.CacheItemType.GetHashCode() * 397 ^ this.FieldName.GetHashCode()) * 397;
    int? nullable1 = this.CacheItem?.GetHashCode();
    int? nullable2 = nullable1.HasValue ? new int?(num1 ^ nullable1.GetValueOrDefault()) : new int?();
    int num2 = nullable2.GetValueOrDefault() * 397;
    string warningText = this.WarningText;
    int? nullable3;
    if (warningText == null)
    {
      nullable2 = new int?();
      nullable3 = nullable2;
    }
    else
      nullable3 = new int?(warningText.GetHashCode());
    nullable1 = nullable3;
    int? nullable4;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new int?(num2 ^ nullable1.GetValueOrDefault());
    nullable2 = nullable4;
    return nullable2.GetValueOrDefault();
  }
}
