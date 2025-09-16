// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXDBCurrencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Marks the field for processing by MultiCurrencyGraph. When attached to a Field that stores Amount in pair with BaseAmount Field
/// MultiCurrencyGraph handles conversion and rounding when this field is updated.
/// Use this Attribute for DB fields. See <see cref="T:PX.Objects.CM.Extensions.PXCurrencyAttribute" /> for Non-DB version.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
/// <summary>Constructor</summary>
/// <param name="keyField">Field in this table used as a key for CurrencyInfo table.</param>
/// <param name="resultField">Field in this table to store the result of currency conversion.</param>
public class PXDBCurrencyAttribute(Type keyField, Type resultField) : PXDBCurrencyAttributeBase(keyField, resultField)
{
  protected Dictionary<long, string> _matches;

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    this._Precision = sender.Graph.GetPrecision(sender, row, this.KeyField?.Name, this._matches);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.KeyField != (Type) null))
      return;
    this._matches = CurrencyInfo.CuryIDStringAttribute.GetMatchesDictionary(sender);
  }
}
