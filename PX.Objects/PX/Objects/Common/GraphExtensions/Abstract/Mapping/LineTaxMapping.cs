// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.LineTaxMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class LineTaxMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  public Type LineNbr = typeof (LineTax.lineNbr);
  public Type TaxID = typeof (LineTax.taxID);
  public Type TaxRate = typeof (LineTax.taxRate);
  public Type CuryTaxableAmt = typeof (LineTax.curyTaxableAmt);
  public Type CuryTaxAmt = typeof (LineTax.curyTaxAmt);
  public Type CuryExpenseAmt = typeof (LineTax.curyExpenseAmt);

  /// <exclude />
  public Type Extension => typeof (LineTax);

  /// <exclude />
  public Type Table => this._table;

  public LineTaxMapping(Type table) => this._table = table;
}
