// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.I1099Settings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AP;

public interface I1099Settings
{
  int? BAccountID { get; set; }

  string TCC { get; set; }

  bool? ForeignEntity { get; set; }

  bool? CFSFiler { get; set; }

  string ContactName { get; set; }

  string CTelNumber { get; set; }

  string CEmail { get; set; }

  string NameControl { get; set; }
}
