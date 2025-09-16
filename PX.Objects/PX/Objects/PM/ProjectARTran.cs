// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectARTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("AR Transactions")]
[PXProjection(typeof (SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<ARTran.tranType>, GroupBy<ARTran.refNbr>, GroupBy<ARTran.projectID>, Sum<ARTran.curyTranAmt>, Sum<ARTran.curyTranBal>>))]
[Serializable]
public class ProjectARTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARTran.tranType))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false)]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARTran.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTran.projectID))]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  public virtual int? ProjectID { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyTranAmt))]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyTranBal))]
  [PXUIField(DisplayName = "Balance", Enabled = false)]
  public virtual Decimal? CuryTranBal { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectARTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectARTran.refNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectARTran.projectID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ProjectARTran.curyTranAmt>
  {
  }

  public abstract class curyTranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ProjectARTran.curyTranBal>
  {
  }
}
