// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionStatic
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequisitionStatic : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _SourceReqNbr;
  protected string _ReqNbr;
  protected int? _LineNbr;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<RQRequisition.reqNbr, Where<RQRequisition.status, Equal<RQRequisitionStatus.hold>>>), new Type[] {typeof (RQRequisition.employeeID), typeof (RQRequisition.vendorID)}, Filterable = true)]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual string SourceReqNbr
  {
    get => this._SourceReqNbr;
    set => this._SourceReqNbr = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public abstract class sourceReqNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionStatic.sourceReqNbr>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionStatic.reqNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionStatic.lineNbr>
  {
  }
}
