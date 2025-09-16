// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPContractRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>Stores the information related to the contract.</summary>
[PXPrimaryGraph(new System.Type[] {typeof (EmployeeMaint)}, new System.Type[] {typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPContractRate.employeeID>>>>)})]
[PXCacheName("Contract Rates")]
[Serializable]
public class EPContractRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _EmployeeID;
  protected 
  #nullable disable
  string _EarningType;
  protected int? _ContractID;
  protected int? _LabourItemID;
  protected bool? _IsActive;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBInt]
  [PXParent(typeof (Select<EPEmployeeContract, Where<EPEmployeeContract.employeeID, Equal<Current<EPContractRate.employeeID>>, And<EPEmployeeContract.contractID, Equal<Current<EPContractRate.contractID>>>>>))]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<EPContractRate.earningType, Equal<Current<EPContractRate.earningType>>, And<EPContractRate.contractID, Equal<Current<EPContractRate.contractID>>>>))]
  [PXDBDefault(typeof (EPEmployeeContract.employeeID))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<EPContractRate.employeeID, Equal<Current<EPContractRate.employeeID>>, And<EPContractRate.contractID, Equal<Current<EPContractRate.contractID>>>>))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningType
  {
    get => this._EarningType;
    set => this._EarningType = value;
  }

  [PXDBInt]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<EPContractRate.employeeID, Equal<Current<EPContractRate.employeeID>>, And<EPContractRate.earningType, Equal<Current<EPContractRate.earningType>>>>))]
  [PXDBDefault(typeof (EPEmployeeContract.contractID))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Labor Item")]
  [PXDefault]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXForeignReference(typeof (Field<EPContractRate.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LabourItemID
  {
    get => this._LabourItemID;
    set => this._LabourItemID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public static int? GetContractLaborClassID(PXGraph graph, PMTimeActivity activity)
  {
    EPContractRate epContractRate = PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelectJoin<EPContractRate, InnerJoin<CRCase, On<CRCase.contractID, Equal<EPContractRate.contractID>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>, InnerJoin<EPEmployee, On<EPContractRate.employeeID, Equal<EPEmployee.bAccountID>>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>, And<EPContractRate.earningType, Equal<Required<PMTimeActivity.earningTypeID>>, And<EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) activity.RefNoteID,
      (object) activity.EarningTypeID,
      (object) activity.OwnerID
    }));
    if (epContractRate == null)
      epContractRate = PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelectJoin<EPContractRate, InnerJoin<CRCase, On<CRCase.contractID, Equal<EPContractRate.contractID>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>, And<EPContractRate.earningType, Equal<Required<PMTimeActivity.earningTypeID>>, And<EPContractRate.employeeID, IsNull>>>>.Config>.Select(graph, new object[2]
      {
        (object) activity.RefNoteID,
        (object) activity.EarningTypeID
      }));
    return epContractRate?.LabourItemID;
  }

  public static int? GetProjectLaborClassID(
    PXGraph graph,
    int projectID,
    int employeeID,
    string earningType)
  {
    return PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Required<EPContractRate.contractID>>, And<EPContractRate.employeeID, Equal<Required<EPContractRate.employeeID>>, And<EPContractRate.earningType, Equal<Required<EPEarningType.typeCD>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) projectID,
      (object) employeeID,
      (object) earningType
    }))?.LabourItemID;
  }

  public static void UpdateKeyFields(
    PXGraph graph,
    int? oldProjectID,
    int? oldEmployeeID,
    int? newProjectID,
    int? newEmployeeID)
  {
    foreach (PXResult<EPContractRate> pxResult in PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Required<EPContractRate.contractID>>, And<EPContractRate.employeeID, Equal<Required<EPContractRate.employeeID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) oldProjectID,
      (object) oldEmployeeID
    }))
    {
      EPContractRate epContractRate = PXResult<EPContractRate>.op_Implicit(pxResult);
      epContractRate.ContractID = newProjectID;
      epContractRate.EmployeeID = newEmployeeID;
      graph.Caches[typeof (EPContractRate)].Update((object) epContractRate);
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPContractRate.recordID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPContractRate.employeeID>
  {
  }

  public abstract class earningType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPContractRate.earningType>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPContractRate.contractID>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPContractRate.labourItemID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPContractRate.isActive>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPContractRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPContractRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPContractRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPContractRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPContractRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPContractRate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPContractRate.Tstamp>
  {
  }
}
