// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractBillingSchedule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXPrimaryGraph(typeof (ContractMaint))]
[PXCacheName("Contract Billing Schedule")]
[Serializable]
public class ContractBillingSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected 
  #nullable disable
  string _Type;
  protected DateTime? _NextDate;
  protected DateTime? _LastDate;
  protected string _BillTo;
  protected DateTime? _StartBilling;
  protected int? _AccountID;
  protected int? _LocationID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Contract.contractID))]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractBillingSchedule.contractID>>>>))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDefault("M")]
  [PXDBString(1, IsFixed = true)]
  [BillingType.ListForContract]
  [PXUIField(DisplayName = "Billing Period", Required = true)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? NextDate
  {
    get => this._NextDate;
    set => this._NextDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastDate
  {
    get => this._LastDate;
    set => this._LastDate = value;
  }

  [PXDefault("M")]
  [PXDBString(1, IsFixed = true)]
  [ContractBillingSchedule.billTo.List]
  [PXUIField(DisplayName = "Bill To")]
  public virtual string BillTo
  {
    get => this._BillTo;
    set => this._BillTo = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Billing Schedule Starts On", Enabled = false)]
  public virtual DateTime? StartBilling
  {
    get => this._StartBilling;
    set => this._StartBilling = value;
  }

  [PXDefault]
  [CustomerActive]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.CR.BAccount.defLocationID, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<ContractBillingSchedule.accountID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ContractBillingSchedule.accountID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXForeignReference(typeof (CompositeKey<Field<ContractBillingSchedule.accountID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<ContractBillingSchedule.locationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXFormulaEditor(DisplayName = "Invoice Description")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [OptionsProviderForFormulaEditor(new string[] {"ActionInvoice"}, new System.Type[] {typeof (Contract), typeof (ContractTemplate), typeof (PX.Objects.AR.Customer), typeof (PX.Objects.CR.Location), typeof (ContractBillingSchedule), typeof (AccessInfo)})]
  [PXDefault("=@ActionInvoice+' '+[Contract.ContractCD]+': '+[Contract.Description]+'.'")]
  public virtual string InvoiceFormula { get; set; }

  [PXFormulaEditor(DisplayName = "Line Description")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [OptionsProviderForFormulaEditor(new string[] {"Prefix", "ActionItem"}, new System.Type[] {typeof (Contract), typeof (PX.Objects.AR.Customer), typeof (ContractItem), typeof (ContractDetail), typeof (PX.Objects.IN.InventoryItem), typeof (ContractBillingSchedule), typeof (PMTran), typeof (UsageData), typeof (AccessInfo)})]
  [PXDefault("=IIf( @Prefix=Null, '', @Prefix+': ')+ IIf( @ActionItem=Null,'',@ActionItem+': ')+[UsageData.Description]")]
  public virtual string TranFormula { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<ContractBillingSchedule>.By<ContractBillingSchedule.contractID>
  {
    public static ContractBillingSchedule Find(
      PXGraph graph,
      int? contractID,
      PKFindOptions options = 0)
    {
      return (ContractBillingSchedule) PrimaryKeyOf<ContractBillingSchedule>.By<ContractBillingSchedule.contractID>.FindBy(graph, (object) contractID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractBillingSchedule>.By<ContractBillingSchedule.contractID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ContractBillingSchedule>.By<ContractBillingSchedule.accountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ContractBillingSchedule>.By<ContractBillingSchedule.accountID, ContractBillingSchedule.locationID>
    {
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractBillingSchedule.contractID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractBillingSchedule.type>
  {
  }

  public abstract class nextDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingSchedule.nextDate>
  {
  }

  public abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingSchedule.lastDate>
  {
  }

  public abstract class billTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractBillingSchedule.billTo>
  {
    public const string ParentAccount = "P";
    public const string CustomerAccount = "M";
    public const string SpecificAccount = "S";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "P", "M", "S" }, new string[3]
        {
          "Parent Account",
          "Customer Account",
          "Specific Account"
        })
      {
      }
    }

    public class parentAccount : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ContractBillingSchedule.billTo.parentAccount>
    {
      public parentAccount()
        : base("P")
      {
      }
    }

    public class customerAccount : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ContractBillingSchedule.billTo.customerAccount>
    {
      public customerAccount()
        : base("M")
      {
      }
    }

    public class specificAccount : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ContractBillingSchedule.billTo.specificAccount>
    {
      public specificAccount()
        : base("S")
      {
      }
    }
  }

  public abstract class startBilling : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingSchedule.startBilling>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractBillingSchedule.accountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractBillingSchedule.locationID>
  {
  }

  public abstract class invoiceFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingSchedule.invoiceFormula>
  {
  }

  public abstract class tranFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingSchedule.tranFormula>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractBillingSchedule.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractBillingSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractBillingSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingSchedule.lastModifiedDateTime>
  {
  }
}
