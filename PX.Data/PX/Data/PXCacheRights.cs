// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheRights
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Maps the user role's access rights for a specific
/// <tt>PXCache&lt;&gt;</tt> object.</summary>
/// <example>
/// The following example uses the enumeration value to configure access rights for the button,
/// which represents a graph action in the user interface. The user with the <tt>Select</tt> rights
/// for the <tt>ApproveBillsFilter</tt> cache can see the <b>View Document</b> button in the user interface.
/// For the user with the <tt>Update</tt> rights for the <tt>ApproveBillsFilter</tt> cache, the <b>View Document</b> button is also available.
/// <code>
/// public PXAction&lt;ApproveBillsFilter&gt; ViewDocument;
/// [PXUIField(DisplayName = "View Document",
///     MapEnableRights = PXCacheRights.Update,
///     MapViewRights = PXCacheRights.Select)]
/// [PXButton]
/// public virtual IEnumerable viewDocument(PXAdapter adapter)
/// {
/// ...
/// }</code>
/// </example>
public enum PXCacheRights : byte
{
  /// <summary>Matches the roles for whom access to a <tt>PXCache</tt>
  /// object is denied.</summary>
  Denied,
  /// <summary>Matches the roles that are allowed to read data records of
  /// the DAC type corresponding to the <tt>PXCache&lt;&gt;</tt> object.</summary>
  Select,
  /// <summary>Matches the roles that are allowed to update data records of
  /// the DAC type corresponding to the <tt>PXCache&lt;&gt;</tt> object.</summary>
  Update,
  /// <summary>Matches the roles that are allowed to insert data records of
  /// the DAC type corresponding to the <tt>PXCache&lt;&gt;</tt> object.</summary>
  Insert,
  /// <summary>Matches the roles that are allowed to delete data records of
  /// the DAC type corresponding to the <tt>PXCache&lt;&gt;</tt> object.</summary>
  Delete,
}
