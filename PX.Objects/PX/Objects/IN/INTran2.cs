// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTran2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

/// <summary>Added for join purpose</summary>
[PXBreakInheritance]
[PXHidden]
public class INTran2 : INTran
{
  public new abstract class released : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  INTran2.released>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.docType>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.origModule>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.refNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran2.lineNbr>
  {
  }

  public new abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.origTranType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.origRefNbr>
  {
  }

  public new abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran2.origLineNbr>
  {
  }

  public new abstract class pOReceiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.pOReceiptType>
  {
  }

  public new abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTran2.pOReceiptNbr>
  {
  }

  public new abstract class pOReceiptLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTran2.pOReceiptLineNbr>
  {
  }
}
