// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ScheduleMaint.DocumentSelection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Overrides.ScheduleMaint;

[PXCacheName("AR Document to Process")]
[Serializable]
public class DocumentSelection : ARRegister
{
  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.GL.Schedule.scheduleID))]
  [PXParent(typeof (Select<PX.Objects.GL.Schedule, Where<PX.Objects.GL.Schedule.scheduleID, Equal<Current<ARRegister.scheduleID>>>>), LeaveChildren = true)]
  public override 
  #nullable disable
  string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("INV")]
  [PXStringList(new string[] {"INV", "DRM", "CRM"}, new string[] {"Invoice", "Debit Memo", "Credit Memo"})]
  [PXUIField]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<ARRegister.refNbr, LeftJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARRegister.docType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARRegister.refNbr>>>>, Where<ARRegister.docType, Equal<Optional<ARRegister.docType>>, And<IsSchedulable<ARRegister>>>>), new Type[] {typeof (ARRegister.finPeriodID), typeof (ARRegister.refNbr), typeof (ARRegister.customerID), typeof (ARRegister.customerID_Customer_acctName), typeof (PX.Objects.AR.ARInvoice.invoiceNbr), typeof (ARRegister.customerLocationID), typeof (ARRegister.status), typeof (ARRegister.curyID), typeof (ARRegister.curyOrigDocAmt)})]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBLong]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PX.Objects.GL.FinPeriodID(typeof (ARRegister.docDate), typeof (ARRegister.branchID), null, null, null, null, true, false, null, typeof (ARRegister.tranPeriodID), null, true, true, IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public new abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentSelection.scheduleID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentSelection.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentSelection.refNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  DocumentSelection.curyInfoID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentSelection.finPeriodID>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.openDoc>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.voided>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DocumentSelection.createdByID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DocumentSelection.lastModifiedByID>
  {
  }
}
