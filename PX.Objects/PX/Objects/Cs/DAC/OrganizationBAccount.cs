// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DAC.OrganizationBAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.CS.DAC;

[PXCacheName("Company")]
[PXPrimaryGraph(typeof (OrganizationMaint))]
[PXProjection(typeof (Select2<PX.Objects.CR.BAccount, InnerJoin<Organization, On<Organization.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<True, Equal<True>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount)})]
[Serializable]
public class OrganizationBAccount : PX.Objects.CR.BAccount
{
  [PXDBString(30, BqlField = typeof (Organization.organizationType))]
  public virtual 
  #nullable disable
  string OrganizationType { get; set; }

  [PXDimensionSelector("COMPANY", typeof (Search2<PX.Objects.CR.BAccount.acctCD, InnerJoin<Organization, On<Organization.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<BqlChainableConditionLite<Match<Organization, Current<AccessInfo.userName>>>.And<BqlOperand<Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName)})]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  public override string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public override string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  [PXDBInt(BqlField = typeof (Organization.organizationID))]
  public virtual int? OrganizationID { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OrganizationBAccount.bAccountID>
  {
  }

  public new abstract class defContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBAccount.defContactID>
  {
  }

  public new abstract class defAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBAccount.defAddressID>
  {
  }

  public new abstract class defLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBAccount.defLocationID>
  {
  }

  public abstract class organizationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBAccount.organizationType>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OrganizationBAccount.acctCD>
  {
  }

  public new abstract class acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBAccount.acctName>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBAccount.organizationID>
  {
  }

  /// <summary>
  /// The registered entity for government payroll reporting.
  /// </summary>
  public new abstract class registeredEntityForReporting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationBAccount.registeredEntityForReporting>
  {
  }
}
