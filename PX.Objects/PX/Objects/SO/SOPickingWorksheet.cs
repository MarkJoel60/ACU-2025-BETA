// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
[PXPrimaryGraph(typeof (SOPickingWorksheetReview))]
public class SOPickingWorksheet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (SOSetup.pickingWorksheetNumberingID), typeof (SOPickingWorksheet.pickDate))]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  [PXSelector(typeof (SearchFor<SOPickingWorksheet.worksheetNbr>.In<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, NotEqual<SOPickingWorksheet.worksheetType.single>>>>>.And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>.OrderBy<BqlField<SOPickingWorksheet.worksheetNbr, IBqlString>.Desc>>))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXExtraKey]
  [PXDefault]
  [SOPickingWorksheet.worksheetType.List]
  [PXUIField]
  public virtual string WorksheetType { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? PickDate { get; set; }

  [DBConditionalModifiedDateTime(typeof (SOPickingWorksheet.status), "I", KeepValue = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.cancelled>>.Else<SOPickingWorksheet.pickStartDate>))]
  [PXUIField(DisplayName = "Picking Started On", Enabled = false)]
  public virtual DateTime? PickStartDate { get; set; }

  [DBConditionalModifiedDateTime(typeof (SOPickingWorksheet.status), "P", KeepValue = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.picking, SOPickingWorksheet.status.open>>.Else<SOPickingWorksheet.pickCompleteDate>))]
  [PXUIField(DisplayName = "Picking Finished On", Enabled = false)]
  public virtual DateTime? PickCompleteDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [SOPickingWorksheet.status.List]
  public virtual string Status { get; set; }

  [PXDBBool]
  [PXDefault(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>>.Else<BqlField<SOSetup.holdShipments, IBqlBool>.FromCurrent>))]
  [PXUIField(DisplayName = "Hold", Visible = false, Enabled = false)]
  public virtual bool? Hold { get; set; }

  [Site]
  [PXDefault]
  [PXForeignReference(typeof (SOPickingWorksheet.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>))]
  public virtual int? SiteID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Weight", Enabled = false)]
  public virtual Decimal? ShipmentWeight { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Volume", Enabled = false)]
  public virtual Decimal? ShipmentVolume { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (SOShipment.shipmentNbr))]
  [PXForeignReference(typeof (SOPickingWorksheet.FK.SingleShipment))]
  public virtual string SingleShipmentNbr { get; set; }

  [PXSearchable(256 /*0x0100*/, "{0}: {1}", new Type[] {typeof (SOPickingWorksheet.worksheetType), typeof (SOPickingWorksheet.worksheetNbr)}, new Type[] {}, NumberFields = new Type[] {typeof (SOPickingWorksheet.worksheetNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new Type[] {typeof (SOPickingWorksheet.pickDate), typeof (SOPickingWorksheet.status), typeof (SOPickingWorksheet.qty)})]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (SOPickingWorksheet.worksheetNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>
  {
    public static SOPickingWorksheet Find(PXGraph graph, string groupNbr, PKFindOptions options = 0)
    {
      return (SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.FindBy(graph, (object) groupNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.siteID>
    {
    }

    public class SingleShipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.singleShipmentNbr>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheet.worksheetNbr>
  {
  }

  public abstract class worksheetType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheet.worksheetType>
  {
    public const string Wave = "WV";
    public const string Batch = "BT";
    public const string Single = "SS";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Wave = "Wave";
      public const string Batch = "Batch";
      public const string Single = "Single-Shipment";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("SS", "Single-Shipment"),
          PXStringListAttribute.Pair("WV", "Wave"),
          PXStringListAttribute.Pair("BT", "Batch")
        })
      {
      }
    }

    public class wave : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.worksheetType.wave>
    {
      public wave()
        : base("WV")
      {
      }
    }

    public class batch : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.worksheetType.batch>
    {
      public batch()
        : base("BT")
      {
      }
    }

    public class single : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheet.worksheetType.single>
    {
      public single()
        : base("SS")
      {
      }
    }
  }

  public abstract class pickDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOPickingWorksheet.pickDate>
  {
  }

  public abstract class pickStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheet.pickStartDate>
  {
  }

  public abstract class pickCompleteDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheet.pickCompleteDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingWorksheet.status>
  {
    public const string Open = "N";
    public const string Hold = "H";
    public const string Picking = "I";
    public const string Picked = "P";
    public const string Completed = "C";
    public const string Cancelled = "L";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Open = "Open";
      public const string Hold = "On Hold";
      public const string Picking = "Picking";
      public const string Picked = "Picked";
      public const string Completed = "Completed";
      public const string Cancelled = "Canceled";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[6]
        {
          PXStringListAttribute.Pair("N", "Open"),
          PXStringListAttribute.Pair("H", "On Hold"),
          PXStringListAttribute.Pair("I", "Picking"),
          PXStringListAttribute.Pair("P", "Picked"),
          PXStringListAttribute.Pair("C", "Completed"),
          PXStringListAttribute.Pair("L", "Canceled")
        })
      {
      }
    }

    public class open : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.status.open>
    {
      public open()
        : base("N")
      {
      }
    }

    public class hold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.status.hold>
    {
      public hold()
        : base("H")
      {
      }
    }

    public class picking : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.status.picking>
    {
      public picking()
        : base("I")
      {
      }
    }

    public class picked : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingWorksheet.status.picked>
    {
      public picked()
        : base("P")
      {
      }
    }

    public class completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheet.status.completed>
    {
      public completed()
        : base("C")
      {
      }
    }

    public class cancelled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingWorksheet.status.cancelled>
    {
      public cancelled()
        : base("L")
      {
      }
    }
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickingWorksheet.hold>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheet.siteID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickingWorksheet.qty>
  {
  }

  public abstract class shipmentWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheet.shipmentWeight>
  {
  }

  public abstract class shipmentVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheet.shipmentVolume>
  {
  }

  public abstract class singleShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheet.singleShipmentNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPickingWorksheet.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPickingWorksheet.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPickingWorksheet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheet.lastModifiedDateTime>
  {
  }
}
