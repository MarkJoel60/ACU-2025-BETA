// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXCacheName("Generic Inquiry Filter")]
public class GIFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIFilter.designID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXCheckUnique(new System.Type[] {typeof (GIFilter.isActive)}, Where = typeof (Where<GIFilter.designID, Equal<Current<GIFilter.designID>>>), ClearOnDuplicate = false)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXStringList(new string[] {}, new string[] {}, ExclusiveValues = false)]
  public 
  #nullable disable
  string Name { get; set; }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Schema Field")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public string FieldName { get; set; }

  [PXDBString(InputMask = "")]
  [PXDefault("string")]
  [PXUIField(DisplayName = "Type", Visible = false)]
  [PXStringList(new string[] {"int", "short", "long", "string", "DateTime", "float", "double", "Guid"}, new string[] {"int", "short", "long", "string", "DateTime", "float", "double", "Guid"}, ExclusiveValues = false)]
  public string DataType { get; set; }

  [PXDBLocalizableString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public string DisplayName { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Available Values", Visible = false, Visibility = PXUIVisibility.Dynamic)]
  public string AvailableValues { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsExpression { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  public string DefaultValue { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Column Span")]
  public int? ColSpan { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Required")]
  public bool? Required { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Hidden", Visible = false)]
  public bool? Hidden { get; set; }

  [PXStringList(new string[] {"XXS", "XS", "S", "SM", "M", "XM", "L"}, new string[] {"XXS", "XS", "S", "SM", "M", "XM", "L"})]
  [PXDBString(3)]
  [PXUIField(DisplayName = "Control Size")]
  public virtual string Size { get; set; }

  [PXStringList(new string[] {"XXS", "XS", "S", "SM", "M", "XM", "L"}, new string[] {"XXS", "XS", "S", "SM", "M", "XM", "L"})]
  [PXDBString(3)]
  [PXUIField(DisplayName = "Label Size", Visible = false)]
  public virtual string LabelSize { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<GIFilter>.By<GIFilter.designID, GIFilter.lineNbr>
  {
    public static GIFilter Find(
      PXGraph graph,
      Guid? designID,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIFilter>.By<GIFilter.designID, GIFilter.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIFilter>.By<GIFilter.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIFilter.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIFilter.lineNbr>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIFilter.isActive>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.name>
  {
  }

  /// <exclude />
  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.fieldName>
  {
  }

  /// <exclude />
  public abstract class dataType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.dataType>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.displayName>
  {
  }

  /// <exclude />
  public abstract class availableValues : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.availableValues>
  {
  }

  /// <exclude />
  public abstract class isExpression : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIFilter.isExpression>
  {
  }

  /// <exclude />
  public abstract class defaultValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.defaultValue>
  {
  }

  /// <exclude />
  public abstract class colSpan : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIFilter.colSpan>
  {
  }

  /// <exclude />
  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIFilter.required>
  {
  }

  /// <exclude />
  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIFilter.hidden>
  {
  }

  /// <exclude />
  public abstract class size : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.size>
  {
  }

  /// <exclude />
  public abstract class labelSize : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIFilter.labelSize>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIFilter.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIFilter.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIFilter.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIFilter.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIFilter.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIFilter.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIFilter.lastModifiedDateTime>
  {
  }
}
