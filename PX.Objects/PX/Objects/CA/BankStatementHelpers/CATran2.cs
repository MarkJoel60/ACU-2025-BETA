// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementHelpers.CATran2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementHelpers;

[PXHidden]
[Serializable]
public class CATran2 : CATran
{
  public new abstract class tranID : BqlType<IBqlLong, long>.Field<
  #nullable disable
  CATran2.tranID>
  {
  }

  public new abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran2.cashAccountID>
  {
  }

  public new abstract class voidedTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATran2.voidedTranID>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran2.origModule>
  {
  }

  public new abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran2.origTranType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran2.origRefNbr>
  {
  }

  public new abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran2.origLineNbr>
  {
  }
}
