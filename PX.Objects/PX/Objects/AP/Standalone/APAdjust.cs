// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone;

[PXHidden]
[Serializable]
public class APAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _AdjdCuryInfoID;

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  public virtual 
  #nullable disable
  string AdjdDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? AdjdLineNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  public virtual string AdjgDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? AdjNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusted document.
  /// </summary>
  [PXDBLong]
  public virtual long? AdjdCuryInfoID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusting document.
  /// </summary>
  [PXDBLong]
  public virtual long? AdjgCuryInfoID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// The amount to be paid for the adjusted document. (See <see cref="!:APRegister.CuryOrigDocAmt" />)
  /// (Presented in the currency of the document, see <see cref="!:APRegister.CuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? CuryAdjdAmt { get; set; }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdRefNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdLineNbr>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjNbr>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAdjust.adjgCuryInfoID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.released>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyAdjdAmt>
  {
  }
}
