// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttributeGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.IN.Matrix.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CS;

[DebuggerDisplay("[{AttributeID}, {EntityClassID}]: {Description}")]
[PXPrimaryGraph(typeof (CSAttributeMaint))]
[PXCacheName("Attribute Group")]
[Serializable]
public class CSAttributeGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AttributeID;
  protected string _EntityClassID;
  protected short? _SortOrder;
  protected string _Description;
  protected bool? _Required;
  protected int? _ControlType;
  protected string _DefaultValue;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CSAttribute.attributeID, Where<CSAttribute.controlType, NotEqual<AttributeControlType.giSelector>, And<CSAttribute.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>>>))]
  public virtual string AttributeID
  {
    get => this._AttributeID;
    set => this._AttributeID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string EntityClassID
  {
    get => this._EntityClassID;
    set => this._EntityClassID = value;
  }

  [PXDBString(200, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Type")]
  [PXDefault]
  public virtual string EntityType { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXFormula(typeof (Selector<CSAttributeGroup.attributeID, CSAttribute.description>))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXAttributeControlType(IsDBField = false, Enabled = false)]
  [PXFormula(typeof (Selector<CSAttributeGroup.attributeID, CSAttribute.controlType>))]
  public virtual int? ControlType
  {
    get => this._ControlType;
    set => this._ControlType = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  [DynamicValueValidation(typeof (Search<CSAttribute.regExp, Where<CSAttribute.attributeID, Equal<Current<CSAttributeGroup.attributeID>>>>))]
  public virtual string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [PXDefault("A")]
  [PXDBString(1, IsUnicode = false, IsFixed = true)]
  [PXUIField(DisplayName = "Category", FieldClass = "MatrixItem")]
  [CSAttributeGroup.attributeCategory.List]
  public virtual string AttributeCategory { get; set; }

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

  public class PK : 
    PrimaryKeyOf<CSAttributeGroup>.By<CSAttributeGroup.attributeID, CSAttributeGroup.entityClassID, CSAttributeGroup.entityType>
  {
    public static CSAttributeGroup Find(
      PXGraph graph,
      string attributeID,
      string entityClassID,
      string entityType,
      PKFindOptions options = 0)
    {
      return (CSAttributeGroup) PrimaryKeyOf<CSAttributeGroup>.By<CSAttributeGroup.attributeID, CSAttributeGroup.entityClassID, CSAttributeGroup.entityType>.FindBy(graph, (object) attributeID, (object) entityClassID, (object) entityType, options);
    }
  }

  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttributeGroup.attributeID>
  {
  }

  public abstract class entityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeGroup.entityClassID>
  {
  }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttributeGroup.entityType>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSAttributeGroup.sortOrder>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttributeGroup.description>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttributeGroup.required>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttributeGroup.isActive>
  {
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSAttributeGroup.controlType>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeGroup.defaultValue>
  {
  }

  public abstract class attributeCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeGroup.attributeCategory>
  {
    public const string Attribute = "A";
    public const string Variant = "V";

    [PXLocalizable]
    public class DisplayNames
    {
      public const string Attribute = "Attribute";
      public const string Variant = "Variant";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("A", "Attribute"),
          PXStringListAttribute.Pair("V", "Variant")
        })
      {
      }
    }

    public class attribute : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CSAttributeGroup.attributeCategory.attribute>
    {
      public attribute()
        : base("A")
      {
      }
    }

    public class variant : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CSAttributeGroup.attributeCategory.variant>
    {
      public variant()
        : base("V")
      {
      }
    }
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSAttributeGroup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttributeGroup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeGroup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSAttributeGroup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSAttributeGroup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeGroup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSAttributeGroup.lastModifiedDateTime>
  {
  }
}
