// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PickingEfficiency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
public class PickingEfficiency : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Enabled = false)]
  public virtual int? SiteID { get; set; }

  [PXDateAndTime]
  public virtual DateTime? OverallStartDate { get; set; }

  [PXDateAndTime]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate { get; set; }

  [PXDateAndTime(IsKey = true)]
  [PXUIField(DisplayName = "End Date", Enabled = false)]
  public virtual DateTime? EndDate { get; set; }

  [PXDateAndTime]
  public virtual DateTime? OverallEndDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Day", Enabled = false)]
  public virtual DateTime? Day
  {
    get => this.StartDate;
    set
    {
    }
  }

  [PXString(4, IsKey = true, IsFixed = true, IsUnicode = false)]
  [SOShipmentProcessedByUser.jobType.List]
  [PXUIField(DisplayName = "Operation Type", Enabled = false)]
  public virtual 
  #nullable disable
  string JobType { get; set; }

  [PXString(4, IsKey = true, IsFixed = true, IsUnicode = false)]
  [SOShipmentProcessedByUser.docType.List]
  public virtual string DocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string ShipmentNbr { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string WorksheetNbr { get; set; }

  [PXInt]
  public virtual int? PickerNbr { get; set; }

  [PXString(IsKey = true)]
  [PXUIField]
  public virtual string PickListNbr { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "User", Enabled = false)]
  [PXSelector(typeof (SearchFor<Users.pKID>.Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>), SubstituteKey = typeof (Users.username))]
  public virtual Guid? UserID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Useful Operations", Enabled = false)]
  public virtual int? NumberOfUsefulOperations { get; set; }

  /// <summary>
  /// The number of all the scans done by user for the pick list.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Number of Actual Operations", Enabled = false)]
  public virtual int? NumberOfScans { get; set; }

  /// <summary>The number of scans that lead to errors.</summary>
  [PXInt]
  [PXUIField(DisplayName = "Number of Errors", Enabled = false)]
  public virtual int? NumberOfFailedScans { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Pick Lists", Enabled = false)]
  public virtual int? NumberOfShipments { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Lines", Enabled = false)]
  public virtual int? NumberOfLines { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Unique Locations", Enabled = false)]
  public virtual int? NumberOfLocations { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Unique Items", Enabled = false)]
  public virtual int? NumberOfInventories { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Number of Packages", Enabled = false)]
  public virtual int? NumberOfPackages { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Total Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDecimal]
  public virtual Decimal? TotalSeconds { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Total Time", Enabled = false, Visible = false)]
  public virtual string TotalTime
  {
    get
    {
      Decimal? totalSeconds1 = this.TotalSeconds;
      Decimal num1 = (Decimal) 3600;
      __Boxed<int> local1 = (ValueType) (int) (totalSeconds1.HasValue ? new Decimal?(totalSeconds1.GetValueOrDefault() / num1) : new Decimal?()).Value;
      Decimal? totalSeconds2 = this.TotalSeconds;
      Decimal num2 = (Decimal) 60;
      __Boxed<int> local2 = (ValueType) ((int) (totalSeconds2.HasValue ? new Decimal?(totalSeconds2.GetValueOrDefault() / num2) : new Decimal?()).Value % 60);
      Decimal? totalSeconds3 = this.TotalSeconds;
      Decimal num3 = (Decimal) 60;
      __Boxed<Decimal?> local3 = (ValueType) (totalSeconds3.HasValue ? new Decimal?(totalSeconds3.GetValueOrDefault() % num3) : new Decimal?());
      return $"{local1}:{local2:00}:{local3:00}";
    }
    set
    {
    }
  }

  [PXDecimal]
  public virtual Decimal? EffectiveSeconds { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Actual Time", Enabled = false)]
  public virtual string EffectiveTime
  {
    get
    {
      Decimal? effectiveSeconds1 = this.EffectiveSeconds;
      Decimal num1 = (Decimal) 3600;
      __Boxed<int> local1 = (ValueType) (int) (effectiveSeconds1.HasValue ? new Decimal?(effectiveSeconds1.GetValueOrDefault() / num1) : new Decimal?()).Value;
      Decimal? effectiveSeconds2 = this.EffectiveSeconds;
      Decimal num2 = (Decimal) 60;
      __Boxed<int> local2 = (ValueType) ((int) (effectiveSeconds2.HasValue ? new Decimal?(effectiveSeconds2.GetValueOrDefault() / num2) : new Decimal?()).Value % 60);
      Decimal? effectiveSeconds3 = this.EffectiveSeconds;
      Decimal num3 = (Decimal) 60;
      __Boxed<Decimal?> local3 = (ValueType) (effectiveSeconds3.HasValue ? new Decimal?(effectiveSeconds3.GetValueOrDefault() % num3) : new Decimal?());
      return $"{local1}:{local2:00}:{local3:00}";
    }
    set
    {
    }
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Efficiency (Operations per Min.)", Enabled = false)]
  public virtual Decimal? Efficiency
  {
    get
    {
      int? usefulOperations = this.NumberOfUsefulOperations;
      Decimal? nullable1 = usefulOperations.HasValue ? new Decimal?((Decimal) usefulOperations.GetValueOrDefault()) : new Decimal?();
      Decimal? efficiency = this.EffectiveSeconds;
      Decimal num = (Decimal) 60;
      Decimal? nullable2 = efficiency.HasValue ? new Decimal?(efficiency.GetValueOrDefault() / num) : new Decimal?();
      if (nullable1.HasValue & nullable2.HasValue)
        return new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault());
      efficiency = new Decimal?();
      return efficiency;
    }
    set
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PickingEfficiency.siteID>
  {
  }

  public abstract class overallStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PickingEfficiency.overallStartDate>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PickingEfficiency.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PickingEfficiency.endDate>
  {
  }

  public abstract class overallEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PickingEfficiency.overallEndDate>
  {
  }

  public abstract class day : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PickingEfficiency.day>
  {
  }

  public abstract class jobType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PickingEfficiency.jobType>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PickingEfficiency.docType>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PickingEfficiency.shipmentNbr>
  {
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PickingEfficiency.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PickingEfficiency.pickerNbr>
  {
  }

  public abstract class pickListNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PickingEfficiency.pickListNbr>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PickingEfficiency.userID>
  {
  }

  public abstract class numberOfUsefulOperations : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfUsefulOperations>
  {
  }

  public abstract class numberOfScans : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PickingEfficiency.numberOfScans>
  {
  }

  public abstract class numberOfFailedScans : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfFailedScans>
  {
  }

  public abstract class numberOfShipments : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfShipments>
  {
  }

  public abstract class numberOfLines : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PickingEfficiency.numberOfLines>
  {
  }

  public abstract class numberOfLocations : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfLocations>
  {
  }

  public abstract class numberOfInventories : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfInventories>
  {
  }

  public abstract class numberOfPackages : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PickingEfficiency.numberOfPackages>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PickingEfficiency.totalQty>
  {
  }

  public abstract class totalSeconds : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PickingEfficiency.totalSeconds>
  {
  }

  public abstract class totalTime : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PickingEfficiency.totalTime>
  {
  }

  public abstract class effectiveSeconds : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PickingEfficiency.effectiveSeconds>
  {
  }

  public abstract class effectiveTime : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PickingEfficiency.effectiveTime>
  {
  }

  public abstract class efficiency : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PickingEfficiency.efficiency>
  {
  }
}
