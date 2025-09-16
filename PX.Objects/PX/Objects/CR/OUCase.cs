// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUCase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class OUCase : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault(typeof (OUMessage.subject))]
  [PXUIField]
  public virtual 
  #nullable disable
  string Subject { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault(typeof (Search<CRSetup.defaultCaseClassID>))]
  [PXUIField(DisplayName = "Case Class")]
  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description), CacheGlobal = true)]
  public virtual string CaseClassID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXDefault("EM")]
  [CaseSources]
  public virtual string Source { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Severity")]
  [CRCaseSeverity]
  public virtual string Severity { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, And<Where<Current<Contact.bAccountID>, IsNull, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<Contact.bAccountID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<ContractBillingSchedule.accountID, Equal<Current<Contact.bAccountID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<Contact.bAccountID>>, And<PX.Objects.CT.Contract.locationID, Equal<Current<CRCase.locationID>>>>, Or<Where<ContractBillingSchedule.accountID, Equal<Current<Contact.bAccountID>>, And<ContractBillingSchedule.locationID, Equal<Current<CRCase.locationID>>>>>>>>>>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), DescriptionField = typeof (PX.Objects.CT.Contract.description), SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD))]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  [PXFormula(typeof (Default<Contact.bAccountID>))]
  public virtual int? ContractID { get; set; }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUCase.subject>
  {
  }

  public abstract class caseClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUCase.caseClassID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUCase.source>
  {
  }

  public abstract class severity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUCase.severity>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OUCase.contractID>
  {
  }
}
