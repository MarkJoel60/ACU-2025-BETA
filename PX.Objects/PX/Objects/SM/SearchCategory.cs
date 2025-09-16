// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.SearchCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.SM;

public class SearchCategory : SearchCategory
{
  public const int AP = 1;
  public const int AR = 2;
  public const int CA = 4;
  public const int FA = 8;
  public const int GL = 16 /*0x10*/;
  public const int IN = 32 /*0x20*/;
  public const int OS = 64 /*0x40*/;
  public const int PO = 128 /*0x80*/;
  public const int SO = 256 /*0x0100*/;
  public const int RQ = 512 /*0x0200*/;
  public const int CR = 1024 /*0x0400*/;
  public const int PM = 2048 /*0x0800*/;
  public const int TM = 4096 /*0x1000*/;
  public const int FS = 8192 /*0x2000*/;
  public const int PR = 16384 /*0x4000*/;

  public static int Parse(string module)
  {
    if (module != null && module.Length == 2)
    {
      switch (module[0])
      {
        case 'A':
          switch (module)
          {
            case "AP":
              return 1;
            case "AR":
              return 2;
          }
          break;
        case 'C':
          switch (module)
          {
            case "CA":
              return 4;
            case "CR":
              return 1024 /*0x0400*/;
          }
          break;
        case 'F':
          switch (module)
          {
            case "FA":
              return 8;
            case "FS":
              return 8192 /*0x2000*/;
          }
          break;
        case 'G':
          if (module == "GL")
            return 16 /*0x10*/;
          break;
        case 'I':
          if (module == "IN")
            return 32 /*0x20*/;
          break;
        case 'O':
          if (module == "OS")
            return 64 /*0x40*/;
          break;
        case 'P':
          switch (module)
          {
            case "PO":
              return 128 /*0x80*/;
            case "PM":
              return 2048 /*0x0800*/;
            case "PR":
              return 16384 /*0x4000*/;
          }
          break;
        case 'R':
          if (module == "RQ")
            return 512 /*0x0200*/;
          break;
        case 'S':
          if (module == "SO")
            return 256 /*0x0100*/;
          break;
        case 'T':
          if (module == "TM")
            return 4096 /*0x1000*/;
          break;
      }
    }
    return SearchCategory.Parse(module);
  }
}
