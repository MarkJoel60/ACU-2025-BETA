// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.EntityInUse.FeatureInUse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.EntityInUse;

[EntityInUseDBSlotOn]
[PXHidden]
[Serializable]
public class FeatureInUse : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true)]
  public virtual string FeatureName { get; set; }

  public abstract class featureName : IBqlField, IBqlOperand
  {
  }
}
