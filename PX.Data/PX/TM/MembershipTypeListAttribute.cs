// Decompiled with JetBrains decompiler
// Type: PX.TM.MembershipTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.TM;

/// <summary>
/// Sets company tree member as a permanent or temporary member of a workgroup.
/// </summary>
public class MembershipTypeListAttribute : PXStringListAttribute
{
  public const string Temporary = "TEMP";
  public const string Permanent = "PERM";

  public MembershipTypeListAttribute()
    : base(("TEMP", nameof (Temporary)), ("PERM", nameof (Permanent)))
  {
  }
}
