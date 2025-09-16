// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARRegister2
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
public class ARRegister2 : PX.Objects.AR.ARRegister
{
  [PXDBLong]
  public override long? CuryInfoID { get; set; }

  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  ARRegister2.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister2.refNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegister2.curyInfoID>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister2.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister2.closedTranPeriodID>
  {
  }
}
