// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseClassLaborMatrix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Case Class Labor")]
[Serializable]
public class CRCaseClassLaborMatrix : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CaseClassID;
  protected string _EarningType;
  protected int? _LabourItemID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CRCaseClass.caseClassID))]
  [PXParent(typeof (Select<CRCaseClass, Where<Current<CRCaseClassLaborMatrix.caseClassID>, Equal<CRCaseClass.caseClassID>>>))]
  public virtual string CaseClassID
  {
    get => this._CaseClassID;
    set => this._CaseClassID = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningType
  {
    get => this._EarningType;
    set => this._EarningType = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Labor Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXForeignReference(typeof (Field<CRCaseClassLaborMatrix.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LabourItemID
  {
    get => this._LabourItemID;
    set => this._LabourItemID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public static int? GetLaborClassID(PXGraph graph, string caseClassID, string earningTypeID)
  {
    return PXResultset<CRCaseClassLaborMatrix>.op_Implicit(PXSelectBase<CRCaseClassLaborMatrix, PXSelect<CRCaseClassLaborMatrix, Where<CRCaseClassLaborMatrix.caseClassID, Equal<Required<CRCaseClass.caseClassID>>, And<CRCaseClassLaborMatrix.earningType, Equal<Required<CRCaseClassLaborMatrix.earningType>>>>>.Config>.Select(graph, new object[2]
    {
      (object) caseClassID,
      (object) earningTypeID
    }))?.LabourItemID;
  }

  public abstract class caseClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.caseClassID>
  {
  }

  public abstract class earningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.earningType>
  {
  }

  public abstract class labourItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.labourItemID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCaseClassLaborMatrix.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseClassLaborMatrix.lastModifiedDateTime>
  {
  }
}
