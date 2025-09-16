// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.Allocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

/// <exclude />
[PXCacheName("Allocations")]
[PXProjection(typeof (Select<AllocationInternal>))]
public class Allocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBLong(IsKey = true, BqlField = typeof (AllocationInternal.planID))]
  public virtual long? PlanID { get; set; }

  [PXString]
  [PXEntityName(typeof (Allocation.refNoteID))]
  [PXUIField(DisplayName = "Document Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXRefNote(BqlField = typeof (AllocationInternal.refNoteID))]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false, BqlField = typeof (AllocationInternal.refEntityType))]
  public string RefEntityType { get; set; }

  [PXDBInt(BqlField = typeof (AllocationInternal.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [StockItem(BqlField = typeof (AllocationInternal.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [Site(BqlField = typeof (AllocationInternal.siteID))]
  public virtual int? SiteID { get; set; }

  [PXUIField(DisplayName = "Allocated Lot/Serial Nbr.", FieldClass = "LotSerial")]
  [PXDBString(100, IsUnicode = true, InputMask = "", BqlField = typeof (AllocationInternal.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBQuantity(BqlField = typeof (AllocationInternal.allocatedQty))]
  [PXUIField(DisplayName = "Allocated Qty.")]
  public virtual Decimal? AllocatedQty { get; set; }

  [DocumentStatus.List]
  [PXDBString(255 /*0xFF*/, IsUnicode = false, BqlField = typeof (AllocationInternal.status))]
  public virtual string Status { get; set; }

  [PXDBInt(BqlField = typeof (AllocationInternal.ownerID))]
  [PXUIField(DisplayName = "Owner")]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>), SubstituteKey = typeof (PX.Objects.CR.Contact.displayName))]
  public virtual int? OwnerID { get; set; }

  [Customer(BqlField = typeof (AllocationInternal.customerID))]
  public virtual int? CustomerID { get; set; }

  [PXDBDate(BqlField = typeof (AllocationInternal.date))]
  [PXUIField(DisplayName = "Document date")]
  public virtual DateTime? Date { get; set; }

  [PXDBDate(BqlField = typeof (AllocationInternal.requestedDate))]
  [PXUIField(DisplayName = "Requested On")]
  public virtual DateTime? RequestedDate { get; set; }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Allocation.planID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Allocation.docType>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Allocation.refNoteID>
  {
  }

  public abstract class refEntityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Allocation.refEntityType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Allocation.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Allocation.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Allocation.siteID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Allocation.lotSerialNbr>
  {
  }

  public abstract class allocatedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Allocation.allocatedQty>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Allocation.status>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Allocation.ownerID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Allocation.customerID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Allocation.date>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Allocation.requestedDate>
  {
  }
}
