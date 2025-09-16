// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranMatch2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABankTranMatch2 : CABankTranMatch
{
  [PXDBLong]
  public override long? CuryInfoID { get; set; }

  public new abstract class tranID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CABankTranMatch2.tranID>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch2.tranType>
  {
  }

  public new abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranMatch2.cATranID>
  {
  }

  public new abstract class docModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch2.docModule>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch2.docType>
  {
  }

  public new abstract class docRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch2.docRefNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranMatch2.curyInfoID>
  {
  }
}
