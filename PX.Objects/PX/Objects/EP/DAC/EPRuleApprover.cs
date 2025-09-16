// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DAC.EPRuleApprover
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP.DAC;

/// <summary>
/// Represent table which connects Approver (Employee) with Rule
/// Records of this type are created and edited on Approval Maps (EP205015) form
/// which corresponds to the <see cref="T:PX.Objects.EP.EPApprovalMapMaint" />
/// </summary>
[PXCacheName("Rule Approver")]
[Serializable]
public class EPRuleApprover : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>ID for each row of RuleApprover entity</summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? RuleApproverID { get; set; }

  /// <summary>Represents ID of Approver</summary>
  [PXCheckUnique(new System.Type[] {typeof (EPRuleApprover.ruleID), typeof (EPRuleApprover.ownerID)})]
  [Owner(null, null, true, false, null, new string[] {"Employee Name", "Job Title", "Email", "Phone 1", "Department", "Employee ID", "Status", "Branch", "Reports To (ID)", "Reports To (Name)", "Type", "User ID"}, null, PXSelectorMode.DisplayModeText, DisplayName = "Employee Name")]
  public virtual int? OwnerID { get; set; }

  /// <summary>Represents ID of Rule for which is Approver defined</summary>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Rule ID")]
  [PXParent(typeof (Select<EPRule, Where<EPRule.ruleID, Equal<Current<EPRuleApprover.ruleID>>>>))]
  [PXDBDefault(typeof (EPRule.ruleID))]
  public virtual Guid? RuleID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Contact</summary>
    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPRuleApprover>.By<EPRuleApprover.ownerID>
    {
    }

    /// <summary>Rule</summary>
    public class Rule : 
      PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.ForeignKeyOf<EPRuleApprover>.By<EPRuleApprover.ownerID>
    {
    }
  }

  public abstract class ruleApproverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRuleApprover.ruleApproverID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRuleApprover.ownerID>
  {
  }

  public abstract class ruleID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRuleApprover.ruleID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRuleApprover.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRuleApprover.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPRuleApprover.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPRuleApprover.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRuleApprover.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPRuleApprover.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPRuleApprover.Tstamp>
  {
  }
}
