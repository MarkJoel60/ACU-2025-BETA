// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Handlers.FileOutlookModernUIOidcGetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CR.Handlers;

[PXInternalUseOnly]
public class FileOutlookModernUIOidcGetter : FileOutlookOidcGetter
{
  protected override string ResourceAddinManifest
  {
    get => "PX.Objects.CR.Handlers.AddinManifestModernUIOidc.xml";
  }

  protected override string OutputFileName => "OutlookAddinModerUIManifestMicrosoft365.xml";
}
