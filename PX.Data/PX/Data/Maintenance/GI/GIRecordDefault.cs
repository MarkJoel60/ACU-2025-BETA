// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIRecordDefault
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
[PXCacheName("Generic Inquiry Default Record")]
public class GIRecordDefault : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIRecordDefault.designID>>>>))]
  public Guid? DesignID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Default ID", Visible = false)]
  public int? RecDefID { get; set; }

  [PXDBString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field", Required = true)]
  [PrimaryViewFieldsList(typeof (GIDesign.primaryScreenID), EnabledOnly = true, VisibleOnly = true, ShowDisplayNameAsLabel = false)]
  [PXUnique(typeof (GIDesign))]
  public 
  #nullable disable
  string FieldName { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "Value")]
  [PrimaryViewValueList(512 /*0x0200*/, typeof (GIDesign.primaryScreenID), typeof (GIRecordDefault.fieldName))]
  public string Value { get; set; }

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
    PrimaryKeyOf<GIRecordDefault>.By<GIRecordDefault.designID, GIRecordDefault.recDefID>
  {
    public static GIRecordDefault Find(
      PXGraph graph,
      Guid? designID,
      int? recDefID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIRecordDefault>.By<GIRecordDefault.designID, GIRecordDefault.recDefID>.FindBy(graph, (object) designID, (object) recDefID, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIRecordDefault>.By<GIRecordDefault.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRecordDefault.designID>
  {
  }

  /// <exclude />
  public abstract class recDefID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIRecordDefault.recDefID>
  {
  }

  /// <exclude />
  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIRecordDefault.fieldName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIRecordDefault.value>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRecordDefault.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIRecordDefault.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIRecordDefault.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GIRecordDefault.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIRecordDefault.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIRecordDefault.lastModifiedDateTime>
  {
  }
}
