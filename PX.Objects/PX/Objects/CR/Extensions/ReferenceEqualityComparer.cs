// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ReferenceEqualityComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions;

public class ReferenceEqualityComparer : EqualityComparer<object>
{
  public override bool Equals(object x, object y) => x == y;

  public override int GetHashCode(object obj) => obj == null ? 0 : obj.GetHashCode();
}
