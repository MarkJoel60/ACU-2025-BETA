// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.AssignedLicense
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

public class AssignedLicense
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<Guid> _disabledPlans = new Collection<Guid>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Guid? _skuId;

  /// <summary>Create a new AssignedLicense object.</summary>
  /// <param name="disabledPlans">Initial value of disabledPlans.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static AssignedLicense CreateAssignedLicense(Collection<Guid> disabledPlans)
  {
    AssignedLicense assignedLicense = new AssignedLicense();
    assignedLicense.disabledPlans = disabledPlans != null ? disabledPlans : throw new ArgumentNullException(nameof (disabledPlans));
    return assignedLicense;
  }

  /// <summary>
  /// There are no comments for Property disabledPlans in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<Guid> disabledPlans
  {
    get => this._disabledPlans;
    set => this._disabledPlans = value;
  }

  /// <summary>
  /// There are no comments for Property skuId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Guid? skuId
  {
    get => this._skuId;
    set => this._skuId = value;
  }
}
