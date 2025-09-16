// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BranchAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>Branch Field.</summary>
/// <remarks>In case your DAC  supports multiple branches add this attribute to the Branch field of your DAC.</remarks>
public class BranchAttribute(
  Type sourceType = null,
  Type searchType = null,
  bool addDefaultAttribute = true,
  bool onlyActive = true,
  bool useDefaulting = true) : BranchBaseAttribute(sourceType, searchType, addDefaultAttribute, onlyActive, useDefaulting)
{
}
