// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIMassAction
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
[PXCacheName("Generic Inquiry Mass Action")]
public class GIMassAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIMassAction.designID>>>>))]
  public Guid? DesignID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Mass Action ID", Visible = false)]
  public int? MassActionID { get; set; }

  [PXDBString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Action")]
  [GIActionsList(typeof (GIDesign.primaryScreenID), AllowedActionTypes = new PXSpecialButtonType[] {PXSpecialButtonType.Default, PXSpecialButtonType.Approve, PXSpecialButtonType.Process}, VisibleOnly = true)]
  [PXUnique(typeof (GIDesign))]
  public 
  #nullable disable
  string ActionName { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  public class PK : PrimaryKeyOf<GIMassAction>.By<GIMassAction.designID, GIMassAction.massActionID>
  {
    public static GIMassAction Find(
      PXGraph graph,
      Guid? designID,
      int? massActionID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIMassAction>.By<GIMassAction.designID, GIMassAction.massActionID>.FindBy(graph, (object) designID, (object) massActionID, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIMassAction>.By<GIMassAction.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIMassAction.designID>
  {
  }

  public abstract class massActionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIMassAction.massActionID>
  {
  }

  /// <exclude />
  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIMassAction.actionName>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIMassAction.isActive>
  {
  }
}
