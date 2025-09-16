// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EMailAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[Serializable]
public class EMailAccountExt : PXCacheExtension<
#nullable disable
EMailAccount>
{
  [PXParent(typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<EMailAccount.defaultOwnerID>>>>))]
  [PXMergeAttributes]
  public virtual int? EmailAccountID { get; set; }

  [PXFormula(typeof (IIf<Where<BqlOperand<EMailAccount.defaultOwnerID, IBqlInt>.IsNotNull>, Null, EMailAccount.defaultWorkgroupID>))]
  [PXMergeAttributes]
  public virtual int? DefaultWorkgroupID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.defContactID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<EMailAccount.userID>>>>))]
  [PXFormula(typeof (Default<EMailAccount.userID>))]
  [Owner(DisplayName = "Default Email Owner")]
  [PXMergeAttributes]
  public virtual int? DefaultOwnerID { get; set; }

  /// <summary>Email address for the corresponding email account</summary>
  [PXFormula(typeof (Switch<Case<Where<EntryStatus, Equal<EntryStatus.inserted>>, Selector<EMailAccount.userID, Users.email>>>))]
  [PXMergeAttributes]
  public virtual string Address { get; set; }

  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeActivity>, Or<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeEmail>>>>))]
  [PXMergeAttributes]
  public virtual int? DefaultEmailAssignmentMapID { get; set; }

  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description), CacheGlobal = true)]
  [PXUIEnabled(typeof (EMailAccount.createCase))]
  [PXMergeAttributes]
  public virtual string CreateCaseClassID { get; set; }

  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  [PXUIEnabled(typeof (EMailAccount.createLead))]
  [PXMergeAttributes]
  public virtual string CreateLeadClassID { get; set; }

  /// <summary>
  /// Field for enable / disable <i>Update Password</i> grid action on <see cref="T:PX.SM.MyProfileMaint">SM203010</see> screen
  /// </summary>
  [PXUIField]
  [PXBool]
  [PXFormula(typeof (IIf<Where<BqlOperand<EMailAccount.authenticationType, IBqlInt>.IsIn<AuthenticationType.none, AuthenticationType.main, AuthenticationType.custom>>, True, False>))]
  public virtual bool? CanUpdatePassword { get; set; }

  /// <summary>
  /// Field for enable / disable <i>Sign In</i> grid action on <see cref="T:PX.SM.MyProfileMaint">SM203010</see> screen
  /// </summary>
  [PXUIField]
  [PXBool]
  [PXFormula(typeof (IIf<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailAccount.authenticationType, In3<AuthenticationType.oAuth2, AuthenticationType.oAuth2New>>>>>.And<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsNotEqual<EmailAccountTypesAttribute.exchange>>>, True, False>))]
  public virtual bool? CanSignIn { get; set; }

  /// <summary>
  /// Field for enable / disable <i>Sign Out</i> grid action on <see cref="T:PX.SM.MyProfileMaint">SM203010</see> screen
  /// </summary>
  [PXUIField]
  [PXBool]
  [PXFormula(typeof (IIf<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailAccount.authenticationType, Equal<AuthenticationType.oAuth2New>>>>>.And<BqlOperand<EMailAccount.emailAccountType, IBqlString>.IsNotEqual<EmailAccountTypesAttribute.exchange>>>, True, False>))]
  public virtual bool? CanSignOut { get; set; }

  public abstract class emailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccountExt.emailAccountID>
  {
  }

  public abstract class defaultWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccountExt.defaultWorkgroupID>
  {
  }

  public abstract class defaultOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccountExt.defaultOwnerID>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccountExt.address>
  {
  }

  public abstract class defaultEmailAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccountExt.defaultEmailAssignmentMapID>
  {
  }

  public abstract class createCaseClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccountExt.createCaseClassID>
  {
  }

  public abstract class createLeadClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccountExt.createLeadClassID>
  {
  }

  public abstract class canUpdatePassword : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccountExt.canUpdatePassword>
  {
  }

  public abstract class canSignIn : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccountExt.canSignIn>
  {
  }

  public abstract class canSignOut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailAccountExt.canSignOut>
  {
  }
}
