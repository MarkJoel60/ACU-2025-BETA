// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIReportIntegration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Reports;
using PX.Reports.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Xml.Linq;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Maintenance.GI;

public class GIReportIntegration : PXGraphExtension<GenericInquiryDesigner>
{
  private static readonly XmlSerializer _serializer = new XmlSerializer(typeof (Report));
  private static readonly Regex _nameRegex = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
  public PXAction<GIDesign> ExportAsReport;

  [PXButton(Category = "Export")]
  [PXUIField(DisplayName = "Export as Report")]
  public void exportAsReport()
  {
    GIDesign current = this.Base.Designs.Current;
    if (current != null)
    {
      string str = GIReportIntegration._nameRegex.Replace(current.Name ?? "", "");
      if (string.IsNullOrEmpty(str))
        str = "Report1";
      Report report1 = new Report();
      ((ReportItem) report1).Name = str;
      report1.SchemaUrl = PXUrl.SiteUrlWithPath();
      ReportItemCollection items1 = ((ReportItem) report1).Items;
      PageHeaderSection pageHeaderSection = new PageHeaderSection();
      ((ReportItem) pageHeaderSection).Name = "pageHeaderSection1";
      items1.Add((ReportItem) pageHeaderSection);
      ReportItemCollection items2 = ((ReportItem) report1).Items;
      DetailSection detailSection = new DetailSection();
      ((ReportItem) detailSection).Name = "detailSection1";
      items2.Add((ReportItem) detailSection);
      ReportItemCollection items3 = ((ReportItem) report1).Items;
      PageFooterSection pageFooterSection = new PageFooterSection();
      ((ReportItem) pageFooterSection).Name = "pageFooterSection1";
      items3.Add((ReportItem) pageFooterSection);
      Report report2 = report1;
      IReadOnlyDictionary<string, System.Type> tableByAlias = this.ConvertTables(report2);
      this.ConvertRelations(report2, tableByAlias);
      this.ConvertParameters(report2);
      this.ConvertFilters(report2);
      this.ConvertGroups(report2);
      this.ConvertSorts(report2);
      byte[] data = this.SerializeReport(report2);
      throw new PXRedirectToFileException(new PX.SM.FileInfo(str + ".rpx", (string) null, data), true);
    }
  }

  protected virtual void _(Events.RowSelected<GIDesign> e)
  {
    this.ExportAsReport.SetEnabled(e.Row != null && e.Cache.GetStatus((object) e.Row) != PXEntryStatus.Inserted);
  }

  private IReadOnlyDictionary<string, System.Type> ConvertTables(Report report)
  {
    PXGraph pxGraph = new PXGraph();
    Dictionary<string, System.Type> dictionary = new Dictionary<string, System.Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (GITable giTable in this.Base.Tables.Select().RowCast<GITable>().Where<GITable>((Func<GITable, bool>) (t => !string.IsNullOrEmpty(t.Name) && !string.IsNullOrEmpty(t.Alias))))
    {
      System.Type type = PXBuildManager.GetType(giTable.Name, false);
      if (!(type == (System.Type) null))
        dictionary[giTable.Alias] = type;
    }
    foreach (System.Type key in dictionary.Values.Distinct<System.Type>())
    {
      ReportTable reportTable = new ReportTable(key.Name)
      {
        FullName = key.FullName
      };
      PXCache cach = pxGraph.Caches[key];
      foreach (string field in (List<string>) cach.Fields)
      {
        ApiFieldInfo apiFieldInfo = ApiFieldInfo.Create(cach, field);
        if (apiFieldInfo != null)
        {
          ReportField reportField = new ReportField(field);
          TypeCode typeCode = System.Type.GetTypeCode(apiFieldInfo.DataType);
          if (typeCode != TypeCode.Empty)
            reportField.DataType = typeCode;
          if (!reportTable.Fields.Contains(field))
            reportTable.Fields.Add(reportField);
        }
      }
      ((List<ReportTable>) report.Tables).Add(reportTable);
    }
    return (IReadOnlyDictionary<string, System.Type>) dictionary;
  }

  private void ConvertRelations(Report report, IReadOnlyDictionary<string, System.Type> tableByAlias)
  {
    foreach (GIRelation giRelation in this.Base.Relations.Select().RowCast<GIRelation>().Where<GIRelation>((Func<GIRelation, bool>) (r =>
    {
      bool? isActive = r.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(r.ParentTable) && !string.IsNullOrEmpty(r.ChildTable);
    })))
    {
      System.Type type1;
      System.Type type2;
      if (tableByAlias.TryGetValue(giRelation.ParentTable, out type1) && tableByAlias.TryGetValue(giRelation.ChildTable, out type2))
      {
        ReportRelation reportRelation = new ReportRelation(type1.Name, type2.Name)
        {
          ParentAlias = giRelation.ParentTable,
          ChildAlias = giRelation.ChildTable,
          JoinType = this.GetJoinType(giRelation.JoinType)
        };
        if (string.Equals(type1.Name, giRelation.ParentTable, StringComparison.Ordinal))
          reportRelation.ParentAlias = (string) null;
        if (string.Equals(type2.Name, giRelation.ChildTable, StringComparison.Ordinal))
          reportRelation.ChildAlias = (string) null;
        foreach (GIOn giOn in PXSelectBase<GIOn, PXSelect<GIOn, Where<GIOn.designID, Equal<Current<GIDesign.designID>>, And<GIOn.relationNbr, Equal<Required<GIRelation.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, (object) giRelation.LineNbr).RowCast<GIOn>().Where<GIOn>((Func<GIOn, bool>) (l => !string.IsNullOrEmpty(l.ParentField) && !string.IsNullOrEmpty(l.ChildField) && !string.IsNullOrEmpty(l.Condition))))
        {
          RelationRow relationRow1 = new RelationRow(this.ReplaceParameters(this.NormalizeFieldName(giOn.ParentField)), this.ReplaceParameters(this.NormalizeFieldName(giOn.ChildField)));
          int? nullable1 = giOn.OpenBrackets?.Trim()?.Length;
          relationRow1.OpenBraces = nullable1.GetValueOrDefault();
          string closeBrackets = giOn.CloseBrackets;
          int? nullable2;
          if (closeBrackets == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
          {
            string str = closeBrackets.Trim();
            if (str == null)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new int?(str.Length);
          }
          nullable1 = nullable2;
          relationRow1.CloseBraces = nullable1.GetValueOrDefault();
          relationRow1.Operator = string.Equals(giOn.Operation?.Trim(), "O", StringComparison.OrdinalIgnoreCase) ? (FilterOperator) 1 : (FilterOperator) 0;
          relationRow1.Condition = this.GetLinkCondition(giOn.Condition);
          RelationRow relationRow2 = relationRow1;
          ((List<RelationRow>) reportRelation.Links).Add(relationRow2);
        }
        report.Relations.Add(reportRelation);
      }
    }
  }

  private void ConvertParameters(Report report)
  {
    foreach (GIFilter giFilter in this.Base.Parameters.Select().RowCast<GIFilter>().Where<GIFilter>((Func<GIFilter, bool>) (p =>
    {
      bool? isActive = p.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(p.Name);
    })))
    {
      ReportParameter reportParameter1 = new ReportParameter(giFilter.Name, (ParameterType) 4);
      bool? nullable = giFilter.Required;
      bool flag1 = true;
      reportParameter1.Required = new bool?(nullable.GetValueOrDefault() == flag1 & nullable.HasValue);
      reportParameter1.ColumnSpan = giFilter.ColSpan ?? 1;
      reportParameter1.DefaultValue = giFilter.DefaultValue;
      nullable = giFilter.Required;
      bool flag2 = true;
      reportParameter1.Nullable = !(nullable.GetValueOrDefault() == flag2 & nullable.HasValue);
      nullable = giFilter.Hidden;
      bool flag3 = true;
      reportParameter1.Visible = new bool?(!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue));
      reportParameter1.Prompt = giFilter.DisplayName;
      ReportParameter reportParameter2 = reportParameter1;
      if (giFilter.FieldName == typeof (CheckboxCombobox.checkbox).FullName)
      {
        reportParameter2.Type = (ParameterType) 0;
        reportParameter2.Nullable = false;
      }
      else if (giFilter.FieldName == typeof (CheckboxCombobox.combobox).FullName && !string.IsNullOrEmpty(giFilter.AvailableValues))
      {
        string availableValues = giFilter.AvailableValues;
        char[] chArray = new char[1]{ ',' };
        foreach (string str in availableValues.Split(chArray))
        {
          char[] separator = new char[1]{ ';' };
          string[] strArray = str.Split(separator, 2);
          if (strArray.Length == 2)
            ((List<ParameterValue>) reportParameter2.ValidValues).Add(new ParameterValue(strArray[0], strArray[1]));
        }
      }
      else
        reportParameter2.ViewName = $"=Report.GetFieldSchema('{this.NormalizeFieldName(giFilter.FieldName)}')";
      ((List<ReportParameter>) report.Parameters).Add(reportParameter2);
    }
  }

  private void ConvertFilters(Report report)
  {
    foreach (GIWhere giWhere in this.Base.Wheres.Select().RowCast<GIWhere>().Where<GIWhere>((Func<GIWhere, bool>) (c =>
    {
      bool? isActive = c.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(c.DataFieldName) && !string.IsNullOrEmpty(c.Condition);
    })))
    {
      FilterExp filterExp1 = new FilterExp(this.ReplaceParameters(this.NormalizeFieldName(giWhere.DataFieldName)), this.GetFilterCondition(giWhere.Condition));
      string openBrackets = giWhere.OpenBrackets;
      int? nullable1;
      int? nullable2;
      if (openBrackets == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
      {
        string str = openBrackets.Trim();
        if (str == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new int?(str.Length);
      }
      nullable1 = nullable2;
      filterExp1.OpenBraces = nullable1.GetValueOrDefault();
      string closeBrackets = giWhere.CloseBrackets;
      int? nullable3;
      if (closeBrackets == null)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
      {
        string str = closeBrackets.Trim();
        if (str == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new int?(str.Length);
      }
      nullable1 = nullable3;
      filterExp1.CloseBraces = nullable1.GetValueOrDefault();
      filterExp1.Operator = string.Equals(giWhere.Operation?.Trim(), "O", StringComparison.OrdinalIgnoreCase) ? (FilterOperator) 1 : (FilterOperator) 0;
      filterExp1.Value = this.ReplaceParameters(giWhere.Value1);
      filterExp1.Value2 = this.ReplaceParameters(giWhere.Value2);
      FilterExp filterExp2 = filterExp1;
      ((List<FilterExp>) report.Filters).Add(filterExp2);
    }
  }

  private void ConvertSorts(Report report)
  {
    foreach (GISort giSort in this.Base.Sortings.Select().RowCast<GISort>().Where<GISort>((Func<GISort, bool>) (s =>
    {
      bool? isActive = s.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(s.DataFieldName) && !string.IsNullOrEmpty(s.SortOrder);
    })))
    {
      SortExp sortExp = new SortExp(this.NormalizeFieldName(giSort.DataFieldName), string.Equals(giSort.SortOrder?.Trim(), "A", StringComparison.OrdinalIgnoreCase) ? (SortOrder) 0 : (SortOrder) 1);
      ((List<SortExp>) report.Sorting).Add(sortExp);
    }
  }

  private void ConvertGroups(Report report)
  {
    List<GroupExp> collection = new List<GroupExp>();
    foreach (GIGroupBy giGroupBy in this.Base.GroupBy.Select().RowCast<GIGroupBy>().Where<GIGroupBy>((Func<GIGroupBy, bool>) (g =>
    {
      bool? isActive = g.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(g.DataFieldName);
    })))
    {
      GroupExp groupExp = new GroupExp(this.NormalizeFieldName(giGroupBy.DataFieldName));
      collection.Add(groupExp);
    }
    if (collection.Count <= 0)
      return;
    Group group = new Group() { Name = "group1" };
    GroupSectionCollection headers = group.Headers;
    GroupHeaderSection groupHeaderSection = new GroupHeaderSection();
    ((ReportItem) groupHeaderSection).Name = "groupHeaderSection1";
    headers.Add((GroupSection) groupHeaderSection);
    GroupSectionCollection footers = group.Footers;
    GroupFooterSection groupFooterSection = new GroupFooterSection();
    ((ReportItem) groupFooterSection).Name = "groupFooterSection1";
    footers.Add((GroupSection) groupFooterSection);
    ((List<GroupExp>) group.Grouping).AddRange((IEnumerable<GroupExp>) collection);
    report.Groups.Add(group);
  }

  private byte[] SerializeReport(Report report)
  {
    using (StringWriter stringWriter = new StringWriter())
    {
      GIReportIntegration._serializer.Serialize((TextWriter) stringWriter, (object) report);
      XDocument xdocument = XDocument.Parse(stringWriter.ToString());
      xdocument.Declaration.Encoding = "utf-8";
      xdocument.Descendants((XName) "OriginalFilters").Remove<XElement>();
      XElement content1 = new XElement((XName) "Sections");
      xdocument.Root?.Add((object) content1);
      foreach (ReportItem reportItem in ((IEnumerable) ((ReportItem) report).Items).Cast<ReportItem>().Where<ReportItem>((Func<ReportItem, bool>) (i => !(i is GroupSection))))
      {
        XElement content2 = new XElement((XName) reportItem.GetType().Name.Replace("Section", ""), (object) new XAttribute((XName) "Name", (object) reportItem.Name));
        content1.Add((object) content2);
      }
      if (((CollectionBase) report.Groups).Count > 0)
      {
        XElement content3 = new XElement((XName) "Groups");
        xdocument.Root?.Add((object) content3);
        foreach (Group group in (CollectionBase) report.Groups)
        {
          XElement content4 = new XElement((XName) "Group", (object) new XAttribute((XName) "Name", (object) group.Name));
          XElement xelement1 = new XElement((XName) "Grouping");
          XElement xelement2 = new XElement((XName) "Headers");
          XElement xelement3 = new XElement((XName) "Footers");
          content4.Add((object) xelement1, (object) xelement2, (object) xelement3);
          content3.Add((object) content4);
          foreach (GroupExp groupExp in (List<GroupExp>) group.Grouping)
          {
            XElement content5 = new XElement((XName) "GroupExp", (object) new XElement((XName) "DataField", (object) groupExp.DataField));
            xelement1.Add((object) content5);
          }
          foreach (ReportItem header in (CollectionBase) group.Headers)
          {
            XElement content6 = new XElement((XName) "Header", (object) new XAttribute((XName) "Name", (object) header.Name));
            xelement2.Add((object) content6);
          }
          foreach (ReportItem footer in (CollectionBase) group.Footers)
          {
            XElement content7 = new XElement((XName) "Footer", (object) new XAttribute((XName) "Name", (object) footer.Name));
            xelement3.Add((object) content7);
          }
        }
      }
      using (Stream stream = (Stream) new MemoryStream())
      {
        xdocument.Save(stream);
        stream.Seek(0L, SeekOrigin.Begin);
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
      }
    }
  }

  private JoinType GetJoinType(string joinType)
  {
    switch (joinType?.ToUpperInvariant()?.Trim())
    {
      case "L":
        return (JoinType) 0;
      case "R":
        return (JoinType) 1;
      case "F":
        return (JoinType) 3;
      case "C":
        return (JoinType) 4;
      default:
        return (JoinType) 2;
    }
  }

  private LinkCondition GetLinkCondition(string condition)
  {
    string str = condition?.ToUpperInvariant()?.Trim();
    if (str != null)
    {
      switch (str.Length)
      {
        case 1:
          switch (str[0])
          {
            case 'E':
              return (LinkCondition) 0;
            case 'G':
              return (LinkCondition) 2;
            case 'L':
              return (LinkCondition) 4;
          }
          break;
        case 2:
          switch (str[0])
          {
            case 'G':
              if (str == "GE")
                return (LinkCondition) 3;
              break;
            case 'L':
              if (str == "LE")
                return (LinkCondition) 5;
              break;
            case 'N':
              switch (str)
              {
                case "NE":
                  return (LinkCondition) 1;
                case "NU":
                  return (LinkCondition) 6;
                case "NN":
                  return (LinkCondition) 7;
              }
              break;
          }
          break;
      }
    }
    throw new PXException("Invalid condition: '{0}'", new object[1]
    {
      (object) condition
    });
  }

  private FilterCondition GetFilterCondition(string condition)
  {
    string str = condition?.ToUpperInvariant()?.Trim();
    if (str != null)
    {
      switch (str.Length)
      {
        case 1:
          switch (str[0])
          {
            case 'B':
              return (FilterCondition) 10;
            case 'E':
              return (FilterCondition) 0;
            case 'G':
              return (FilterCondition) 2;
            case 'L':
              return (FilterCondition) 4;
          }
          break;
        case 2:
          switch (str[1])
          {
            case 'E':
              switch (str)
              {
                case "NE":
                  return (FilterCondition) 1;
                case "GE":
                  return (FilterCondition) 3;
                case "LE":
                  return (FilterCondition) 5;
              }
              break;
            case 'I':
              if (str == "LI")
                return (FilterCondition) 6;
              break;
            case 'L':
              switch (str)
              {
                case "NL":
                  return (FilterCondition) 9;
                case "RL":
                  return (FilterCondition) 7;
                case "LL":
                  return (FilterCondition) 8;
              }
              break;
            case 'N':
              if (str == "NN")
                return (FilterCondition) 12;
              break;
            case 'U':
              if (str == "NU")
                return (FilterCondition) 11;
              break;
          }
          break;
      }
    }
    throw new PXException("Invalid condition: '{0}'", new object[1]
    {
      (object) condition
    });
  }

  private string CapitalizeFirstLetter(string fieldName)
  {
    return string.IsNullOrEmpty(fieldName) ? fieldName : char.ToUpperInvariant(fieldName[0]).ToString() + fieldName.Substring(1, fieldName.Length - 1);
  }

  private string NormalizeFieldName(string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName))
      return fieldName;
    string[] strArray = fieldName.Split(new char[1]{ '.' }, 2);
    return strArray.Length == 2 ? $"{strArray[0]}.{this.CapitalizeFirstLetter(strArray[1])}" : this.CapitalizeFirstLetter(fieldName);
  }

  private string ReplaceParameters(string value)
  {
    if (string.IsNullOrEmpty(value))
      return value;
    foreach (GIFilter giFilter in this.Base.Parameters.Select().RowCast<GIFilter>())
      value = value.Replace($"[{giFilter.Name}]", "@" + giFilter.Name, StringComparison.OrdinalIgnoreCase);
    return value;
  }
}
