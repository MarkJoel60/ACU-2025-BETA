// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.HookWithSource
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.PushNotifications;

internal sealed class HookWithSource
{
  private bool Equals(HookWithSource other)
  {
    if (string.Equals(this.HookType, other.HookType) && string.Equals(this.Address, other.Address) && string.Equals(this.SourceType, other.SourceType) && string.Equals(this.HeaderName, other.HeaderName) && string.Equals(this.HeaderValue, other.HeaderValue) && string.Equals(this.Name, other.Name) && this.HookId.Equals((object) other.HookId))
    {
      bool? nullable = this.SourceActive;
      bool? sourceActive = other.SourceActive;
      if (nullable.GetValueOrDefault() == sourceActive.GetValueOrDefault() & nullable.HasValue == sourceActive.HasValue)
      {
        bool? hookActive = this.HookActive;
        nullable = other.HookActive;
        if (hookActive.GetValueOrDefault() == nullable.GetValueOrDefault() & hookActive.HasValue == nullable.HasValue && this.GiId.Equals((object) other.GiId) && string.Equals(this.ClassName, other.ClassName))
        {
          Guid? filterId1 = this.FilterId;
          Guid? filterId2 = other.FilterId;
          if ((filterId1.HasValue == filterId2.HasValue ? (filterId1.HasValue ? (filterId1.GetValueOrDefault() == filterId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && this.TrackAllFields == other.TrackAllFields)
          {
            (string, string)[] trackingFields = this.TrackingFields;
            return trackingFields == null ? other.TrackingFields == null : ((IStructuralEquatable) trackingFields).Equals((object) other.TrackingFields, (IEqualityComparer) new OrdinalStringTupleComparer());
          }
        }
      }
    }
    return false;
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals((HookWithSource) obj);
  }

  public override int GetHashCode()
  {
    int num1 = ((((((this.HookType != null ? this.HookType.GetHashCode() : 0) * 397 ^ (this.Address != null ? this.Address.GetHashCode() : 0)) * 397 ^ (this.SourceType != null ? this.SourceType.GetHashCode() : 0)) * 397 ^ (this.HeaderName != null ? this.HeaderName.GetHashCode() : 0)) * 397 ^ (this.HeaderValue != null ? this.HeaderValue.GetHashCode() : 0)) * 397 ^ (this.Name != null ? this.Name.GetHashCode() : 0)) * 397;
    Guid? nullable = this.HookId;
    int hashCode1 = nullable.GetHashCode();
    int num2 = (((num1 ^ hashCode1) * 397 ^ this.SourceActive.GetHashCode()) * 397 ^ this.HookActive.GetHashCode()) * 397;
    nullable = this.GiId;
    int hashCode2 = nullable.GetHashCode();
    int num3 = ((num2 ^ hashCode2) * 397 ^ (this.ClassName != null ? this.ClassName.GetHashCode() : 0)) * 397;
    nullable = this.FilterId;
    int hashCode3 = nullable.GetHashCode();
    int num4 = ((num3 ^ hashCode3) * 397 ^ this.TrackAllFields.GetHashCode()) * 397;
    int? hashCode4 = ((IStructuralEquatable) this.TrackingFields)?.GetHashCode((IEqualityComparer) new OrdinalStringTupleComparer());
    return (hashCode4.HasValue ? new int?(num4 ^ hashCode4.GetValueOrDefault()) : new int?()).GetValueOrDefault();
  }

  public string HookType { get; }

  public string Address { get; }

  public string SourceType { get; }

  public string HeaderName { get; }

  public string HeaderValue { get; }

  public string Name { get; }

  public Guid? HookId { get; }

  public bool? SourceActive { get; }

  public bool? HookActive { get; }

  public Guid? GiId { get; }

  public string ClassName { get; }

  public Guid? FilterId { get; }

  public (string, string)[] TrackingFields { get; }

  public bool TrackAllFields { get; }

  public Dictionary<string, object> AdditionalInfo { get; }

  public HookWithSource(
    string hookType,
    string address,
    string sourceType,
    string headerName,
    string headerValue,
    string name,
    Guid? hookId,
    bool? sourceActive,
    bool? hookActive,
    Guid? giId,
    string className,
    Guid? filterId,
    bool? trackAllFields,
    (string, string)[] trackingFields,
    Dictionary<string, object> additionalInfo = null)
  {
    this.HookType = hookType;
    this.Address = address;
    this.SourceType = sourceType;
    this.HeaderName = headerName;
    this.HeaderValue = headerValue;
    this.Name = name;
    this.HookId = hookId;
    this.SourceActive = sourceActive;
    this.HookActive = hookActive;
    this.GiId = giId;
    this.ClassName = className;
    this.FilterId = filterId;
    this.TrackAllFields = ((int) trackAllFields ?? 1) != 0;
    this.TrackingFields = trackingFields;
    this.AdditionalInfo = additionalInfo ?? new Dictionary<string, object>();
  }
}
