// Decompiled with JetBrains decompiler
// Type: PX.Api.ProcessingOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Api;

internal static class ProcessingOperation
{
  public static readonly 
  #nullable disable
  string[] Values = new string[4]{ "C", "P", "I", "R" };
  public static readonly string[] Labels = new string[4]
  {
    "Prepare & Import",
    nameof (Prepare),
    "Import",
    "Clear Data"
  };
  public static readonly string[] LabelsExport = new string[4]
  {
    "Prepare & Export",
    nameof (Prepare),
    "Export",
    "Clear Data"
  };
  public const string PrepareAndProcess = "C";
  public const string Prepare = "P";
  public const string Process = "I";
  public const string RollBack = "R";

  public static class UI
  {
    public const string PrepareAndProcess = "Prepare & Import";
    public const string Prepare = "Prepare";
    public const string Process = "Import";
    public const string RollBack = "Clear Data";
  }

  public static class UIExport
  {
    public const string PrepareAndProcess = "Prepare & Export";
    public const string Prepare = "Prepare";
    public const string Process = "Export";
    public const string RollBack = "Clear Data";
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(ProcessingOperation.Values, ProcessingOperation.Labels)
    {
    }
  }

  public class prepareAndProcess : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProcessingOperation.prepareAndProcess>
  {
    public prepareAndProcess()
      : base("C")
    {
    }
  }

  public class prepare : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProcessingOperation.prepare>
  {
    public prepare()
      : base("P")
    {
    }
  }

  public class process : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProcessingOperation.process>
  {
    public process()
      : base("I")
    {
    }
  }

  public class rollback : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProcessingOperation.rollback>
  {
    public rollback()
      : base("R")
    {
    }
  }
}
