// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class AssetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ClassID;
  protected 
  #nullable disable
  string _AssetTypeID;
  protected string _PropertyType;
  protected string _Condition;
  protected string _ReceiptNbr;
  protected string _PONumber;
  protected string _BillNumber;
  protected int? _BranchID;
  protected int? _BuildingID;
  protected string _Floor;
  protected string _Room;
  protected int? _EmployeeID;
  protected string _Department;
  protected DateTime? _AcqDateFrom;
  protected DateTime? _AcqDateTo;

  [PXDBInt]
  [PXSelector(typeof (Search<FAClass.assetID, Where<FAClass.recordType, Equal<FARecordType.classType>>>), SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description))]
  [PXUIField(DisplayName = "Asset Class")]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<FAType.assetTypeID>), DescriptionField = typeof (FAType.description))]
  [PXUIField(DisplayName = "Asset Type")]
  public virtual string AssetTypeID
  {
    get => this._AssetTypeID;
    set => this._AssetTypeID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [FADetails.propertyType.List]
  [PXUIField(DisplayName = "Property Type")]
  public virtual string PropertyType
  {
    get => this._PropertyType;
    set => this._PropertyType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Condition")]
  [FADetails.condition.List]
  public virtual string Condition
  {
    get => this._Condition;
    set => this._Condition = value;
  }

  [PXString(2, IsFixed = true)]
  [PXUnboundDefault("AL")]
  [POReceiptType.ListAttribute.WithAll]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<BqlField<AssetFilter.receiptType, IBqlString>.FromCurrent>>>), Filterable = true)]
  [PXFormula(typeof (Default<AssetFilter.receiptType>))]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<AssetFilter.receiptType, IsNotNull>>>>.And<BqlOperand<AssetFilter.receiptType, IBqlString>.IsNotEqual<POReceiptType.all>>))]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search4<FADetails.pONumber, Where<FADetails.pONumber, IsNotNull>, Aggregate<GroupBy<FADetails.pONumber>>>), new System.Type[] {typeof (FADetails.pONumber)})]
  [PXUIField(DisplayName = "PO Number")]
  public virtual string PONumber
  {
    get => this._PONumber;
    set => this._PONumber = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search4<FADetails.billNumber, Where<FADetails.billNumber, IsNotNull>, Aggregate<GroupBy<FADetails.billNumber>>>), new System.Type[] {typeof (FADetails.billNumber)})]
  [PXUIField(DisplayName = "Bill Number")]
  public virtual string BillNumber
  {
    get => this._BillNumber;
    set => this._BillNumber = value;
  }

  [Organization(true)]
  public int? OrganizationID { get; set; }

  [Branch(null, null, true, true, true)]
  [PXRestrictor(typeof (Where<True, Equal<True>>), "Branch is inactive.", new System.Type[] {}, ReplaceInherited = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [OrganizationTree(typeof (AssetFilter.organizationID), typeof (AssetFilter.branchID), null, false)]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>))]
  public int? OrgBAccountID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<Building.buildingID, Where<Building.branchID, Equal<Current<AssetFilter.branchID>>>>), SubstituteKey = typeof (Building.buildingCD))]
  [PXUIField(DisplayName = "Building")]
  public virtual int? BuildingID
  {
    get => this._BuildingID;
    set => this._BuildingID = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Floor")]
  public virtual string Floor
  {
    get => this._Floor;
    set => this._Floor = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Room")]
  public virtual string Room
  {
    get => this._Room;
    set => this._Room = value;
  }

  [PXDBInt]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Custodian")]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<AssetFilter.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField(DisplayName = "Department", Required = false)]
  public virtual string Department
  {
    get => this._Department;
    set => this._Department = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Placed-in-Service Date From")]
  public virtual DateTime? AcqDateFrom
  {
    get => this._AcqDateFrom;
    set => this._AcqDateFrom = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "To")]
  public virtual DateTime? AcqDateTo
  {
    get => this._AcqDateTo;
    set => this._AcqDateTo = value;
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AssetFilter.classID>
  {
  }

  public abstract class assetTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.assetTypeID>
  {
  }

  public abstract class propertyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.propertyType>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.condition>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.receiptNbr>
  {
  }

  public abstract class pONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.pONumber>
  {
  }

  public abstract class billNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.billNumber>
  {
  }

  public abstract class organizationID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AssetFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class buildingID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AssetFilter.buildingID>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.floor>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.room>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AssetFilter.employeeID>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AssetFilter.department>
  {
  }

  public abstract class acqDateFrom : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AssetFilter.acqDateFrom>
  {
  }

  public abstract class acqDateTo : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AssetFilter.acqDateTo>
  {
  }
}
