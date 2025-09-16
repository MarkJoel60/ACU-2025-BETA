// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectSearch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CT;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PXBreakInheritance]
[Serializable]
public class PMProjectSearch : PMProject, IAssign, IIncludable, IRestricted
{
  [PXDimensionSelector("PROJECT", typeof (Search2<PMProjectSearch.contractCD, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProjectSearch.customerID>>, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProjectSearch.contractID>>>>, Where<PMProjectSearch.baseType, Equal<CTPRType.project>, And<PMProjectSearch.nonProject, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (PMProjectSearch.contractCD), new System.Type[] {typeof (PMProjectSearch.contractCD), typeof (PMProjectSearch.description), typeof (PMProjectSearch.customerID), typeof (PMProjectSearch.customerID_Customer_acctName), typeof (PMProjectSearch.locationID), typeof (PMProjectSearch.status), typeof (PMProjectSearch.ownerID), typeof (PMProjectSearch.startDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (PMProjectSearch.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Template ID")]
  public new 
  #nullable disable
  string ContractCD
  {
    get => this._ContractCD;
    set => this._ContractCD = value;
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectSearch.customerID>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectSearch.contractID>
  {
  }

  public new abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectSearch.baseType>
  {
  }

  public new abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProjectSearch.nonProject>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectSearch.description>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectSearch.customerID_Customer_acctName>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectSearch.locationID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectSearch.status>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectSearch.ownerID>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectSearch.startDate>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProjectSearch.isActive>
  {
  }

  public new abstract class budgetLevel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectSearch.budgetLevel>
  {
  }

  public new abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectSearch.billingID>
  {
  }

  public new abstract class contractCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectSearch.contractCD>
  {
  }
}
