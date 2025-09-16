// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GS1UOMSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("GS1 Unit Setup")]
public class GS1UOMSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [INUnit(DisplayName = "Kilogram")]
  public virtual 
  #nullable disable
  string Kilogram { get; set; }

  [INUnit(DisplayName = "Pound")]
  public virtual string Pound { get; set; }

  [INUnit(DisplayName = "Ounce")]
  public virtual string Ounce { get; set; }

  [INUnit(DisplayName = "Troy Ounce")]
  public virtual string TroyOunce { get; set; }

  [INUnit(DisplayName = "Metre")]
  public virtual string Metre { get; set; }

  [INUnit(DisplayName = "Inch")]
  public virtual string Inch { get; set; }

  [INUnit(DisplayName = "Foot")]
  public virtual string Foot { get; set; }

  [INUnit(DisplayName = "Yard")]
  public virtual string Yard { get; set; }

  [INUnit(DisplayName = "Square Metre")]
  public virtual string SqrMetre { get; set; }

  [INUnit(DisplayName = "Square Inch")]
  public virtual string SqrInch { get; set; }

  [INUnit(DisplayName = "Square Foot")]
  public virtual string SqrFoot { get; set; }

  [INUnit(DisplayName = "Square Yard")]
  public virtual string SqrYard { get; set; }

  [INUnit(DisplayName = "Cubic Metre")]
  public virtual string CubicMetre { get; set; }

  [INUnit(DisplayName = "Cubic Inch")]
  public virtual string CubicInch { get; set; }

  [INUnit(DisplayName = "Cubic Foot")]
  public virtual string CubicFoot { get; set; }

  [INUnit(DisplayName = "Cubic Yard")]
  public virtual string CubicYard { get; set; }

  [INUnit(DisplayName = "Litre")]
  public virtual string Litre { get; set; }

  [INUnit(DisplayName = "Quart")]
  public virtual string Quart { get; set; }

  [INUnit(DisplayName = "Gallon U.S.")]
  public virtual string GallonUS { get; set; }

  [INUnit(DisplayName = "Kilogram per Square Metre")]
  public virtual string KilogramPerSqrMetre { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public static class FK
  {
  }

  public abstract class kilogram : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.kilogram>
  {
  }

  public abstract class pound : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.pound>
  {
  }

  public abstract class ounce : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.ounce>
  {
  }

  public abstract class troyOunce : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.troyOunce>
  {
  }

  public abstract class metre : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.metre>
  {
  }

  public abstract class inch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.inch>
  {
  }

  public abstract class foot : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.foot>
  {
  }

  public abstract class yard : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.yard>
  {
  }

  public abstract class sqrMetre : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.sqrMetre>
  {
  }

  public abstract class sqrInch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.sqrInch>
  {
  }

  public abstract class sqrFoot : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.sqrFoot>
  {
  }

  public abstract class sqrYard : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.sqrYard>
  {
  }

  public abstract class cubicMetre : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.cubicMetre>
  {
  }

  public abstract class cubicInch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.cubicInch>
  {
  }

  public abstract class cubicFoot : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.cubicFoot>
  {
  }

  public abstract class cubicYard : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.cubicYard>
  {
  }

  public abstract class litre : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.litre>
  {
  }

  public abstract class quart : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.quart>
  {
  }

  public abstract class gallonUS : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GS1UOMSetup.gallonUS>
  {
  }

  public abstract class kilogramPerSqrMetre : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GS1UOMSetup.kilogramPerSqrMetre>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GS1UOMSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GS1UOMSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GS1UOMSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GS1UOMSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GS1UOMSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GS1UOMSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GS1UOMSetup.lastModifiedDateTime>
  {
  }
}
