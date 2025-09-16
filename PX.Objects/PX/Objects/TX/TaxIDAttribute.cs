// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.TX;

public class TaxIDAttribute : PXDBStringAttribute
{
  public TaxIDAttribute()
    : base(60)
  {
    this.IsUnicode = true;
    ((PXDBFieldAttribute) this).IsKey = true;
    this.InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC";
  }
}
