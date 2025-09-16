// Decompiled with JetBrains decompiler
// Type: PX.SP.Alias.SPPortal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SP.Alias;

[PXInternalUseOnly]
public class SPPortal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public int? PortalID { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "")]
  public virtual 
  #nullable disable
  string PortalName { get; set; }

  [PXDBBool]
  public virtual bool? IsActive { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  public virtual string PortalURL { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  public virtual string AccessRole { get; set; }

  /// <summary>Theme for the portal</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  public virtual 
  #nullable enable
  string? InterfaceTheme { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class portalID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  SPPortal.portalID>
  {
  }

  public abstract class portalName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPPortal.portalName>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SPPortal.isActive>
  {
  }

  public abstract class portalURL : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPPortal.portalURL>
  {
  }

  public abstract class accessRole : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPPortal.accessRole>
  {
  }

  public abstract class interfaceTheme : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPPortal.interfaceTheme>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SPPortal.noteID>
  {
  }
}
