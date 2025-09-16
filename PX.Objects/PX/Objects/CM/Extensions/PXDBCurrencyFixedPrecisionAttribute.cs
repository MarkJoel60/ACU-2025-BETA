// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXDBCurrencyFixedPrecisionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBCurrencyFixedPrecisionAttribute(int precision, Type keyField, Type resultField) : 
  PXDBCurrencyAttributeBase(precision, keyField, resultField)
{
  public override int? CustomPrecision => this._Precision;

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
  }
}
