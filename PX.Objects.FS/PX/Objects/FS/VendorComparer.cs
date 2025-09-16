// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.VendorComparer
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class VendorComparer : IEqualityComparer<PX.Objects.AP.Vendor>
{
  public bool Equals(PX.Objects.AP.Vendor x, PX.Objects.AP.Vendor y)
  {
    return x.BAccountID.HasValue && x.BAccountID.HasValue && x.BAccountID.Value == y.BAccountID.Value;
  }

  public int GetHashCode(PX.Objects.AP.Vendor v)
  {
    return v.BAccountID.HasValue ? v.BAccountID.GetHashCode() : 0;
  }
}
