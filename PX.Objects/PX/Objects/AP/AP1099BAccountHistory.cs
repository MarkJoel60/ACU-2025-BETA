// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099BAccountHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (SelectFromBase<AP1099History, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<AP1099History.branchID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.branchID>>>, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<MatchWithBranch<AP1099History.branchID>>))]
[PXHidden]
public class AP1099BAccountHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(null, null, true, true, false, IsKey = true, BqlTable = typeof (AP1099History))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (AP1099History))]
  public virtual int? VendorID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true, BqlTable = typeof (AP1099History))]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [PXDBShort(IsKey = true, BqlTable = typeof (AP1099History))]
  public virtual short? BoxNbr { get; set; }

  [PXDBBaseCury(null, null, BqlTable = typeof (AP1099History))]
  public virtual Decimal? HistAmt { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<PX.Data.Where<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, IBqlBool>.IsEqual<True>>, PX.Objects.GL.Branch.bAccountID, PX.Objects.GL.DAC.Organization.bAccountID>), typeof (int))]
  public virtual int? BAccountID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099BAccountHistory.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099BAccountHistory.vendorID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099BAccountHistory.finYear>
  {
  }

  public abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AP1099BAccountHistory.boxNbr>
  {
  }

  public abstract class histAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AP1099BAccountHistory.histAmt>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099BAccountHistory.bAccountID>
  {
  }
}
