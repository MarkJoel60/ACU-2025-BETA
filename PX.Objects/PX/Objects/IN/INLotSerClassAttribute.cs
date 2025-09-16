// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerClassAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.IN;

/// <exclude />
[DebuggerDisplay("[{AttributeID}, {LotSerClassID}]")]
[PXCacheName("Lot/Serial Class Attribute")]
public class INLotSerClassAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXParent(typeof (INLotSerClassAttribute.FK.LotSerClass))]
  [PXDefault(typeof (INLotSerClass.lotSerClassID))]
  [PXUIField]
  public virtual 
  #nullable disable
  string LotSerClassID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CSAttribute.attributeID, Where<CSAttribute.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>>))]
  public virtual string AttributeID { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Required { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

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

  public class PK : 
    PrimaryKeyOf<INLotSerClassAttribute>.By<INLotSerClassAttribute.lotSerClassID, INLotSerClassAttribute.attributeID>
  {
    public static INLotSerClassAttribute Find(
      PXGraph graph,
      string lotSerClassID,
      string attributeID,
      PKFindOptions options = 0)
    {
      return (INLotSerClassAttribute) PrimaryKeyOf<INLotSerClassAttribute>.By<INLotSerClassAttribute.lotSerClassID, INLotSerClassAttribute.attributeID>.FindBy(graph, (object) lotSerClassID, (object) attributeID, options);
    }
  }

  public static class FK
  {
    public class LotSerClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<INLotSerClassAttribute>.By<INLotSerClassAttribute.lotSerClassID>
    {
    }

    public class Attribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INLotSerClassAttribute>.By<INLotSerClassAttribute.attributeID>
    {
    }
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassAttribute.lotSerClassID>
  {
  }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassAttribute.attributeID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INLotSerClassAttribute.sortOrder>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLotSerClassAttribute.required>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLotSerClassAttribute.isActive>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INLotSerClassAttribute.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerClassAttribute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassAttribute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClassAttribute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerClassAttribute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClassAttribute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClassAttribute.lastModifiedDateTime>
  {
  }
}
