// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[Serializable]
public class RQRequestEnq : PXGraph<
#nullable disable
RQRequestEnq>
{
  public PXCancel<RQRequestEnq.RQRequestSelection> Cancel;
  public PXFilter<RQRequestEnq.RQRequestSelection> Filter;
  public PXFilter<PX.Objects.CR.BAccount> BAccount;
  public PXFilter<PX.Objects.AP.Vendor> Vendor;
  public PXSelect<PX.Objects.RQ.RQRequest> RQRequest;
  public PXSelectJoin<RQRequestLine, InnerJoin<PX.Objects.RQ.RQRequest, On<PX.Objects.RQ.RQRequest.orderNbr, Equal<RQRequestLine.orderNbr>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.RQ.RQRequest.employeeID>>, LeftJoin<INSubItem, On<RQRequestLine.FK.SubItem>>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>> Records;
  public PXSetup<RQSetup> setup;

  public RQRequestEnq()
  {
    ((PXSelectBase) this.Records).View.WhereAndCurrent<RQRequestEnq.RQRequestSelection>();
    PXStringListAttribute.SetList<PX.Objects.IN.InventoryItem.itemType>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) null, new string[8]
    {
      "F",
      "M",
      "A",
      "N",
      "L",
      "S",
      "C",
      "E"
    }, new string[8]
    {
      "Finished Good",
      "Component Part",
      "Subassembly",
      "Non-Stock Item",
      "Labor",
      "Service",
      "Charge",
      "Expense"
    });
  }

  protected virtual void RQRequestSelection_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RQRequestEnq.RQRequestSelection row = (RQRequestEnq.RQRequestSelection) e.Row;
    if (row == null)
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    row.EmployeeID = (int?) epEmployee?.BAccountID;
    row.ReqClassID = ((PXSelectBase<RQSetup>) this.setup).Current.DefaultReqClassID;
  }

  [PXDBInt]
  [PXDimensionSelector("BIZACCT", typeof (Search2<PX.Objects.CR.BAccount.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)}, DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField]
  protected void RQRequest_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [Serializable]
  public class RQRequestSelection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ReqClassID;
    protected int? _EmployeeID;
    protected string _DepartmentID;
    protected string _Description;
    protected string _DescriptionWildcard;
    protected int? _InventoryID;
    protected string _SubItemCD;

    [PXDBString(10, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
    public virtual string ReqClassID
    {
      get => this._ReqClassID;
      set => this._ReqClassID = value;
    }

    [PXDBInt]
    [RQRequesterSelector(typeof (RQRequestEnq.RQRequestSelection.reqClassID))]
    [PXUIField]
    public virtual int? EmployeeID
    {
      get => this._EmployeeID;
      set => this._EmployeeID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
    [PXUIField]
    public virtual string DepartmentID
    {
      get => this._DepartmentID;
      set => this._DepartmentID = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXDBString(60, IsUnicode = true)]
    public virtual string DescriptionWildcard => $"%{this._Description}%";

    [RQRequestInventoryItem(typeof (RQRequestEnq.RQRequestSelection.reqClassID))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [SubItemRawExt(typeof (InventorySummaryEnqFilter.inventoryID), DisplayName = "Subitem")]
    public virtual string SubItemCD
    {
      get => this._SubItemCD;
      set => this._SubItemCD = value;
    }

    [PXDBString(30, IsUnicode = true)]
    public virtual string SubItemCDWildcard
    {
      get => SubCDUtils.CreateSubCDWildcard(this._SubItemCD, "INSUBITEM");
    }

    public abstract class reqClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.reqClassID>
    {
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.employeeID>
    {
    }

    public abstract class departmentID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.departmentID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.description>
    {
    }

    public abstract class descriptionWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.descriptionWildcard>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.inventoryID>
    {
    }

    public abstract class subItemCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.subItemCD>
    {
    }

    public abstract class subItemCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequestEnq.RQRequestSelection.subItemCDWildcard>
    {
    }
  }
}
