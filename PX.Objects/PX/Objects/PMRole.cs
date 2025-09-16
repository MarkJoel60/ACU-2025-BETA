// Decompiled with JetBrains decompiler
// Type: PX.Objects.PMRole
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Serialization;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects;

/// <summary>Project role.</summary>
[PXSerializable]
[PXCacheName("Project Role")]
public class PMRole : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;

  /// <summary>Role identifier.</summary>
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField]
  public string RoleID { get; set; }

  /// <summary>Role description.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visible = false)]
  public string Description { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>Primary Key.</summary>
  public class PK : PrimaryKeyOf<PMRole>.By<PMRole.roleID>
  {
    public static PMRole Find(PXGraph graph, string roleID, PKFindOptions options = 0)
    {
      return (PMRole) PrimaryKeyOf<PMRole>.By<PMRole.roleID>.FindBy(graph, (object) roleID, options);
    }
  }

  public abstract class roleID : IBqlField, IBqlOperand
  {
    public const int Length = 64 /*0x40*/;
  }

  public abstract class description : IBqlField, IBqlOperand
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRole.Tstamp>
  {
  }
}
