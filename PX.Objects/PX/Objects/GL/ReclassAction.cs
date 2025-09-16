// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReclassAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

public class ReclassAction
{
  public class List : PXStringListAttribute
  {
    public List()
      : base(new string[2]{ "C", "S" }, new string[2]
      {
        "Reclassification",
        "Split"
      })
    {
    }
  }
}
