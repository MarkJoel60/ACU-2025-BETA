// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GINavigationParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.GenericInquiry.Attributes;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXCacheName("Generic Inquiry Navigation Parameter")]
public class GINavigationParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GINavigationParameter.designID>>, And<GINavigationScreen.lineNbr, Equal<Current<GINavigationParameter.navigationScreenLineNbr>>>>>))]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GINavigationScreen))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (GINavigationScreen.lineNbr))]
  public virtual int? NavigationScreenLineNbr { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field", Required = true)]
  [PrimaryViewFieldsList(typeof (GINavigationScreen.link), KeysOnly = true, ShowDisplayNameAsLabel = false, ShowHiddenFields = true)]
  [PXUnique(typeof (GINavigationScreen))]
  public 
  #nullable disable
  string FieldName { get; set; }

  [PXFormulaEditor(DisplayName = "Parameter", Required = true)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [GIParameterNamePrimaryViewValueList]
  [GiStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  [PXDefault]
  public string ParameterName { get; set; }

  public string TableAlias
  {
    get
    {
      if (string.IsNullOrEmpty(this.ParameterName) || this.ParameterName.StartsWith("="))
        return (string) null;
      int length = this.ParameterName.IndexOf('.');
      return length >= 0 ? this.ParameterName.Substring(0, length) : (string) null;
    }
  }

  public string ParameterFieldName
  {
    get
    {
      if (string.IsNullOrEmpty(this.ParameterName) || this.ParameterName.StartsWith("="))
        return (string) null;
      int num = this.ParameterName.IndexOf('.');
      return num >= 0 ? this.ParameterName.Substring(num + 1, this.ParameterName.Length - num - 1) : (string) null;
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsExpression { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false, Visible = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false, Visible = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<GINavigationParameter>.By<GINavigationParameter.designID, GINavigationParameter.navigationScreenLineNbr, GINavigationParameter.lineNbr>
  {
    public static GINavigationParameter Find(
      PXGraph graph,
      Guid? designID,
      int? navigationScreenLineNbr,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GINavigationParameter>.By<GINavigationParameter.designID, GINavigationParameter.navigationScreenLineNbr, GINavigationParameter.lineNbr>.FindBy(graph, (object) designID, (object) navigationScreenLineNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GINavigationParameter>.By<GINavigationParameter.designID>
    {
    }

    public class NavigationScreen : 
      PrimaryKeyOf<GINavigationScreen>.By<GINavigationScreen.designID, GINavigationScreen.lineNbr>.ForeignKeyOf<GINavigationParameter>.By<GINavigationParameter.designID, GINavigationParameter.navigationScreenLineNbr>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GINavigationParameter.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GINavigationParameter.lineNbr>
  {
  }

  public abstract class navigationScreenLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GINavigationParameter.navigationScreenLineNbr>
  {
  }

  /// <exclude />
  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationParameter.fieldName>
  {
  }

  /// <exclude />
  public abstract class parameterName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationParameter.parameterName>
  {
  }

  /// <exclude />
  public abstract class isExpression : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GINavigationParameter.isExpression>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GINavigationParameter.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationParameter.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationParameter.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GINavigationParameter.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationParameter.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationParameter.lastModifiedDateTime>
  {
  }
}
