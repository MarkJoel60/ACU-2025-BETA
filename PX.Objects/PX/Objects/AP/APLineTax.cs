// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APLineTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
///  Represents aggrigated tax amount for same tax type for AP document line.
/// </summary>
[PXProjection(typeof (SelectFromBase<APTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<APTax.taxID, IBqlString>.IsEqual<PX.Objects.TX.Tax.taxID>>>>.AggregateTo<GroupBy<APTax.tranType>, GroupBy<APTax.refNbr>, GroupBy<APTax.lineNbr>, GroupBy<PX.Objects.TX.Tax.taxType>, Sum<APTax.taxAmt>>))]
[PXCacheName("AP Line Tax")]
[Serializable]
public class APLineTax : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APTax.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APTax.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (APTax.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.TX.Tax.taxType))]
  public virtual string TaxType { get; set; }

  [PXDBDecimal(4, BqlField = typeof (APTax.taxAmt))]
  public virtual Decimal? TaxAmt { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APLineTax.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APLineTax.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APLineTax.lineNbr>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APLineTax.taxType>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APLineTax.taxAmt>
  {
  }
}
