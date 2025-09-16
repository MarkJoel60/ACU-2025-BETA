// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentProcessedByUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOShipmentProcessedByUser : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBString(4, IsFixed = true, IsUnicode = false)]
  [PXDefault]
  [SOShipmentProcessedByUser.jobType.List]
  [PXUIField(DisplayName = "Operation Type", Enabled = false)]
  public virtual 
  #nullable disable
  string JobType { get; set; }

  [PXDBString(4, IsFixed = true, IsUnicode = false)]
  [PXDefault]
  [SOShipmentProcessedByUser.docType.List]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXParent(typeof (SOShipmentProcessedByUser.FK.Shipment))]
  public virtual string ShipmentNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXParent(typeof (SOShipmentProcessedByUser.FK.Worksheet))]
  public virtual string WorksheetNbr { get; set; }

  [PXDBInt]
  [PXParent(typeof (SOShipmentProcessedByUser.FK.Picker))]
  public virtual int? PickerNbr { get; set; }

  [PXString]
  [PXUIField]
  [PXFormula(typeof (BqlFunctionMirror<IsNull<Parent<SOShipment.shipmentNbr>, BqlField<SOPickingWorksheet.singleShipmentNbr, IBqlString>.FromParent>, IBqlString>.IfNullThen<BqlField<SOPicker.pickListNbr, IBqlString>.FromParent>))]
  public virtual string PickListNbr { get; set; }

  [PXDBGuid(false)]
  [PXDefault(typeof (AccessInfo.userID))]
  [PXParent(typeof (SOShipmentProcessedByUser.FK.User))]
  public virtual Guid? UserID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? Confirmed { get; set; }

  /// <summary>
  /// The number of all the scans done by user for the pick list.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? NumberOfScans { get; set; }

  /// <summary>The number of scans that lead to errors.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? NumberOfFailedScans { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? OverallStartDateTime { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? StartDateTime { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? EndDateTime { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? OverallEndDateTime { get; set; }

  public class PK : PrimaryKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.recordID>
  {
    public static SOShipmentProcessedByUser Find(
      PXGraph graph,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (SOShipmentProcessedByUser) PrimaryKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.shipmentNbr>
    {
    }

    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.worksheetNbr>
    {
    }

    public class Picker : 
      PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.ForeignKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.worksheetNbr, SOShipmentProcessedByUser.pickerNbr>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOShipmentProcessedByUser>.By<SOShipmentProcessedByUser.userID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentProcessedByUser.recordID>
  {
  }

  public abstract class jobType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentProcessedByUser.jobType>
  {
    public const string Pick = "PICK";
    public const string Pack = "PACK";
    public const string PackOnly = "PPCK";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Pick = "Pick";
      public const string Pack = "Pack";
      public const string PackOnly = "Pack Only";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("PICK", "Pick"),
          PXStringListAttribute.Pair("PACK", "Pack"),
          PXStringListAttribute.Pair("PPCK", "Pack Only")
        })
      {
      }
    }

    public class pick : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOShipmentProcessedByUser.jobType.pick>
    {
      public pick()
        : base("PICK")
      {
      }
    }

    public class pack : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOShipmentProcessedByUser.jobType.pack>
    {
      public pack()
        : base("PACK")
      {
      }
    }

    public class packOnly : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOShipmentProcessedByUser.jobType.packOnly>
    {
      public packOnly()
        : base("PPCK")
      {
      }
    }
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentProcessedByUser.docType>
  {
    public const string PickList = "PLST";
    public const string Shipment = "SHPT";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string PickList = "Pick List";
      public const string Shipment = "Shipment";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("PLST", "Pick List"),
          PXStringListAttribute.Pair("SHPT", "Shipment")
        })
      {
      }
    }

    public class pickList : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOShipmentProcessedByUser.docType.pickList>
    {
      public pickList()
        : base("PLST")
      {
      }
    }

    public class shipment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOShipmentProcessedByUser.docType.shipment>
    {
      public shipment()
        : base("SHPT")
      {
      }
    }
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentProcessedByUser.shipmentNbr>
  {
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentProcessedByUser.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentProcessedByUser.pickerNbr>
  {
  }

  public abstract class pickListNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentProcessedByUser.pickListNbr>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipmentProcessedByUser.userID>
  {
  }

  public abstract class confirmed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentProcessedByUser.confirmed>
  {
  }

  public abstract class numberOfScans : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentProcessedByUser.numberOfScans>
  {
  }

  public abstract class numberOfFailedScans : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentProcessedByUser.numberOfFailedScans>
  {
  }

  public abstract class overallStartDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentProcessedByUser.overallStartDateTime>
  {
  }

  public abstract class startDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentProcessedByUser.startDateTime>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentProcessedByUser.lastModifiedDateTime>
  {
  }

  public abstract class endDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentProcessedByUser.endDateTime>
  {
  }

  public abstract class overallEndDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentProcessedByUser.overallEndDateTime>
  {
  }
}
