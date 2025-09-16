// Decompiled with JetBrains decompiler
// Type: PX.SM.ExtraActionFormFieldDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.SM;

internal class ExtraActionFormFieldDefinition
{
  public string ScreenId { get; set; }

  public string FormName { get; set; }

  public string FieldName { get; set; }

  public string SchemaField { get; set; }

  public int LineNumber { get; set; }

  public string Title { get; set; }

  public AUWorkflowFormField ToAU()
  {
    AUWorkflowFormField au = new AUWorkflowFormField();
    au.Screen = this.ScreenId;
    au.FormName = this.FormName;
    au.FieldName = this.FieldName;
    au.SchemaField = this.SchemaField;
    au.LineNumber = new int?(this.LineNumber);
    au.DisplayName = this.Title ?? this.FieldName;
    au.ComboBoxValuesSource = "E";
    au.DefaultValueSource = "E";
    au.IsActive = new bool?(true);
    au.IsVisible = true;
    return au;
  }
}
