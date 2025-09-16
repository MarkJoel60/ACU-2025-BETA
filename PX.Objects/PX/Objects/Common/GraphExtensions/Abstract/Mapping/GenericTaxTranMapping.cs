// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.GenericTaxTranMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class GenericTaxTranMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  public Type TaxID = typeof (GenericTaxTran.taxID);
  public Type TaxRate = typeof (GenericTaxTran.taxRate);
  public Type CuryTaxableAmt = typeof (GenericTaxTran.curyTaxableAmt);
  public Type CuryTaxAmt = typeof (GenericTaxTran.curyTaxAmt);
  public Type CuryTaxAmtSumm = typeof (GenericTaxTran.curyTaxAmtSumm);
  public Type CuryExpenseAmt = typeof (GenericTaxTran.curyExpenseAmt);
  public Type NonDeductibleTaxRate = typeof (GenericTaxTran.nonDeductibleTaxRate);

  /// <exclude />
  public Type Extension => typeof (GenericTaxTran);

  /// <exclude />
  public Type Table => this._table;

  public GenericTaxTranMapping(Type table) => this._table = table;
}
