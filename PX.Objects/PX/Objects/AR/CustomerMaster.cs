// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Customer (alias)")]
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (CustomerMaint)}, new System.Type[] {typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CustomerMaster.bAccountID>>>>)})]
[PXProjection(typeof (Select<Customer>))]
[Serializable]
public class CustomerMaster : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Customer(IsKey = true, DisplayName = "Customer ID", BqlTable = typeof (Customer))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXDefault]
  [PXUIField]
  [PXDimension("BIZACCT")]
  public virtual 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(60, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataField]
  public virtual string AcctName { get; set; }

  [PXDBString(10, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXUIField(DisplayName = "Statement Cycle ID")]
  [PXSelector(typeof (ARStatementCycle.statementCycleId))]
  [PXDefault(typeof (Search<CustomerClass.statementCycleId, Where<CustomerClass.customerClassID, Equal<Current<CustomerMaster.customerClassID>>>>))]
  public virtual string StatementCycleId { get; set; }

  [PXDBBool(BqlTable = typeof (Customer))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidate Balance")]
  public virtual bool? ConsolidateToParent { get; set; }

  [PXDBInt(BqlTable = typeof (Customer))]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.CR.BAccount.parentBAccountID, IsNotNull, And<PX.Objects.CR.BAccount.consolidateToParent, Equal<True>>>, PX.Objects.CR.BAccount.parentBAccountID>, PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? ConsolidatingBAccountID { get; set; }

  [PXDBInt(BqlTable = typeof (Customer))]
  [PXUIField]
  [PXDimensionSelector("BIZACCT", typeof (Search2<BAccountR.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<BAccountR.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>>, Where<Current<PX.Objects.CR.BAccount.bAccountID>, NotEqual<BAccountR.bAccountID>, And2<Where<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.prospectType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.vendorType>>>>>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type), typeof (BAccountR.classID), typeof (BAccountR.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID), typeof (PX.Objects.CR.Contact.eMail)}, DescriptionField = typeof (BAccountR.acctName))]
  [PXForeignReference(typeof (Field<CustomerMaster.parentBAccountID>.IsRelatedTo<CustomerMaster.bAccountID>))]
  public virtual int? ParentBAccountID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXDefault(typeof (Search<ARSetup.dfltCustomerClassID>))]
  [PXSelector(typeof (Search<CustomerClass.customerClassID>), CacheGlobal = true, DescriptionField = typeof (CustomerClass.descr))]
  [PXUIField(DisplayName = "Customer Class")]
  [PXForeignReference(typeof (Field<CustomerMaster.customerClassID>.IsRelatedTo<CustomerClass.customerClassID>))]
  public virtual string CustomerClassID { get; set; }

  [PXDBInt(BqlTable = typeof (Customer))]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<Customer.bAccountID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>))]
  [PXUIField(DisplayName = "Default Contact")]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public virtual int? BaseBillContactID { get; set; }

  public class PK : PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.bAccountID>
  {
    public static CustomerMaster Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (CustomerMaster) PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public class UK : PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.acctCD>
  {
    public static CustomerMaster Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (CustomerMaster) PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public static class FK
  {
    public class StatementCycle : 
      PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>.ForeignKeyOf<CustomerMaster>.By<CustomerMaster.statementCycleId>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerMaster.bAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerMaster.acctCD>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerMaster.acctName>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerMaster.statementCycleId>
  {
  }

  public abstract class consolidateToParent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerMaster.consolidateToParent>
  {
  }

  public abstract class consolidatingBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerMaster.consolidatingBAccountID>
  {
  }

  public abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerMaster.parentBAccountID>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerMaster.customerClassID>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public abstract class baseBillContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerMaster.baseBillContactID>
  {
  }
}
