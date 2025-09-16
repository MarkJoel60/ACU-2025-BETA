// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPWingman
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Delegate")]
[Serializable]
public class EPWingman : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _EmployeeID;
  protected int? _WingmanID;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPWingman.employeeID>>>>))]
  [PXEPEmployeeSelector]
  public int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXRestrictor(typeof (Where<EPEmployee.vStatus, Equal<VendorStatus.active>>), "The employee is not active.", new System.Type[] {})]
  [PXCheckUnique(new System.Type[] {typeof (EPWingman.employeeID), typeof (EPWingman.delegationOf)}, Where = typeof (Where<EPWingman.delegationOf, Equal<EPDelegationOf.expenses>, Or<EPWingman.delegationOf, Equal<EPDelegationOf.timeEntries>>>))]
  [PXUIField(DisplayName = "Delegated To", Required = true)]
  [PXDefault]
  public int? WingmanID
  {
    get => this._WingmanID;
    set => this._WingmanID = value;
  }

  /// <summary>Represents the type of the delegation.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.EP.EPDelegationOf" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [EPDelegationOf.List]
  [PXUIField(DisplayName = "Delegation Of")]
  [PXDefault(typeof (IIf<Where<IsContractBasedAPI, Equal<True>>, EPDelegationOf.expenses, EPDelegationOf.approvals>))]
  public virtual string DelegationOf { get; set; }

  /// <summary>Delegation start date</summary>
  [PXDBDate]
  [PXUIField]
  [PXDefault]
  [PXUIEnabled(typeof (Where<BqlOperand<EPWingman.delegationOf, IBqlString>.IsEqual<EPDelegationOf.approvals>>))]
  [PXUIRequired(typeof (Where<BqlOperand<EPWingman.delegationOf, IBqlString>.IsEqual<EPDelegationOf.approvals>>))]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.approvalWorkflow>))]
  public virtual DateTime? StartsOn { get; set; }

  /// <summary>Delegation end date</summary>
  [PXDBDate]
  [PXUIField]
  [PXUIEnabled(typeof (Where<BqlOperand<EPWingman.delegationOf, IBqlString>.IsEqual<EPDelegationOf.approvals>>))]
  [EPVerifyEndDate(typeof (EPWingman.startsOn), AllowAutoChange = true, AutoChangeWarning = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.approvalWorkflow>))]
  public virtual DateTime? ExpiresOn { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, this field indicates that the delegation is active.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<EPWingman>.By<EPWingman.recordID>
  {
    public static EPWingman Find(PXGraph graph, int recordID, PKFindOptions options = 0)
    {
      return (EPWingman) PrimaryKeyOf<EPWingman>.By<EPWingman.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWingman.recordID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWingman.employeeID>
  {
  }

  public abstract class wingmanID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWingman.wingmanID>
  {
  }

  public abstract class delegationOf : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPWingman.delegationOf>
  {
  }

  public abstract class startsOn : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPWingman.startsOn>
  {
  }

  public abstract class expiresOn : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPWingman.expiresOn>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPWingman.isActive>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPWingman.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPWingman.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPWingman.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPWingman.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPWingman.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPWingman.lastModifiedDateTime>
  {
  }
}
