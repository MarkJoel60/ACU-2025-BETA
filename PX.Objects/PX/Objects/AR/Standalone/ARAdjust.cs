// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

[PXHidden]
[Serializable]
public class ARAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
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
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusted document.
  /// </summary>
  [PXDBLong]
  public virtual long? AdjgCuryInfoID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdRefNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdLineNbr>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjgRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjNbr>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust.adjgCuryInfoID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.released>
  {
  }
}
