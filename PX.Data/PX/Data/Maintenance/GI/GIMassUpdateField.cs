// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIMassUpdateField
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
[PXCacheName("Generic Inquiry Mass Update Field")]
public class GIMassUpdateField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIMassUpdateField.designID>>>>))]
  public Guid? DesignID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Mass Update Field ID", Visible = false)]
  public int? FieldID { get; set; }

  [PXDBString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name")]
  [PrimaryViewFieldsList(typeof (GIDesign.primaryScreenID), VisibleOnly = true, ShowWorkflowStateField = false)]
  [PXUnique(typeof (GIDesign))]
  public 
  #nullable disable
  string FieldName { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  public class PK : 
    PrimaryKeyOf<GIMassUpdateField>.By<GIMassUpdateField.designID, GIMassUpdateField.fieldId>
  {
    public static GIMassUpdateField Find(
      PXGraph graph,
      Guid? designID,
      int? fieldId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIMassUpdateField>.By<GIMassUpdateField.designID, GIMassUpdateField.fieldId>.FindBy(graph, (object) designID, (object) fieldId, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIMassUpdateField>.By<GIMassUpdateField.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIMassUpdateField.designID>
  {
  }

  /// <exclude />
  public abstract class fieldId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIMassUpdateField.fieldId>
  {
  }

  /// <exclude />
  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIMassUpdateField.fieldName>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIMassUpdateField.isActive>
  {
  }
}
