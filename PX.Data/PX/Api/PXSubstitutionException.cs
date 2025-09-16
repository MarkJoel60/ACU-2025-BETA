// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSubstitutionException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal class PXSubstitutionException : PXException
{
  internal SYSubstitutionInfo Info { get; set; }

  public PXSubstitutionException(
    string substitutionID,
    string originalValue,
    string format,
    params object[] args)
    : base(format, args)
  {
    this.Info = new SYSubstitutionInfo()
    {
      SubstitutionID = substitutionID,
      OriginalValue = originalValue
    };
  }

  public string Serialize()
  {
    return string.Join(SYData.PARAM_SEPARATOR.ToString(), nameof (PXSubstitutionException), this.Info.Serialize());
  }

  public override string ToString() => this.Serialize();
}
