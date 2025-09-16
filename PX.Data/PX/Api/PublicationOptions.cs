// Decompiled with JetBrains decompiler
// Type: PX.Api.PublicationOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

public class PublicationOptions
{
  /// <summary>
  ///  If <see langword="true" />, then <see cref="F:PX.Api.PublicationOptions.CompanyList" /> is ingored.
  /// </summary>
  public bool AllCompanies;
  /// <summary>
  ///  Companies (company login names) to publish a customization into.
  /// </summary>
  public string[] CompanyList;
}
