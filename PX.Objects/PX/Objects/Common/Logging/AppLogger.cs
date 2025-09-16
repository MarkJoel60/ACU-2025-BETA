// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Logging.AppLogger
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Logging;

public class AppLogger : IAppLogger
{
  public virtual void WriteWarning(string message) => PXTrace.WriteWarning(message);
}
