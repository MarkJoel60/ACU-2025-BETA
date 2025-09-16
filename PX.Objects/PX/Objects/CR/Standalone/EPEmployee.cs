// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.EPEmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

/// <summary>
/// Represents an Employee of the organization utilizing Acumatica ERP.
/// </summary>
/// <remarks>
/// An employee is a person working for the organization that utilizes Acumatica ERP.
/// The records of this type are created and edited on the <i>Employees (EP203000)</i> form,
/// which correspond to the <see cref="T:PX.Objects.EP.EmployeeMaint" /> graph.
/// </remarks>
[PXTable(new System.Type[] {typeof (PX.Objects.CR.BAccount.bAccountID)})]
[PXCacheName("Employee")]
[CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>))]
[PXHidden]
[Serializable]
public class EPEmployee : PX.Objects.CR.BAccount
{
  protected 
  #nullable disable
  string _DepartmentID;
  protected int? _SupervisorID;

  /// <summary>
  /// The human-readable identifier of the employee that is
  /// specified by the user or defined by the EMPLOYEE auto-numbering sequence during the
  /// creation of the employee. This field is a natural key, as opposed
  /// to the surrogate key <see cref="!:BAccountID" />.
  /// </summary>
  [EmployeeRaw]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> object linked with the current employee as Contact Info.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXForeignReference(typeof (Field<EPEmployee.defContactID>.IsRelatedTo<PX.Objects.CR.Contact.contactID>))]
  [PXUIField(DisplayName = "Default Contact")]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>))]
  public override int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> object linked with the current employee as Address Info.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [PXForeignReference(typeof (Field<EPEmployee.defAddressID>.IsRelatedTo<PX.Objects.CR.Address.addressID>))]
  [PXUIField(DisplayName = "Default Address")]
  [PXSelector(typeof (Search<PX.Objects.CR.Address.addressID, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>))]
  public override int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  /// <summary>
  /// The employee name, which is usually a concatenation of the
  /// <see cref="P:PX.Objects.CR.Contact.FirstName">first</see> and <see cref="P:PX.Objects.CR.Contact.LastName">last name</see>
  /// of the appropriate contact.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public override string AcctName
  {
    get => base.AcctName;
    set => base.AcctName = value;
  }

  /// <summary>The external reference number of the employee.</summary>
  /// <remarks>It can be an additional number of the employee used in external integration.
  /// </remarks>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public override string AcctReferenceNbr
  {
    get => base.AcctReferenceNbr;
    set => base.AcctReferenceNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.EP.EPDepartment">employee department</see>
  /// that the employee belongs to.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Standalone.Location" /> object linked with the employee and marked as default.
  /// The fields from the linked location are shown on the <b>Financial Settings</b> tab.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Standalone.Location.LocationID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Standalone.Location.BAccountID" /> value must also be equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> value of the current employee.
  /// </remarks>
  [PXDefault]
  [PXDBInt]
  [PXUIField]
  [PX.Objects.CR.DefLocationID(typeof (Search<Location.locationID, Where<Location.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>), SubstituteKey = typeof (Location.locationCD), DescriptionField = typeof (Location.descr))]
  [PXDBChildIdentity(typeof (Location.locationID))]
  public override int? DefLocationID
  {
    get => base.DefLocationID;
    set => base.DefLocationID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.PrimaryContactID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Primary Contact")]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  public override int? PrimaryContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Standalone.EPEmployee" /> that the current employee sends reports to.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXUIField]
  public virtual int? SupervisorID
  {
    get => this._SupervisorID;
    set => this._SupervisorID = value;
  }

  /// <summary>The status of the employee.</summary>
  /// <value>
  /// The possible values of the field are listed in
  /// the <see cref="T:PX.Objects.AP.VendorStatus" /> class. These values can be changed and extended by using the workflow engine.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("A")]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.SM.Users">Users</see> to be used for the employee to sign into the system.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUser]
  [PXUIField]
  public virtual Guid? UserID { get; set; }

  /// <inheritdoc />
  [PXSearchable(64 /*0x40*/, "Employee: {0} {1}", new System.Type[] {typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName)}, new System.Type[] {typeof (EPEmployee.defContactID), typeof (PX.Objects.CR.Contact.eMail)}, NumberFields = new System.Type[] {typeof (EPEmployee.acctCD)}, Line1Format = "{1}{2}", Line1Fields = new System.Type[] {typeof (EPEmployee.defContactID), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1)}, Line2Format = "{1}", Line2Fields = new System.Type[] {typeof (EPEmployee.departmentID), typeof (EPDepartment.description)})]
  [PXUniqueNote(DescriptionField = typeof (EPEmployee.acctCD), Selector = typeof (EPEmployee.acctCD))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>Primary Key</summary>
  public new class PK : PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>
  {
    public static EPEmployee Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (EPEmployee) PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  /// <summary>Unique Key</summary>
  public new class UK : PrimaryKeyOf<EPEmployee>.By<EPEmployee.acctCD>
  {
    public static EPEmployee Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (EPEmployee) PrimaryKeyOf<EPEmployee>.By<EPEmployee.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public new static class FK
  {
    /// <summary>Customer Class</summary>
    public class EmployeeClass : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.classID>
    {
    }

    /// <summary>Branch or location</summary>
    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.parentBAccountID>
    {
    }

    /// <summary>Address</summary>
    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.defAddressID>
    {
    }

    /// <summary>Contact</summary>
    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.defContactID>
    {
    }

    /// <summary>Default Location</summary>
    public class DefaultLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.bAccountID, EPEmployee.defLocationID>
    {
    }

    /// <summary>Primary Contact</summary>
    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.primaryContactID>
    {
    }

    /// <summary>Department</summary>
    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.departmentID>
    {
    }

    /// <summary>
    /// The employee's supervisor to whom the reports are sent
    /// </summary>
    public class ReportsTo : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.supervisorID>
    {
    }

    /// <summary>Vendor's owner</summary>
    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.CR.BAccount.ownerID>
    {
    }

    /// <summary>Workgroup</summary>
    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<EPEmployee>.By<PX.Objects.CR.BAccount.workgroupID>
    {
    }

    /// <summary>Login information</summary>
    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EPEmployee>.By<EPEmployee.userID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctCD>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defContactID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defAddressID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctName>
  {
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.departmentID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defLocationID>
  {
  }

  public new abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.primaryContactID>
  {
  }

  public abstract class supervisorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.supervisorID>
  {
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.vStatus>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.classID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.userID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.noteID>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.parentBAccountID>
  {
  }
}
