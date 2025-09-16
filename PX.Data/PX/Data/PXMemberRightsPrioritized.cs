// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMemberRightsPrioritized
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public struct PXMemberRightsPrioritized(bool prioritized, PXMemberRights rights)
{
  public readonly bool Prioritized = prioritized;
  public readonly PXMemberRights Rights = rights;

  private bool EqualsIntern(PXMemberRightsPrioritized other)
  {
    return this.Prioritized == other.Prioritized && this.Rights == other.Rights;
  }

  public override bool Equals(object obj)
  {
    return obj is PXMemberRightsPrioritized other && this.EqualsIntern(other);
  }

  public override int GetHashCode()
  {
    return this.Prioritized.GetHashCode() * 397 ^ this.Rights.GetHashCode();
  }
}
