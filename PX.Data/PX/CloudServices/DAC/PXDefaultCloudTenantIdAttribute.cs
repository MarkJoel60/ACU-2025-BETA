// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.PXDefaultCloudTenantIdAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.CloudServices.Tenants;
using PX.Data;

#nullable disable
namespace PX.CloudServices.DAC;

public class PXDefaultCloudTenantIdAttribute : PXDefaultAttribute
{
  [InjectDependencyOnTypeLevel]
  internal ICloudTenantService _cloudTenantService { get; set; }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) this._cloudTenantService.TenantId;
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, nullable: new bool?(false), required: this._PersistingCheck == PXPersistingCheck.Nothing ? new int?() : new int?(1), defaultValue: (object) this._cloudTenantService.TenantId, fieldName: this._FieldName);
  }
}
