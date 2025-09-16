// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectAPTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("AP Transactions")]
[PXProjection(typeof (SelectFromBase<APTran, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<APTran.tranType>, GroupBy<APTran.refNbr>, GroupBy<APTran.projectID>, Sum<APTran.curyTranAmt>, Sum<APTran.curyTranBal>>))]
[Serializable]
public class ProjectAPTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APTran.tranType))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false)]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APTran.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (APTran.projectID))]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  public virtual int? ProjectID { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.curyTranAmt))]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.curyTranBal))]
  [PXUIField(DisplayName = "Balance", Enabled = false)]
  public virtual Decimal? CuryTranBal { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectAPTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectAPTran.refNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectAPTran.projectID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ProjectAPTran.curyTranAmt>
  {
  }

  public abstract class curyTranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ProjectAPTran.curyTranBal>
  {
  }
}
