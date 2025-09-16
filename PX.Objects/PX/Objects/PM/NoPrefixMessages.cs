// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.NoPrefixMessages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.PM;

[PXLocalizable]
public static class NoPrefixMessages
{
  public const string SameInaccessibleProjectExists = "The project cannot be created because the specified project ID ({0}) already exists in the system but your user has no sufficient access rights for it. Specify another Project ID.";
  public const string SameInaccessibleProjectTemplateExists = "The project template cannot be created because the specified project template ID ({0}) already exists in the system but your user has no sufficient access rights for it. Specify another Template ID.";
  public const string SameProjectExists = "The project cannot be created because the specified project ID ({0}) already exists in the system. Specify another Project ID.";
  public const string SameProjectTemplateExists = "The project template cannot be created because the specified project template ID ({0}) already exists in the system. Specify another Template ID.";
  public const string CostCodeAlreadyExists = "The cost code with the {0} identifier already exists. Specify another cost code ID.";
  public const string CanNotEnablePayByLine = "The Pay by Line check box cannot be selected because the document discount is applicable to the current document.";
}
