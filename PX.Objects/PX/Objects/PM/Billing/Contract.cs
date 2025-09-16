// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Billing.Contract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Billing;

[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class Contract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _GroupMask;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? ContractID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Project")]
  public virtual string ContractCD { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.AR.Customer.bAccountID), SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD), DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt]
  public virtual int? TemplateID { get; set; }

  /// <summary>The <see cref="T:PX.Objects.PM.ProjectStatus">status</see> of the project.</summary>
  [PXDBString(1, IsFixed = true)]
  [ProjectStatus.ProjectStatusList]
  [PXUIField]
  public virtual string Status { get; set; }

  [Branch(null, null, true, false, true)]
  public virtual int? DefaultBranchID { get; set; }

  /// <summary>The user who is responsible for managing the project.</summary>
  [PXDefault]
  [Owner]
  public virtual int? OwnerID { get; set; }

  [PXDBBool]
  public virtual bool? CreateProforma { get; set; }

  [PXString]
  [PXUIField]
  public virtual string BillingResult { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCompleted { get; set; }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Last Billing Date", Enabled = false)]
  public virtual DateTime? LastDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "From")]
  public virtual DateTime? FromDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "To")]
  public virtual DateTime? NextDate { get; set; }

  [PXNote(DescriptionField = typeof (Contract.contractCD))]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.selected>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.contractID>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.contractCD>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.customerID>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.templateID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.status>
  {
  }

  public abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.defaultBranchID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.ownerID>
  {
  }

  public abstract class createProforma : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.createProforma>
  {
  }

  public abstract class billingResult : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.billingResult>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isActive>
  {
  }

  public abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isCompleted>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Contract.groupMask>
  {
  }

  public abstract class lastDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.lastDate>
  {
  }

  public abstract class fromDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.fromDate>
  {
  }

  public abstract class nextDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contract.nextDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contract.noteID>
  {
  }
}
