// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CT;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>A project billing record.</summary>
[PXCacheName("Project Billing Record")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBillingRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _Date;
  protected 
  #nullable disable
  byte[] _tstamp;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMProject.contractID))]
  [PXUIField(DisplayName = "Project ID")]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The sequence of line numbers of the records that belong to one project can include gaps.
  /// </summary>
  /// <value>
  /// Note that the sequence of line numbers of the records belonging to a single project may include gaps.
  /// </value>
  [PXDefault(typeof (Contract.billingLineCntr))]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Billing Number")]
  public virtual int? RecordID { get; set; }

  /// <summary>
  /// An internal field that is used during the billing to group and segregate transactions between billing rules, tasks, and invoices.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// T: [PMTask.TaskID], if [PMTask.BillSeparately] is <see langword="true" /> for the billed task.
  /// L: [PMTask.LocationID], if [PMTask.LocationID] is not equal to the location ID from the parent project.
  /// P: Otherwise.
  /// </value>
  [PXDefault("P")]
  [PXDBString(30, IsKey = true, IsUnicode = true)]
  public virtual string BillingTag { get; set; }

  /// <summary>The date when the billing is applied.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.
  /// </summary>
  [PXSelector(typeof (Search<PMProforma.refNbr, Where<PMProforma.projectID, Equal<Current<PMBillingRecord.projectID>>>>))]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Pro Forma Reference Nbr.")]
  public virtual string ProformaRefNbr { get; set; }

  /// <summary>
  /// The type of the AR document that is created during the billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARInvoice.DocType" /> field.
  /// </value>
  [PXUIField(DisplayName = "AR Doc. Type")]
  [ARInvoiceType.List]
  [PXDBString(3, IsFixed = true)]
  public virtual string ARDocType { get; set; }

  /// <summary>
  /// The reference number of the AR document that is created during the billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
  [PXSelector(typeof (Search<ARInvoice.refNbr>))]
  public virtual string ARRefNbr { get; set; }

  /// <summary>
  /// An internal field that is used to order records on the Invoices tab of the Projects (PM301000) form.
  /// </summary>
  [PXInt]
  public virtual int? SortOrder { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// The sequence number of the invoice that is being assigned to the invoices of the project in order of the creation of the invoices.
  /// </summary>
  /// <value>
  /// Retrives the value from the <see cref="P:PX.Objects.PM.PMBillingRecord.RecordID" /> field.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Billing Number", Visible = false)]
  public virtual int? RecordNumber
  {
    get
    {
      int? recordId = this.RecordID;
      int num = 0;
      return !(recordId.GetValueOrDefault() < num & recordId.HasValue) ? this.RecordID : new int?();
    }
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecord.projectID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecord.recordID>
  {
  }

  public abstract class billingTag : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRecord.billingTag>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMBillingRecord.date>
  {
  }

  public abstract class proformaRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRecord.proformaRefNbr>
  {
  }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRecord.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRecord.aRRefNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecord.sortOrder>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMBillingRecord.Tstamp>
  {
  }

  public abstract class recordNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecord.recordNumber>
  {
  }
}
