// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.IAssign
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.EP;

/// <summary>
/// Represents a document that can be assigned to a workgroup, a single owner, or both.
/// This workgroup or owner will be responsible for processing the document.
/// If a workgroup is specified, an owner that belongs to that workgroup can be specified.
/// </summary>
public interface IAssign
{
  /// <summary>
  /// The identifier of the workgroup responsible for the current document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  int? WorkgroupID { get; set; }

  /// <summary>
  /// The identifier of the user responsible for the current document.
  /// If the <see cref="P:PX.Data.EP.IAssign.WorkgroupID" /> is specified, only a user that belongs
  /// to the specified workgroup can be used.
  /// </summary>
  /// <value>Corresponds to the PX.Objects.CR.Contact.ContactID field.</value>
  int? OwnerID { get; set; }
}
