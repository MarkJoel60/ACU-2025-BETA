// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EPEmployeeFSRouteEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (EPEmployeeMaintBridge))]
[PXProjection(typeof (Select2<PX.Objects.EP.EPEmployee, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>, InnerJoin<FSRouteEmployee, On<FSRouteEmployee.employeeID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>>>))]
[Serializable]
public class EPEmployeeFSRouteEmployee : PX.Objects.EP.EPEmployee
{
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXUIField]
  public override int? BAccountID { get; set; }

  [EmployeeRaw]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXDefault]
  [PXFieldDescription]
  [PXUIField]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXDefault]
  [PXUIField]
  public override string AcctName { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (FSRouteEmployee.routeID))]
  public virtual int? RouteID { get; set; }

  [PXDBInt(BqlField = typeof (FSRouteEmployee.priorityPreference))]
  [PXDefault(1)]
  [PXUIField]
  public virtual int? PriorityPreference { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.BAccount.vStatus))]
  [PXUIField]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.EP.EPEmployee.departmentID))]
  [PXDefault]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public override string DepartmentID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Driver Name", Enabled = false)]
  public virtual string MemDriverName { get; set; }

  public new class PK : 
    PrimaryKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.bAccountID, EPEmployeeFSRouteEmployee.acctCD>
  {
    public static EPEmployeeFSRouteEmployee Find(
      PXGraph graph,
      int? bAccountID,
      string acctCD,
      PKFindOptions options = 0)
    {
      return (EPEmployeeFSRouteEmployee) PrimaryKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.bAccountID, EPEmployeeFSRouteEmployee.acctCD>.FindBy(graph, (object) bAccountID, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class Class : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.classID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.parentBAccountID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.defContactID>
    {
    }

    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.primaryContactID>
    {
    }

    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.departmentID>
    {
    }

    public class ReportsTo : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.supervisorID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.salesPersonID>
    {
    }

    public class LabourItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.labourItemID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.salesSubID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.discTakenSubID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.expenseSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.prepaymentSubID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.taxZoneID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.workgroupID>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<EPEmployeeFSRouteEmployee>.By<EPEmployeeFSRouteEmployee.userID>
    {
    }
  }

  public new abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.bAccountID>
  {
  }

  public new abstract class acctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.acctCD>
  {
  }

  public new abstract class acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.acctName>
  {
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeFSRouteEmployee.routeID>
  {
  }

  public abstract class priorityPreference : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.priorityPreference>
  {
  }

  public new abstract class vStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.vStatus>
  {
  }

  public new abstract class departmentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.departmentID>
  {
  }

  public abstract class memDriverName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.memDriverName>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.parentBAccountID>
  {
  }

  public new abstract class defContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.defContactID>
  {
  }

  public new abstract class defAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.defAddressID>
  {
  }

  public new abstract class supervisorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.supervisorID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.salesPersonID>
  {
  }

  public new abstract class labourItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.labourItemID>
  {
  }

  public new abstract class classID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.classID>
  {
  }

  public new abstract class salesAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.salesAcctID>
  {
  }

  public new abstract class salesSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.salesSubID>
  {
  }

  public new abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.primaryContactID>
  {
  }

  public new abstract class discTakenAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.discTakenAcctID>
  {
  }

  public new abstract class discTakenSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.discTakenSubID>
  {
  }

  public new abstract class expenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.expenseAcctID>
  {
  }

  public new abstract class expenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.expenseSubID>
  {
  }

  public new abstract class prepaymentAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.prepaymentAcctID>
  {
  }

  public new abstract class prepaymentSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.prepaymentSubID>
  {
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.taxZoneID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeFSRouteEmployee.ownerID>
  {
  }

  public new abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeFSRouteEmployee.workgroupID>
  {
  }

  public new abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployeeFSRouteEmployee.userID>
  {
  }
}
