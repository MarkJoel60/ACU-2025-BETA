// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectPOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXHidden]
[PXProjection(typeof (SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<POLine.orderType>, GroupBy<POLine.orderNbr>, GroupBy<POLine.projectID>, Sum<POLine.curyLineAmt>>))]
[Serializable]
public class ProjectPOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POLine.projectID))]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  public virtual int? ProjectID { get; set; }

  [PXDBDecimal(BqlField = typeof (POLine.curyLineAmt))]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  public virtual Decimal? CuryLineAmt { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectPOLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProjectPOLine.orderNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectPOLine.projectID>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ProjectPOLine.curyLineAmt>
  {
  }
}
