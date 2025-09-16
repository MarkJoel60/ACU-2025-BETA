// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranRefNbr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CATranRefNbr : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Ref. Number")]
  [PXSelector(typeof (Search<CATran.origRefNbr, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, Like<Optional<CATran.origTranType>>>>>), new Type[] {typeof (CATran.origRefNbr), typeof (CATran.origTranType), typeof (CATran.tranDate), typeof (CATran.finPeriodID)})]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATranRefNbr.refNbr>
  {
  }
}
