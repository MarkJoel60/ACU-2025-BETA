// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UnitOfMeasure
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CC;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>Localizable descriptions for units of measure.</summary>
[PXCacheName("Unit of Measure")]
[PXPrimaryGraph(typeof (UnitOfMeasureMaint))]
[Serializable]
public class UnitOfMeasure : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unit of measure.</summary>
  [PXDBString(6, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Unit ID")]
  [PXSelector(typeof (Search<UnitOfMeasure.unit>), new Type[] {typeof (UnitOfMeasure.unit), typeof (UnitOfMeasure.descr), typeof (UnitOfMeasure.l3Code)})]
  public virtual 
  #nullable disable
  string Unit { get; set; }

  /// <summary>The localizable description for use in reports.</summary>
  [PXDBLocalizableString(6, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description for Reports")]
  public virtual string Descr { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  /// <summary>Level 3 Code for Unit of Measure.</summary>
  [PXDefault]
  [PXUIField(DisplayName = "Level 3 Unit ID")]
  [PXSelectorByMethod(typeof (UoML3Helper), "GetCodes", typeof (Search<UoML3Helper.UoML3Code.l3Code>), new Type[] {typeof (UoML3Helper.UoML3Code.l3Code), typeof (UoML3Helper.UoML3Code.description)}, DescriptionField = typeof (UoML3Helper.UoML3Code.description))]
  [PXDBString(3)]
  public virtual string L3Code { get; set; }

  /// <summary>Unit of measure primary key</summary>
  public class PK : PrimaryKeyOf<UnitOfMeasure>.By<UnitOfMeasure.unit>
  {
    public static UnitOfMeasure Find(PXGraph graph, string unit)
    {
      return (UnitOfMeasure) PrimaryKeyOf<UnitOfMeasure>.By<UnitOfMeasure.unit>.FindBy(graph, (object) unit, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Conversion : 
      PrimaryKeyOf<INUnit>.By<INUnit.recordID>.ForeignKeyOf<UnitOfMeasure>.By<UnitOfMeasure.unit>
    {
    }
  }

  public abstract class unit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnitOfMeasure.unit>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnitOfMeasure.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UnitOfMeasure.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UnitOfMeasure.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UnitOfMeasure.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnitOfMeasure.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UnitOfMeasure.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UnitOfMeasure.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnitOfMeasure.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UnitOfMeasure.lastModifiedDateTime>
  {
  }

  public abstract class l3Code : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnitOfMeasure.l3Code>
  {
  }
}
