// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSBLOCContact
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXBreakInheritance]
[PXProjection(typeof (Select<FSContact, Where<FSContact.entityType, Equal<ListField.ACEntityType.BranchLocation>>>))]
[Serializable]
public class FSBLOCContact : FSContact
{
  [PXDBString(4, IsFixed = true)]
  [PXDefault("BLOC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  public override string EntityType { get; set; }

  public new abstract class contactID : IBqlField, IBqlOperand
  {
  }

  public new abstract class entityType : ListField.ACEntityType
  {
  }

  public new abstract class revisionID : IBqlField, IBqlOperand
  {
  }
}
