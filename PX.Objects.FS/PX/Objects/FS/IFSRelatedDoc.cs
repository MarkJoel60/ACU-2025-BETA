// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.IFSRelatedDoc
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS;

public interface IFSRelatedDoc
{
  string SrvOrdType { get; }

  string ServiceOrderRefNbr { get; }

  int? ServiceOrderLineNbr { get; }

  string AppointmentRefNbr { get; }

  int? AppointmentLineNbr { get; }

  string ServiceContractRefNbr { get; }

  int? ServiceContractPeriodID { get; }
}
