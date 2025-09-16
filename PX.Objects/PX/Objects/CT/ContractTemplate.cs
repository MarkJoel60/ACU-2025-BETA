// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractTemplate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXCacheName("Contract Template")]
[PXPrimaryGraph(typeof (TemplateMaint))]
[PXBreakInheritance]
[Serializable]
public class ContractTemplate : Contract
{
  [PXDBIdentity]
  [PXSelector(typeof (ContractTemplate.contractID))]
  [PXUIField(DisplayName = "Template ID")]
  public override int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXUIField(DisplayName = "Entity Type", Enabled = false)]
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [CTPRType.List]
  [PXDefault("T")]
  public override 
  #nullable disable
  string BaseType
  {
    get => base.BaseType;
    set => base.BaseType = value;
  }

  [PXDimensionSelector("TMCONTRACT", typeof (Search<ContractTemplate.contractCD, Where<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>), typeof (ContractTemplate.contractCD), DescriptionField = typeof (ContractTemplate.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override string ContractCD
  {
    get => this._ContractCD;
    set => this._ContractCD = value;
  }

  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [Contract.status.List]
  [PXDefault("A")]
  [PXUIField]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBInt]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBDate]
  public override DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  public override DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? IsContinuous
  {
    get => this._IsContinuous;
    set => this._IsContinuous = value;
  }

  [PXDBInt]
  public override int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public override bool? AutomaticReleaseAR
  {
    get => this._AutomaticReleaseAR;
    set => this._AutomaticReleaseAR = value;
  }

  [PXDBInt]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Termination Date")]
  public override DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Grace Period")]
  [PXUIEnabled(typeof (Where<Contract.type, Equal<Contract.type.renewable>>))]
  public override int? GracePeriod
  {
    get => this._GracePeriod;
    set => this._GracePeriod = value;
  }

  /// <summary>End Date of Grace Period.</summary>
  [PXDBCalced(typeof (Add<Contract.expireDate, Contract.gracePeriod>), typeof (DateTime))]
  public override DateTime? GraceDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Overriding Formulas in Contracts")]
  public virtual bool? AllowOverrideFormulaDescription { get; set; }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXUIField(DisplayName = "Contract Activity Approver")]
  public override int? ApproverID
  {
    get => this._ApproverID;
    set => this._ApproverID = value;
  }

  [PXDefault]
  [Owner(typeof (Contract.workgroupID))]
  public override int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [CRAttributesField(typeof (ContractTemplate.contractID), typeof (Contract.noteID))]
  public override string[] Attributes { get; set; }

  [PXString]
  [PXUIField]
  public virtual string ContractStrID => this.ContractID.ToString();

  public new class PK : PrimaryKeyOf<ContractTemplate>.By<ContractTemplate.contractID>
  {
    public static ContractTemplate Find(PXGraph graph, int? contractID, PKFindOptions options = 0)
    {
      return (ContractTemplate) PrimaryKeyOf<ContractTemplate>.By<ContractTemplate.contractID>.FindBy(graph, (object) contractID, options);
    }
  }

  public new class UK : 
    PrimaryKeyOf<ContractTemplate>.By<ContractTemplate.baseType, ContractTemplate.contractCD>
  {
    public static ContractTemplate Find(
      PXGraph graph,
      string baseType,
      string contractCD,
      PKFindOptions options = 0)
    {
      return (ContractTemplate) PrimaryKeyOf<ContractTemplate>.By<ContractTemplate.baseType, ContractTemplate.contractCD>.FindBy(graph, (object) baseType, (object) contractCD, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.customerID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.customerID, ContractTemplate.locationID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.curyID>
    {
    }

    public class RateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.rateTypeID>
    {
    }

    public class CSCalendar : 
      PrimaryKeyOf<PX.Objects.CS.CSCalendar>.By<PX.Objects.CS.CSCalendar.calendarID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.calendarID>
    {
    }

    public class DefaultAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.defaultAccountID>
    {
    }

    public class DefaultSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.defaultSubID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.defaultAccrualAccountID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.defaultAccrualSubID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.defaultBranchID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.salesPersonID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.termsID>
    {
    }

    public class PromoCode : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.discountID>
    {
    }

    public class ContractActivityApprover : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<ContractTemplate>.By<ContractTemplate.approverID>
    {
    }
  }

  public new abstract class rateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.rateTypeID>
  {
  }

  public new abstract class calendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.calendarID>
  {
  }

  public abstract class defaultAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.defaultAccountID>
  {
  }

  public abstract class defaultSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.defaultSubID>
  {
  }

  public new abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.defaultAccrualAccountID>
  {
  }

  public new abstract class defaultAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.defaultAccrualSubID>
  {
  }

  public new abstract class defaultBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.defaultBranchID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.salesPersonID>
  {
  }

  public new abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTemplate.termsID>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.discountID>
  {
  }

  public new abstract class caseItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.caseItemID>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.contractID>
  {
  }

  public new abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTemplate.baseType>
  {
  }

  public new abstract class contractCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.contractCD>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractTemplate.noteID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.description>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTemplate.status>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.customerID>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractTemplate.startDate>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractTemplate.expireDate>
  {
  }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  public new abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractTemplate.isTemplate>
  {
  }

  public new abstract class isContinuous : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractTemplate.isContinuous>
  {
  }

  public new abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.templateID>
  {
  }

  public new abstract class automaticReleaseAR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractTemplate.automaticReleaseAR>
  {
  }

  public new abstract class detailedBilling : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractTemplate.detailedBilling>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.locationID>
  {
  }

  public new abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractTemplate.terminationDate>
  {
  }

  public new abstract class gracePeriod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.gracePeriod>
  {
  }

  public new abstract class graceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractTemplate.graceDate>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTemplate.curyID>
  {
  }

  public abstract class allowOverrideFormulaDescription : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractTemplate.allowOverrideFormulaDescription>
  {
  }

  public new abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.approverID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.ownerID>
  {
  }

  public new abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.revID>
  {
  }

  public new abstract class lineCtr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTemplate.lineCtr>
  {
  }

  public new abstract class durationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.durationType>
  {
  }

  public new abstract class attributes : 
    BqlType<IBqlAttributes, string[]>.Field<ContractTemplate.attributes>
  {
  }

  public abstract class contractStrID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTemplate.contractStrID>
  {
  }
}
