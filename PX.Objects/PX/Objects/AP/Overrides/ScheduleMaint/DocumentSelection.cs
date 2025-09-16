// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.ScheduleMaint.DocumentSelection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.ScheduleMaint;

[Serializable]
public class DocumentSelection : PX.Objects.AP.APRegister
{
  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.GL.Schedule.scheduleID))]
  [PXParent(typeof (Select<PX.Objects.GL.Schedule, Where<PX.Objects.GL.Schedule.scheduleID, Equal<Current<PX.Objects.AP.APRegister.scheduleID>>>>))]
  public override 
  #nullable disable
  string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("INV")]
  [PXStringList(new string[] {"INV", "PPM", "ADR", "ACR"}, new string[] {"Bill", "Prepayment", "Debit Adj.", "Credit Adj."})]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [PXSelector(typeof (Search2<PX.Objects.AP.APRegister.refNbr, LeftJoinSingleTable<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<PX.Objects.AP.APRegister.docType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<PX.Objects.AP.APRegister.refNbr>>>>, Where<PX.Objects.AP.APRegister.docType, Equal<Optional<PX.Objects.AP.APRegister.docType>>, PX.Data.And<IsSchedulable<PX.Objects.AP.APRegister>>>>), new System.Type[] {typeof (PX.Objects.AP.APRegister.finPeriodID), typeof (PX.Objects.AP.APRegister.refNbr), typeof (PX.Objects.AP.APRegister.vendorID), typeof (PX.Objects.AP.APRegister.vendorID_Vendor_acctName), typeof (PX.Objects.AP.APInvoice.invoiceNbr), typeof (PX.Objects.AP.APRegister.vendorLocationID), typeof (PX.Objects.AP.APRegister.status), typeof (PX.Objects.AP.APRegister.curyID), typeof (PX.Objects.AP.APRegister.curyOrigDocAmt)})]
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

  [PX.Objects.GL.FinPeriodID(typeof (PX.Objects.AP.APRegister.docDate), typeof (PX.Objects.AP.APRegister.branchID), null, null, null, null, true, false, null, typeof (PX.Objects.AP.APRegister.tranPeriodID), null, true, true, IsHeader = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible)]
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

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentSelection.rejected>
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

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentSelection.hasMultipleProjects>
  {
  }
}
