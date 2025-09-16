// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.DataSync.BAICode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA.BankFeed.DataSync;

/// <summary>Information about BAI2 codes</summary>
public class BAICode
{
  public int Code { get; set; }

  public BAICodeType CodeType { get; set; }

  public string Descripton { get; set; }

  public BAICode(int code, BAICodeType codeType, string descripton)
  {
    this.Code = code;
    this.CodeType = codeType;
    this.Descripton = descripton;
  }
}
