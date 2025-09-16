// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranInTransit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>))]
public class INTranInTransit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTransitLineStatus.transferNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INTransitLineStatus.transferLineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLineStatus.origModule))]
  public virtual string OrigModule { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranInTransit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranInTransit.lineNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranInTransit.origModule>
  {
  }
}
