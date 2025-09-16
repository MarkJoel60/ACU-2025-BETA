// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.ErrorMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.SQLTree.Linq;

/// <summary>
/// Contains strings for exceptions that can be thrown in Acumatica Platform.
/// </summary>
[PXLocalizable("Error")]
public class ErrorMessages
{
  public const string QueryIsNotSupportedBySqLinq = "This kind of query is not supported by SQLinq. Make sure you are not using inherited View or BqlDelegate.";
}
